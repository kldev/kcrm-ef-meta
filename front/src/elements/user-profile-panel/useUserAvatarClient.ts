import { useState, useCallback } from 'react';
import { useDispatch } from 'react-redux';
import { useLocale } from 'i18n/useLocale';
import { addMessage } from 'store/message';
import { updateAvatarId } from 'store/app';
import Axios from 'axios';
import { AppConfig } from 'config';
import { MessageBarType } from '@fluentui/react';

export const useUserAvatarClient = () => {
  const [uploadInProgress, setUploadInProgres] = useState(false);
  const dispatch = useDispatch();

  const upload = useCallback(
    async (file: File): Promise<boolean> => {
      let isSucess = false;

      setUploadInProgres(true);
      try {
        const client = Axios.create({
          baseURL: AppConfig.apiUrl,
          withCredentials: true,
          timeout: 30 * 1000,
        });

        const formData = new FormData();
        formData.append('files', file);

        const result = await client.post<{ avatarId: string | null }>(
          '/api/users-avatar/upload-self',
          formData,
          {
            headers: {
              'Content-Type': 'multipart/form-data',
            },
          }
        );

        if (result.status === 200) {
          isSucess = true;

          dispatch(
            updateAvatarId({
              avatarId: result.data.avatarId,
            })
          );

          dispatch(
            addMessage({
              messageType: MessageBarType.warning,
              content: 'Upload completed. Please login again too load avatar',
            })
          );
        } else {
          dispatch(
            addMessage({
              messageType: MessageBarType.error,
              content: 'Upload has failed',
            })
          );
        }
      } catch {
        isSucess = false;
      }

      setUploadInProgres(false);

      return Promise.resolve(isSucess);
    },
    [dispatch]
  );

  return {
    uploadInProgress,
    upload,
  };
};
