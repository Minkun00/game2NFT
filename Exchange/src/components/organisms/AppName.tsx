// AppName.tsx

import React, { memo } from 'react';
import Span from '../atoms/Span.tsx';

interface AppNameProps {
    spanContent: string;
};


const AppName = ({ spanContent }: AppNameProps) => {
    console.log('AppName rendering'); // 렌더링이 되는지 콘솔에 출력
    console.log('spanContent:', spanContent); // spanContent 값 확인
    return (
        <div>
            <Span>
                {spanContent}
            </Span>
        </div>
    );
};

export default memo(AppName);