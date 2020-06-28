import { RoleTypes } from 'model/Roles';

export interface AuthDto {
  token: string;
  refreshToken: string;
  role: RoleTypes;
}
