import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import 'roboto-fontface/css/roboto/roboto-fontface.css';
import 'semantic-ui-flag/flag.css';
import { initializeIcons } from '@uifabric/icons';
import { configureStore } from 'store';
import { Provider } from 'react-redux';

import { registerExtraIcons } from 'icon/registerIcons';
import App from './App';
import * as serviceWorker from './serviceWorker';

initializeIcons();
registerExtraIcons();
const store = configureStore();

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,

  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.register();
