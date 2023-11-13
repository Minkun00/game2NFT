// GameSwitchButton.tsx

import React from 'react';
import styled from 'styled-components';
import Button from '../atoms/Button';

const StyledAtomButton = styled(Button) `
    width: 10px;
    height: 10px;
`;

interface GameSwitchButtonProps {
    label: string;
    onClick: () => void;
};

const GameSwitchButton: React.FC <GameSwitchButtonProps> = ({ label, onClick }) => {
    const handleClick = () => {
         // 원하는 클릭 동작을 정의
    };
    return (
        <StyledAtomButton label = {label} onClick = {handleClick} />
    );
};

export default GameSwitchButton;