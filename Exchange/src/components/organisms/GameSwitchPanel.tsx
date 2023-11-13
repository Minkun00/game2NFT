// GameSwitchPanel.tsx

import React from 'react';
import styled from 'styled-components';
import GameSwitchButton from '../molecules/GameSwitchButton';

const GameSwitchPanelContainer = styled.div `
    display: flex;
    justify-content: space-between;
    width: 100%;
`;

const GameSwitchPanel: React.FC = () => {
    const handleButtonClick1 = () => {
        // 원하는 동작을 여기에 추가
        // label이나 onClick를 사용해줘야함
    };

    const handleButtonClick2 = () => {
        // 원하는 동작을 여기에 추가
    };

    const handleButtonClick3 = () => {
        // 원하는 동작을 여기에 추가
    };

    const handleButtonClick4 = () => {
        // 원하는 동작을 여기에 추가
    };

    return (
        <GameSwitchPanelContainer>
            <GameSwitchButton label = "Button 1" onClick = {handleButtonClick1}/>
            <GameSwitchButton label = "Button 2" onClick = {handleButtonClick2}/>
            <GameSwitchButton label = "Button 3" onClick = {handleButtonClick3}/>
            <GameSwitchButton label = "Button 4" onClick = {handleButtonClick4}/>
        </GameSwitchPanelContainer>
    );
};

export default GameSwitchPanel;