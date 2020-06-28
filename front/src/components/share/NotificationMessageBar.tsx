import React, { useEffect } from 'react';
import { MessageBarNotification } from 'model/MessageBarNotification';
import { MessageBar } from '@fluentui/react/lib/MessageBar';
import { useDispatch } from 'react-redux';
import { removeMessage } from 'store/message';

interface Props {
  message: MessageBarNotification;
}

const NotificationMessageBar: React.FC<Props> = (props) => {
  const { message } = props;
  const dispatch = useDispatch();

  const handleOnClose = (id: string) => {
    dispatch(removeMessage({ id: message.id }));
  };

  useEffect(() => {
    let notificationTimeout: NodeJS.Timeout | null = null;

    notificationTimeout = setTimeout(() => {
      dispatch(removeMessage({ id: message.id }));
      notificationTimeout = null;
    }, 3000);

    return () => {
      if (notificationTimeout) {
        clearTimeout(notificationTimeout);
        notificationTimeout = null;
      }
    };
  }, [dispatch, message]);

  return (
    <MessageBar
      style={{ position: 'relative' }}
      key={message.id}
      onDismiss={() => {
        handleOnClose(message.id);
      }}
      dismissButtonAriaLabel="Close"
      messageBarType={message.messageType}
    >
      {message.content}
    </MessageBar>
  );
};

export default NotificationMessageBar;
