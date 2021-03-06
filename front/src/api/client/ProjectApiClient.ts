import { AppConfig } from 'config';
import {
  ProjectDto,
  TagDto,
  ProjectStatDto,
  EntityCreatedDto,
  CreateProjectDto,
  ErrorDto,
} from 'api/response';
import { BaseApiClient } from './BaseApiClient';

class ProjectApiClient extends BaseApiClient {
  getProjects = async (): Promise<ProjectDto[]> => {
    const { data } = await this.get<ProjectDto[]>('/api/projects');
    return data || [];
  };

  getProjectTags = async (projectId: string): Promise<TagDto[]> => {
    const { data } = await this.get<TagDto[]>(
      `/api/projects/${projectId}/tags`
    );
    return data || [];
  };

  getProjectsStats = async (year: number): Promise<ProjectStatDto[]> => {
    const { data } = await this.get<ProjectStatDto[]>(
      `/api/projects/stats/${year}`
    );
    return data || [];
  };

  createProject = async (
    project: CreateProjectDto
  ): Promise<{ data?: EntityCreatedDto; error?: ErrorDto }> => {
    const { data, error } = await this.post<EntityCreatedDto, CreateProjectDto>(
      '/api/projects/create',
      project
    );

    return { data, error };
  };
}

export const projectApi = new ProjectApiClient(AppConfig.apiUrl);
