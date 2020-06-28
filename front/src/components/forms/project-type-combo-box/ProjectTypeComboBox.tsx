import React, { useCallback } from 'react';
import { ProjectType } from 'api/response';
import { IComboBoxOption } from '@fluentui/react/lib/ComboBox';
import { useLocale } from 'i18n/useLocale';
import { Dropdown } from '@fluentui/react';

interface Props {
  onChange: (projectType: ProjectType) => void;
  selectedValue: ProjectType;
  errorMessage?: string;
}

const ProjectTypeComboBox: React.FC<Props> = (props) => {
  const t = useLocale();

  const options = useCallback(() => {
    return [
      {
        key: ProjectType.CRM,
        text: t('ProjectTypeCRM'),
      },
      {
        key: ProjectType.ERP,
        text: t('ProjectTypeERP'),
      },
      {
        key: ProjectType.FinTech,
        text: t('ProjectTypeFinTech'),
      },
      {
        key: ProjectType.Medical,
        text: t('ProjectTypeMedical'),
      },
      {
        key: ProjectType.OpenSource,
        text: t('ProjectTypeOpenSource'),
      },
      {
        key: ProjectType.ResearchAndDevelopment,
        text: t('ProjectTypeResearchAndDevelopment'),
      },
    ] as IComboBoxOption[];
  }, [t]);

  return (
    <>
      <Dropdown
        options={options()}
        selectedKey={props.selectedValue}
        onChange={(_ev, option) => {
          if (option) {
            props.onChange(option.key as ProjectType);
          }
        }}
        errorMessage={props.errorMessage}
        label={t('ProjectsType')}
      />
    </>
  );
};

export default ProjectTypeComboBox;
