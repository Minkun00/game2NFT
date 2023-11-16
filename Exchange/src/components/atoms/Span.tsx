// Span.tsx

import React, { memo } from 'react';
import styled from 'styled-components';

interface SpanProps {
    children;
}

const StyledSpan = styled.span<SpanProps>`

`;

const Span = ({ children }: SpanProps) => {

    return (
        <StyledSpan>
            {children}
        </StyledSpan>
    );
};

export default memo(Span);