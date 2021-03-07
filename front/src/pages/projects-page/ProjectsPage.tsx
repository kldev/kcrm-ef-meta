import React, { useState, useEffect, useRef, useCallback } from 'react';
import { IAppCommandBarItemProps, ListCommandBar } from 'components/share';
import { useProjectsApiClient } from 'hooks/useProjectsApiClient';
import { ProjectDto } from 'api/response';
import { Spinner } from '@fluentui/react/lib/Spinner';
import ProjectLits from './ProjectList';
import ProjectControl from './ProjectControl';
import { ProjectControlCommand } from './ProjectControlCommand';

const ProjectsPage: React.FC = () => {
  const { projects, getProjects, isLoading } = useProjectsApiClient();
  const [selectedProjects, setSelectedProjects] = useState<ProjectDto[]>([]);
  const projectControl = useRef<ProjectControlCommand>(null);

  useEffect(() => {
    getProjects();
  }, [getProjects]);

  const onSelectionChange = (items: ProjectDto[]) => {
    setSelectedProjects(items);
  };

  const handleAddProject = useCallback(async () => {
    await getProjects();
  }, [getProjects]);

  const extraItems = useCallback((): IAppCommandBarItemProps[] => {
    const localItems: IAppCommandBarItemProps[] = [
      {
        key: 'assignPeople',
        iconProps: {
          iconName: 'PeopleAdd',
        },
        text: 'Assign people',
        skip: selectedProjects && selectedProjects.length === 0,
      },
    ];

    return localItems;
  }, [selectedProjects]);

  return (
    <>
      <ListCommandBar
        items={extraItems()}
        selectedItems={selectedProjects}
        onNew={() => {
          projectControl &&
            projectControl.current &&
            projectControl.current.openNew();
        }}
      />
      {isLoading ? (
        <Spinner />
      ) : (
        <ProjectLits
          onSelectionChange={onSelectionChange}
          projects={projects}
        />
      )}

      <ProjectControl
        componentRef={projectControl}
        onAdd={() => {
          handleAddProject();
        }}
      />
    </>
  );
};

export default ProjectsPage;
