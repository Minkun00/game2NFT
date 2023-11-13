import React from 'react';

const BuyTokenButton = ({ caver, tokenContractAddress, tokenAbi }) => {
  const buyTokens = async (klayValue) => {
    try {
      const accounts = await caver.klay.getAccounts();
      const account = accounts[0];

      const tokenContract = new caver.klay.Contract(tokenAbi, tokenContractAddress);
      
      // Klay를 토큰 구매 함수로 전송
      tokenContract.methods.purchase().send({
        from: account,
        value: caver.utils.toPeb(klayValue, 'KLAY'),
        gas: 500000,
      })
        .on('transactionHash', (hash) => {
          console.log('Transaction Hash:', hash);
        })
        .on('receipt', (receipt) => {
          console.log('Transaction receipt:', receipt);
        })
        .on('error', console.error);
    } catch (error) {
      console.error('Error buying tokens:', error);
    }
  };

  return (
    <button onClick={() => buyTokens('5')}>Buy Tokens with 5 KLAY</button>
  );
};

export default BuyTokenButton;
