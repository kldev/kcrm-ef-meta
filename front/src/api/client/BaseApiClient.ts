import Axios, { AxiosInstance } from 'axios';
import { ErrorDto } from 'api/response';

export class BaseApiClient {
  private api: AxiosInstance;

  constructor(private baseUrl: string) {
    this.api = this.setupAxios();
  }

  private setupAxios = () => {
    return Axios.create({
      baseURL: `${this.baseUrl}`,
      withCredentials: true,
      // / handle api status when used
      validateStatus: (status) => {
        return true;
      },
      timeout: 15 * 1000,
    });
  };

  protected post = async <TResponse, TRequest>(
    path: string,
    input?: TRequest
  ): Promise<{ data?: TResponse; error?: ErrorDto }> => {
    const response = await this.api.post<TResponse>(path, input);

    if (response.status === 200) {
      return { data: response.data };
    }

    if (response.status === 500) {
      return { error: (response.data as unknown) as ErrorDto };
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

    return {};
  };

  protected get = async <TResponse>(
    path: string
  ): Promise<{ data?: TResponse; error?: ErrorDto }> => {
    const response = await this.api.get<TResponse>(path);

    if (response.status === 200) {
      return { data: response.data as TResponse };
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

    return {};
  };
}
