import React, { useState, useEffect, useRef } from 'react';
import { ListCommandBar } from 'components/share';
import { useProjectsApiClient } from 'hooks/useProjectsApiClient';
import { ProjectDto } from 'api/response';
import ProjectLits from './ProjectList';
import ProjectControl from './ProjectControl';
import { ProjectControlCommand } from './ProjectControlCommand';

const ProjectsPage: React.FC = () => {
  const { projects, getProjects } = useProjectsApiClient();
  const [selectedProjects, setSelectedProjects] = useState<ProjectDto[]>([]);
  const projectControl = useRef<ProjectControlCommand>(null);

  useEffect(() => {
    getProjects();
  }, [getProjects]);

  const onSelectionChange = (items: ProjectDto[]) => {
    setSelectedProjects(items);
  };

  return (
    <>
      <ListCommandBar
        selectedItems={selectedProjects}
        onNew={() => {
          projectControl &&
            projectControl.current &&
            projectControl.current.openNew();
        }}
      />
      <ProjectLits onSelectionChange={onSelectionChange} projects={projects} />;
      <ProjectControl
        componentRef={projectControl}
        onAdd={() => {
          getProjects();
        }}
      />
    </>
  );
};

export default ProjectsPage;
