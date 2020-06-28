import React from 'react';
import { Route, Switch } from 'react-router';
import { Routes } from '../../model/Routes';

const LoginPage = React.lazy(() => import('pages/login-page/LoginPage'));
const MainLayout = React.lazy(() => import('components/layout/MainLayout'));

const MainRouter: React.FC = () => {
  return (
    <Switch>
      <React.Suspense fallback={null}>
        <Switch>
          <Route path={Routes.login} component={LoginPage} />
          <Route path={Routes.home} component={MainLayout} exact={false} />
        </Switch>
      </React.Suspense>
    </Switch>
  );
};

export default MainRouter;
