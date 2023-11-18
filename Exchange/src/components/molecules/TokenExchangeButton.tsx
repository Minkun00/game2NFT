// TokenExchangeButton.jsx

import React, { memo } from 'react';
import Button from '../atoms/Button.tsx';

interface TokenExchangeButtonProps {
    onConnectKaikas: () => void;
    onConvertKlayToToken: () => void;
}

const TokenExchangeButton = ({ onConnectKaikas, onConvertKlayToToken }: TokenExchangeButtonProps) => {
    return (
        <div>
            <Button onClick = { onConnectKaikas }> ConnectKaikas </Button>
            <Button onClick = { onConvertKlayToToken }> KlayToToken </Button>
        </div>
        
    );
};

export default memo(TokenExchangeButton);