using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeBlog.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddingTagNavigationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "ImageRL",
            //    table: "BlogPosts",
            //    newName: "ImageURL");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_BlogPostId",
                table: "Tags",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_BlogPosts_BlogPostId",
                table: "Tags",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_BlogPosts_BlogPostId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_BlogPostId",
                table: "Tags");

            //migrationBuilder.RenameColumn(
            //    name: "ImageURL",
            //    table: "BlogPosts",
            //    newName: "ImageRL");
        }
    }
}
