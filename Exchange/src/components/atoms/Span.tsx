// Span.tsx

import React, { memo } from 'react';
import styled from 'styled-components';

interface SpanProps {
    children;
}

const StyledSpan = styled.span<SpanProps>`
    color: #333;
    font-size: 1.2em;
    font-weight: bold;
    background-color: #f0f0f0;
    padding: 0.5em;
    border-radius: 5px;
    margin: 0.5em;
`;

const Span = ({ children }: SpanProps) => {

    return (
        <StyledSpan>
            {children}
        </StyledSpan>
    );
};

export default memo(Span);