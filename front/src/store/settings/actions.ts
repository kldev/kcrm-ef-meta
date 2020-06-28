import actionCreatorFactory from 'typescript-fsa';

const actionCreator = actionCreatorFactory('settings');

interface LocalePayload {
  locale: 'en' | 'pl';
}

interface ShowNavPayload {
  show: boolean;
}

export const setLocale = actionCreator<LocalePayload>('setLocale');
export const setShowNav = actionCreator<ShowNavPayload>('setShowNav');
export const toggleShowNav = actionCreator('toggleShowNav');
