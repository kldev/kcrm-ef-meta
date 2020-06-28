import { authProvider } from 'auth/AuthProvider';
import { AppConfig } from 'config';
import { BaseApiClient } from './BaseApiClient';

class UsersApiClient extends BaseApiClient {
  logOut = async (): Promise<void> => {
    await this.get('/api/users/logout');
    authProvider.deleteAuth();
  };
}

export const usersApi = new UsersApiClient(AppConfig.apiUrl);
