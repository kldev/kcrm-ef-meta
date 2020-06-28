import React, { useState, useEffect } from 'react';
import { Panel, PanelType } from '@fluentui/react/lib/Panel';
import { Stack } from '@fluentui/react/lib/Stack';
import { ControlGroup } from 'components/panel';
import { TextField } from '@fluentui/react/lib/TextField';
import { ProjectType } from 'api/response';
import { useLocale } from 'i18n/useLocale';
import { ProjectTypeComboBox, AppDatePicker } from 'components/forms';
import { useProjectsApiClient } from 'hooks/useProjectsApiClient';

interface Props {
  onClose: () => void;
  onAdded: (newId?: string) => void;
}

const ProjectAddPanel: React.FC<Props> = (props) => {
  const [name, setName] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [projectType, setProjectType] = useState<ProjectType>(ProjectType.CRM);
  const [startDate, setStartDate] = useState<Date>(new Date());
  const { addProject, errors } = useProjectsApiClient();
  const [addedId, setAddedId] = useState('');

  const t = useLocale();

  useEffect(() => {
    if (addedId) {
      props.onAdded(addedId);
    }
  }, [addedId, props]);

  const handelOnDismiss = () => {
    props.onClose();
  };

  const handleOnSubmit = async () => {
    const newProjectId = await addProject({
      name,
      description,
      projectType,
      startDateTimeUtc: startDate,
      planedEndDateTimeUtc: null,
    });

    if (newProjectId) setAddedId(newProjectId);
  };

  return (
    <>
      <Panel
        isOpen={true}
        headerText=""
        onDismiss={handelOnDismiss}
        type={PanelType.smallFixedFar}
      >
        <Stack
          tokens={{ childrenGap: 15 }}
          styles={{ root: { marginBottom: 30, marginTop: 20 } }}
        >
          <TextField
            label={t('ProjectsName')}
            value={name}
            onChange={(_ev, value) => {
              setName(value as string);
            }}
            autoComplete="off"
            errorMessage={errors?.name}
          />
          <TextField
            multiline={true}
            rows={6}
            max={512}
            label={t('ProjectsDescription')}
            value={description}
            onChange={(ev, value) => {
              setDescription(value as string);
            }}
            autoComplete="off"
            errorMessage={errors?.description}
          />
          <ProjectTypeComboBox
            onChange={(value) => {
              setProjectType(value);
            }}
            selectedValue={projectType}
          />
          <AppDatePicker
            value={+startDate}
            label="ProjectsStartDateTimeUtc"
            onSelectDate={(value) => {
              setStartDate(value as Date);
            }}
          />
        </Stack>
        <ControlGroup onSubmit={handleOnSubmit} onCancel={props.onClose} />
      </Panel>
    </>
  );
};

export default ProjectAddPanel;
