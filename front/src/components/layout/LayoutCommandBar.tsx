import React, { useState, useEffect } from 'react';
import {
  IContextualMenuItem,
  ContextualMenuItemType,
} from '@fluentui/react/lib/ContextualMenu';
import { getTheme } from '@fluentui/react/lib/Styling';
import {
  CommandBar,
  ICommandBarItemProps,
} from '@fluentui/react/lib/CommandBar';
import { AppConfig } from 'config';
import { Stack } from '@fluentui/react/lib/Stack';

import HeaderRouter from 'components/content/HeaderRouter';
import { useDispatch } from 'react-redux';
import { toggleShowNav } from 'store/settings';

import { isMobile } from 'react-device-detect';
import {
  outerCommandBarStyles,
  commandBarWaffleButtonStyles,
} from './LayoutCommandBarConst';
import UserMenu from './UserMenu';
import LocaleMenu from './LocaleMenu';

const theme = getTheme();

const LayoutCommandBar: React.FC = () => {
  const [farItems, setFarItems] = useState<IContextualMenuItem[]>([]);
  const [items, setItems] = useState<ICommandBarItemProps[]>([]);
  const [username] = useState('');

  const dispatch = useDispatch();

  useEffect(() => {
    const appItems: IContextualMenuItem[] = [
      {
        key: 'WaffleButton',
        iconOnly: true,
        iconProps: {
          iconName: 'Waffle',
          styles: {
            root: {
              color: 'white',
              fontSize: 20,
              fontWeight: 600,
            },
          },
        },
        buttonStyles: commandBarWaffleButtonStyles,
        onClick: () => {
          dispatch(toggleShowNav());
        },
      },
      {
        key: 'appName',
        text: `${AppConfig.appName}`,
        buttonStyles: {
          root: {
            backgroundColor: '#eee',
            fontSize: 22,
            marginLeft: 20,
            padding: 0,
            cursor: 'pointer',
          },
          rootHovered: {
            backgroundColor: '#eee',
            cursor: 'pointer',
          },
          rootPressed: {
            backgroundColor: '#eee',
            cursor: 'pointer',
          },
          label: {
            color: theme.palette.themeDarkAlt,
            margin: 0,
            cursor: 'pointer',
          },
        },
      },
      {
        key: 'appHeader',
        onRender: () => <HeaderRouter />,
      },
    ];

    setItems(isMobile ? appItems.filter((x) => x.key !== 'appName') : appItems);

    const localItems: IContextualMenuItem[] = [
      {
        key: 'welcome',
        itemType: ContextualMenuItemType.Normal,
        onRender: (item) => {
          return username ? null : (
            <Stack verticalAlign="center">
              <span>
                <span> {username} </span>
              </span>
            </Stack>
          );
        },
      },
      {
        key: 'locale',
        onRender: () => {
          return <LocaleMenu />;
        },
      },
      {
        key: 'button',
        onRender: () => {
          return <UserMenu />;
        },
      },
    ];

    setFarItems(localItems);
  }, [username, dispatch]);

  return (
    <CommandBar
      items={items}
      farItems={farItems}
      styles={outerCommandBarStyles}
    />
  );
};

export default LayoutCommandBar;
