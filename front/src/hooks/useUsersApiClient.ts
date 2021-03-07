import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router';
import { Routes } from 'model/Routes';
import { usersApi } from 'api/client/UsersApiClient';
import { addMessage } from 'store/message';
import { MessageBarType } from '@fluentui/react/lib/MessageBar';
import { useLocale } from 'i18n/useLocale';
import { setSession } from 'store/app';

export const useUsersApiClient = () => {
  const dispatch = useDispatch();
  const history = useHistory();
  const t = useLocale();

  const logOut = async () => {
    await usersApi.logOut();

    dispatch(setSession({ role: 'none', username: '', fullname: '' }));

    dispatch(
      addMessage({
        messageType: MessageBarType.info,
        content: t('YouHaveBeenLogOut'),
      })
    );

    history.push(Routes.login);
  };

  return {
    logOut,
  };
};
