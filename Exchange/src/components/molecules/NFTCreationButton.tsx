// NFTCreationButton.tsx

import React from 'react';
import styled from 'styled-components';
import Button from '../atoms/Button';

const StyledAtomButton = styled(Button) `
    width: 20px;
    height: 20px;
`;

const NFTCreationButton: React.FC <any> = ({ label, onClick}) => {
    return (
        <StyledAtomButton label = {label} onClick = {onClick} />
    );
};

export default NFTCreationButton;