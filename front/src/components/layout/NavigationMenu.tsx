import React, { useEffect, useState } from 'react';

import { Nav, INavLink, INavStyles } from '@fluentui/react/lib/Nav';

import { Routes } from 'model/Routes';
import { RoleTypes } from 'model/Roles';
import { RootAppState } from 'store';

import { Stack, IStackItemStyles } from '@fluentui/react/lib/Stack';
import { useSelector } from 'react-redux';
import { useLocale } from 'i18n/useLocale';
import { useHistory } from 'react-router';

const styles: Partial<INavStyles> = {
  root: {
    width: 208,
    height: 350,
    boxSizing: 'border-box',
    overflowY: 'auto',
    paddingLeft: '5px',
    paddingRight: '5px',
  },
  navItem: {
    marginTop: '2px',
  },
};

const stackStyles: Partial<IStackItemStyles> = {
  root: {
    background: 'none',
    borderRight: '1px solid #ddd',
    paddingTop: '5px',
    boxSizing: 'border-box',
  },
};

interface RoleNavLink extends INavLink {
  allowed: RoleTypes[];
}

interface StateProps {
  role: RoleTypes;
  showNav: boolean;
}

const NavigationMenu: React.FC = (props) => {
  const t = useLocale();
  const history = useHistory();

  const [links, setLinks] = useState<RoleNavLink[]>([]);
  const { role, showNav } = useSelector<RootAppState, StateProps>(
    ({ app, settings }) => ({
      role: app.role,
      showNav: settings.showNav,
    })
  );

  useEffect(() => {
    const menuLinks: RoleNavLink[] = [
      {
        isLink: false,
        key: 'contacts',
        name: t('MenuContacts'),
        url: Routes.contacts,
        icon: 'ContactList',
        allowed: ['root', 'admin', 'seller'],
      },
      {
        isLink: false,
        key: 'projects',
        name: t('MenuProjects'),
        url: Routes.projects,
        icon: 'CompanyDirectory',
        allowed: ['root', 'admin', 'seller'],
      },
      {
        isLink: false,
        key: 'files',
        name: 'Files',
        url: Routes.files,
        icon: 'FabricFolder',
        allowed: ['root', 'admin'],
      },
    ];

    setLinks(menuLinks.filter((x) => x.allowed.some((y) => y === role)));
  }, [role, t]);

  const handleOnLinkClick = (
    ev?: React.MouseEvent<HTMLElement>,
    item?: INavLink
  ) => {
    if (ev) {
      ev.nativeEvent.preventDefault();
    }

    if (item) {
      history.push(item.url);
    }
  };

  return !showNav ? null : (
    <Stack.Item styles={stackStyles} verticalFill={true}>
      <Nav
        onLinkClick={handleOnLinkClick}
        styles={styles}
        groups={[
          {
            links,
          },
        ]}
      />
    </Stack.Item>
  );
};

export default NavigationMenu;
