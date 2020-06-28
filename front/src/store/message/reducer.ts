import { reducerWithInitialState } from 'typescript-fsa-reducers';
import { MessageBarNotification } from 'model/MessageBarNotification';
import { uuid } from 'uuidv4';

import * as actions from './actions';

const initialState = {
  messages: [] as MessageBarNotification[],
};

const reducer = reducerWithInitialState(initialState);

reducer.caseWithAction(actions.addMessage, (state, { payload }) => ({
  ...state,
  messages: [{ id: uuid(), ...payload }, ...state.messages],
}));

reducer.caseWithAction(actions.removeMessage, (state, { payload }) => ({
  ...state,
  messages: [...state.messages.filter((message) => message.id !== payload.id)],
}));

export const MessageReducer = reducer.build();
