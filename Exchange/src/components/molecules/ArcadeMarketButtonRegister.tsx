// ArcadeMarketButtonRegister.tsx

import React, { memo } from 'react';
import Button from '../atoms/Button.tsx';

interface ArcadeMarketButtonRegisterProps {
    
    onCreateNFT: () => void;
    onAuctionNFT: () => void;
    
}

const ArcadeMarketButtonRegister = ({   
    onCreateNFT, 
    onAuctionNFT
}: ArcadeMarketButtonRegisterProps) => {
    return (
        <div>
            <Button onClick = { onCreateNFT } > Create </Button>
            <Button onClick = { onAuctionNFT }> Auction </Button>
        </div>
    );
};

export default memo(ArcadeMarketButtonRegister);
