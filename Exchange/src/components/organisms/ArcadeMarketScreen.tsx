// ArcadeMarketSreen.tsx

import React, { useState } from 'react';
import ArcadeMarketButtonGroup from '../molecules/ArcadeMarketButtonGroup.tsx';
import CheckKlaytnAPI from '../../contracts/checkKlaytnAPI/checkKlaytnAPI_NFT';
import ItemToImg from '../../contracts/itemToIMG/itemToImg';
import Marketplace from '../../contracts/buyItem/market';
import CreateAuction from '../../contracts/createAuction/createAuction';
import myToken from '../../contracts/Hardhat_abis/MyToken.json';
import myNFT from '../../contracts/Hardhat_abis/MyNFT.json';
import myMarcket from '../../contracts/Hardhat_abis/MyMarketplace.json';


const ArcadeMarektScreen = () => {

    const [isPowerOn, setIsPowerOn] = useState(false);
    const [currentScreen, setCurrentScreen] = useState<string | null > (null);

    const nftContractABI = myNFT.abi;
    const tokenContractABI = myToken.abi;
    const marcketContractABI = myMarcket.abi;
    const nftContractAddress = process.env.REACT_APP_NFT_CONTRACT_ADDRESS;
    const tokenContractAddress = process.env.REACT_APP_TOKEN_CONTRACT_ADDRESS;
    const marcketContractAddress = process.env.REACT_APP_MARKET_CONTRACT_ADDRESS;


    const handlePowerButtonClick = () => {
        setIsPowerOn(!isPowerOn);
        setCurrentScreen('showMarketPlace');


    };

    const handleCreationNFTButtonClick = () => {
        setCurrentScreen('createNFT');

    };

    const handleAuctionNFTButtonClick = () => {
        setCurrentScreen('auctionNFT');

    };

    const handleMyNFTsButtonClick = () => {
        setCurrentScreen('showMyNFTs');

    };


    return (
        <div>
            <ArcadeMarketButtonGroup
            onPower = {handlePowerButtonClick}
            onCreateNFT = {handleCreationNFTButtonClick}
            onAuctionNFT = {handleAuctionNFTButtonClick}
            onShowMyNFTs = {handleMyNFTsButtonClick}
            />

            {currentScreen === 'showMarketPlace' && <Marketplace
                                                        tokenContractABI={tokenContractABI}
                                                        tokenContractAddress={tokenContractAddress}
                                                        nftContractABI={nftContractABI}
                                                        nftContractAddress={nftContractAddress}
                                                        marcketContractABI={marcketContractABI}
                                                        marcketContractAddress={marcketContractAddress}
            />}
            {currentScreen === 'createNFT' && <ItemToImg
                                                nftContractABI={nftContractABI}
                                                nftContractAddress={nftContractAddress}/>}
            {currentScreen === 'auctionNFT' && <CreateAuction 
                                                nftContractABI={nftContractABI}
                                                nftContractAddress={nftContractAddress}
                                                marcketContractABI={marcketContractABI}
                                                marcketContractAddress={marcketContractAddress}/>}
            {currentScreen === 'showMyNFTs' && <CheckKlaytnAPI />}

        </div>
    );

};

export default ArcadeMarektScreen;