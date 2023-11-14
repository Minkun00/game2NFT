// TokenExchangeButton.jsx

import React from 'react';
import styled from 'styled-components';
import Button from '../atoms/Button';

const StyledAtomButton = styled(Button) `
    width: 50px;
    height: 50px;
`;

const TokenExchangeButton: React.FC <any> = ({ label, onClick }) => {
    return (
        <StyledAtomButton label = {label} onClick = {onClick} />
    );
};

export default TokenExchangeButton;