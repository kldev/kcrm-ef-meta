export const AppConfig = {
  appName: `${process.env.REACT_APP_NAME}`,
  apiUrl: `${process.env.REACT_APP_API_URL}`,
  username:
    `${process.env.NODE_ENV || ''}`.trim() === 'development'
      ? `${process.env.REACT_APP_USERNAME}`
      : '',
  password:
    `${process.env.NODE_ENV || ''}`.trim() === 'development'
      ? `${process.env.REACT_APP_PASSWORD}`
      : '',
  gitSha: `${process.env.REACT_APP_GIT_SHA}`
};
