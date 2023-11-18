// ArcadeMarketButtonScreen.tsx

import React, { memo } from 'react';
import Button from '../atoms/Button.tsx';

interface ArcadeMarketButtonScreenProps {
    onShowMarketPlace: () => void;
    onShowMyNFTs: () => void;
}

const ArcadeMarketButtonScreen = ({ 
    onShowMarketPlace, 
    onShowMyNFTs,
}: ArcadeMarketButtonScreenProps) => {
    return (
        <div>
            <Button onClick = { onShowMarketPlace }> MarketPlace </Button>
            <Button onClick = {onShowMyNFTs }> MyNTFs </Button>
        </div>
    );
};

export default memo(ArcadeMarketButtonScreen);
