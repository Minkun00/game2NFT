import React, { useState, useEffect, useRef } from 'react';
import useImageGenerator from './useImageGenerator';
import Caver from 'caver-js';

const caver = new Caver(window.klaytn);

export default function ItemToImg({ nftContractABI, nftContractAddress, connectedWallet }) {
  const [code, setCode] = useState('');
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [imgLoaded, setImgLoaded] = useState(false);
  const { imgUri, tokenUri, generateImage } = useImageGenerator();

  const imgRef = useRef(null);

  const handleSubmit = async () => {
    try {
      await generateImage(code, name, description);
      console.log(`imgUri : ${imgUri}, tokenUri : ${tokenUri}`);
    } catch(error) {
      console.log('Error generating image: ', error);
      alert('Error generating NFT. Might be wrong code.');
    }
  };

  const handleImgLoad = () => {
    setImgLoaded(true);
  };

  useEffect(() => {
    if (imgLoaded) {
      console.log(`connectedWallet : ${connectedWallet}`);
      if (connectedWallet === 'Kaikas') {
        const mintNFT = async () => {
          try {
            const nftContract = new caver.klay.Contract(nftContractABI, nftContractAddress);
            const response = await nftContract.methods.mint(tokenUri).send({
              from: window.klaytn.selectedAddress,
              gas: '2000000',
            });
            console.log('NFT Minted!', response);
          } catch (error) {
            console.error('Error minting NFT:', error);
          }
        };

        mintNFT();
      } else if (connectedWallet === "Metamask") {
        // 현재 에러 있음. metamask에서 web3.js를 더이상 쓰지 않는다고 함. 좀 더 찾아봐야함.
        const mintNFT = async () => {
          try {
            const nftContract = new window.ethereum.Contract(nftContractABI, nftContractAddress);
            const accounts = await window.ethereum.request({ method: 'eth_requestAccounts' });
            
            const response = await nftContract.methods.mint(tokenUri).send({
              from: accounts[0],
              gas: '2000000',
            });
            console.log('NFT Minted!', response);
          } catch (error) {
            console.log('Error minting NFT: ', error);
          }
        }
        mintNFT();
      }
    } else {
      alert('Wallet is not Connected!')
    }
  }, [imgLoaded, tokenUri, nftContractABI, nftContractAddress]);

  return (
    <div>
      <h2>Enter Your Code</h2>

      <div className="item-form">
        <input
          type="text"
          placeholder="Code"
          value={code}
          onChange={e => setCode(e.target.value)}
        />
        <input
          type="text"
          placeholder="Name"
          value={name}
          onChange={e => setName(e.target.value)}
        />
        <input
          type="text"
          placeholder="Description"
          value={description}
          onChange={e => setDescription(e.target.value)}
        />
      
        {imgUri && (
          <div>
            <img
              ref={imgRef}
              src={imgUri.replace('ipfs://', 'https://ipfs.io/ipfs/')}
              alt="Generated"
              onLoad={handleImgLoad}
            />
          </div>
        )}
        <button className="input-button" onClick={handleSubmit}>Generate</button>
      </div>
    </div>
  );
}
