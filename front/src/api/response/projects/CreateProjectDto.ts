import { ProjectType } from './ProjectType';

export interface CreateProjectDto {
  name: string;
  description: string;
  projectType: ProjectType;
  startDateTimeUtc: Date | null;
  planedEndDateTimeUtc: Date | null;
}
