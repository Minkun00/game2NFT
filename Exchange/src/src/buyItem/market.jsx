import React, { useState, useEffect } from 'react';
import Caver from 'caver-js';

export default function Marcketplace({ tokenContractABI, tokenContractAddress, nftContractABI, nftContractAddress, marcketContractABI, marcketContractAddress }) {
  const caver = new Caver(window.klaytn);
  const [listedNfts, setListedNfts] = useState([]);
  
  const loadListedNFTs = async () => {
    const marketplaceContract = new caver.klay.Contract(marcketContractABI, marcketContractAddress );
    const result = await marketplaceContract.methods.getListedNFTs().call();
  
    // result 객체에서 배열을 추출합니다.
    const tokenIds = result[0];
    const prices = result[1];
  
    // result 객체의 각 키에 접근해서 배열을 얻어낸 후 나머지 로직을 수 행합니다.
    const nftContract = new caver.klay.Contract(nftContractABI, nftContractAddress);
  
    const listedItems = await Promise.all(tokenIds.map(async (tokenId, index) => {
      const tokenURI = await nftContract.methods.tokenURI(tokenId).call();
      const metadata = await fetchMetadata(tokenURI);
      const imageUrl = convertIPFStoHTTP(metadata.image);
      return {
        tokenId,
        imageUrl,
        price: prices[index]
      };
    }));
    checkAllowance()
    setListedNfts(listedItems);
  };
  
  function convertIPFStoHTTP(ipfsUrl) {
    return ipfsUrl.replace('ipfs://', 'https://ipfs.io/ipfs/');
  }

  async function fetchMetadata(uri) {
    const url = convertIPFStoHTTP(uri);
    const response = await fetch(url);
    const metadata = await response.json();
    return metadata;
  }

  useEffect(() => {
    loadListedNFTs();
  }, []);

  const approveToken = async (price, tokenId) => {
    const tokenContract = new caver.klay.Contract(tokenContractABI, tokenContractAddress);
    const fromAddress = window.klaytn.selectedAddress; // 사용자 주소
  
    try {
      // 스마트 컨트랙트에 대한 승인 처리
      await tokenContract.methods.approve(marcketContractAddress, price).send({
        from: fromAddress,
        gas: '200000' // 예시 가스 한도
      });
      // 승인 후 구매 함수 호출
      purchaseNFT(tokenId, price);
    } catch (error) {
      console.error('Approve token failed', error);
    }
  };
  
  const purchaseNFT = async (tokenId, price) => {
    const marketplaceContract = new caver.klay.Contract(marcketContractABI, marcketContractAddress);
    const fromAddress = window.klaytn.selectedAddress; // 사용자 주소
  
    try {
      // 구매 처리
      await marketplaceContract.methods.purchaseNFT(tokenId).send({
        from: fromAddress,
        value: price,
        gas: '300000' // 예시 가스 한도
      });
      // 구매 후 NFT 목록을 다시 로드
      loadListedNFTs();
    } catch (error) {
      console.error('Buy NFT failed', error);
    }
  };

  const checkAllowance = async () => {
    const tokenContract = new caver.klay.Contract(tokenContractABI, tokenContractAddress);
    const fromAddress = window.klaytn.selectedAddress;
    try {
      const allowance = await tokenContract.methods.allowance(fromAddress, marcketContractAddress).call();
      console.log(`Allowance : ${allowance}`)
    } catch (error) {
      console.error('Error checking allowance', error);
    }
  };
  

  return (
    <div>
      <h2>Listed NFTs</h2>
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gridGap: '10px' }}>
        {listedNfts.map((nft) => (
          <div key={nft.tokenId} style={{ border: '1px solid #ddd', padding: '10px', borderRadius: '5px' }}>
            <img src={nft.imageUrl} alt={`NFT ${nft.tokenId}`} style={{ maxWidth: '100%', display: 'block', marginBottom: '5px' }} />
            <p>Token ID: {nft.tokenId}</p>
            <button onClick={() => approveToken(nft.price, nft.tokenId)}>
              Buy for {caver.utils.convertFromPeb(caver.utils.toBN(nft.price), 'KLAY')} MTK
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}
