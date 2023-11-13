// MyNFTsButton.tsx

import React from 'react';
import styled from 'styled-components';
import Button from '../atoms/Button';

const StyledAtomButton = styled(Button) `
    width: 20px;
    height: 20px;
`;

interface MyNFTsButtonProps {
    label: string;
    onClick: () => void;
};

const MyNFTsButton: React.FC <MyNFTsButtonProps> = ({ label, onClick }) => {
    const handleClick = () => {
         // 원하는 클릭 동작을 정의
    };
    return (
        <StyledAtomButton label = {label} onClick = {handleClick} />
    );
};

export default MyNFTsButton;