import Axios, { AxiosInstance } from 'axios';
import { AuthDto, ErrorDto } from 'api/response';
import { authProvider } from 'auth/AuthProvider';
import authApi from './AuthApiClient';

export class BaseApiClient {
  private api: AxiosInstance;

  private auth: AuthDto;

  constructor(private baseUrl: string) {
    const storeAuth = authProvider.retrieveUser();
    if (storeAuth) {
      this.auth = storeAuth as AuthDto;
    } else {
      this.auth = { token: '', refreshToken: '', role: 'none' };
    }

    this.api = this.setupAxios();
  }

  private setupAxios = () => {
    return Axios.create({
      baseURL: `${this.baseUrl}`,
      headers: {
        Authorization: `Bearer ${this.auth.token}`,
      },
      // / handle api status when used
      validateStatus: (status) => {
        return true;
      },
      timeout: 15 * 1000,
    });
  };

  protected useRefreshToken = async (): Promise<boolean> => {
    if (this.auth && this.auth.refreshToken) {
      const refreshTokenResponse = await authApi.useRefreshToken(
        this.auth.refreshToken
      );
      if (refreshTokenResponse.data) {
        this.auth = refreshTokenResponse.data;
        authProvider.storeAuth(this.auth);
        this.api = this.setupAxios();
        return true;
      }
    }
    return false;
  };

  protected post = async <TResponse, TRequest>(
    path: string,
    input?: TRequest
  ): Promise<{ data?: TResponse; error?: ErrorDto }> => {
    const response = await this.api.post<TResponse>(path, input);

    if (response.status === 200) {
      return { data: response.data };
    }

    if (response.status === 401) {
      if (await this.useRefreshToken()) {
        const retryResponse = await this.api.post<TResponse>(path, input);

        if (retryResponse.status === 200) {
          return { data: retryResponse.data };
        }
      }
    }
    return {};
  };

  protected put = async <TResponse, TRequest>(
    path: string,
    input?: TRequest
  ): Promise<{ data?: TResponse; error?: ErrorDto }> => {
    const response = await this.api.put<TResponse>(path, input);

    if (response.status === 200) {
      return { data: response.data };
    }

    if (response.status === 401) {
      if (await this.useRefreshToken()) {
        const retryResponse = await this.api.put<TResponse>(path, input);

        if (retryResponse.status === 200) {
          return { data: retryResponse.data };
        }
      }
    }
    return {};
  };

  protected get = async <TResponse>(
    path: string
  ): Promise<{ data?: TResponse; error?: ErrorDto }> => {
    const response = await this.api.get<TResponse>(path);

    if (response.status === 200) {
      return { data: response.data as TResponse };
    }

    if (response.status === 401) {
      if (await this.useRefreshToken()) {
        const retryResponse = await this.api.get<TResponse>(path);

        if (retryResponse.status === 200) {
          return { data: retryResponse.data };
        }
      }
    }
    return {};
  };

  protected delete = async <TResponse, TRequest>(
    path: string
  ): Promise<{ data?: TResponse; error?: ErrorDto }> => {
    const response = await this.api.delete<TResponse>(path);

    if (response.status === 200) {
      return { data: response.data as TResponse };
    }

    if (response.status === 401) {
      if (await this.useRefreshToken()) {
        const retryResponse = await this.api.delete<TResponse>(path);

        if (retryResponse.status === 200) {
          return { data: retryResponse.data };
        }
      }
    }
    return {};
  };
}
