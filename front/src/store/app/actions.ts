import actionCreatorFactory from 'typescript-fsa';
import { RoleTypes } from 'model/Roles';

const actionCreator = actionCreatorFactory('app');

interface Role {
  role: RoleTypes;
  username: string;
  fullname: string;
  avatarId: string | null;
}

interface UpdateAvatarId {
  avatarId: string | null;
}

export const setSession = actionCreator<Role>('setSession');
export const updateAvatarId = actionCreator<UpdateAvatarId>('updateAvatarId');
