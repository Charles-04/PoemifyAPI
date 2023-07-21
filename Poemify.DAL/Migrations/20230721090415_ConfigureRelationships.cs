using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poemify.DAL.Migrations
{
    public partial class ConfigureRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Poems_PoemId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Poems_AspNetUsers_AuthorId",
                table: "Poems");

            migrationBuilder.DropForeignKey(
                name: "FK_PoemTag_Tags_TagsId",
                table: "PoemTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PoemTag",
                table: "PoemTag");

            migrationBuilder.DropIndex(
                name: "IX_PoemTag_TagsId",
                table: "PoemTag");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "PoemTag",
                newName: "PoemTagsId");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Poems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PoemId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PoemTag",
                table: "PoemTag",
                columns: new[] { "PoemTagsId", "PoemsId" });

            migrationBuilder.CreateIndex(
                name: "IX_PoemTag_PoemsId",
                table: "PoemTag",
                column: "PoemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Poems_PoemId",
                table: "Comments",
                column: "PoemId",
                principalTable: "Poems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Poems_AspNetUsers_AuthorId",
                table: "Poems",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PoemTag_Tags_PoemTagsId",
                table: "PoemTag",
                column: "PoemTagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Poems_PoemId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Poems_AspNetUsers_AuthorId",
                table: "Poems");

            migrationBuilder.DropForeignKey(
                name: "FK_PoemTag_Tags_PoemTagsId",
                table: "PoemTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PoemTag",
                table: "PoemTag");

            migrationBuilder.DropIndex(
                name: "IX_PoemTag_PoemsId",
                table: "PoemTag");

            migrationBuilder.RenameColumn(
                name: "PoemTagsId",
                table: "PoemTag",
                newName: "TagsId");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Poems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PoemId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PoemTag",
                table: "PoemTag",
                columns: new[] { "PoemsId", "TagsId" });

            migrationBuilder.CreateIndex(
                name: "IX_PoemTag_TagsId",
                table: "PoemTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Poems_PoemId",
                table: "Comments",
                column: "PoemId",
                principalTable: "Poems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Poems_AspNetUsers_AuthorId",
                table: "Poems",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PoemTag_Tags_TagsId",
                table: "PoemTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
