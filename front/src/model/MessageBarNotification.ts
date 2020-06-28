import { MessageBarType } from '@fluentui/react/lib/MessageBar';

export interface MessageBarNotification {
  id: string;
  content: string;
  messageType: MessageBarType;
}
