import actionCreatorFactory from 'typescript-fsa';
import { RoleTypes } from 'model/Roles';

const actionCreator = actionCreatorFactory('app');

interface Role {
  role: RoleTypes;
  username: string;
}

export const setSession = actionCreator<Role>('setSession');
