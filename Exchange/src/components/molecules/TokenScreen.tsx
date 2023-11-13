// TokenScreen.tsx

import React from 'react';
import styled from 'styled-components';
import Screen from '../atoms/Screen';

const StyledAtomScreen = styled(Screen) `
    width: 50px;
    height: 50px;
    background-color: lightblue;
`;

const TokenScreen: React.FC = () => {
    return (
        <StyledAtomScreen content = "Token Screen" />
    );
};

export default TokenScreen;