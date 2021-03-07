using System;
using System.Threading.Tasks;
using KCrm.Core.Entity.Users;
using KCrm.Core.Security;
using KCrm.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KCrm.ConsoleSample {
    public class UserSample : SampleBase<AppUserContext> {

        private readonly IPasswordHasher _hasher = new BCryptPasswordHasher ( );

        protected override async Task SeedData() {
            await SeedUserRoles ( );
            await SeedUsers ( );
            await SeedAssignRoles ( );
            _logger.LogInformation ("Add users to roles");
        }

        private async Task SeedUsers() {
            if (await Context.UserAccounts.CountAsync ( ) == 0) {
                _logger.LogInformation ("Seed users");
                await Context.UserAccounts.AddAsync (new UserAccountEntity {
                    Username = "root",
                    Id = UserHelper.RootId,
                    Email = "root@fake.mail",
                    Name = "Root",
                    LastName = string.Empty,
                    Password = _hasher.Hash ("toor123"),
                    IsEnabled = true
                });

                await Context.UserAccounts.AddAsync (new UserAccountEntity {
                    Username = "admin",
                    Id = UserHelper.AdminId,
                    Email = "admin@fakemail.fake",
                    Name = "The admin",
                    LastName = string.Empty,
                    Password = _hasher.Hash ("admin123"),
                    IsEnabled = true
                });

                await Context.UserAccounts.AddAsync (new UserAccountEntity {
                    Username = "john.smith@fakemail.fake",
                    Id = UserHelper.JohnId,
                    Email = "john.smith@fakemail.fake",
                    Name = "John Adam",
                    LastName = "Smith",
                    Password = _hasher.Hash ("123456"),
                    IsEnabled = true
                });

                await Context.SaveChangesAsync ( );
            }
            else {
                _logger.LogInformation ("Users already added");
            }
        }

        private async Task SeedUserRoles() {
            if (await Context.UserRoles.CountAsync ( ) == 0) {
                _logger.LogInformation ("Seed users roles");
                await Context.UserRoles.AddAsync (new UserRoleEntity ( ) {
                    Id = UserRoleHelper.RootRoleId,
                    Name = "root",
                    Description = "The super admin/root"
                });

                await Context.UserRoles.AddAsync (new UserRoleEntity ( ) {
                    Id = UserRoleHelper.AdminRoleId,
                    Name = "admin",
                    Description = "The admin"
                });

                await Context.UserRoles.AddAsync (new UserRoleEntity ( ) {
                    Id = UserRoleHelper.SellerRoleId,
                    Name = "seller",
                    Description = "The seller"
                });

                await Context.SaveChangesAsync ( );
            }
            else {
                _logger.LogInformation ("User roles already added");
            }
        }

        private async Task SeedAssignRoles() {
            if (await Context.UserHasRoles.CountAsync ( ) == 0) {

                await Context.UserHasRoles.AddRangeAsync (new[] {
                    new UserHasRoleEntity {
                        Id = Guid.NewGuid ( ), UserId = UserHelper.RootId, RoleId = UserRoleHelper.RootRoleId
                    },
                    new UserHasRoleEntity {
                        Id = Guid.NewGuid (  ), UserId = UserHelper.AdminId, RoleId = UserRoleHelper.AdminRoleId
                    }
                });

                await Context.SaveChangesAsync ( );
            }
        }
    }
}
