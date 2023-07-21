using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poemify.DAL.Migrations
{
    public partial class ConfigureManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoemTag");

            migrationBuilder.CreateTable(
                name: "PoemTags",
                columns: table => new
                {
                    PoemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoemTags", x => new { x.PoemId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PoemTags_Poems_PoemId",
                        column: x => x.PoemId,
                        principalTable: "Poems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoemTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoemTags_TagId",
                table: "PoemTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoemTags");

            migrationBuilder.CreateTable(
                name: "PoemTag",
                columns: table => new
                {
                    PoemTagsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PoemsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoemTag", x => new { x.PoemTagsId, x.PoemsId });
                    table.ForeignKey(
                        name: "FK_PoemTag_Poems_PoemsId",
                        column: x => x.PoemsId,
                        principalTable: "Poems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoemTag_Tags_PoemTagsId",
                        column: x => x.PoemTagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoemTag_PoemsId",
                table: "PoemTag",
                column: "PoemsId");
        }
    }
}
