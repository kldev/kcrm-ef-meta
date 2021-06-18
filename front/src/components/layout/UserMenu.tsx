import React, { useCallback, useState } from 'react';
import { IContextualMenuProps } from '@fluentui/react/lib/ContextualMenu';
import { Stack } from '@fluentui/react/lib/Stack';
import { ActionButton } from '@fluentui/react/lib/Button';
import { Image, ImageFit } from '@fluentui/react/lib/Image';

import { useLocale } from 'i18n/useLocale';
import { useUsersApiClient } from 'hooks/useUsersApiClient';
import { useSelector } from 'react-redux';
import { RootAppState } from 'store';
import { UserProfilePanel } from 'elements/user-profile-panel';
import { AppConfig } from 'config';

interface StateProps {
  fullName: string;
  avatarId: string;
}

const UserMenu: React.FC = (props) => {
  const t = useLocale();
  const { logOut } = useUsersApiClient();
  const { fullName, avatarId } = useSelector<RootAppState, StateProps>(
    ({ app }) => ({
      fullName: app.fullname || app.username,
      avatarId: app.avatarId || '',
    })
  );

  const [userProfile, setUserProfile] = useState(false);

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
          onClick: () => {
            setUserProfile(true);
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
    <>
      <Stack verticalAlign="center" horizontal={true}>
        {avatarId ? (
          <Image
            src={`${AppConfig.apiUrl}/api/users-avatar/${avatarId}`}
            height={48}
            width={48}
            imageFit={ImageFit.contain}
            styles={{
              root: {
                marginRight: '5px',
              },
            }}
          />
        ) : null}
        <span style={{ fontWeight: 'bold' }}>{fullName}</span>
        <ActionButton
          iconProps={{ iconName: 'CollapseMenu' }}
          menuIconProps={{ iconName: '' }}
          menuProps={subMenu()}
        />
      </Stack>
      {userProfile ? (
        <UserProfilePanel
          onClose={() => {
            setUserProfile(false);
          }}
        />
      ) : null}
    </>
  );
};

export default UserMenu;
