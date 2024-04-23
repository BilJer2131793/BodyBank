using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BodyBank.Migrations
{
    public partial class debut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresse",
                columns: table => new
                {
                    AddresseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoCivique = table.Column<int>(type: "int", nullable: false),
                    Rue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresse", x => x.AddresseId);
                });

            migrationBuilder.CreateTable(
                name: "Donneur",
                columns: table => new
                {
                    DonneurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Poids = table.Column<double>(type: "float", nullable: true),
                    Taille = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donneur", x => x.DonneurId);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<int>(type: "int", nullable: false),
                    PrixBase = table.Column<double>(type: "float", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Util",
                columns: table => new
                {
                    UtilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrenomUtil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomUtil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdresseUtilAddresseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Util", x => x.UtilId);
                    table.ForeignKey(
                        name: "FK_Util_Adresse_AdresseUtilAddresseId",
                        column: x => x.AdresseUtilAddresseId,
                        principalTable: "Adresse",
                        principalColumn: "AddresseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organne",
                columns: table => new
                {
                    OrganneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disponible = table.Column<bool>(type: "bit", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    DonneurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organne", x => x.OrganneId);
                    table.ForeignKey(
                        name: "FK_Organne_Donneur_DonneurId",
                        column: x => x.DonneurId,
                        principalTable: "Donneur",
                        principalColumn: "DonneurId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Organne_Type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Type",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commande",
                columns: table => new
                {
                    CommandeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdresseLivraisonAddresseId = table.Column<int>(type: "int", nullable: true),
                    UtilId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commande", x => x.CommandeId);
                    table.ForeignKey(
                        name: "FK_Commande_Adresse_AdresseLivraisonAddresseId",
                        column: x => x.AdresseLivraisonAddresseId,
                        principalTable: "Adresse",
                        principalColumn: "AddresseId");
                    table.ForeignKey(
                        name: "FK_Commande_Util_UtilId",
                        column: x => x.UtilId,
                        principalTable: "Util",
                        principalColumn: "UtilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandeOrgane",
                columns: table => new
                {
                    CommandeOrganeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganneId = table.Column<int>(type: "int", nullable: false),
                    CommandeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandeOrgane", x => x.CommandeOrganeId);
                    table.ForeignKey(
                        name: "FK_CommandeOrgane_Commande_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commande",
                        principalColumn: "CommandeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandeOrgane_Organne_OrganneId",
                        column: x => x.OrganneId,
                        principalTable: "Organne",
                        principalColumn: "OrganneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commande_AdresseLivraisonAddresseId",
                table: "Commande",
                column: "AdresseLivraisonAddresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_UtilId",
                table: "Commande",
                column: "UtilId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeOrgane_CommandeId",
                table: "CommandeOrgane",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeOrgane_OrganneId",
                table: "CommandeOrgane",
                column: "OrganneId");

            migrationBuilder.CreateIndex(
                name: "IX_Organne_DonneurId",
                table: "Organne",
                column: "DonneurId");

            migrationBuilder.CreateIndex(
                name: "IX_Organne_TypeId",
                table: "Organne",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Util_AdresseUtilAddresseId",
                table: "Util",
                column: "AdresseUtilAddresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Util_Email",
                table: "Util",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandeOrgane");

            migrationBuilder.DropTable(
                name: "Commande");

            migrationBuilder.DropTable(
                name: "Organne");

            migrationBuilder.DropTable(
                name: "Util");

            migrationBuilder.DropTable(
                name: "Donneur");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropTable(
                name: "Adresse");
        }
    }
}
