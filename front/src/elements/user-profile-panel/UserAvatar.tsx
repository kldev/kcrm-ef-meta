import React, { useState } from 'react';
import { Stack } from '@fluentui/react/lib/Stack';
import { Image, ImageFit } from '@fluentui/react/lib/Image';
import { RootAppState } from 'store';
import { useSelector } from 'react-redux';

import { AppConfig } from 'config';
import defaultUser from './default.png';
import UploadUserAvatar from './UploadUserAvatar';

interface StateProps {
  avatarId: string;
}

const UserAvatar: React.FC = () => {
  const [isUpload, setIsUpload] = useState(false);

  const { avatarId } = useSelector<RootAppState, StateProps>(({ app }) => ({
    avatarId: app.avatarId,
  }));

  return (
    <Stack horizontalAlign="center" verticalAlign="start" verticalFill={true}>
      {!isUpload ? (
        <Image
          imageFit={ImageFit.contain}
          onClick={() => {
            setIsUpload(true);
          }}
          src={
            avatarId
              ? `${AppConfig.apiUrl}/api/users-avatar/${avatarId}`
              : defaultUser
          }
          styles={{
            root: {
              maxHeight: 120,
              maxWidth: 120,
              height: 120,
              marginTop: '40px',
              minWidth: 80,
            },
          }}
        />
      ) : null}

      {isUpload ? (
        <UploadUserAvatar
          onClose={() => {
            setIsUpload(false);
          }}
        />
      ) : null}
    </Stack>
  );
};

export default UserAvatar;
