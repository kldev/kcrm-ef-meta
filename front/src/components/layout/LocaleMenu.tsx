import React, { useCallback } from 'react';
import { IContextualMenuProps } from '@fluentui/react/lib/ContextualMenu';
import { Stack } from '@fluentui/react/lib/Stack';
import { ActionButton } from '@fluentui/react/lib/Button';

import { useLocale } from 'i18n/useLocale';
import { setLocale } from 'store/settings/actions';
import { useDispatch, useSelector } from 'react-redux';
import { LocalTypes } from 'model/LocaleType';
import { RootAppState } from 'store';

interface StateProps {
  locale: LocalTypes;
}

const LocaleMenu: React.FC = (props) => {
  const { locale } = useSelector<RootAppState, StateProps>(({ settings }) => ({
    locale: settings.locale,
  }));

  const t = useLocale();

  const disptatch = useDispatch();

  const subMenu = useCallback(() => {
    const localMenu: IContextualMenuProps = {
      items: [
        {
          key: 'pl',
          text: t('Lang_Polish'),
          iconProps: {
            iconName: 'FlagPoland',
          },
          onClick: () => {
            disptatch(setLocale({ locale: 'pl' }));
          },
        },
        {
          key: 'en',
          text: t('Lang_English'),
          iconProps: {
            iconName: 'FlagEngland',
          },
          onClick: () => {
            disptatch(setLocale({ locale: 'en' }));
          },
        },
      ],
    };

    return localMenu;
  }, [t, disptatch]);

  const getLabel = (current: LocalTypes) => {
    return current === 'pl' ? t('Lang_Polish') : t('Lang_English');
  };

  const getFlag = (current: LocalTypes): string => {
    return current === 'pl' ? 'FlagPoland' : 'FlagEngland';
  };

  return (
    <Stack verticalAlign="center">
      <ActionButton
        text={getLabel(locale)}
        menuIconProps={{
          iconName: getFlag(locale),
        }}
        menuProps={subMenu()}
      />
    </Stack>
  );
};

export default LocaleMenu;
