import React, { useState, useEffect } from 'react';
import './createAuction.css';

import Caver from 'caver-js';

/**
 * @description user가 소유한 NFT 보여주고, market에 업로드할 수 있게 함
 * @returns {React.ReactComponentElement}
 */
export default function CreateAuction({ nftContractABI, marcketContractABI, nftContractAddress, marcketContractAddress }) {
  const [tokens, setTokens] = useState([]); // user가 소유한 NFT 정보 저장
  const caver = new Caver(window.klaytn);
  const [selectedTokenId, setSelectedTokenId] = useState(null); // 화면에서 선택한 tokenId 저장
  const [price, setPrice] = useState('');

  const ITEMS_PER_PAGE = 8;   // 너무 많은 이미지를 소유할 경우, ipfs에서 이미지 로드에 문제가 있어 8개씩 제한
  const [currentPage, setCurrentPage] = useState(1);

  const [tokenBalance, setTokenBalance] = useState(0);  // 총 NFT 소유 수

  const [loading, setLoading] = useState(false);

  /**
   * @description 어떤 NFT 선택했는지 저장
   * @param {Number} tokenId 화면에서 선택한 토큰의 id
   */
  const selectNFT = (tokenId) => {
    setSelectedTokenId(tokenId);
  };

  /**
   * @description NFT 판매 가격 설정 저장
   * @param {Number} e user가 입력한 NFT 판매 가격(단위 : MTK)
   */
  const handlePriceChange = (e) => {
    setPrice(e.target.value);
  };

  /**
   * @description user가 market에 1회는 market에 approve해야 하기에 확인용
   * @returns {Boolean} user가 이미 market에 approve 했는가에 대한 확인
   */
  const isApprovedForMarket = async () => {
    try {
      alert('Approve to MarketPlace.');
      const nftContract = new caver.klay.Contract(nftContractABI, nftContractAddress);
      
      const response = await nftContract.methods.isMarketplaceApproved().call();

      return response
    } catch (e) {
      console.log(`Error for 'isApprovedForMarket' : ${e}`);
    }
  }
  /**
   * @description user의 NFT를 market에 업로드. approve가 선행되어야 가능함
   * @see isApprovedForMarket
   */
  const listNFT = async () => {
    if (selectedTokenId && price) {
      const nftContract = new caver.klay.Contract(nftContractABI, nftContractAddress);
  
      try {
        const response = await isApprovedForMarket()

        if (!response) {    // !approved면 approve하는 과정 진행
          await nftContract.methods.setMarketplaceApproval(marcketContractAddress, true).send({
            from: window.klaytn.selectedAddress,
            gas: '2000000',
          });
          alert(`Marketplace approval set for token ID: ${selectedTokenId}`);
        }
      
        const marketplaceContract = new caver.klay.Contract(marcketContractABI, marcketContractAddress);
        const listingPrice = caver.utils.toWei(price.toString(), 'ether');

        // Market에 업로드하기
        await marketplaceContract.methods.listNFT(selectedTokenId, listingPrice).send({
          from: window.klaytn.selectedAddress,
          gas: '2000000',
        });

        // 화면 새로고침(업로드한 NFT 화면에서 지우기)
        loadNFTs();

        alert('Your NFT is listed to MarketPlace');
      } catch (error) {
        console.error("Setting marketplace approval or listing NFT failed", error);
      }
    } else {
      alert("No token selected or price set");
    }
  };
  
  /**
   * @description user소유의 NFT burn하는 과정
   * @return {String} item code 값 
   */
  const burnNFT = async () => {
    if(selectedTokenId) {
      const nftContract = new caver.klay.Contract(nftContractABI, nftContractAddress);
      const tokenURI = await nftContract.methods.tokenURI(selectedTokenId).call();
      const metadata = await fetchMetadata(tokenURI);
      try {
        // NFT burn
        await nftContract.methods.burn(selectedTokenId).send({
          from: window.klaytn.selectedAddress,
          gas: '2000000',
        });
        // code 값 alert를 통해 보여줌(암호/복호화 과정 없는 상태임)
        alert(`Code is : ${metadata.code}`);
      } catch (e) {
        console.log(`Error burnNFT : ${e}`);
      }
    }
  }

  /**
   * @description user소유의 NFT정보 pageIndex에 따라 load함
   * @returns {Object} tokenDetails
   */
  const loadNFTs = async () => {
    try {
      setLoading(true);
    
      const nftContract = new caver.klay.Contract(nftContractABI, nftContractAddress);
      const balanceOf = await nftContract.methods.balanceOf(window.klaytn.selectedAddress).call();
      setTokenBalance(balanceOf);

      const tokenDetails = [];
      
      const startIndex = (currentPage - 1) * ITEMS_PER_PAGE;
      const endIndex = Math.min(currentPage * ITEMS_PER_PAGE, balanceOf);
     
      for (let i = startIndex; i < endIndex; i++) {
        const tokenId = await nftContract.methods.tokenOfOwnerByIndex(window.klaytn.selectedAddress, i).call();
        const tokenURI = await nftContract.methods.tokenURI(tokenId).call();
        const metadata = await fetchMetadata(tokenURI);
        const imageUrl = metadata.image.replace('ipfs://', 'https://ipfs.io/ipfs/');
        const imageName = metadata.name;
        tokenDetails.push({
          tokenId,
          imageUrl,
          imageName
        });
      }
      setTokens(tokenDetails);
    } catch (error) {
      console.error('Error loading NFTs : ', error);
    } finally {
      setLoading(false);
    }
  };

  /**
   * @description ipfsURL 재설정(이미지 바로 보일 수 있게)
   * @param {URL} ipfsUrl
   * @returns {URL}
   */
  function convertIPFStoHTTP(ipfsUrl) {
    return ipfsUrl.replace('ipfs://', 'https://ipfs.io/ipfs/')
  }
  
  /**
   * @description metadata의 json객체 다운로드
   * @param {URL} uri NFT의 정보가 저장된 metadata의 URL
   * @returns {Object} metadata
   */
  async function fetchMetadata(uri) {
    const url = convertIPFStoHTTP(uri);
    const response = await fetch(url);
    const metadata = await response.json();
    return metadata;
  }

  useEffect(() => {
    if (window.klaytn.selectedAddress) {
        loadNFTs();
    }
  }, [window.klaytn.selectedAddress]);

  /**
   * @description page handler
   */
  const handleNextPage = async () => {
    setCurrentPage((prevPage) => prevPage + 1);
    await loadNFTs();
  }
  
  /**
   * @description page handler
   */
  const handlePrevPage = async () => {
    setCurrentPage((prevPage) => prevPage - 1);
    await loadNFTs();
  }

  return (
    <div style={{ overflowY: 'auto', maxHeight: '600px' }}>
      <h2>Create Auction</h2>
      {loading ? (
       <div className='spinner'></div> 
      ) : (
        <div>
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gridGap: '10px', marginBottom: '10px' }}>
            {tokens.map((tokenDetail) => (
              <div key={tokenDetail.tokenId}
                  style={{
                    cursor: 'pointer',
                    border: '1px solid #ddd',
                    padding: '10px',
                    borderRadius: '5px',
                    backgroundColor: selectedTokenId === tokenDetail.tokenId ? 'lightgrey' : 'white',
                  }}
                  onClick={() => selectNFT(tokenDetail.tokenId)}
              >
                <img src={tokenDetail.imageUrl} alt="NFT" style={{ maxWidth: '100%', display: 'block', marginBottom: '5px', }} />
                <p className="account-info">{tokenDetail.imageName}</p>
              </div>
            ))}
          </div>
          <input type="text" placeholder="Price in KLAY" value={price} onChange={handlePriceChange} />
          <button className="input-button" onClick={listNFT}>List NFT for Sale</button>
          <button className="input-button" onClick={burnNFT}>Burn</button>
          <div>
            <button onClick={handlePrevPage} disabled={currentPage === 1}>Previous Page</button>
            <button onClick={handleNextPage} disabled={currentPage * ITEMS_PER_PAGE >= tokenBalance}>Next Page</button>
          </div>
        </div>)}
    </div>
  );
}


