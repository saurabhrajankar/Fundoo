using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoLayer.Migrations
{
    public partial class fifthmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Label_Notes_NoteEntityNoteID",
                table: "Label");

            migrationBuilder.DropIndex(
                name: "IX_Label_NoteEntityNoteID",
                table: "Label");

            migrationBuilder.DropColumn(
                name: "NoteEntityNoteID",
                table: "Label");

            migrationBuilder.CreateIndex(
                name: "IX_Label_NoteId",
                table: "Label",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Label_Notes_NoteId",
                table: "Label",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Label_Notes_NoteId",
                table: "Label");

            migrationBuilder.DropIndex(
                name: "IX_Label_NoteId",
                table: "Label");

            migrationBuilder.AddColumn<long>(
                name: "NoteEntityNoteID",
                table: "Label",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Label_NoteEntityNoteID",
                table: "Label",
                column: "NoteEntityNoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Label_Notes_NoteEntityNoteID",
                table: "Label",
                column: "NoteEntityNoteID",
                principalTable: "Notes",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
