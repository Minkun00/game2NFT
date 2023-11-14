// MyNFTsButton.tsx

import React from 'react';
import styled from 'styled-components';
import Button from '../atoms/Button';

const StyledAtomButton = styled(Button) `
    width: 20px;
    height: 20px;
`;


const MyNFTsButton: React.FC <any> = ({ label, onClick }) => {
    return (
        <StyledAtomButton label = {label} onClick = {onClick} />
    );
};

export default MyNFTsButton;