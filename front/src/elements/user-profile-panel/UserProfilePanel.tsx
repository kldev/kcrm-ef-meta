import React from 'react';
import { Panel, PanelType } from '@fluentui/react/lib/Panel';
import UserAvatar from './UserAvatar';

interface OwnProps {
  onClose: () => void;
}

type Props = OwnProps;

const UserProfilePanel: React.FC<Props> = (props) => {
  return (
    <Panel isOpen={true} type={PanelType.medium} onDismiss={props.onClose}>
      <UserAvatar />
    </Panel>
  );
};

export default UserProfilePanel;
