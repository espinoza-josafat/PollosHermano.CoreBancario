using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PollosHermano.CoreBancario.Infraestructure.Core.Migrations
{
    public partial class Migration_20211130063410 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

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
                name: "Sucursal",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zona",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursal = table.Column<byte>(type: "tinyint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Estatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zona_Sucursal_IdSucursal",
                        column: x => x.IdSucursal,
                        principalSchema: "dbo",
                        principalTable: "Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendedor",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    IdZona = table.Column<int>(type: "int", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendedor_Zona_IdZona",
                        column: x => x.IdZona,
                        principalSchema: "dbo",
                        principalTable: "Zona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreContrato",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProspecto = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    FechaNacimientoProspecto = table.Column<DateTime>(type: "Date", nullable: false),
                    ApellidoMaternoProspecto = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    IdVendedor = table.Column<int>(type: "int", nullable: false),
                    ApellidoPaternoProspecto = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreContrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreContrato_Vendedor_IdVendedor",
                        column: x => x.IdVendedor,
                        principalSchema: "dbo",
                        principalTable: "Vendedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPreContrato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contrato_PreContrato_IdPreContrato",
                        column: x => x.IdPreContrato,
                        principalSchema: "dbo",
                        principalTable: "PreContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Contrato_IdPreContrato",
                schema: "dbo",
                table: "Contrato",
                column: "IdPreContrato");

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

            migrationBuilder.CreateIndex(
                name: "IX_PreContrato_IdVendedor",
                schema: "dbo",
                table: "PreContrato",
                column: "IdVendedor");

            migrationBuilder.CreateIndex(
                name: "IX_Vendedor_IdZona",
                schema: "dbo",
                table: "Vendedor",
                column: "IdZona");

            migrationBuilder.CreateIndex(
                name: "IX_Zona_IdSucursal",
                schema: "dbo",
                table: "Zona",
                column: "IdSucursal");
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

            migrationBuilder.DropTable(
                name: "Contrato",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PreContrato",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Vendedor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Zona",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sucursal",
                schema: "dbo");
        }
    }
}
