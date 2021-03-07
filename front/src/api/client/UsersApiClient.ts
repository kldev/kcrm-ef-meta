import { SessionDto } from 'api/response';

import { AppConfig } from 'config';
import { BaseApiClient } from './BaseApiClient';

class UsersApiClient extends BaseApiClient {
  UsersAPI = '/api/users';

  logOut = async (): Promise<void> => {
    await this.get(`${this.UsersAPI}/logout/`);
  };

  session = async (): Promise<SessionDto | null> => {
    const { data } = await this.get<SessionDto>(`${this.UsersAPI}/session`);
    return data || null;
  };
}

export const usersApi = new UsersApiClient(AppConfig.apiUrl);
