import React from 'react';
import { Stack } from '@fluentui/react/lib/Stack';

const Header: React.FC = (props) => {
  return (
    <Stack
      verticalFill={false}
      styles={{
        root: {
          paddingLeft: '5px',
          color: '#7c8781',
          fontSize: '24px',
          fontWeight: '600',
        },
      }}
      verticalAlign="center"
    >
      {props.children}
    </Stack>
  );
};

export default Header;
