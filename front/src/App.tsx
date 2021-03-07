import React from 'react';
import { Customizations, ThemeProvider } from '@fluentui/react';
import './app.scss';
import { MessageBarProvider, MainApp } from 'components';
import { LocaleProvider } from 'components/provider';
import { BrowserRouter } from 'react-router-dom';
import { createTheme } from '@uifabric/styling';

const myTheme = createTheme({
  defaultFontStyle: {
    fontFamily: 'Roboto, Selawik Regular, Sans-serif',
  },
  palette: {
    themePrimary: '#55595e',
    themeLighterAlt: '#f8f8f9',
    themeLighter: '#e2e3e5',
    themeLight: '#c9cbcf',
    themeTertiary: '#95999f',
    themeSecondary: '#686c72',
    themeDarkAlt: '#4c5055',
    themeDark: '#414348',
    themeDarker: '#303235',
    neutralLighterAlt: '#f8f8f8',
    neutralLighter: '#f4f4f4',
    neutralLight: '#eaeaea',
    neutralQuaternaryAlt: '#dadada',
    neutralQuaternary: '#d0d0d0',
    neutralTertiaryAlt: '#c8c8c8',
    neutralTertiary: '#c6a2cf',
    neutralSecondary: '#915a9f',
    neutralPrimaryAlt: '#632a72',
    neutralPrimary: '#511a5e',
    neutralDark: '#3d1448',
    black: '#2d0f35',
    white: '#ffffff',
  },
});

interface StateProps {}

const App: React.FC = () => {
  Customizations.applySettings({ theme: myTheme });
  return (
    <BrowserRouter>
      <ThemeProvider id="app">
        <LocaleProvider>
          <MessageBarProvider>
            <MainApp />
          </MessageBarProvider>
        </LocaleProvider>
      </ThemeProvider>
    </BrowserRouter>
  );
};

export default App;
