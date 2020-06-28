import { IColumn } from '@fluentui/react/lib/DetailsList';
import { ProjectsTranslationKeys } from 'i18n/en/projectsTranslation';
import { AppColumnRenderTypes } from './AppColumnRenderTypes';

export interface IAppColumn<T> extends IColumn {
  translateKey: ProjectsTranslationKeys;
  dtoField: keyof T;
  fieldRender?: AppColumnRenderTypes;
}
