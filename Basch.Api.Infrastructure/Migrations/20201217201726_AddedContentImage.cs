using Microsoft.EntityFrameworkCore.Migrations;

namespace Basch.Api.Infrastructure.Migrations
{
    public partial class AddedContentImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentImage",
                table: "Announcements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentImage",
                table: "Announcements");
        }
    }
}
