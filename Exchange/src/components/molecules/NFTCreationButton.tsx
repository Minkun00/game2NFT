// NFTCreationButton.tsx

import React from 'react';
import styled from 'styled-components';
import Button from '../atoms/Button';

const StyledAtomButton = styled(Button) `
    width: 20px;
    height: 20px;
`;

const NFTCreationButton: React.FC = () => {
    const handleClick = () => {
         // 원하는 클릭 동작을 정의
    };
    return (
        <StyledAtomButton label = "NFT Creation Button" onClick = {handleClick} />
    );
};

export default NFTCreationButton;