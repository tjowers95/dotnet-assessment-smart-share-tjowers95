using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "created",
                table: "smart_share_file",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created",
                table: "smart_share_file");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "smart_share_file",
                newName: "Id");
        }
    }
}
