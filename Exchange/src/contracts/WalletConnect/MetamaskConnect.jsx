import React, {useState, useEffect} from 'react';
import "../../App.css";

const MetamaskConnect = ({ setIsMetamaskConnected, setConnectedWallet }) => {
    const [account, setAccount] = useState('');

    const loadMetaMaskData = async () => {
        try {
            await window.ethereum.request({ method: 'eth_requestAccounts' });
            const accounts = await window.ethereum.request({ method: 'eth_accounts' });
            if (accounts.length > 0) {
                setAccount(accounts[0]);
                setIsMetamaskConnected(true);
                setConnectedWallet('Metamask');
            } 
        } catch (error) {
                console.log("Error loading Metamask Data: ", error);
                setIsMetamaskConnected(false);
        }
    };

    const connectWalletHandler = async () => {
        if (window.ethereum) {
            await loadMetaMaskData();
        } else {
            alert('Please install Metamask!');
        }
    };

    return (
        <div>
            <button className='button' onClick={connectWalletHandler}>
                Connect to Metamask
            </button>
        </div>
    )
}

export default MetamaskConnect;