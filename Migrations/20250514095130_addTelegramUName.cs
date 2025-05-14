using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TempTake_Server.Migrations
{
    /// <inheritdoc />
    public partial class addTelegramUName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TelegramUsername",
                table: "Users",
                type: "varchar(32)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelegramUsername",
                table: "Users");
        }
    }
}
