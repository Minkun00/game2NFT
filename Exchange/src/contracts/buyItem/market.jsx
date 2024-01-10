import React, { useState, useEffect } from 'react';
import Caver from 'caver-js';

export default function Marcketplace ({ 
  tokenContractABI, 
  tokenContractAddress, 
  nftContractABI, 
  nftContractAddress, 
  marcketContractABI, 
  marcketContractAddress
}) {
  const caver = new Caver(window.klaytn);
  // market에 올라간 NFT 정보 저장
  const [listedNfts, setListedNfts] = useState([]);
  
  /**
   * @description Market에 올라간 NFT 정보 Contract에서 받아옴
   */
  const loadListedNFTs = async () => {
    const marketplaceContract = new caver.klay.Contract(marcketContractABI, marcketContractAddress );
    /**
     * @return tokenIds NFT의 tokenId   
     * @return prices NFT의 가격 배열
     */
    const result = await marketplaceContract.methods.getListedNFTs().call();
    const tokenIds = result[0];
    const prices = result[1];
  
    const nftContract = new caver.klay.Contract(nftContractABI, nftContractAddress);
    // tokenId에 대한 Uri 받아서 Metadata의 정보들을 받아옴
    const listedItems = await Promise.all(tokenIds.map(async (tokenId, index) => {
      const tokenURI = await nftContract.methods.tokenURI(tokenId).call();
      const metadata = await fetchMetadata(tokenURI);
      const imageUrl = convertIPFStoHTTP(metadata.image);
      const imageName = metadata.name;
      return {
        tokenId,
        imageName,
        imageUrl,
        price: prices[index]
      };
    }));
    checkAllowance()
    setListedNfts(listedItems);
  };
  
  /**
   * @description metadata에 저장된 ipfs에 있는 image의 url 볼 수 있게 수정
   * @param {URL} ipfsUrl metadata에 저장된 ipfs에 있는 image의 url
   * @returns {img} item의 이미지
   */
  function convertIPFStoHTTP(ipfsUrl) {
    return ipfsUrl.replace('ipfs://', 'https://ipfs.io/ipfs/');
  }

  /**
   * 
   * @param {URL} uri metadata의 url 
   * @returns {Object} metadata 내용
   * @throws {Error} Network문제 || JSON 형식 아닌 경우
   */
  async function fetchMetadata(uri) {
    try {
      const url = convertIPFStoHTTP(uri);
      const response = await fetch(url);
      const metadata = await response.json();
      return metadata;
    } catch (error) {
      throw new Error(`Failed to fetch metadata: ${error.message}`);
    }
  }

  useEffect(() => {
    loadListedNFTs();
  }, []);

  /**
   * @description 선택한 NFT 가격만큼 token 사용에 대한 approval실행
   * @param {int} price NFT가격 
   * @param {int} tokenId NFT의 tokenId
   */
  const approveToken = async (price, tokenId) => {
    const tokenContract = new caver.klay.Contract(tokenContractABI, tokenContractAddress);
    const fromAddress = window.klaytn.selectedAddress;
  
    try {
      // 스마트 컨트랙트에 대한 승인 처리
      await tokenContract.methods.approve(marcketContractAddress, price).send({
        from: fromAddress,
        gas: '200000'
      });
      // 승인 후 구매 함수 호출
      purchaseNFT(tokenId, price);
    } catch (error) {
      console.error('Approve token failed', error);
    }
  };
  
  /**
   * @description NFT 구매
   * @param {int} tokenId NFT의 tokenId
   * @param {int} price NFT가격
   */
  const purchaseNFT = async (tokenId, price) => {
    const marketplaceContract = new caver.klay.Contract(marcketContractABI, marcketContractAddress);
    const fromAddress = window.klaytn.selectedAddress;
  
    try {
      // 구매 처리
      await marketplaceContract.methods.purchaseNFT(tokenId).send({
        from: fromAddress,
        value: price,
        gas: '300000'
      });
      // 구매 후 NFT 목록 환기
      loadListedNFTs();
    } catch (error) {
      console.error('Buy NFT failed', error);
    }
  };

  /**
   * @description NFT구매를 위한 approved된 token양 확인. for test
   */
  const checkAllowance = async () => {
    const tokenContract = new caver.klay.Contract(tokenContractABI, tokenContractAddress);
    const fromAddress = window.klaytn.selectedAddress;
    try {
      await tokenContract.methods.allowance(fromAddress, marcketContractAddress).call();
    } catch (error) {
      console.error('Error checking allowance', error);
    }
  };
  

  return (
    <div>
      <h2>Listed NFTs</h2>
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(6, 1fr)', gridGap: '10px' }}>
        {listedNfts.map((nft) => (
          <div key={nft.tokenId} style={{ border: '1px solid #ddd', padding: '10px', borderRadius: '5px' }}>
            <img src={nft.imageUrl} alt={`NFT ${nft.tokenId}`} style={{ maxWidth: '100%', display: 'block', marginBottom: '5px' }} />
            <p className="account-info">{nft.imageName}</p>
            <button className="input-button" onClick={() => approveToken(nft.price, nft.tokenId)}>
              {caver.utils.convertFromPeb(caver.utils.toBN(nft.price), 'KLAY')} MTK
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}
