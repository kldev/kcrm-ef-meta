export const Messages = {
  LoginHasBeenSuccessful: 'Login has been successful',
  LoginHasFailed: 'Login has failed',
  YouHaveBeenLogOut: 'You have been logged out',
  IsRequired: 'is required',
};

export type MessagesKeys = keyof typeof Messages;

export type MessagesType = typeof Messages;
