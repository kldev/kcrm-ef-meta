import { IButtonStyles } from '@fluentui/react/lib/Button';
import { ICommandBarStyles } from '@fluentui/react/lib/CommandBar';
import { getTheme } from '@uifabric/styling';

const theme = getTheme();

export const outerCommandBarStyles: ICommandBarStyles = {
  root: {
    backgroundColor: '#eee',
    height: 50,
    padding: '0.2em 0.2em',
    boxShadow: '0 4px 6px -6px #222',
    boxSizing: 'border-box',
  },
};

export const commandBarWaffleButtonStyles: IButtonStyles = {
  root: {
    backgroundColor: theme.palette.themeDarkAlt,
    width: 50,
  },
  rootHovered: {
    backgroundColor: '#104a7d',
  },
  rootPressed: {
    backgroundColor: '#104a7d',
  },
};
