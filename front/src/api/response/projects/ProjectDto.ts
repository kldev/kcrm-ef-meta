import { ProjectType } from './ProjectType';
import { BaseIdDto } from '../BaseIdDto';

export interface ProjectDto extends BaseIdDto {
  name: string;
  description: string;
  projectType: ProjectType;
  startDateTimeUtc: Date | null;
  planedEndDateTimeUtc: Date | null;
  endDateTimeUtc: Date | null;
}
