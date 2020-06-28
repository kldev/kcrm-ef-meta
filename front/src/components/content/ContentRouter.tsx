import React from 'react';

import { Routes } from 'model/Routes';

import { Switch, Route } from 'react-router-dom';

import Status404Page from 'pages/status-pages/Status404Page';

const ContactsPage = React.lazy(() =>
  import('pages/contacts-page/ContactsPage')
);
const FilesPage = React.lazy(() => import('pages/files-page/FilesPage'));
const ProjectsPage = React.lazy(() =>
  import('pages/projects-page/ProjectsPage')
);

const ContentRouter: React.FC = () => {
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
        <Route path={Routes.contacts} component={ContactsPage} />
        <Route path={Routes.projects} component={ProjectsPage} />
        <Route path={Routes.files} component={FilesPage} />
        <Route component={Status404Page} />
      </Switch>
    </React.Suspense>
  );
};

export default ContentRouter;
