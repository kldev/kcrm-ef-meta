import React, { useCallback } from 'react';
import { IContextualMenuProps } from '@fluentui/react/lib/ContextualMenu';
import { Stack } from '@fluentui/react/lib/Stack';
import { ActionButton } from '@fluentui/react/lib/Button';

import { useLocale } from 'i18n/useLocale';
import { useUsersApiClient } from 'hooks/useUsersApiClient';
import { useSelector } from 'react-redux';
import { RootAppState } from 'store';

interface StateProps {
  fullName: string;
}

const UserMenu: React.FC = (props) => {
  const t = useLocale();
  const { logOut } = useUsersApiClient();
  const { fullName } = useSelector<RootAppState, StateProps>(({ app }) => ({
    fullName: app.fullname || app.username,
  }));

  const handleSignOut = useCallback(() => {
    logOut();
  }, [logOut]);

  const subMenu = useCallback(() => {
    const menuItems: IContextualMenuProps = {
      shouldFocusOnMount: true,
      items: [
        {
          key: 'myProfile',
          text: t('MyProfile'),
          iconProps: {
            iconName: 'ProfileSearch',
          },
        },

        {
          key: 'changePassword',
          text: t('ChangePassword'),
          iconProps: {
            iconName: 'PasswordField',
          },
        },

        {
          key: 'logOut',
          text: t('SignOut'),
          iconProps: {
            iconName: 'SignOut',
          },
          onClick: handleSignOut,
        },
      ],
    };

    return menuItems;
  }, [t, handleSignOut]);

  return (
    <Stack verticalAlign="center" horizontal={true}>
      <span style={{ fontWeight: 'bold' }}>{fullName}</span>
      <ActionButton
        iconProps={{ iconName: 'CollapseMenu' }}
        menuIconProps={{ iconName: '' }}
        menuProps={subMenu()}
      />
    </Stack>
  );
};

export default UserMenu;
