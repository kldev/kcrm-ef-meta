using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KCrm.Data.Migrations.Users {
    public partial class InitialUsers : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.EnsureSchema (
                name: "app");

            migrationBuilder.CreateTable (
                name: "app_users",
                schema: "app",
                columns: table => new {
                    id = table.Column<Guid> (nullable: false),
                    username = table.Column<string> (maxLength: 100, nullable: false),
                    password = table.Column<string> (maxLength: 100, nullable: false),
                    name = table.Column<string> (maxLength: 100, nullable: false),
                    last_name = table.Column<string> (maxLength: 100, nullable: false),
                    email = table.Column<string> (maxLength: 100, nullable: false),
                    is_enabled = table.Column<bool> (nullable: false),
                    provider = table.Column<string> (maxLength: 100, nullable: false, defaultValue: "crm"),
                    provider_id = table.Column<string> (maxLength: 100, nullable: true),
                    created = table.Column<DateTime> (nullable: true),
                    deleted = table.Column<bool> (nullable: false),
                    updated = table.Column<DateTime> (nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey ("pk_app_users", x => x.id);
                });

            migrationBuilder.CreateTable (
                name: "user_logins",
                schema: "app",
                columns: table => new {
                    id = table.Column<Guid> (nullable: false),
                    refresh_token = table.Column<string> (nullable: true),
                    user_id = table.Column<Guid> (nullable: false),
                    valid_period = table.Column<TimeSpan> (nullable: false),
                    created = table.Column<DateTime> (nullable: true),
                    updated = table.Column<DateTime> (nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey ("pk_user_logins", x => x.id);
                });

            migrationBuilder.CreateTable (
                name: "app_user_roles",
                schema: "app",
                columns: table => new {
                    id = table.Column<Guid> (nullable: false),
                    name = table.Column<string> (maxLength: 100, nullable: false),
                    description = table.Column<string> (maxLength: 512, nullable: true),
                    deleted = table.Column<bool> (nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey ("pk_app_user_roles", x => x.id);
                });

            migrationBuilder.CreateTable (
                name: "user_has_roles",
                schema: "app",
                columns: table => new {
                    id = table.Column<Guid> (nullable: false),
                    user_role_id = table.Column<Guid> (nullable: false),
                    user_id = table.Column<Guid> (nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey ("pk_user_has_roles", x => x.id);
                    table.ForeignKey (
                        name: "fk_user_has_roles_app_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "app",
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey (
                        name: "fk_user_has_roles_app_user_roles_user_role_id",
                        column: x => x.user_role_id,
                        principalSchema: "app",
                        principalTable: "app_user_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex (
                name: "ix_user_has_roles_user_role_id",
                schema: "app",
                table: "user_has_roles",
                column: "user_role_id");

            migrationBuilder.CreateIndex (
                name: "ix_user_has_roles_user_id_user_role_id",
                schema: "app",
                table: "user_has_roles",
                columns: new[] { "user_id", "user_role_id" },
                unique: true);

            migrationBuilder.CreateIndex (
                name: "ix_user_logins_refresh_token",
                schema: "app",
                table: "user_logins",
                column: "refresh_token",
                unique: true);

            migrationBuilder.CreateIndex (
                name: "ix_user_logins_user_id",
                schema: "app",
                table: "user_logins",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable (
                name: "user_has_roles",
                schema: "app");

            migrationBuilder.DropTable (
                name: "user_logins",
                schema: "app");

            migrationBuilder.DropTable (
                name: "app_users",
                schema: "app");

            migrationBuilder.DropTable (
                name: "app_user_roles",
                schema: "app");
        }
    }
}
