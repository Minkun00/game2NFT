import axios from 'axios';
import { useState, useCallback } from 'react';

export default function useImageGenerator() {
  const [imgUri, setImgUri] = useState(''); 
  const [tokenUri, setTokenUri] = useState('');
  
  const generateImage = useCallback(async (code, name, description) => {
    try {
      const response = await axios.post('http://localhost:3001/api/pinata', { code, name, description }); // 서버의 엔드포인트 URL로 수정하세요.
      const { imgUrl, tokenUri, metaData } = response.data;
      setImgUri(imgUrl);
      setTokenUri(tokenUri);
    } catch (error) {
      console.error("Error during image generation", error);
      if (error.response) {
        // 서버에서 반환된 응답을 로그에 기록합니다.
        console.error("Server response error:", error.response.data);
      }
    }
  }, []);

  return { imgUri, tokenUri, generateImage };
}
