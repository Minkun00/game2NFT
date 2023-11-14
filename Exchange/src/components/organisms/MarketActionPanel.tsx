// MarketActionPanel.tsx

import React from 'react';
import styled from 'styled-components';
import NFTCreationButton from '../molecules/NFTCreationButton';
import NFTAuctionButton from '../molecules/NFTAuctionButton';
import MyNFTsButton from '../molecules/MyNFTsButton';

const MarketActionPanelContainer = styled.div `
    display: flex;
    justify-content: space-between;
    width: 100%;

`;

interface MarketActionPanelProps {
    label: string;
    onClick: () => void;
};

const MarketActionPanel: React.FC <MarketActionPanelProps> = () => {
    const handleButtonClick1 = () => {
        // 원하는 동작
    };

    const handleButtonClick2 = () => {
        // 원하는 동작
    };

    const handleButtonClick3 = () => {
        // 원하는 동작
    };

    return (
        <MarketActionPanelContainer>
            <NFTCreationButton label = "NFT Creation Button" onClick = {handleButtonClick1}/>
            <NFTAuctionButton label = "NFT Auction Button" onClick = {handleButtonClick2}/>
            <MyNFTsButton label = "My NFTs Button" onClick = {handleButtonClick3}/>
        </MarketActionPanelContainer>
    );
};

export default MarketActionPanel;