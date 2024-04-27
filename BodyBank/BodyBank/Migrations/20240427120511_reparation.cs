using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BodyBank.Migrations
{
    public partial class reparation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Util_Adresse_AdresseUtilAddresseId",
                table: "Util");

            migrationBuilder.AlterColumn<int>(
                name: "AdresseUtilAddresseId",
                table: "Util",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Util_Adresse_AdresseUtilAddresseId",
                table: "Util",
                column: "AdresseUtilAddresseId",
                principalTable: "Adresse",
                principalColumn: "AddresseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Util_Adresse_AdresseUtilAddresseId",
                table: "Util");

            migrationBuilder.AlterColumn<int>(
                name: "AdresseUtilAddresseId",
                table: "Util",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Util_Adresse_AdresseUtilAddresseId",
                table: "Util",
                column: "AdresseUtilAddresseId",
                principalTable: "Adresse",
                principalColumn: "AddresseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
