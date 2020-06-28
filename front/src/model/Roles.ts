export const Root = 'root';
export const Admin = 'admin';
export const Seller = 'seller';
export const Guest = 'guest';
export const None = 'none';

export type RoleTypes =
  | typeof Root
  | typeof Admin
  | typeof Seller
  | typeof Guest
  | typeof None;
