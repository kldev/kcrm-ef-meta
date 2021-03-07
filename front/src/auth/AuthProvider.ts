import { SessionDto } from 'api/response';

import { usersApi } from '../api/client/UsersApiClient';

class AuthProvider {
  retrieveUser = async (): Promise<SessionDto | null> => usersApi.session();
}

export const authProvider = new AuthProvider();
