// AppName.tsx

import React, { memo } from 'react';
import Span from '../atoms/Span';

interface AppNameProps {
    spanContent: string;
};


const AppName = ({ spanContent }: AppNameProps) => {
    return (
        <div>
            <Span>
                {spanContent}
            </Span>
        </div>
    );
};

export default memo(AppName);