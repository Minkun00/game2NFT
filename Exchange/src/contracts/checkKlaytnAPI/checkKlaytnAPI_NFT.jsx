import React, { useState, useEffect } from 'react';
import axios from 'axios';

const CheckKlaytnAPI = () => {
  const [data, setData] = useState(null);
  const [images, setImages] = useState([]); // 이미지 URL을 저장할 상태
  const [isLoading, setIsLoading] = useState(true);
  const [isImagesLoading, setIsImagesLoading] = useState(false); // 이미지 로딩 상태
  const [error, setError] = useState(null);

  // .env 파일을 로드하기 위한 환경 변수
  const API_URL = 'https://th-api.klaytnapi.com';
  const address = '0x2b85Fc92EbF1e8f516d4aea7f3051867DeA03e16';
  const xChainId = '1001';
  const username = process.env.REACT_APP_USERNAME;
  const password = process.env.REACT_APP_PASSWORD;

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(`${API_URL}/v2/account/${address}/token`, {
          headers: {
            'x-chain-id': xChainId,
            Authorization: `Basic ${Buffer.from(`${username}:${password}`).toString('base64')}`,
          }
        });
        setData(response.data);
        // 새로운 이미지 URL을 불러오는 상태를 true로 설정
        setIsImagesLoading(true);
        await fetchImages(response.data.items);
      } catch (error) {
        setError(error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchData();
  }, [username, password]);

  const fetchImages = async (items) => {
    try {
      const imageRequests = items.map(item => {
        const tokenUri = item.extras.tokenUri;
        return axios.get(tokenUri).then(res => {
          // "image"가 정의되어 있지 않거나 문자열이 아니면 처리할 수 없으므로 건너뛴다
          if (!res.data || typeof res.data.image !== 'string') {
            return null;
          }
          const image = res.data.image;
          // IPFS 링크인 경우 HTTP URL로 변환
          if (image.startsWith('ipfs://')) {
            return `https://ipfs.io/ipfs/${image.substring(7)}`;
          }
          return image;
        });
      });
      const imageUrls = (await Promise.all(imageRequests)).filter(url => url); // null 값 제거
      setImages(imageUrls);
    } catch (error) {
      setError(error);
    } finally {
      setIsImagesLoading(false); // 이미지 로딩 완료
    }
  };
  
  

  if (isLoading) return <div>Loading...</div>;
  if (isImagesLoading) return <div>Loading images...</div>;
  if (error) return <div>Error: {error.message}</div>;

  return (
    <div>
      <h1>Data Fetched</h1>
      <h2>Images</h2>
      <div>
        {images.map((image, index) => (
          <img key={index} src={image} alt={`Item ${index}`} />
        ))}
      </div>
    </div>
  );
};

export default CheckKlaytnAPI;
