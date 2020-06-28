/* eslint-disable @typescript-eslint/indent */
import React from 'react';

import { CompactPeoplePicker } from '@fluentui/react/lib/Pickers';
import { IPersonaProps } from '@fluentui/react/lib/Persona';
import { Label } from '@fluentui/react/lib/Label';
import { Stack } from '@fluentui/react/lib/Stack';
import { CountryDto } from 'api/response';
import { useCommonApiClient } from 'hooks/useCommonApiClient';
import { useLocale } from 'i18n/useLocale';
import { LabelsKeys } from 'i18n/en';

interface Props {
  onChange: (countries: CountryDto[]) => void;
  initial?: CountryDto[];
  limit?: number;
  label?: LabelsKeys;
}

const CountryPicker: React.FunctionComponent<Props> = (props) => {
  const t = useLocale();
  const { getCountries } = useCommonApiClient();

  let defaultSelection: IPersonaProps[] = [];

  if (props.initial && props.initial.length > 0) {
    defaultSelection = defaultSelection.concat(
      props.initial.map<IPersonaProps>((x) => ({
        text: x.name,
        id: `${x.code}`,
        secondaryText: x.iso,
      }))
    );
  }

  const handleEmptyFocus = () => {
    // / in future here should select most used countries from api
    return handleResolveSuggestions('');
  };

  const handleResolveSuggestions = async (filter: string) => {
    const list = await getCountries(filter);

    if (list && list.length) {
      return list
        .sort((a, b) => a.name.localeCompare(b.name))
        .map<IPersonaProps>((x) => {
          return {
            text: x.name,
            id: x.code,
            secondaryText: x.iso,
          };
        });
    }

    return [];
  };

  return (
    <Stack horizontal={false}>
      <Label>{props.label ? t(props.label) : t('SelectCountry')}</Label>
      <CompactPeoplePicker
        defaultSelectedItems={defaultSelection}
        onEmptyInputFocus={handleEmptyFocus}
        onResolveSuggestions={handleResolveSuggestions}
        itemLimit={props.limit ? props.limit : 1}
      />
    </Stack>
  );
};

export default CountryPicker;
