// GameSwitchButton.tsx

import React from 'react';
import styled from 'styled-components';
import Button from '../atoms/Button';

const StyledAtomButton = styled(Button) `
    width: 10px;
    height: 10px;
`;


const GameSwitchButton: React.FC<any> = ({ label, onClick }) => {
    return (
        <StyledAtomButton label = {label} onClick = {onClick} />
    );
};

export default GameSwitchButton;