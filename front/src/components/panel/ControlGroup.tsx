import React from 'react';
import { Stack } from '@fluentui/react/lib/Stack';
import { DefaultButton } from '@fluentui/react/lib/Button';
import { AppActionButton } from 'components/share';
import { useLocale } from 'i18n/useLocale';

interface Props {
  onSubmit: () => Promise<void>;
  onCancel: () => void;
}

const ControlGroup: React.FC<Props> = (props) => {
  const t = useLocale();

  return (
    <Stack
      horizontal
      horizontalAlign="end"
      grow
      tokens={{
        childrenGap: 15,
      }}
    >
      <AppActionButton
        onClick={async () => {
          await props.onSubmit();
        }}
      >
        {t('Save')}
      </AppActionButton>
      <DefaultButton onClick={() => props.onCancel()}>
        {t('Cancel')}
      </DefaultButton>
    </Stack>
  );
};

export default ControlGroup;
