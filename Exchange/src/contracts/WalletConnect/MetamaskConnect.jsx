import React, {useState, useEffect} from 'react';

const MetamaskConnect = () => {
    const [account, setAccount] = useState('');

    useEffect(() => {
        const initialize = async () => {
            if (window.ethereum) {
                await loadMetaMaskData();
            }
        };
        initialize();
    }, []);

    const loadMetaMaskData = async () => {
        try {
            await window.ethereum.request({ method: 'eth_requestAccounts' });
            const accounts = await window.ethereum.request({ method: 'eth_accounts' });
            if (accounts.length > 0) {
                setAccount(accounts[0]);
            } 
        } catch (error) {
                console.log("Error loading Metamask Data: ", error);
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
            {account ? (
                <p className='account info'>Connected account (Metamask): {account}</p>
            ) : <button className='button' onClick={connectWalletHandler}>
                Connect to Metamask
                </button>}
        </div>
    )
}

export default MetamaskConnect;