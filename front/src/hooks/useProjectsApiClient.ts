import { projectApi } from 'api/client/ProjectApiClient';

import { useState, useCallback } from 'react';
import { ProjectDto, CreateProjectDto } from 'api/response';
import { useDispatch } from 'react-redux';
import { useLocale } from 'i18n/useLocale';
import { addMessage } from 'store/message';
import { MessageBarType } from '@fluentui/react/lib/MessageBar';

type ProjectDtoKeys = keyof ProjectDto;

type ModelErrors<TType, TKeys extends keyof TType> = {
  [key in TKeys]?: string;
};

export const useProjectsApiClient = () => {
  const dispatch = useDispatch();
  const t = useLocale();
  const [projects, setProjects] = useState<ProjectDto[]>([]);
  const [errors, setErrors] = useState<
    ModelErrors<ProjectDto, ProjectDtoKeys>
  >();

  const getProjects = useCallback(async () => {
    const result = await projectApi.getProjects();

    setProjects(result);
    return result;
  }, []);

  const validateProject = useCallback(
    (project: CreateProjectDto) => {
      let hasErrors = false;

      const currentErrors: ModelErrors<ProjectDto, ProjectDtoKeys> = {};

      if (!project.name || !project.name.length) {
        currentErrors.name = `${t('ProjectsName')} ${t('IsRequired')}`;
        hasErrors = true;

        dispatch(
          addMessage({
            messageType: MessageBarType.warning,
            content: `${t('ProjectsName')} ${t('IsRequired')}`,
          })
        );
      }
      if (!project.description || !project.description.length) {
        currentErrors.description = `${t('ProjectsDescription')} ${t(
          'IsRequired'
        )}`;
        hasErrors = true;

        hasErrors = true;
        dispatch(
          addMessage({
            messageType: MessageBarType.warning,
            content: `${t('ProjectsDescription')} ${t('IsRequired')}`,
          })
        );
      }
      setErrors(currentErrors);

      if (hasErrors) return false;

      return true;
    },
    [dispatch, t]
  );

  const addProject = useCallback(
    async (project: CreateProjectDto) => {
      setErrors({});
      if (!validateProject(project)) {
        return null;
      }

      const result = await projectApi.createProject(project);

      return result.id;
    },
    [validateProject]
  );

  return {
    addProject,
    getProjects,
    projects,
    errors,
  };
};
