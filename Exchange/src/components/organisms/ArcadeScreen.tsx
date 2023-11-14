// ArcadeScreen.tsx

import React from 'react';
import styled from 'styled-components';
import Screen from '../atoms/Screen';

const StyledAtomScreen = styled(Screen) `
    width: 400px;
    height: 300px;
    background-color: lightblue;
`;

const ArcadeScreen: React.FC = () => {
    return (
        <StyledAtomScreen content = "Arcade Screen" />
    );
};

export default ArcadeScreen;