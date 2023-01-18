using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoLayer.Migrations
{
    public partial class thirdmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteEntity_Users_UserId",
                table: "NoteEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteEntity",
                table: "NoteEntity");

            migrationBuilder.RenameTable(
                name: "NoteEntity",
                newName: "Notes");

            migrationBuilder.RenameIndex(
                name: "IX_NoteEntity_UserId",
                table: "Notes",
                newName: "IX_Notes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "NoteID");

            migrationBuilder.CreateTable(
                name: "CollabDetails",
                columns: table => new
                {
                    CollabId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NotesID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabDetails", x => x.CollabId);
                    table.ForeignKey(
                        name: "FK_CollabDetails_Notes_NotesID",
                        column: x => x.NotesID,
                        principalTable: "Notes",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollabDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabDetails_NotesID",
                table: "CollabDetails",
                column: "NotesID");

            migrationBuilder.CreateIndex(
                name: "IX_CollabDetails_UserId",
                table: "CollabDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "CollabDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "NoteEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_UserId",
                table: "NoteEntity",
                newName: "IX_NoteEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteEntity",
                table: "NoteEntity",
                column: "NoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteEntity_Users_UserId",
                table: "NoteEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
