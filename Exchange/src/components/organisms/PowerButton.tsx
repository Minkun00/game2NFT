// PowerButton.tsx

import React from 'react';
import styled from 'styled-components';
import Button from '../atoms/Button';

const StyledAtomButton = styled(Button) `
    width: 30px;
    height: 30px;
`;

interface PowerButtonProps {
    label: string;
    onClick: () => void;
};

const PowerButton: React.FC <PowerButtonProps> = ({ label, onClick }) => {
    const handleClick = () => {
         // 원하는 클릭 동작을 정의
    };
    return (
        <StyledAtomButton label = {label} onClick = {handleClick} />
    );
};

export default PowerButton;