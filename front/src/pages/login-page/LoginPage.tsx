import './LoginPage.scss';

import React, { useState } from 'react';
import { TextField } from '@fluentui/react/lib/TextField';
import { IStackTokens, Stack } from '@fluentui/react/lib/Stack';
import { useLocale } from 'i18n/useLocale';

import LocaleMenu from 'components/layout/LocaleMenu';

import { AppActionButton } from 'components/share';
import { AppConfig } from 'config';
import { useAuthApiClient } from 'hooks/useAuthApiClient';

const stackTokens: Partial<IStackTokens> = { childrenGap: 20 };

const LoginPage: React.FC = (props) => {
  const [username, setUsername] = useState(`${AppConfig.username}`);
  const [password, setPassword] = useState(`${AppConfig.password}`);
  const { login } = useAuthApiClient();

  const t = useLocale();

  const handleSubmit = async () => {
    await login(username, password);
  };

  return (
    <div className="login-page">
      <div className="left">
        <div className="bg" />
      </div>
      <div className="right">
        <div className="login-form">
          <Stack tokens={stackTokens}>
            <TextField
              value={username}
              label={t('LoginFormUsername')}
              required={true}
              underlined={true}
              autoComplete="off"
              onChange={(_ev, value) => {
                setUsername(value as string);
              }}
            />
            <TextField
              value={password}
              type="password"
              label={t('LoginFormPassword')}
              required={true}
              underlined={true}
              autoComplete="off"
              onChange={(_ev, value) => {
                setPassword(value as string);
              }}
            />

            <Stack horizontal={true}>
              <Stack.Item grow={true} disableShrink={true}>
                <AppActionButton
                  onClick={handleSubmit}
                  iconProps={{ iconName: 'Permissions' }}
                >
                  {t('SignIn')}
                </AppActionButton>
              </Stack.Item>

              <LocaleMenu />
            </Stack>
          </Stack>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
