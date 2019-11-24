using Microsoft.EntityFrameworkCore.Migrations;

namespace NTR2.Migrations
{
    public partial class FixedRelationshipsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NoteCategories_NoteID",
                table: "NoteCategories",
                column: "NoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCategories_Categories_CategoryID",
                table: "NoteCategories",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CateogryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCategories_Notes_NoteID",
                table: "NoteCategories",
                column: "NoteID",
                principalTable: "Notes",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategories_Categories_CategoryID",
                table: "NoteCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategories_Notes_NoteID",
                table: "NoteCategories");

            migrationBuilder.DropIndex(
                name: "IX_NoteCategories_NoteID",
                table: "NoteCategories");
        }
    }
}
