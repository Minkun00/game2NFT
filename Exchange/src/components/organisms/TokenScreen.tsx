// TokenScreen.tsx

import React, { useState } from 'react';
import TokenExchangeButton from '../molecules/TokenExchangeButton';
import KaikasConnect from '../../contracts/kaikasConnect/KaikasConnect';
import myToken from '../../contracts/Hardhat_abis/MyToken.json';

const TokenScreen = () => {

    const [isPowerOn, setIsPowerOn] = useState(false);
    const [currentScreen, setCurrentScreen] = useState<string | null > (null);

    const tokenContractABI = myToken.abi;
    const tokenContractAddress = process.env.REACT_APP_TOKEN_CONTRACT_ADDRESS;

    const handleConnectionKaikasButtonClick = () => {
        setIsPowerOn(!isPowerOn);
        // setCurrentScreen('connectKaikas');

    };

    const handleExchangeTokenButtonClick = () => {
        setCurrentScreen('exchangeToken');
    };


    return (
        <div>
            <TokenExchangeButton
                onConnectKaikas = {handleConnectionKaikasButtonClick}
                onConvertKlayToToken={handleExchangeTokenButtonClick}
            />

            {currentScreen == 'connectKaikas' && <KaikasConnect 
                                                    tokenContractABI={tokenContractABI} 
                                                    tokenContractAddress={tokenContractAddress} />}  
        </div>
    );




};

export default TokenScreen;