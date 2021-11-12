using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PollosHermano.CoreBancario.Infraestructure.Core.Migrations
{
    public partial class Migration_20211111060615 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

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
                    ApellidoPaternoProspecto = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
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
