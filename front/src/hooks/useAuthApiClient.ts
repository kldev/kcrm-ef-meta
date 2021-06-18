import authApi from 'api/client/AuthApiClient';
import { useLocale } from 'i18n/useLocale';
import { addMessage } from 'store/message/actions';
import { MessageBarType } from '@fluentui/react/lib/MessageBar';

import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router';
import { Routes } from 'model/Routes';
import { setSession } from 'store/app';

export const useAuthApiClient = () => {
  const dispatch = useDispatch();
  const history = useHistory();
  const t = useLocale();

  const login = async (username: string, password: string) => {
    if (!username || !password) return;

    const { data } = await authApi.login({
      username,
      password,
    });

    if (data) {
      dispatch(
        setSession({
          username,
          role: data.role,
          fullname: data.fullname,
          avatarId: data.avatarId,
        })
      );

      dispatch(
        addMessage({
          messageType: MessageBarType.info,
          content: t('LoginHasBeenSuccessful'),
        })
      );

      history.push(Routes.home);
    } else {
      dispatch(
        addMessage({
          messageType: MessageBarType.error,
          content: t('LoginHasFailed'),
        })
      );
    }
  };

  return {
    login,
  };
};
