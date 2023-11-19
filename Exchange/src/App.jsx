import React, { useState } from 'react';
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

function App() {
  const nftContractABI = myNFT.abi;
  const tokenContractABI = myToken.abi;
  const marcketContractABI = myMarcket.abi;
  const nftContractAddress = process.env.REACT_APP_NFT_CONTRACT_ADDRESS;
  const tokenContractAddress = process.env.REACT_APP_TOKEN_CONTRACT_ADDRESS;
  const marcketContractAddress = process.env.REACT_APP_MARKET_CONTRACT_ADDRESS;

  const [currentScreen, setCurrentScreen] = useState('black');
  const [registerScreen, setRegisterScreen] = useState(null);


  const handleMarketPlaceButtonClick = () => {
    console.log('Marketplace 버튼이 클릭되었습니다.');
    setCurrentScreen((prevScreen) => (prevScreen === 'market' ? 'black' : 'market'));

    
  };

  const handleMyNFTsButtonClick = () => {
    console.log('Klaytn NFT 버튼이 클릭되었습니다.');
    setCurrentScreen((prevScreen) => (prevScreen === 'klaytnNFT' ? 'black' : 'klaytnNFT'));

    
  };

  const handleCreateButtonClick = () => {
    setRegisterScreen((prevScreen) => (prevScreen === 'create' ? 'black' : 'create'));
  }

  const handleAuctionButtonClick = () => {
    setRegisterScreen((prevScreen) => (prevScreen === 'auction' ? 'black' : 'auction'));
  }


  const renderCurrentScreen = () => {
    switch (currentScreen) {
      case 'market':
        return (
          <Marketplace
            tokenContractABI={tokenContractABI}
            tokenContractAddress={tokenContractAddress}
            nftContractABI={nftContractABI}
            nftContractAddress={nftContractAddress}
            marcketContractABI={marcketContractABI}
            marcketContractAddress={marcketContractAddress}
          />
        );
      case 'klaytnNFT':
        return <CheckKlaytnAPI />;
      case 'black':
        return (
          <OffScreen/>
        );
      default:
        return null;
    }
  };
  
  const renderRegisterScreen = () => {
    switch (registerScreen) {
      case 'create':
        return (
          <ItemToImg
            nftContractABI={nftContractABI}
            nftContractAddress={nftContractAddress}
          />
        );
      case 'auction':
        return (
          <CreateAuction
            nftContractABI={nftContractABI}
            tokenContractABI={tokenContractABI}
            nftContractAddress={nftContractAddress}
            marcketContractABI={marcketContractABI}
            marcketContractAddress={marcketContractAddress}
          />
        );
      case 'black':
        return (
            <OffScreen/>
        );
      default:
        return null;
    }
  };

  const OffScreen = () => {
    return (
      <div
        style={{
          width: '100%',
          height: '600px',
          backgroundColor: 'black',
  
        }}
      ></div>
    );
  };




  return (
    <Router> 
      <div>
        <h1>Exchange</h1>
        <div style= {{maxHeight: '600px', overflowY: 'auto'}}>

        <button onClick={() => handleMarketPlaceButtonClick('market')}>Marketplace</button>
        <button onClick={() => handleMyNFTsButtonClick('klaytnNFT')}>Klaytn NFT</button>
    

        {renderCurrentScreen()}



        </div>  

        <div style= {{maxHeight: '200px', overflowY: 'auto'}}>
        <button onClick={() => handleCreateButtonClick('create')}>Create</button>
        <button onClick={() => handleAuctionButtonClick('auction')}>Auction</button>

        {renderRegisterScreen()}
        </div>
        <div style={{ position: 'fixed', bottom: 0, left: 0, width: '100%' }}>

        <KaikasConnect />
        <BuyTokenButton
          tokenContractAddress={tokenContractAddress}
          tokenContractAbi={tokenContractABI}
        />

    
    
          

        </div>
      
              
      </div >
    </Router>
  );
}

export default App;

