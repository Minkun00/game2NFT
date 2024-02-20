import React, { useState, useEffect, useRef } from 'react';
import useImageGenerator from './useImageGenerator';
import Caver from 'caver-js';
import { useNavigate } from 'react-router-dom';

const caver = new Caver(window.klaytn);

/**
 * @description 입력된 code값을 기반으로 NFT minting 진행
 * @returns {React.ReactComponentElement}
 */
export default function ItemToImg({ nftContractABI, nftContractAddress }) {
  // params for generateImage
  const [code, setCode] = useState('');

  const [imgLoaded, setImgLoaded] = useState(false);
  const { imgUri, tokenUri, generateImage } = useImageGenerator();
  const navigate = useNavigate();

  // api를 통해 받아온 img
  const imgRef = useRef(null);

  /**
   * @description Pinata.js api 호출
   */
  const handleSubmit = async () => {
    try {
      await generateImage(code);
    } catch(error) {
      console.log('Error generating image: ', error);
      alert('Error generating NFT. Might be wrong code.');
    }
  };

  /**
   * @description img loading 표시용
   */
  const handleImgLoad = () => {
    setImgLoaded(true);
  };

  useEffect(() => {
    if (imgLoaded) {
      const mintNFT = async () => {
        try {
          const nftContract = new caver.klay.Contract(nftContractABI, nftContractAddress);
          // minting 진행
          await nftContract.methods.mint(tokenUri).send({
            from: window.klaytn.selectedAddress,
            gas: '2000000',
          });

          // minting 완료 시, /CreateAuction 으로 넘김(중복 실행 방지)
          alert('NFT Minted! Check it on "CreactAuction"!');
          navigate('/createAuction');
        } catch (error) {
          console.error('Error minting NFT:', error);
        }
      };
  
      mintNFT();
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
