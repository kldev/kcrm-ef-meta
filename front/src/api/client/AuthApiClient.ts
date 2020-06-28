import Axios, { AxiosInstance } from 'axios';

import { AppConfig } from 'config/AppConfig';
import { LoginRequest } from 'api/request';
import { AuthDto, ErrorDto } from 'api/response';
import { authProvider } from 'auth/AuthProvider';

const LoginPath = 'api/auth/login';
const RefreshTokenPath = 'api/auth/refresh';

class AuthApi {
  private api: AxiosInstance;

  constructor(baseUrl: string) {
    this.api = Axios.create({
      baseURL: `${baseUrl}`,
      headers: {},
      // / handle api status when used
      validateStatus: (status) => {
        return true;
      },

      timeout: 15 * 1000,
    });
  }

  login = async (
    req: LoginRequest
  ): Promise<{ data?: AuthDto; error?: ErrorDto }> => {
    const response = await this.api.post<AuthDto | ErrorDto>(LoginPath, req);

    if (response.status === 200) {
      const auth = response.data as AuthDto;

      authProvider.storeAuth(auth);

      return { data: auth };
    }

    return { error: response.data as ErrorDto };
  };

  useRefreshToken = async (
    refreshToken: string
  ): Promise<{ data?: AuthDto; error?: ErrorDto }> => {
    const response = await this.api.get<AuthDto | ErrorDto>(
      `${RefreshTokenPath}/${refreshToken}`
    );

    if (response.status === 200) {
      const auth = response.data as AuthDto;

      authProvider.storeAuth(auth);

      return { data: auth };
    }

    return { error: response.data as ErrorDto };
  };
}

export default new AuthApi(AppConfig.apiUrl);
