import { createStore, combineReducers, applyMiddleware } from 'redux';
import { composeWithDevTools } from 'redux-devtools-extension';

import { MessageReducer } from './message';
import { SettingsReducer } from './settings';
import { AppReducer } from './app';

const rootReducer = combineReducers({
  message: MessageReducer,
  settings: SettingsReducer,
  app: AppReducer,
});

export type RootAppState = ReturnType<typeof rootReducer>; // create type base on object

export const configureStore = () => {
  let middleWareEnhancer = applyMiddleware();

  if (`${process.env.NODE_ENV || ''}`.trim() === 'development') {
    middleWareEnhancer = composeWithDevTools(middleWareEnhancer);
  }

  const store = createStore(rootReducer, middleWareEnhancer);

  return store;
};
