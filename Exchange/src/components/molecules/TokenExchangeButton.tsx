// TokenExchangeButton.jsx

import React from 'react';
import styled from 'styled-components';
import Button from '../atoms/Button';

const StyledAtomButton = styled(Button) `
    width: 50px;
    height: 50px;
`;

const TokenExchangeButton: React.FC = () => {
    const handleClick = () => {
         // 원하는 클릭 동작을 정의
    };
    return (
        <StyledAtomButton label = "Token Exchange Button" onClick = {handleClick} />
    );
};

export default TokenExchangeButton;