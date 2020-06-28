import React from 'react';
import { registerIcons } from '@uifabric/styling';

export const registerExtraIcons = () => {
  registerIcons({
    icons: {
      FlagPoland: <i className="pl flag" />,
      FlagEngland: <i className="gb flag" />,
    },
  });
};
