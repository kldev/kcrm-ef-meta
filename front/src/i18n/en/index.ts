import { Labels, LabelsKeys } from './labels';
import { Messages, MessagesKeys } from './messages';
import {
  ProjectsTranslation,
  ProjectsTranslationKeys,
} from './projectsTranslation';

export * from './labels';
export * from './messages';

export const en = {
  en: {
    ...Labels,
    ...Messages,
    ...ProjectsTranslation,
  },
};

export type AllTranslationKeys =
  | LabelsKeys
  | ProjectsTranslationKeys
  | MessagesKeys;
