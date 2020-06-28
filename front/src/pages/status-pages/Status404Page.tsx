import React from 'react';
import PageNotFound from './page-not-found.svg';

const Status404Page: React.FunctionComponent = () => {
  return (
    <img
      src={PageNotFound}
      alt="404"
      style={{ padding: '20px', maxHeight: '400px' }}
    />
  );
};

export default Status404Page;
