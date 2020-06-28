import { reducerWithInitialState } from 'typescript-fsa-reducers';
import { LocalTypes } from 'model/LocaleType';
import * as actions from './actions';

interface State {
  locale: LocalTypes;
  showNav: boolean;
}

const STORE_PREFIX = 'crm-ui';

const getLocale = () => {
  const value = localStorage.getItem(`${STORE_PREFIX}/locale`);
  if (value && value.length) {
    return value;
  }

  return 'en';
};

const getShowNav = () => {
  const value = localStorage.getItem(`${STORE_PREFIX}/showNav`);
  if (value && value.length) {
    return value === 'true';
  }

  return true;
};

const locale = getLocale();

const initialState: State = {
  locale: locale as LocalTypes,
  showNav: getShowNav(),
};

const reducer = reducerWithInitialState(initialState);

reducer.caseWithAction(actions.setLocale, (state, { payload }) => {
  localStorage.setItem(`${STORE_PREFIX}/locale`, payload.locale);
  return {
    ...state,
    locale: payload.locale,
  };
});

reducer.caseWithAction(actions.setShowNav, (state, { payload }) => {
  localStorage.setItem(`${STORE_PREFIX}/showNav`, JSON.stringify(payload.show));

  return {
    ...state,
    showNav: payload.show,
  };
});

reducer.caseWithAction(actions.toggleShowNav, (state) => {
  localStorage.setItem(
    `${STORE_PREFIX}/showNav`,
    JSON.stringify(!state.showNav)
  );

  return {
    ...state,
    showNav: !state.showNav,
  };
});

export const SettingsReducer = reducer.build();
