using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendAlquimia.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotaYUpdatePiramideOlfativa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamiliasOlfativas_PirameOlfativa_SectorId",
                table: "FamiliasOlfativas");

            migrationBuilder.DropTable(
                name: "PirameOlfativa");

            migrationBuilder.DropIndex(
                name: "IX_FamiliasOlfativas_SectorId",
                table: "FamiliasOlfativas");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Intensidades");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "FamiliasOlfativas");

            migrationBuilder.AddColumn<int>(
                name: "IdIntensidad",
                table: "Notas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IntensidadId",
                table: "Notas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "Notas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grado",
                table: "Intensidades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PiramideOlfativa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duracion = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PiramideOlfativa", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notas_IntensidadId",
                table: "Notas",
                column: "IntensidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_SectorId",
                table: "Notas",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Intensidades_IntensidadId",
                table: "Notas",
                column: "IntensidadId",
                principalTable: "Intensidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_PiramideOlfativa_SectorId",
                table: "Notas",
                column: "SectorId",
                principalTable: "PiramideOlfativa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Intensidades_IntensidadId",
                table: "Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_PiramideOlfativa_SectorId",
                table: "Notas");

            migrationBuilder.DropTable(
                name: "PiramideOlfativa");

            migrationBuilder.DropIndex(
                name: "IX_Notas_IntensidadId",
                table: "Notas");

            migrationBuilder.DropIndex(
                name: "IX_Notas_SectorId",
                table: "Notas");

            migrationBuilder.DropColumn(
                name: "IdIntensidad",
                table: "Notas");

            migrationBuilder.DropColumn(
                name: "IntensidadId",
                table: "Notas");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "Notas");

            migrationBuilder.DropColumn(
                name: "Grado",
                table: "Intensidades");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Intensidades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "FamiliasOlfativas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PirameOlfativa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PirameOlfativa", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FamiliasOlfativas_SectorId",
                table: "FamiliasOlfativas",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamiliasOlfativas_PirameOlfativa_SectorId",
                table: "FamiliasOlfativas",
                column: "SectorId",
                principalTable: "PirameOlfativa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
