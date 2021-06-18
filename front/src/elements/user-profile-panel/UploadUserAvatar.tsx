import React from 'react';
import { Stack } from '@fluentui/react/lib/Stack';
import { Label } from '@fluentui/react/lib/Label';
import { PrimaryButton } from '@fluentui/react/lib/Button';
import { useUserAvatarClient } from './useUserAvatarClient';

interface StateProps {
  avatarId: string;
}

interface OwnProps {
  onClose: () => void;
}

type Props = OwnProps;

const UploadUserAvatar: React.FC<Props> = (props) => {
  const { upload } = useUserAvatarClient();
  const fileInput = React.useRef<HTMLInputElement>(null);

  const handleFileUpload = async () => {
    if (fileInput && fileInput.current) {
      if (fileInput.current.files && fileInput.current.files.length) {
        const sucess = await upload(fileInput.current.files[0]);
        if (sucess) {
          props.onClose();
        }
      }
    }
  };

  return (
    <Stack horizontalAlign="center" verticalAlign="start" verticalFill={true}>
      <Label>Select avtar</Label>
      <input
        ref={fileInput}
        type="file"
        accept="image/png;image/jpeg;image/jpg"
        onChange={handleFileUpload}
      />
      <PrimaryButton
        text="Close"
        onClick={() => {
          props.onClose();
        }}
      />
    </Stack>
  );
};

export default UploadUserAvatar;
