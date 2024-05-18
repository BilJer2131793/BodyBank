using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BodyBank.Migrations
{
    public partial class apresChangementIdeentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commande_Util_UtilId",
                table: "Commande");

            migrationBuilder.DropTable(
                name: "Util");

            migrationBuilder.AlterColumn<string>(
                name: "UtilId",
                table: "Commande",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AdresseUtilAddresseId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomUtil",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrenomUtil",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdresseUtilAddresseId",
                table: "AspNetUsers",
                column: "AdresseUtilAddresseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresse_AdresseUtilAddresseId",
                table: "AspNetUsers",
                column: "AdresseUtilAddresseId",
                principalTable: "Addresse",
                principalColumn: "AddresseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commande_AspNetUsers_UtilId",
                table: "Commande",
                column: "UtilId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresse_AdresseUtilAddresseId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Commande_AspNetUsers_UtilId",
                table: "Commande");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AdresseUtilAddresseId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AdresseUtilAddresseId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NomUtil",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PrenomUtil",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UtilId",
                table: "Commande",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Util",
                columns: table => new
                {
                    UtilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresseUtilAddresseId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomUtil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrenomUtil = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Util", x => x.UtilId);
                    table.ForeignKey(
                        name: "FK_Util_Addresse_AdresseUtilAddresseId",
                        column: x => x.AdresseUtilAddresseId,
                        principalTable: "Addresse",
                        principalColumn: "AddresseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Util_AdresseUtilAddresseId",
                table: "Util",
                column: "AdresseUtilAddresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Util_Email",
                table: "Util",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Commande_Util_UtilId",
                table: "Commande",
                column: "UtilId",
                principalTable: "Util",
                principalColumn: "UtilId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
