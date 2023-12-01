import React, { useEffect } from 'react';
import './styles/SnowEffect.css'; 
import './styles/BitcoinLogo.css';

const SnowEffect = ({ showSnowEffect }) => {
  useEffect(() => {
    if (showSnowEffect) {
      // 눈송이 생성 함수
      const createSnowflake = () => {
        const snowflake = document.createElement('div');
        snowflake.className = 'snowflake';
        snowflake.style.left = Math.random() * window.innerWidth + 'px';
        document.body.appendChild(snowflake);



        // 눈송이 애니메이션 및 삭제
        snowflake.animate(
          [
            { transform: 'translateY(0)' },
            { transform: `translateY(${window.innerHeight}px)` },
          ],
          {
            duration: Math.random() * 5000 + 5000, // 5초에서 10초 사이의 랜덤한 애니메이션 시간
            iterations: Infinity,
            easing: 'linear',
            fill: 'forwards',
          }
        ).onfinish = () => {
          snowflake.remove();
          createSnowflake();
        };
      };

      const createBitcoinLogo = () => {
          const bitcoinLogo = document.createElement('div');
          bitcoinLogo.className = 'bitcoin-logo';
          bitcoinLogo.style.left = Math.random() * window.innerWidth + 'px';
          document.body.appendChild(bitcoinLogo);
    
          // 비트코인 로고 애니메이션 및 삭제
          bitcoinLogo.animate(
            [
              { transform: 'translateY(0)' },
              { transform: `translateY(${window.innerHeight}px)` },
            ],
            {
              duration: Math.random() * 5000 + 5000,
              iterations: Infinity,
              easing: 'linear',
              fill: 'forwards',
            }
          ).onfinish = () => {
            bitcoinLogo.remove();
            createBitcoinLogo();
          };
        };

      // 눈송이 생성 시작
      for (let i = 0; i < 50; i++) {
        createSnowflake();
        createBitcoinLogo();
      }
    }
  }, [showSnowEffect]); // useEffect 디펜던시 배열에 빈 배열을 전달하여 한 번만 실행

  return <div className="snow-effect" />;
};

export default SnowEffect;
