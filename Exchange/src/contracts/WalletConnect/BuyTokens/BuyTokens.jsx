import React from 'react';
import Caver from 'caver-js';
import "../../../App.css"

const BuyTokenButton = ({ tokenContractAddress, tokenContractAbi }) => {
  
  const buyTokens = async (klayValue) => {
    try {
      const caver = new Caver(window.klaytn);
      const accounts = await caver.klay.getAccounts();
      const account = accounts[0];

      const tokenContract = new caver.klay.Contract(tokenContractAbi, tokenContractAddress);
      
      tokenContract.methods.purchase().send({
        from: account,
        value: caver.utils.toPeb(klayValue, 'KLAY'),
        gas: 500000,
      })
    } catch (error) {
      console.error('Error buying tokens:', error);
    }
  };

  return (
    <button className="button" onClick={() => buyTokens('5')}>Buy Tokens with 5 KLAY</button>
  );
};

export default BuyTokenButton;
