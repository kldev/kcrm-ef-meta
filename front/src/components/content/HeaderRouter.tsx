import React from 'react';

import { Routes } from 'model/Routes';

import { Switch, Route } from 'react-router-dom';
import { useLocale } from 'i18n/useLocale';
import Header from './Header';

const HeaderRouter: React.FC = () => {
  const t = useLocale();

  return (
    <React.Suspense fallback={null}>
      <Switch>
        <Route
          path="/"
          exact
          component={(): JSX.Element | null => {
            return null;
          }}
        />
        <Route
          path={Routes.contacts}
          component={() => <Header>{t('MenuContacts')}</Header>}
        />
        <Route
          path={Routes.projects}
          component={() => <Header>{t('MenuProjects')}</Header>}
        />
        <Route
          path={Routes.files}
          component={() => <Header>{t('MenuFiles')}</Header>}
        />
      </Switch>
    </React.Suspense>
  );
};

export default HeaderRouter;
