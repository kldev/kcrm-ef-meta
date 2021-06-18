import { reducerWithInitialState } from 'typescript-fsa-reducers';
import { RoleTypes } from 'model/Roles';
import * as actions from './actions';

interface State {
  role: RoleTypes;
  username: string;
  fullname: string;
  avatarId: string;
}

const initialState: State = {
  role: 'none',
  username: '',
  fullname: '',
  avatarId: '',
};

const reducer = reducerWithInitialState(initialState);

reducer.caseWithAction(actions.setSession, (state, { payload }) => ({
  ...state,
  role: payload.role,
  username: payload.username,
  fullname: payload.fullname,
  avatarId: payload.avatarId || '',
}));

reducer.caseWithAction(actions.updateAvatarId, (state, { payload }) => ({
  ...state,
  avatarId: payload.avatarId || '',
}));

export const AppReducer = reducer.build();
