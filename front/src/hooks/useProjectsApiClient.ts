import { projectApi } from 'api/client/ProjectApiClient';

import { useState, useCallback } from 'react';
import { ProjectDto, CreateProjectDto } from 'api/response';
import { useDispatch } from 'react-redux';
import { useLocale } from 'i18n/useLocale';
import { addMessage } from 'store/message';
import { MessageBarType } from '@fluentui/react/lib/MessageBar';
import { AllTranslationKeys } from 'i18n/en';

type ProjectDtoKeys = keyof ProjectDto;

type ModelErrors<TType, TKeys extends keyof TType> = {
  [key in TKeys]?: string;
};

export const useProjectsApiClient = () => {
  const dispatch = useDispatch();
  const t = useLocale();
  const [isLoading, setIsLoading] = useState(false);
  const [projects, setProjects] = useState<ProjectDto[]>([]);
  const [errors, setErrors] = useState<
    ModelErrors<ProjectDto, ProjectDtoKeys>
  >();

  const getProjects = useCallback(async () => {
    setIsLoading(true);
    try {
      const result = await projectApi.getProjects();

      setProjects(result);
      setIsLoading(false);
      return result;
    } catch (err) {
      //
    }
    setIsLoading(false);
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

      const { data, error } = await projectApi.createProject(project);

      if (error) {
        dispatch(
          addMessage({
            messageType: MessageBarType.error,
            content: t(error.error as AllTranslationKeys),
          })
        );
        return null;
      }

      return data && data.id;
    },
    [validateProject, dispatch, t]
  );

  return {
    addProject,
    getProjects,
    projects,
    errors,
    isLoading,
  };
};
