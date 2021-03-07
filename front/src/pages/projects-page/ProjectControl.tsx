import React from 'react';
import { IRefObject, initializeComponentRef } from '@fluentui/react';
import { EntityId } from 'model/EntityId';
import { ProjectControlCommand } from './ProjectControlCommand';
import ProjectAddPanel from './ProjectAddPanel';

interface Props {
  componentRef?: IRefObject<ProjectControlCommand>;
  onAdd?: () => void;
  onUpdate?: () => void;
}

interface State {
  isOpen: boolean;
}

class ProjectControl extends React.Component<Props, State>
  implements ProjectControlCommand {
  constructor(props: Props) {
    super(props);
    this.state = {
      isOpen: false,
    };
    initializeComponentRef(this);
  }

  openNew = () => {
    this.setState({
      isOpen: true,
    });
  };

  openEdit = (projectId: EntityId) => {
    //
  };

  close = () => {
    this.setState({
      isOpen: false,
    });
  };

  onCloseHandler = () => {
    this.setState({
      isOpen: false,
    });
  };

  addedHandler = () => {
    if (this.props.onAdd) {
      this.props.onAdd();
    }
  };

  render() {
    const { isOpen } = this.state;

    if (!isOpen) return null;

    return (
      <ProjectAddPanel
        onClose={this.onCloseHandler}
        onAdded={this.addedHandler}
      />
    );
  }
}

export default ProjectControl;
