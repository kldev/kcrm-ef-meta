using System;
using KCrm.Core.Entity;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KCrm.Data.__Migrations.Projects
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "project");

            migrationBuilder.CreateTable(
                name: "project_activities",
                schema: "project",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_name = table.Column<string>(type: "text", nullable: false),
                    event_data = table.Column<EventPayload>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_activities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project_has_tags",
                schema: "project",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tag_id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_has_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project_has_users",
                schema: "project",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_role_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_has_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                schema: "project",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    description = table.Column<string>(type: "character varying(8096)", maxLength: 8096, nullable: true),
                    project_type = table.Column<string>(type: "text", nullable: false),
                    start_date_time_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    planed_end_date_time_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    end_date_time_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_project_activities_event_name",
                schema: "project",
                table: "project_activities",
                column: "event_name");

            migrationBuilder.CreateIndex(
                name: "ix_project_activities_project_id",
                schema: "project",
                table: "project_activities",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_has_tags_tag_id_project_id",
                schema: "project",
                table: "project_has_tags",
                columns: new[] { "tag_id", "project_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_project_has_users_project_id_user_id",
                schema: "project",
                table: "project_has_users",
                columns: new[] { "project_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_projects_name",
                schema: "project",
                table: "projects",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_activities",
                schema: "project");

            migrationBuilder.DropTable(
                name: "project_has_tags",
                schema: "project");

            migrationBuilder.DropTable(
                name: "project_has_users",
                schema: "project");

            migrationBuilder.DropTable(
                name: "projects",
                schema: "project");
        }
    }
}
