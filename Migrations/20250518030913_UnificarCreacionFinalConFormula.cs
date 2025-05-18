using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendAlquimia.Migrations
{
    /// <inheritdoc />
    public partial class UnificarCreacionFinalConFormula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_CreacionesFinales_CreacionFinalId",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "CreacionesFinales");

            migrationBuilder.DropIndex(
                name: "IX_Productos_CreacionFinalId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "CreacionFinalId",
                table: "Productos");

            migrationBuilder.AddColumn<double>(
                name: "ConcentracionAgua",
                table: "Formulas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ConcentracionAlcohol",
                table: "Formulas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ConcentracionEsencia",
                table: "Formulas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcentracionAgua",
                table: "Formulas");

            migrationBuilder.DropColumn(
                name: "ConcentracionAlcohol",
                table: "Formulas");

            migrationBuilder.DropColumn(
                name: "ConcentracionEsencia",
                table: "Formulas");

            migrationBuilder.AddColumn<int>(
                name: "CreacionFinalId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CreacionesFinales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadorId = table.Column<int>(type: "int", nullable: false),
                    IdFormula = table.Column<int>(type: "int", nullable: false),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    ConcentracionAgua = table.Column<double>(type: "float", nullable: false),
                    ConcentracionAlcohol = table.Column<double>(type: "float", nullable: false),
                    ConcentracionEsencia = table.Column<double>(type: "float", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreacionesFinales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreacionesFinales_Formulas_IdFormula",
                        column: x => x.IdFormula,
                        principalTable: "Formulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreacionesFinales_Pedidos_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreacionesFinales_Usuarios_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CreacionFinalId",
                table: "Productos",
                column: "CreacionFinalId");

            migrationBuilder.CreateIndex(
                name: "IX_CreacionesFinales_CreadorId",
                table: "CreacionesFinales",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_CreacionesFinales_IdFormula",
                table: "CreacionesFinales",
                column: "IdFormula");

            migrationBuilder.CreateIndex(
                name: "IX_CreacionesFinales_IdPedido",
                table: "CreacionesFinales",
                column: "IdPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_CreacionesFinales_CreacionFinalId",
                table: "Productos",
                column: "CreacionFinalId",
                principalTable: "CreacionesFinales",
                principalColumn: "Id");
        }
    }
}
