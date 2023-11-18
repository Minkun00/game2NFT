// pages/templates/index.tsx
import React from 'react';
import { Routes, Route } from 'react-router-dom'
import AppName from '../../components/organisms/AppName.tsx';
import ArcadeMarketScreen from '../../components/organisms/ArcadeMarketScreen.tsx';
import TokenScreen from '../../components/organisms/TokenScreen.tsx';

const IndexTemplate = () => {
  
  return (

    <div>
      <AppName spanContent="ArcadeMarket" />
      <ArcadeMarketScreen />
      <TokenScreen />
    </div>
   
      // <Routes>
      //   <Route path= '/' element={<AppName spanContent='ArcadeMarket' />} />
      //   <Route path= '/' element={<ArcadeMarketScreen />} />
      //   <Route path= '/' element={<TokenScreen />} />
      // </Routes>
    
  );
};

export default IndexTemplate;

