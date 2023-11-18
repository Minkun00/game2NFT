// ArcadeMarketSreen.tsx

import React, { useState } from 'react';
import ArcadeMarketButtonScreen from '../molecules/ArcadeMarketButtonScreen.tsx';
import ArcadeMarketButtonRegister from '../molecules/ArcadeMarketButtonRegister.tsx';
import CheckKlaytnAPI from '../../contracts/checkKlaytnAPI/checkKlaytnAPI_NFT';
import ItemToImg from '../../contracts/itemToIMG/itemToImg';
import Marketplace from '../../contracts/buyItem/market';
import CreateAuction from '../../contracts/createAuction/createAuction';
import myToken from '../../contracts/Hardhat_abis/MyToken.json';
import myNFT from '../../contracts/Hardhat_abis/MyNFT.json';
import myMarcket from '../../contracts/Hardhat_abis/MyMarketplace.json';


const ArcadeMarketScreen = () => {

    // const [isPowerOn, setIsPowerOn] = useState(false);
    const [currentScreen1, setCurrentScreen1] = useState<string | null > (null);
    const [currentScreen2, setCurrentScreen2] = useState<string | null > (null);

    const nftContractABI = myNFT.abi;
    const tokenContractABI = myToken.abi;
    const marcketContractABI = myMarcket.abi;
    const nftContractAddress = process.env.REACT_APP_NFT_CONTRACT_ADDRESS;
    const tokenContractAddress = process.env.REACT_APP_TOKEN_CONTRACT_ADDRESS;
    const marcketContractAddress = process.env.REACT_APP_MARKET_CONTRACT_ADDRESS;


    const handlePowerButtonClick = () => {
        // setIsPowerOn(!isPowerOn);
        setCurrentScreen1((prevScreen) => (prevScreen === 'showMarketPlace' ? null : 'showMarketPlace'));
        

    };

    const handleCreationNFTButtonClick = () => {
        setCurrentScreen2((prevScreen) => (prevScreen === 'createNFT' ? null : 'createNFT'));

    };

    const handleAuctionNFTButtonClick = () => {
        setCurrentScreen2((prevScreen) => (prevScreen === 'auctionNFT' ? null : 'auctionNFT'));

    };

    const handleMyNFTsButtonClick = () => {
        setCurrentScreen1((prevScreen) => (prevScreen === 'showMyNFTs' ? null : 'showMyNFTs'));
        

    };

    const renderScreen1 = () => {

        
        switch (currentScreen1) {
            case 'showMarketPlace' :
                return <Marketplace
                tokenContractABI={tokenContractABI}
                tokenContractAddress={tokenContractAddress}
                nftContractABI={nftContractABI}
                nftContractAddress={nftContractAddress}
                marcketContractABI={marcketContractABI}
                marcketContractAddress={marcketContractAddress}
                />
            case 'showMyNFTs':
                return <CheckKlaytnAPI />
            default:
                return null;

            
        }        
    }

    const renderScreen2 = () => {

        
    
    switch (currentScreen2) {
        case 'createNFT':
            return <ItemToImg
            nftContractABI={nftContractABI}
            nftContractAddress={nftContractAddress}/>
        case 'auctionNFT':
            return <CreateAuction 
            nftContractABI={nftContractABI}
            nftContractAddress={nftContractAddress}
            marcketContractABI={marcketContractABI}
            marcketContractAddress={marcketContractAddress}/>
        default:
            return null;
    

    }


    }



    return (
        <div>
            <div style={{ maxHeight: '500px', overflowY: 'auto' }}>
                <ArcadeMarketButtonScreen
                onShowMarketPlace = {handlePowerButtonClick}
                onShowMyNFTs = {handleMyNFTsButtonClick}
                
            />
            {renderScreen1()}
            </div>
        
            <div style={{ maxHeight: '200px', overflowY: 'auto' }}>
                <ArcadeMarketButtonRegister
                onCreateNFT = {handleCreationNFTButtonClick}
                onAuctionNFT = {handleAuctionNFTButtonClick}
            />
            {renderScreen2()}
            </div>

        </div>
    );

};

export default ArcadeMarketScreen;