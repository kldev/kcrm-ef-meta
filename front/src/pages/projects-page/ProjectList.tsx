import React, { useCallback } from 'react';
import AppDetailsList from 'components/share/app-details-list/AppDetailsList';
import { IAppColumn } from 'components/share/app-details-list/IAppColumn';
import { ProjectDto, ProjectType } from 'api/response';
import { ProjectsTranslationKeys } from 'i18n/en/projectsTranslation';
import { useLocale } from 'i18n/useLocale';

interface Props {
  projects: ProjectDto[];
  onSelectionChange: (selectedProjects: ProjectDto[]) => void;
}

const ProjectLits: React.FC<Props> = (props) => {
  const t = useLocale();

  const getProjectTranslateKey = useCallback(
    (type: number): ProjectsTranslationKeys => {
      switch (type) {
        case ProjectType.FinTech:
          return 'ProjectTypeFinTech';
        case ProjectType.Medical:
          return 'ProjectTypeMedical';
        case ProjectType.CRM:
          return 'ProjectTypeCRM';
        case ProjectType.ERP:
          return 'ProjectTypeERP';
        case ProjectType.OpenSource:
          return 'ProjectTypeOpenSource';
        case ProjectType.ResearchAndDevelopment:
          return 'ProjectTypeResearchAndDevelopment';
      }
      return 'UnknownProjectType';
    },
    []
  );

  const columns = useCallback(() => {
    return [
      {
        key: 'name',
        name: '',
        dtoField: 'name',
        translateKey: 'ProjectsName',
        minWidth: 200,
        maxWidth: 300,
      },
      {
        key: 'description',
        name: '',
        dtoField: 'description',
        translateKey: 'ProjectsDescription',
      },
      {
        key: 'projectType',
        name: '',
        dtoField: 'projectType',
        translateKey: 'ProjectsType',
        onRender: (item: ProjectDto) => {
          return t(getProjectTranslateKey(item.projectType));
        },
        minWidth: 50,

        maxWidth: 150,
      },
      {
        key: 'startDateTimeUtc',
        name: '',
        dtoField: 'startDateTimeUtc',
        translateKey: 'ProjectsStartDateTimeUtc',
        fieldRender: 'DateTimeField',
        minWidth: 100,
      },
      {
        key: 'planedEnDateTimeUtc',

        dtoField: 'planedEndDateTimeUtc',
        translateKey: 'ProjectsPlanedEnDateTimeUtc',
        fieldRender: 'DateTimeField',
        minWidth: 100,
      },
      {
        key: 'endDateTimeUtc',
        name: '',
        dtoField: 'endDateTimeUtc',
        translateKey: 'ProjectsEndDateTimeUtc',
        fieldRender: 'DateTimeField',
        minWidth: 100,
      },
    ] as IAppColumn<ProjectDto>[];
  }, [getProjectTranslateKey, t]);

  return (
    <AppDetailsList
      onSelectionChanged={props.onSelectionChange}
      items={props.projects}
      columns={columns()}
    />
  );
};

export default ProjectLits;
