import { commonApi } from 'api/client/CommonApiClient';

import { useState, useCallback } from 'react';
import { CountryDto } from 'api/response';

export const useCommonApiClient = () => {
  const [countries, setCountries] = useState<CountryDto[]>([]);

  const getCountries = useCallback(async (query: string) => {
    const result = await commonApi.getCountries(query);
    setCountries(result);
    return result;
  }, []);

  return {
    getCountries,
    countries,
  };
};
