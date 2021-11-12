using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PollosHermano.CoreBancario.Infraestructure.Core.Migrations
{
    public partial class Migration_20211111062456 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatTipoCuenta",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatTipoCuenta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cuenta",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdContrato = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdTipoCuenta = table.Column<byte>(type: "tinyint", nullable: false),
                    NumeroCuenta = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuenta_CatTipoCuenta_IdTipoCuenta",
                        column: x => x.IdTipoCuenta,
                        principalSchema: "dbo",
                        principalTable: "CatTipoCuenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cuenta_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalSchema: "dbo",
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cuenta_Contrato_IdContrato",
                        column: x => x.IdContrato,
                        principalSchema: "dbo",
                        principalTable: "Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_IdCliente",
                schema: "dbo",
                table: "Cuenta",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_IdContrato",
                schema: "dbo",
                table: "Cuenta",
                column: "IdContrato");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_IdTipoCuenta",
                schema: "dbo",
                table: "Cuenta",
                column: "IdTipoCuenta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuenta",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CatTipoCuenta",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Cliente",
                schema: "dbo");
        }
    }
}
