using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Core.Migrations
{
    /// <inheritdoc />
    public partial class TagsSiteIdColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tags_sites_SiteId",
                table: "tags");

            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "tags",
                newName: "site_id");

            migrationBuilder.RenameIndex(
                name: "IX_tags_SiteId",
                table: "tags",
                newName: "IX_tags_site_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tags_sites_site_id",
                table: "tags",
                column: "site_id",
                principalTable: "sites",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tags_sites_site_id",
                table: "tags");

            migrationBuilder.RenameColumn(
                name: "site_id",
                table: "tags",
                newName: "SiteId");

            migrationBuilder.RenameIndex(
                name: "IX_tags_site_id",
                table: "tags",
                newName: "IX_tags_SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_tags_sites_SiteId",
                table: "tags",
                column: "SiteId",
                principalTable: "sites",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
