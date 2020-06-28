import actionCreatorFactory from 'typescript-fsa';
import { MessageBarType } from '@fluentui/react/lib/MessageBar';

const actionCreator = actionCreatorFactory('message-bar');

export const addMessage = actionCreator<{
  content: string;
  messageType: MessageBarType;
}>('addMessage');
export const removeMessage = actionCreator<{
  id: string;
}>('removeMessage');
