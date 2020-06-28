using System;
using System.Threading.Tasks;
using KCrm.Core.Entity.Users;
using KCrm.Core.Security;
using KCrm.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KCrm.ConsoleSample {
    public class UserSample : SampleBase<AppUserContext> {

        private IPasswordHasher _hasher = new BCryptPasswordHasher ( );

        protected override async Task SeedData() {
            await SeedUserRoles ( );
            await SeedUsers ( );
            await SeeedAssignRoles ( );
            _logger.LogInformation ("Add users to roles");
        }

        private async Task SeedUsers() {
            if (await Context.AppUsers.CountAsync ( ) == 0) {
                _logger.LogInformation ("Seed users");
                await Context.AppUsers.AddAsync (new User {
                    Username = "root",
                    Id = UserHelper.RootId,
                    Email = "root@fakemail.fake",
                    Name = "Root",
                    LastName = string.Empty,
                    Password = _hasher.Hash ("123456"),
                    IsEnabled = true
                });

                await Context.AppUsers.AddAsync (new User {
                    Username = "admin",
                    Id = UserHelper.AdminId,
                    Email = "admin@fakemail.fake",
                    Name = "The admin",
                    LastName = string.Empty,
                    Password = _hasher.Hash ("654321"),
                    IsEnabled = true
                });

                await Context.AppUsers.AddAsync (new User {
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
            if (await Context.AppUserRoles.CountAsync ( ) == 0) {
                _logger.LogInformation ("Seed users roles");
                await Context.AppUserRoles.AddAsync (new UserRole ( ) {
                    Id = UserRoleHelper.RootRoleId,
                    Name = "root",
                    Description = "The super admin/root"
                });

                await Context.AppUserRoles.AddAsync (new UserRole ( ) {
                    Id = UserRoleHelper.AdminRoleId,
                    Name = "admin",
                    Description = "The admin"
                });

                await Context.AppUserRoles.AddAsync (new UserRole ( ) {
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

        private async Task SeeedAssignRoles() {
            if (await Context.UserHasRoles.CountAsync ( ) == 0) {

                await Context.UserHasRoles.AddRangeAsync (new[] {
                    new UserHasRole {
                        Id = Guid.NewGuid ( ), UserId = UserHelper.RootId, UserRoleId = UserRoleHelper.RootRoleId
                    },
                    new UserHasRole {
                        Id = Guid.NewGuid (  ), UserId = UserHelper.AdminId, UserRoleId = UserRoleHelper.AdminRoleId
                    }
                });

                await Context.SaveChangesAsync ( );
            }
        }
    }
}
