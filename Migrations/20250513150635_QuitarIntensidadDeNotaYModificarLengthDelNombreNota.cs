using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendAlquimia.Migrations
{
    /// <inheritdoc />
    public partial class QuitarIntensidadDeNotaYModificarLengthDelNombreNota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Intensidades_IntensidadId",
                table: "Notas");

            migrationBuilder.DropIndex(
                name: "IX_Notas_IntensidadId",
                table: "Notas");

            migrationBuilder.DropColumn(
                name: "IdIntensidad",
                table: "Notas");

            migrationBuilder.DropColumn(
                name: "IntensidadId",
                table: "Notas");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Notas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Notas",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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

            migrationBuilder.CreateIndex(
                name: "IX_Notas_IntensidadId",
                table: "Notas",
                column: "IntensidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Intensidades_IntensidadId",
                table: "Notas",
                column: "IntensidadId",
                principalTable: "Intensidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
