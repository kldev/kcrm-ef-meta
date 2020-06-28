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
    const setLocalePl = () => {
      disptatch(setLocale({ locale: 'pl' }));
    };

    const setLocaleEn = () => {
      disptatch(setLocale({ locale: 'en' }));
    };

    const localMenu: IContextualMenuProps = {
      items: [
        {
          key: 'pl',
          text: t('Lang_Polish'),
          iconProps: {
            iconName: 'FlagPoland',
          },
          onClick: setLocalePl,
        },
        {
          key: 'en',
          text: t('Lang_English'),
          iconProps: {
            iconName: 'FlagEngland',
          },
          onClick: setLocaleEn,
        },
      ],
    };

    return localMenu;
  }, [t, disptatch]);

  return (
    <Stack verticalAlign="center">
      <ActionButton
        text={locale === 'pl' ? t('Lang_Polish') : t('Lang_English')}
        menuIconProps={{
          iconName: locale === 'pl' ? 'FlagPoland' : 'FlagEngland',
        }}
        menuProps={subMenu()}
      />
    </Stack>
  );
};

export default LocaleMenu;
