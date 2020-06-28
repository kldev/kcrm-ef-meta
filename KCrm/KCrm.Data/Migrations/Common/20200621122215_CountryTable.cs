using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KCrm.Data.Migrations.Common {
    public partial class CountryTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.EnsureSchema (
                name: "common");

            migrationBuilder.CreateTable (
                name: "countries",
                schema: "common",
                columns: table => new {
                    id = table.Column<Guid> (nullable: false),
                    name = table.Column<string> (maxLength: 100, nullable: false),
                    code = table.Column<string> (maxLength: 4, nullable: false),
                    iso = table.Column<string> (maxLength: 4, nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey ("pk_countries", x => x.id);
                });

            migrationBuilder.CreateIndex (
                name: "ix_countries_iso",
                schema: "common",
                table: "countries",
                column: "iso",
                unique: true);

            migrationBuilder.CreateIndex (
                name: "ix_countries_name",
                schema: "common",
                table: "countries",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable (
                name: "countries",
                schema: "common");
        }
    }
}
