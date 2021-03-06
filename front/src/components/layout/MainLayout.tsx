import React, { useEffect } from 'react';
import { Stack, IStackStyles } from '@fluentui/react/lib/Stack';
import { authProvider } from 'auth/AuthProvider';
import { useHistory } from 'react-router';
import { Routes } from 'model/Routes';
import ContentRouter from 'components/content/ContentRouter';
import { setSession } from 'store/app';

import { useDispatch } from 'react-redux';
import { SessionDto } from 'api/response';
import NavigationMenu from './NavigationMenu';
import LayoutCommandBar from './LayoutCommandBar';

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
  const dispatch = useDispatch();
  useEffect(() => {
    const load = async () => {
      const auth = await authProvider.retrieveUser();
      if (!auth) {
        history.push(Routes.login);
        return;
      }

      dispatch(setSession(auth as SessionDto));
    };

    load();
  }, [history, dispatch]);

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
