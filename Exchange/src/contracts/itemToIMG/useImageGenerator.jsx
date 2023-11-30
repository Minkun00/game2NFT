import axios from 'axios';
import { useState, useCallback } from 'react';

export default function useImageGenerator() {
  const [imgUri, setImgUri] = useState(''); 
  const [tokenUri, setTokenUri] = useState('');
  
  const generateImage = useCallback(async (code, name, description) => {
    try {
      const response = await axios.post('http://localhost:3001/api/pinata', { code, name, description }); // 서버의 엔드포인트 URL로 수정하세요.
      const { imgUrl, tokenUri, metaData } = response.data;

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
