using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddTagDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "tags",
                type: "TEXT",
                maxLength: 1024,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "tags");
        }
    }
}
