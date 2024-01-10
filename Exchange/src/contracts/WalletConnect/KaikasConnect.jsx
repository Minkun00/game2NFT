import React, { useState } from 'react';
import Caver from 'caver-js';
import "../../App.css";

/**
 * @description Kaikas 연결. 현재 github.page에도 이 상태라 고치지 않고 냅둠. 
 *              원래는 alert하는 내용 한 번만 해도 충분한데 안고쳐놨었음
 * @param {String} setConnectedWallet  
 * @returns {React.ButtonHTMLAttributes}
 */
const KaikasConnect = ({ setConnectedWallet }) => {
  const [account, setAccount] = useState('');

  /**
   * @description account 정보 받아옴
   * @param {Caver} caverInstance 
   */
  const loadBlockchainData = async (caverInstance) => {
    const accounts = await caverInstance.klay.getAccounts();
    if (accounts.length > 0) {
      setAccount(accounts[0]);
    } else {
      alert('Please connect to Kaikas!');
      console.log("loadBlockchainData Error!")
    }
  };

  /**
   * @description account 정보 저장
   * @see loadBlockchainData
   */
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