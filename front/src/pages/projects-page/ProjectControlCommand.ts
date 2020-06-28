import { EntityId } from 'model/EntityId';

export interface ProjectControlCommand {
  openNew: () => void;
  openEdit: (projectId: EntityId) => void;
  close: () => void;
}
