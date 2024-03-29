import React, { useState } from 'react';
import KaikasConnect from './contracts/WalletConnect/KaikasConnect';
import { BrowserRouter as Router, Route, Routes, NavLink } from 'react-router-dom';
import ItemToImg from './contracts/itemToIMG/itemToImg';
import CreateAuction from './contracts/createAuction/createAuction';
import Marketplace from './contracts/buyItem/market';
import myToken from './contracts/Hardhat_abis/MyToken.json';
import myNFT from './contracts/Hardhat_abis/MyNFT.json';
import myMarcket from './contracts/Hardhat_abis/MyMarketplace.json';
import BuyTokenButton from './contracts/WalletConnect/BuyTokens/BuyTokens';
import './App.css';
import SnowEffect from './SnowEffect';

function AppContent() {
  // contract 사용을 위한 변수 선언
  const nftContractABI = myNFT.abi;
  const tokenContractABI = myToken.abi;
  const marcketContractABI = myMarcket.abi;
  const nftContractAddress = process.env.REACT_APP_NFT_CONTRACT_ADDRESS;
  const tokenContractAddress = process.env.REACT_APP_TOKEN_CONTRACT_ADDRESS;
  const marcketContractAddress = process.env.REACT_APP_MARKET_CONTRACT_ADDRESS;

  const [showSnowEffect, setShowSnowEffect] = useState(false);
  const [connectedWallet, setConnectedWallet] = useState(null);

  /**
   * @description SnowEffect On/Off
   */
  const toggleSnowEffect = () => {
    setShowSnowEffect((prev) => !prev);
    console.log("showSnowEffect", showSnowEffect);
    console.log("Connected Wallet : ", connectedWallet);
  };
  
  /**
   * @description Kaikas On/Off
   */
  const handleWalletOff = () => {
    if (connectedWallet) {
      setConnectedWallet(null);
    }
  };


  return (
    <> 
      <SnowEffect showSnowEffect={showSnowEffect}/>
      <div className="app-container">    
        <h1>EXCHANGE</h1>
        <button className="button" onClick={toggleSnowEffect}>{showSnowEffect ? 'Hide Snow Effect' : 'Show Snow Effect'}</button>

        {/*Nav Bar : 1*/}
        <nav className='nav'>
          <ul>
            <li>         
                <KaikasConnect 
                  setConnectedWallet={setConnectedWallet}/>
            </li>
            <li>
              {connectedWallet && (
                <button className="button" onClick={handleWalletOff}>Wallet Off</button>
              )}
            </li>
            <li>
            <BuyTokenButton
              tokenContractAddress={tokenContractAddress}
              tokenContractAbi={tokenContractABI}
            />
            </li>
          </ul>
        </nav><br/>

        {/*Nav Bar : 2*/}
        <nav className = "nav">
          <ul>
            <li>
              <NavLink to="/inputCode">Item to Image</NavLink>
            </li>
            <li>
              <NavLink to="/createAuction">Create Auction</NavLink>
            </li>
            <li>
              <NavLink to="/market">Marketplace</NavLink>
            </li>
          </ul>
        </nav>

         
        <Routes>
          <Route path="/inputCode" element={
            <ItemToImg 
              nftContractABI={nftContractABI}
              nftContractAddress={nftContractAddress}
            />} 
          /> 
          <Route path="/createAuction" element={
            <CreateAuction
              nftContractABI={nftContractABI}
              tokenContractABI={tokenContractABI}
              nftContractAddress={nftContractAddress}
              marcketContractABI={marcketContractABI}
              marcketContractAddress={marcketContractAddress}
            />}
          />
            <Route path="/market" element={
            <Marketplace
              tokenContractABI={tokenContractABI}
              tokenContractAddress={tokenContractAddress}
              nftContractABI={nftContractABI}
              nftContractAddress={nftContractAddress}
              marcketContractABI={marcketContractABI}
              marcketContractAddress={marcketContractAddress}
            />}
          />
        </Routes>
        <h6>Network : Baobab testnet</h6>
      </div> 
    </>
  );
}

function App() {
  return (
    <Router>
      <Routes>
        <Route path='*' element={<AppContent/>}/>
      </Routes>
    </Router>
  );
}

export default App;