import Axios, { AxiosInstance } from 'axios';

import { AppConfig } from 'config/AppConfig';
import { LoginRequest } from 'api/request';
import { SessionDto, ErrorDto } from 'api/response';

const LoginPath = 'api/auth/login';

class AuthApi {
  private api: AxiosInstance;

  constructor(baseUrl: string) {
    this.api = Axios.create({
      baseURL: `${baseUrl}`,
      headers: {},
      withCredentials: true,
      // / handle api status when used
      validateStatus: (status) => {
        return true;
      },

      timeout: 15 * 1000,
    });
  }

  login = async (
    req: LoginRequest
  ): Promise<{ data?: SessionDto; error?: ErrorDto }> => {
    const response = await this.api.post<SessionDto | ErrorDto>(LoginPath, req);

    if (response.status === 200) {
      return { data: response.data as SessionDto };
    }

    return { error: response.data as ErrorDto };
  };
}

export default new AuthApi(AppConfig.apiUrl);
