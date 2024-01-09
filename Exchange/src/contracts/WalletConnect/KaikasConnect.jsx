import React, { useState, useEffect } from 'react';
import Caver from 'caver-js';
import "../../App.css";


const KaikasConnect = ({ setConnectedWallet }) => {
  const [account, setAccount] = useState('');

  const loadBlockchainData = async (caverInstance) => {
    const accounts = await caverInstance.klay.getAccounts();
    if (accounts.length > 0) {
      setAccount(accounts[0]);
    } else {
      alert('Please connect to Kaikas!');
      console.log("loadBlockchainData Error!")
    }
  };

  const connectWalletHandler = async () => {
    if (window.klaytn) {
      const caverInstance = new Caver(window.klaytn);
      loadBlockchainData(caverInstance);

      const accounts = await window.klaytn.enable();
      setAccount(accounts[0]);
      setConnectedWallet('Kaikas');
    } else {
      alert('Please install Kaikas!');
      console.log("connectWalletHandler Error!")
    }
  };

  return (
    <div>
        <button className = "button" onClick={connectWalletHandler}>
          Connect to Kaikas
        </button>
    </div>
  );
};

export default KaikasConnect;