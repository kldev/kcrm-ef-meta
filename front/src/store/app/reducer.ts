import { reducerWithInitialState } from 'typescript-fsa-reducers';
import { RoleTypes } from 'model/Roles';
import { authProvider } from 'auth/AuthProvider';
import * as actions from './actions';

interface State {
  role: RoleTypes;
  username: string;
}

const initialState: State = {
  role: 'none',
  username: '',
};

const auth = authProvider.retrieveUser();

if (auth) {
  initialState.role = auth.role;
}

const reducer = reducerWithInitialState(initialState);

reducer.caseWithAction(actions.setSession, (state, { payload }) => ({
  ...state,
  role: payload.role,
  username: payload.username,
}));

export const AppReducer = reducer.build();
