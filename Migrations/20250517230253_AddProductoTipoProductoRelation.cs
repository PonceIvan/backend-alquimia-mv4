using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendAlquimia.Migrations
{
    /// <inheritdoc />
    public partial class AddProductoTipoProductoRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_TiposProducto_TipoProductoId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_TipoProductoId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "TipoProductoId",
                table: "Productos");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TiposProducto",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdTipoProducto",
                table: "Productos",
                column: "IdTipoProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_TiposProducto_IdTipoProducto",
                table: "Productos",
                column: "IdTipoProducto",
                principalTable: "TiposProducto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_TiposProducto_IdTipoProducto",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_IdTipoProducto",
                table: "Productos");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TiposProducto",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "TipoProductoId",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_TipoProductoId",
                table: "Productos",
                column: "TipoProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_TiposProducto_TipoProductoId",
                table: "Productos",
                column: "TipoProductoId",
                principalTable: "TiposProducto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
