// TokenActionPanel.tsx

import React from 'react';
import styled from 'styled-components';
import TokenScreen from '../molecules/TokenScreen';
import TokenExchangeButton from '../molecules/TokenExchangeButton';

const TokenActionPanelContainer = styled.div`
    display: flex;
    justify-content: space-between;
    width: 100%
`;

interface TokenActionPanelProps {
    label: string;
    onClick: () => void;
};

const TokenActionPanel: React.FC <TokenActionPanelProps> = ({ label, onClick }) => {
    const handleButtonClick = () => {
        // 원하는 동작
    };
    return (
        <TokenActionPanelContainer>
            <TokenScreen content = "Token Screen" />
            <TokenExchangeButton label = "Exchange" onClick = {handleButtonClick}/>
        </TokenActionPanelContainer>

    );
};

export default TokenActionPanel;