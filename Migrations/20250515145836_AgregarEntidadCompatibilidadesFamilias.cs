using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendAlquimia.Migrations
{
    /// <inheritdoc />
    public partial class AgregarEntidadCompatibilidadesFamilias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompatibilidadesFamilias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Familia1Id = table.Column<int>(type: "int", nullable: false),
                    Familia2Id = table.Column<int>(type: "int", nullable: false),
                    GradoDeCompatibilidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompatibilidadesFamilias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompatibilidadesFamilias_FamiliasOlfativas_Familia1Id",
                        column: x => x.Familia1Id,
                        principalTable: "FamiliasOlfativas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompatibilidadesFamilias_FamiliasOlfativas_Familia2Id",
                        column: x => x.Familia2Id,
                        principalTable: "FamiliasOlfativas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompatibilidadesFamilias_Familia1Id",
                table: "CompatibilidadesFamilias",
                column: "Familia1Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompatibilidadesFamilias_Familia2Id",
                table: "CompatibilidadesFamilias",
                column: "Familia2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompatibilidadesFamilias");
        }
    }
}
