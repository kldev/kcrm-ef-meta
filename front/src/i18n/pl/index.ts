import { Labels } from './labelsPL';
import { Messages } from './messages';
import { ProjectsTranslationPL } from './projectsTranslationPL';

export * from './labelsPL';
export * from './messages';

export const pl = {
  pl: {
    ...Labels,
    ...Messages,
    ...ProjectsTranslationPL,
  },
};
