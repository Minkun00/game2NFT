// pages/index.tsx

import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import AppTemplate from './templates/index.tsx';

const Index = (): React.JSX.Element => {
  return ( 
    <Router>
      <Routes>
        <Route path = "/" element = { <AppTemplate/> } />
      </Routes>
    </Router>
  )
};

export default Index;