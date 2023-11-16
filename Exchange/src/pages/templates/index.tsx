// index.tsx
import React from 'react';
import AppName from '../../components/organisms/AppName';
import ArcadeMarketScreen from '../../components/organisms/ArcadeMarketScreen';
import TokenScreen from '../../components/organisms/TokenScreen';

const index = () => {
  
  return (
    <div>
      {window.location.pathname === '/app' && <AppName spanContent='ArcadeMarket' />}
      {window.location.pathname === '/arcade' && <ArcadeMarketScreen />}
      {window.location.pathname === '/token' && <TokenScreen />}

    </div>
  );
};

export default index;

