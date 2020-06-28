import { AppConfig } from 'config';
import { CountryDto } from 'api/response';
import { BaseApiClient } from './BaseApiClient';

class CommonApiClient extends BaseApiClient {
  getCountries = async (query: string): Promise<CountryDto[]> => {
    const { data } = await this.post<CountryDto[], {}>(
      '/api/common/countries',
      {
        query,
      }
    );
    return data || [];
  };
}

export const commonApi = new CommonApiClient(AppConfig.apiUrl);
