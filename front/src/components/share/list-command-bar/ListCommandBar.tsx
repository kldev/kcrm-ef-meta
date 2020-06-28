import React, { useCallback } from 'react';
import {
  CommandBar,
  ICommandBarItemProps,
  ICommandBarStyles,
} from '@fluentui/react/lib/CommandBar';
import { useLocale } from 'i18n/useLocale';
import { BaseIdDto } from 'api/response/BaseIdDto';

interface Props {
  onNew?: () => void; //
  items?: ICommandBarItemProps[];
  farItems?: ICommandBarItemProps[];
  selectedItems?: BaseIdDto[];
}

const ListCommandBar: React.FC<Props> = (props) => {
  const t = useLocale();

  const style: Partial<ICommandBarStyles> = {
    root: {
      borderBottom: '1px solid #ddd',
      marginBottom: '20px',
      marginTop: 10,
    },
  };

  const items = useCallback(() => {
    return [
      {
        key: 'New',
        onClick: props.onNew,
        text: t('AddNew'),
        iconProps: {
          iconName: 'Add',
        },
      },
      {
        disabled: !props.selectedItems || !props.selectedItems.length,
        key: 'Delete',
        text: `${t('DeleteSelected')} (${
          props.selectedItems && props.selectedItems.length
        })`,
        iconProps: {
          iconName: 'Trash',
        },
      },
    ].filter((x) => !x.disabled) as ICommandBarItemProps[];
  }, [props, t]);

  const setupFarItems = useCallback(() => {
    const farItems: ICommandBarItemProps[] = [];
    if (props.farItems && props.farItems.length > 0) {
      farItems.concat(props.farItems);
    }
    return farItems;
  }, [props.farItems]);

  return (
    <CommandBar styles={style} items={items()} farItems={setupFarItems()} />
  );
};

export default ListCommandBar;
