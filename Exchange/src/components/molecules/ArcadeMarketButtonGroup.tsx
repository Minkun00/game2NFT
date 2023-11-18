// ArcadeMarketButtonGroup.tsx

import React, { memo } from 'react';
import Button from '../atoms/Button.tsx';

interface ArcadeMarketButtonGroupProps {
    onPower: () => void;
    onCreateNFT: () => void;
    onAuctionNFT: () => void;
    onShowMyNFTs: () => void;
}

const ArcadeMarketButtonGroup = ({ 
    onPower, 
    onCreateNFT, 
    onAuctionNFT, 
    onShowMyNFTs,
}: ArcadeMarketButtonGroupProps) => {
    return (
        <div>
            <Button onClick = { onPower }> Power </Button>
            <Button onClick = { onCreateNFT } > Create </Button>
            <Button onClick = { onAuctionNFT }> Auction </Button>
            <Button onClick = {onShowMyNFTs }> MyNTFs </Button>
        </div>
    );
};

export default memo(ArcadeMarketButtonGroup);
