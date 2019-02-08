using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Server.Migrations
{
    public partial class smartshare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "smart_share_file",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    file_name = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: true),
                    maximum_downloads = table.Column<int>(nullable: false),
                    download_count = table.Column<int>(nullable: false),
                    created = table.Column<string>(nullable: true),
                    expiration = table.Column<string>(nullable: true),
                    file_data = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_smart_share_file", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "smart_share_file");
        }
    }
}
