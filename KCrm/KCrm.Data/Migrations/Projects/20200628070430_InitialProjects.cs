using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KCrm.Data.Migrations.Projects {
    public partial class InitialProjects : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.EnsureSchema (
                name: "project");

            migrationBuilder.CreateTable (
                name: "project_has_tags",
                schema: "project",
                columns: table => new {
                    id = table.Column<Guid> (nullable: false),
                    tag_id = table.Column<Guid> (nullable: false),
                    project_id = table.Column<Guid> (nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey ("pk_project_has_tags", x => x.id);
                });

            migrationBuilder.CreateTable (
                name: "project_has_users",
                schema: "project",
                columns: table => new {
                    id = table.Column<Guid> (nullable: false),
                    user_id = table.Column<Guid> (nullable: false),
                    project_id = table.Column<Guid> (nullable: false),
                    user_role_type = table.Column<string> (nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey ("pk_project_has_users", x => x.id);
                });

            migrationBuilder.CreateTable (
                name: "projects",
                schema: "project",
                columns: table => new {
                    id = table.Column<Guid> (nullable: false),
                    name = table.Column<string> (maxLength: 1024, nullable: false),
                    description = table.Column<string> (maxLength: 8096, nullable: true),
                    project_type = table.Column<string> (nullable: false),
                    start_date_time_utc = table.Column<DateTime> (nullable: true),
                    planed_end_date_time_utc = table.Column<DateTime> (nullable: true),
                    end_date_time_utc = table.Column<DateTime> (nullable: true),
                    created = table.Column<DateTime> (nullable: true),
                    deleted = table.Column<bool> (nullable: false),
                    updated = table.Column<DateTime> (nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey ("pk_projects", x => x.id);
                });

            migrationBuilder.CreateIndex (
                name: "ix_project_has_tags_tag_id_project_id",
                schema: "project",
                table: "project_has_tags",
                columns: new[] { "tag_id", "project_id" },
                unique: true);

            migrationBuilder.CreateIndex (
                name: "ix_project_has_users_project_id_user_id",
                schema: "project",
                table: "project_has_users",
                columns: new[] { "project_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex (
                name: "ix_projects_name",
                schema: "project",
                table: "projects",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable (
                name: "project_has_tags",
                schema: "project");

            migrationBuilder.DropTable (
                name: "project_has_users",
                schema: "project");

            migrationBuilder.DropTable (
                name: "projects",
                schema: "project");
        }
    }
}
