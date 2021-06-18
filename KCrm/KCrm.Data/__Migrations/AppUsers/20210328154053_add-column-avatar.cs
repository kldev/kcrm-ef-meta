using Microsoft.EntityFrameworkCore.Migrations;

namespace KCrm.Data.__Migrations.AppUsers
{
    public partial class addcolumnavatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "avatar_id",
                schema: "app",
                table: "user_accounts",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar_id",
                schema: "app",
                table: "user_accounts");
        }
    }
}
