import React, { useEffect } from 'react';
import { Stack, IStackStyles } from '@fluentui/react/lib/Stack';
import { authProvider } from 'auth/AuthProvider';
import { useHistory } from 'react-router';
import { Routes } from 'model/Routes';
import ContentRouter from 'components/content/ContentRouter';
import LayoutCommandBar from './LayoutCommandBar';
import NavigationMenu from './NavigationMenu';

const contentStackStyles: IStackStyles = {
  root: {
    flexBasis: '400px',
    paddingLeft: '0px',
    paddingRight: '0px',
    boxSizing: 'border-box',
  },
};

const containerStackStyles: IStackStyles = {
  root: {
    flexBasis: '800px',
  },
};

const MainLayout: React.FC = (props) => {
  const history = useHistory();
  useEffect(() => {
    const auth = authProvider.retrieveUser();
    if (!auth) {
      history.push(Routes.login);
    }
  }, [history]);

  return (
    <Stack verticalFill styles={containerStackStyles}>
      <LayoutCommandBar />
      <Stack horizontal verticalFill>
        <NavigationMenu />
        <Stack grow verticalFill styles={contentStackStyles}>
          <Stack.Item
            verticalFill={false}
            grow={true}
            styles={{
              root: {
                display: 'flex',
                flexDirection: 'column',
              },
            }}
          >
            <ContentRouter />
          </Stack.Item>
        </Stack>
      </Stack>
    </Stack>
  );
};

export default MainLayout;
