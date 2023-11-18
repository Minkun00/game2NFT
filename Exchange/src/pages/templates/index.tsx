// pages/templates/index.tsx
import React from 'react';
import AppName from '../../components/organisms/AppName.tsx';
import ArcadeMarketScreen from '../../components/organisms/ArcadeMarketScreen.tsx';
import TokenScreen from '../../components/organisms/TokenScreen.tsx';

const IndexTemplate = () => {
  
  return (
    <div>
      {window.location.pathname === '/app' && <AppName spanContent='ArcadeMarket' />}
      {window.location.pathname === '/arcade' && <ArcadeMarketScreen />}
      {window.location.pathname === '/token' && <TokenScreen />}

    </div>
  );
};

export default IndexTemplate;

