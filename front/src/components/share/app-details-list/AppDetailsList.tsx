/* eslint-disable @typescript-eslint/indent */
import React, { useCallback, PropsWithChildren } from 'react';
import { Stack } from '@fluentui/react/lib/Stack';
import { ScrollablePane } from '@fluentui/react/lib/ScrollablePane';
import { ShimmeredDetailsList } from '@fluentui/react/lib/ShimmeredDetailsList';
import { Selection, SelectionMode } from '@fluentui/react/lib/Selection';
import { useLocale } from 'i18n/useLocale';
import { BaseIdDto } from 'api/response/BaseIdDto';

import { IColumn } from '@fluentui/react/lib/DetailsList';
import { IAppColumn } from './IAppColumn';
import DateTimeColumn from './columnRender/DateTimeColumn';

interface Props<T> {
  items: T[];
  columns: IAppColumn<T>[];
  onSelectionChanged?: (items: T[]) => void;
}

const AppDetailsList = <T extends BaseIdDto>(
  props: PropsWithChildren<Props<T>>
) => {
  const t = useLocale();
  const selection: Selection | undefined = !props.onSelectionChanged
    ? undefined
    : new Selection({
        onSelectionChanged: () => {
          if (props.onSelectionChanged && selection) {
            props.onSelectionChanged(selection.getSelection() as T[]);
          }
        },
      });

  const getFieldRender = useCallback(
    (item?: T, index?: number, column?: IAppColumn<T>): JSX.Element | null => {
      if (column && column.fieldRender && item) {
        switch (column.fieldRender) {
          case 'DateTimeField':
            return (
              <DateTimeColumn
                value={(item[column.dtoField] as unknown) as Date | null}
              />
            );
        }
      }
      return null;
    },
    []
  );

  const setupRenderColumn = useCallback(
    (x: IAppColumn<T>) => {
      if (x.onRender) return x.onRender;
      if (x.fieldRender) {
        return (item?: T, index?: number, column?: IColumn) =>
          getFieldRender(item, index, column as IAppColumn<T>);
      }

      return undefined;
    },
    [getFieldRender]
  );

  const columns = useCallback(() => {
    return props.columns.map<IAppColumn<T>>((x) => {
      return {
        ...x,
        name: t(x.translateKey),
        fieldName: x.dtoField as string,
        isResizable: x.isResizable !== undefined ? x.isResizable : true,
        minWidth: x.minWidth ? x.minWidth : 50,
        maxWidth: x.maxWidth ? x.maxWidth : 300,
        onRender: setupRenderColumn(x),
      };
    });
  }, [props.columns, t, setupRenderColumn]);

  return (
    <Stack.Item
      grow={true}
      verticalFill={false}
      styles={{
        root: {
          position: 'relative',
        },
      }}
    >
      <ScrollablePane>
        <ShimmeredDetailsList
          selectionMode={
            props.onSelectionChanged
              ? SelectionMode.multiple
              : SelectionMode.none
          }
          selection={selection}
          columns={columns()}
          items={(props.items as unknown) as never[]}
        />
      </ScrollablePane>
    </Stack.Item>
  );
};

export default AppDetailsList;
