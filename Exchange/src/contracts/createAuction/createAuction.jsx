import React, { useState, useEffect } from 'react';
import './createAuction.css';

import Caver from 'caver-js';

export default function CreateAuction({ nftContractABI, marcketContractABI, nftContractAddress, marcketContractAddress, connectedWallet }) {
  const [tokens, setTokens] = useState([]);
  const caver = new Caver(window.klaytn);
  const [selectedTokenId, setSelectedTokenId] = useState(null);
  const [price, setPrice] = useState('');

  const ITEMS_PER_PAGE = 8;
  const [currentPage, setCurrentPage] = useState(1);
  const [tokenBalance, setTokenBalance] = useState(0);

  const [loading, setLoading] = useState(false);

  const selectNFT = (tokenId) => {
    setSelectedTokenId(tokenId);
  };
  const handlePriceChange = (e) => {
    setPrice(e.target.value);
  };

  // marketApproved is for only once
  const isApprovedForMarket = async () => {
    try {
      const nftContract = new caver.klay.Contract(nftContractABI, nftContractAddress);
      
      const response = await nftContract.methods.isMarketplaceApproved().call({
        from: window.klaytn.selectedAddress,
        gas: '2000000'
      })

      return response
    } catch (e) {
      console.log(`Error for 'isApprovedForMarket' : ${e}`);
    }
  }

  const listNFT = async () => {
    if (selectedTokenId && price) {
      const nftContract = new caver.klay.Contract(nftContractABI, nftContractAddress);
  
      try {
        await nftContract.methods.setMarketplaceApproval(marcketContractAddress, true).send({
          from: window.klaytn.selectedAddress,
          gas: '2000000',
        });
        console.log(`Marketplace approval set for token ID: ${selectedTokenId}`);
  
        const marketplaceContract = new caver.klay.Contract(marcketContractABI, marcketContractAddress);
  
        const listingPrice = caver.utils.toWei(price.toString(), 'ether');

        await marketplaceContract.methods.listNFT(selectedTokenId, listingPrice).send({
          from: window.klaytn.selectedAddress,
          gas: '2000000',
        });
        loadNFTs()
      } catch (error) {
        console.error("Setting marketplace approval or listing NFT failed", error);
      }
    } else {
      console.log("No token selected or price set");
    }
  };
  


  const loadNFTs = async () => {
    try {
      setLoading(true);
    
      const nftContract = new caver.klay.Contract(nftContractABI, nftContractAddress);
      const balanceOf = await nftContract.methods.balanceOf(window.klaytn.selectedAddress).call();
      setTokenBalance(balanceOf);

      const tokenDetails = [];
      
      const startIndex = (currentPage - 1) * ITEMS_PER_PAGE;
      const endIndex = Math.min(currentPage * ITEMS_PER_PAGE, balanceOf);
      console.log(`tokens : ${tokenBalance}`)
     
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
      console.log(startIndex, endIndex)
      setTokens(tokenDetails);
    } catch (error) {
      console.error('Error loading NFTs : ', error);
    } finally {
      setLoading(false);
    }
    
  };

  function convertIPFStoHTTP(ipfsUrl) {
    return ipfsUrl.replace('ipfs://', 'https://ipfs.io/ipfs/')
  }
  
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


  const handleNextPage = async () => {
    setCurrentPage((prevPage) => prevPage + 1);
    await loadNFTs();
  }

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
          <button className="input-button"onClick={listNFT}>List NFT for Sale</button>
          <div>
            <button onClick={handlePrevPage} disabled={currentPage === 1}>Previous Page</button>
            <button onClick={handleNextPage} disabled={currentPage * ITEMS_PER_PAGE >= tokenBalance}>Next Page</button>
          </div>
        </div>)}
    </div>
  );
}


