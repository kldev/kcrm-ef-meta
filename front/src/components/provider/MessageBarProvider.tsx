import React, { useCallback } from 'react';
import { Stack, IStackStyles, IStackTokens } from '@fluentui/react/lib/Stack';
import { Layer } from '@fluentui/react/lib/Layer';
import { useSelector } from 'react-redux';
import { RootAppState } from 'store';
import { MessageBarNotification } from 'model/MessageBarNotification';
import NotificationMessageBar from 'components/share/NotificationMessageBar';

const stackStyles: Partial<IStackStyles> = {
  root: {
    position: 'fixed',
    bottom: 20,
    right: 20,
    maxHeight: '75vh',
    maxWidth: '500px',
    overflow: 'auto',
    padding: 20,
    opacity: 1,
  },
};

const stackTokens: Partial<IStackTokens> = {
  childrenGap: 20,
};

interface StateProps {
  messages: MessageBarNotification[];
}

const MessageBarProvider: React.FunctionComponent = (props) => {
  const { messages } = useSelector<RootAppState, StateProps>(({ message }) => ({
    messages: message.messages,
  }));

  const renderMessages = useCallback(() => {
    return messages.map<React.ReactNode>((message) => {
      return <NotificationMessageBar key={message.id} message={message} />;
    });
  }, [messages]);

  return (
    <>
      {props.children}
      {messages.length ? (
        <Layer className="message-bar-layer">
          <Stack
            className="message-bar-stack"
            styles={stackStyles}
            tokens={stackTokens}
          >
            {renderMessages()}
          </Stack>
        </Layer>
      ) : null}
    </>
  );
};

export default MessageBarProvider;
