import axios from 'axios';
import { useState, useCallback } from 'react';

/**
 * @description `server/Pinata/Pinata.js`의 api에 request
 * @returns {URL} ipfs에 저장된 imgURL
 * @returns {URL} ipfs에 저장된 NFT의 metadata가 저장된 URL
 * @returns {Function} generateImage
 */
export default function useImageGenerator() {
  const [imgUri, setImgUri] = useState(''); 
  const [tokenUri, setTokenUri] = useState('');
  
  /**
   * @description `server/Pinata/Pinata.js`의 api에 request 실행. 현재 `code`외의 값들은 NFT 발행에 큰 의미 없음
   * @param {String} code item code
   */
  const generateImage = useCallback(async (code) => {
    try {
      const action = 'encrypt';
      const response = await axios.post('http://localhost:3001/api/pinata', { code, action }); // 서버의 엔드포인트 URL로 수정하세요.
      const { imgUrl, tokenUri } = response.data;

      if (response.data.error) {
        
      }
      setImgUri(imgUrl);
      setTokenUri(tokenUri);
    } catch (error) {
      console.error("Error during image generation", error);
      if (error.response) {
        console.error("Server response error:", error.response.data);
      }
    }
  }, []);

  return { imgUri, tokenUri, generateImage };
}
