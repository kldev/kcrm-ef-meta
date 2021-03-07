export const ProjectsTranslation = {
  ProjectsName: 'Name',
  ProjectsDescription: 'Description',
  ProjectsType: 'Type',
  ProjectsStartDateTimeUtc: 'Start date',
  ProjectsPlanedEnDateTimeUtc: 'Planned end date',
  ProjectsEndDateTimeUtc: 'End date',
  UnknownProjectType: 'Unknown type',
  ProjectTypeFinTech: 'Fin-Tech',
  ProjectTypeMedical: 'Medical',
  ProjectTypeCRM: 'CRM',
  ProjectTypeERP: 'ERP',
  ProjectTypeOpenSource: 'Open-source',
  ProjectTypeResearchAndDevelopment: 'R&D',
  CreateProjectCommandNameAlreadyUsed: 'Name is already in use',
};

export type ProjectsTranslationKeys = keyof typeof ProjectsTranslation;
export type ProjectsTranslationType = typeof ProjectsTranslation;
