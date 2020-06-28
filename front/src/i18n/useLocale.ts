import { useIntl } from 'react-intl';
import { useCallback } from 'react';
import { AllTranslationKeys } from './en';

export const useLocale = () => {
  const intl = useIntl();

  // import useCallback to have function const
  const t = useCallback(
    (key: AllTranslationKeys) => {
      return intl.formatMessage({
        id: key,
        defaultMessage: key,
      });
    },
    [intl]
  );

  return t;
};
