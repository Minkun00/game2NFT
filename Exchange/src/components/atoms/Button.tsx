// Button.tsx

import React, { memo } from 'react';
import styled from 'styled-components';

interface ButtonProps {
    children?: React.ReactNode;
    onClick?: () => void;

};

const StyledButton = styled.button <ButtonProps>`

    justify-content: center;
    align-items: stretch;
    border-radius: 3.7px;
    cursor: pointer;
    border: 1px solid black;

    &.small {
        padding: 7px 7px;
        font-size: 1rem;
    }

    &.normal {
        padding: 10px 10px;
        font-size: 1.2rem;
    }
    &.big {
        padding: 14px 14px;
        font-size: 1.4rem;
    }


`;

const Button = ({ onClick, children }: ButtonProps) => (
    <StyledButton onClick={onClick}>{children}</StyledButton>
);

export default memo(Button);