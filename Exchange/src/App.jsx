import React from 'react';
import KaikasConnect from './contracts/kaikasConnect/KaikasConnect';
import { BrowserRouter as Router, Route, Routes, NavLink } from 'react-router-dom';
import ItemToImg from './contracts/itemToIMG/itemToImg';
import CreateAuction from './contracts/createAuction/createAuction';
import Marketplace from './contracts/buyItem/market';
import myToken from './contracts/Hardhat_abis/MyToken.json';
import myNFT from './contracts/Hardhat_abis/MyNFT.json';
import myMarcket from './contracts/Hardhat_abis/MyMarketplace.json';
import CheckKlaytnAPI from './contracts/checkKlaytnAPI/checkKlaytnAPI_NFT';
import BuyTokenButton from './contracts/kaikasConnect/BuyTokens/BuyTokens';
import './App.css';

function App() {
  const nftContractABI = myNFT.abi;
  const tokenContractABI = myToken.abi;
  const marcketContractABI = myMarcket.abi;
  const nftContractAddress = process.env.REACT_APP_NFT_CONTRACT_ADDRESS;
  const tokenContractAddress = process.env.REACT_APP_TOKEN_CONTRACT_ADDRESS;
  const marcketContractAddress = process.env.REACT_APP_MARKET_CONTRACT_ADDRESS;


  return (
    <Router> 
      <div className="app-container">
        <h1>Welcome to "Game Item Market Place"</h1>
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
        <KaikasConnect />
        <BuyTokenButton
          tokenContractAddress={tokenContractAddress}
          tokenContractAbi={tokenContractABI}
        />
        <Routes>
          <Route path="/inputCode" element={
            <ItemToImg 
              nftContractABI={nftContractABI}
              nftContractAddress={nftContractAddress}
            />} /> 
          <Route path="/createAuction" element={
            <CreateAuction
              nftContractABI={nftContractABI}
              tokenContractABI={tokenContractABI}
              nftContractAddress={nftContractAddress}
              marcketContractABI={marcketContractABI}
              marcketContractAddress={marcketContractAddress}
            />
          } />
            <Route path="/market" element={
            <Marketplace
              tokenContractABI={tokenContractABI}
              tokenContractAddress={tokenContractAddress}
              nftContractABI={nftContractABI}
              nftContractAddress={nftContractAddress}
              marcketContractABI={marcketContractABI}
              marcketContractAddress={marcketContractAddress}
            />}/>
           
        </Routes>
      </div>
    </Router>
  );
}

export default App;