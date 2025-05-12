using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendAlquimia.Migrations
{
    /// <inheritdoc />
    public partial class AgregarEntidadPiramideOlfativa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreadorId",
                table: "Notas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreadorId",
                table: "FamiliasOlfativas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "FamiliasOlfativas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Opinion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opinion_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

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
                name: "IX_Notas_CreadorId",
                table: "Notas",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_FamiliasOlfativas_CreadorId",
                table: "FamiliasOlfativas",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_FamiliasOlfativas_SectorId",
                table: "FamiliasOlfativas",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Opinion_UsuarioId",
                table: "Opinion",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamiliasOlfativas_Creadores_CreadorId",
                table: "FamiliasOlfativas",
                column: "CreadorId",
                principalTable: "Creadores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FamiliasOlfativas_PirameOlfativa_SectorId",
                table: "FamiliasOlfativas",
                column: "SectorId",
                principalTable: "PirameOlfativa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Creadores_CreadorId",
                table: "Notas",
                column: "CreadorId",
                principalTable: "Creadores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamiliasOlfativas_Creadores_CreadorId",
                table: "FamiliasOlfativas");

            migrationBuilder.DropForeignKey(
                name: "FK_FamiliasOlfativas_PirameOlfativa_SectorId",
                table: "FamiliasOlfativas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Creadores_CreadorId",
                table: "Notas");

            migrationBuilder.DropTable(
                name: "Opinion");

            migrationBuilder.DropTable(
                name: "PirameOlfativa");

            migrationBuilder.DropIndex(
                name: "IX_Notas_CreadorId",
                table: "Notas");

            migrationBuilder.DropIndex(
                name: "IX_FamiliasOlfativas_CreadorId",
                table: "FamiliasOlfativas");

            migrationBuilder.DropIndex(
                name: "IX_FamiliasOlfativas_SectorId",
                table: "FamiliasOlfativas");

            migrationBuilder.DropColumn(
                name: "CreadorId",
                table: "Notas");

            migrationBuilder.DropColumn(
                name: "CreadorId",
                table: "FamiliasOlfativas");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "FamiliasOlfativas");
        }
    }
}
