using Microsoft.EntityFrameworkCore.Migrations;

namespace NTR2.Migrations
{
    public partial class FixedRelationshipsMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Notes_NoteID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategories_Categories_CategoryID",
                table: "NoteCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_NoteID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CateogryID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "NoteID",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Categories",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCategories_Categories_CategoryID",
                table: "NoteCategories",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategories_Categories_CategoryID",
                table: "NoteCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "CateogryID",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "NoteID",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CateogryID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_NoteID",
                table: "Categories",
                column: "NoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Notes_NoteID",
                table: "Categories",
                column: "NoteID",
                principalTable: "Notes",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCategories_Categories_CategoryID",
                table: "NoteCategories",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CateogryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
