import { AuthDto } from 'api/response';

const AUTH_ITEM_NAME = 'crm-auth';

class AuthProvider {
  retrieveUser = (): AuthDto | null => {
    const auth = sessionStorage.getItem(AUTH_ITEM_NAME) || '';
    if (auth) {
      return JSON.parse(auth) as AuthDto;
    }

    return null;
  };

  storeAuth = (auth: AuthDto) => {
    sessionStorage.setItem(AUTH_ITEM_NAME, JSON.stringify(auth));
  };

  deleteAuth = () => {
    sessionStorage.removeItem(AUTH_ITEM_NAME);
  };
}

export const authProvider = new AuthProvider();
