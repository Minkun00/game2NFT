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

function App() {
  const nftContractABI = myNFT.abi;
  const tokenContractABI = myToken.abi;
  const marcketContractABI = myMarcket.abi;
  const nftContractAddress = process.env.REACT_APP_NFT_CONTRACT_ADDRESS;
  const tokenContractAddress = process.env.REACT_APP_TOKEN_CONTRACT_ADDRESS;
  const marcketContractAddress = process.env.REACT_APP_MARKET_CONTRACT_ADDRESS;


  return (
    <Router> 
      <div>
        <h1>Exchange</h1>
        <nav>
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
            <li>
              <NavLink to="/klaytnNFT">Klaytn NFT</NavLink>
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
            <Route path="/klaytnNFT" element={<CheckKlaytnAPI/>}/>
        </Routes>
      </div>
      <p>ambition</p>
    </Router>
  );
}

export default App;


// import React from 'react';
// import AppTemplate from './templates/index';

// const App = () => {
//   return <AppTemplate />;
// };

// export default App;