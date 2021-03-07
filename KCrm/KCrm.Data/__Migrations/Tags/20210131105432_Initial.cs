using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KCrm.Data.__Migrations.Tags
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tag");

            migrationBuilder.CreateTable(
                name: "tag_groups",
                schema: "tag",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "tag",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tag_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.id);
                    table.ForeignKey(
                        name: "fk_tags_tag_groups_tag_group_entity_id",
                        column: x => x.tag_group_id,
                        principalSchema: "tag",
                        principalTable: "tag_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "ix_tag_groups_name",
                schema: "tag",
                table: "tag_groups",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tags_name_tag_group_id",
                schema: "tag",
                table: "tags",
                columns: new[] { "name", "tag_group_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tags_tag_group_id",
                schema: "tag",
                table: "tags",
                column: "tag_group_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tags",
                schema: "tag");

            migrationBuilder.DropTable(
                name: "tag_groups",
                schema: "tag");
        }
    }
}
