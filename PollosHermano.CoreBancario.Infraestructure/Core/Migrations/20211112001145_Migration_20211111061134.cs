using Microsoft.EntityFrameworkCore.Migrations;

namespace PollosHermano.CoreBancario.Infraestructure.Core.Migrations
{
    public partial class Migration_20211111061134 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Numero",
                schema: "dbo",
                table: "PreContrato",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_IdPreContrato",
                schema: "dbo",
                table: "Contrato",
                column: "IdPreContrato");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contrato",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "Numero",
                schema: "dbo",
                table: "PreContrato");
        }
    }
}
