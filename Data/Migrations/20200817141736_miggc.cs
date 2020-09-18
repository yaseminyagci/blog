using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class miggc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Selected",
                table: "TagPosts",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Selected",
                table: "TagPosts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(byte));
        }
    }
}
