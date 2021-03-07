import { RoleTypes } from 'model/Roles';

export interface SessionDto {
  username: string;
  fullname: string;
  role: RoleTypes;
}
