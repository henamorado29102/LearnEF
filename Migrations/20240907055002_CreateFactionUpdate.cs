using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkLearn.Migrations
{
    /// <inheritdoc />
    public partial class CreateFactionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Factions_FactionId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Factions_Factions_FactionId",
                table: "Factions");

            migrationBuilder.DropIndex(
                name: "IX_Factions_FactionId",
                table: "Factions");

            migrationBuilder.DropIndex(
                name: "IX_Characters_FactionId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "FactionId",
                table: "Factions");

            migrationBuilder.DropColumn(
                name: "FactionId",
                table: "Characters");

            migrationBuilder.CreateTable(
                name: "CharacterFaction",
                columns: table => new
                {
                    charactersId = table.Column<int>(type: "int", nullable: false),
                    factionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterFaction", x => new { x.charactersId, x.factionsId });
                    table.ForeignKey(
                        name: "FK_CharacterFaction_Characters_charactersId",
                        column: x => x.charactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterFaction_Factions_factionsId",
                        column: x => x.factionsId,
                        principalTable: "Factions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFaction_factionsId",
                table: "CharacterFaction",
                column: "factionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterFaction");

            migrationBuilder.AddColumn<int>(
                name: "FactionId",
                table: "Factions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FactionId",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factions_FactionId",
                table: "Factions",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_FactionId",
                table: "Characters",
                column: "FactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Factions_FactionId",
                table: "Characters",
                column: "FactionId",
                principalTable: "Factions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Factions_Factions_FactionId",
                table: "Factions",
                column: "FactionId",
                principalTable: "Factions",
                principalColumn: "Id");
        }
    }
}
