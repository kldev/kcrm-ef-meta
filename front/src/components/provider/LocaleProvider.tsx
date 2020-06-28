import React from 'react';
import { IntlProvider } from 'react-intl';
import { Messages } from 'i18n/messages';
import { useSelector } from 'react-redux';
import { RootAppState } from 'store';
import { LocalTypes } from 'model/LocaleType';

interface StateProps {
  locale: LocalTypes;
}

const LocaleProvider: React.FC = (props) => {
  const { locale } = useSelector<RootAppState, StateProps>(({ settings }) => ({
    locale: settings.locale,
  }));

  return (
    <IntlProvider locale={locale} messages={Messages[locale]}>
      {props.children}
    </IntlProvider>
  );
};

export default LocaleProvider;
