// GameSwitchButton.tsx

import React, { memo } from 'react';
import Button from '../atoms/Button';

interface GameSwitchButtonProps {
    startFirstGame: () => void;
    startSecondGame: () => void;
    startThirdGame: () => void;
    startFourthGame: () => void;
}

const GameSwitchButton = ({
    startFirstGame,
    startSecondGame,
    startThirdGame,
    startFourthGame
}: GameSwitchButtonProps)=> {
    return (
        <div>
            <Button onClick = { startFirstGame }> Game 1 </Button>
            <Button onClick = { startSecondGame }> Game 2 </Button>
            <Button onClick = { startThirdGame }> Game 3 </Button>
            <Button onClick = { startFourthGame }> Game 4 </Button>
        </div>
    );
};

export default memo(GameSwitchButton);