import React, { useCallback } from 'react';
import { CommandBar, ICommandBarStyles } from '@fluentui/react/lib/CommandBar';
import { useLocale } from 'i18n/useLocale';
import { BaseIdDto } from 'api/response/BaseIdDto';
import { IAppCommandBarItemProps } from '..';

interface Props {
  onNew?: () => void; //
  items?: IAppCommandBarItemProps[];
  farItems?: IAppCommandBarItemProps[];
  selectedItems?: BaseIdDto[];
}

const ListCommandBar: React.FC<Props> = (props) => {
  const { items, farItems, selectedItems, onNew } = props;
  const t = useLocale();

  const style: Partial<ICommandBarStyles> = {
    root: {
      borderBottom: '1px solid #ddd',
      marginBottom: '20px',
      marginTop: 10,
    },
  };

  const displayItems = useCallback(() => {
    let localItems = [
      {
        key: 'New',
        onClick: onNew,
        text: t('AddNew'),
        iconProps: {
          iconName: 'Add',
        },
      },
      {
        disabled: !selectedItems || !selectedItems.length,

        key: 'Delete',
        text: `${t('DeleteSelected')} (${
          (selectedItems && selectedItems.length) || 0
        })`,
        iconProps: {
          iconName: 'Trash',
        },
      },
    ] as IAppCommandBarItemProps[];

    if (items && items.length > 0) {
      localItems = localItems.concat(items);
    }

    return localItems.filter((x) => x.skip !== true);
  }, [items, selectedItems, onNew, t]);

  const setupFarItems = useCallback(() => {
    let localItems: IAppCommandBarItemProps[] = [];
    if (farItems && farItems.length > 0) {
      localItems = localItems.concat(farItems);
    }
    return localItems;
  }, [farItems]);

  return (
    <CommandBar
      styles={style}
      items={displayItems()}
      farItems={setupFarItems()}
    />
  );
};

export default ListCommandBar;
