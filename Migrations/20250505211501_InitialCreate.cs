using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendAlquimia.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Combinaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combinaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FamiliasOlfativas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamiliasOlfativas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Intensidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcentracionAlcohol = table.Column<double>(type: "float", nullable: false),
                    ConcentracionAgua = table.Column<double>(type: "float", nullable: false),
                    ConcentracionEsencia = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intensidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposProducto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposProducto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    FamiliaOlfativaId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_FamiliasOlfativas_FamiliaOlfativaId",
                        column: x => x.FamiliaOlfativaId,
                        principalTable: "FamiliasOlfativas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Formulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntensidadId = table.Column<int>(type: "int", nullable: false),
                    CombinacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formulas_Combinaciones_CombinacionId",
                        column: x => x.CombinacionId,
                        principalTable: "Combinaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Formulas_Intensidades_IntensidadId",
                        column: x => x.IntensidadId,
                        principalTable: "Intensidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Creadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Creadores_Usuarios_Id",
                        column: x => x.Id,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Cuil = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proveedores_Usuarios_Id",
                        column: x => x.Id,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombinacionNotaCorazon",
                columns: table => new
                {
                    Combinacion1Id = table.Column<int>(type: "int", nullable: false),
                    NotaCorazonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombinacionNotaCorazon", x => new { x.Combinacion1Id, x.NotaCorazonId });
                    table.ForeignKey(
                        name: "FK_CombinacionNotaCorazon_Combinaciones_Combinacion1Id",
                        column: x => x.Combinacion1Id,
                        principalTable: "Combinaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombinacionNotaCorazon_Notas_NotaCorazonId",
                        column: x => x.NotaCorazonId,
                        principalTable: "Notas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombinacionNotaFondo",
                columns: table => new
                {
                    Combinacion2Id = table.Column<int>(type: "int", nullable: false),
                    NotaFondoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombinacionNotaFondo", x => new { x.Combinacion2Id, x.NotaFondoId });
                    table.ForeignKey(
                        name: "FK_CombinacionNotaFondo_Combinaciones_Combinacion2Id",
                        column: x => x.Combinacion2Id,
                        principalTable: "Combinaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombinacionNotaFondo_Notas_NotaFondoId",
                        column: x => x.NotaFondoId,
                        principalTable: "Notas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombinacionNotaSalida",
                columns: table => new
                {
                    CombinacionId = table.Column<int>(type: "int", nullable: false),
                    NotaSalidaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombinacionNotaSalida", x => new { x.CombinacionId, x.NotaSalidaId });
                    table.ForeignKey(
                        name: "FK_CombinacionNotaSalida_Combinaciones_CombinacionId",
                        column: x => x.CombinacionId,
                        principalTable: "Combinaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombinacionNotaSalida_Notas_NotaSalidaId",
                        column: x => x.NotaSalidaId,
                        principalTable: "Notas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreacionesFinales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormulaId = table.Column<int>(type: "int", nullable: false),
                    CreadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreacionesFinales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreacionesFinales_Creadores_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "Creadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreacionesFinales_Formulas_FormulaId",
                        column: x => x.FormulaId,
                        principalTable: "Formulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoProductoId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    CreacionFinalId = table.Column<int>(type: "int", nullable: true),
                    CreadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_CreacionesFinales_CreacionFinalId",
                        column: x => x.CreacionFinalId,
                        principalTable: "CreacionesFinales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Productos_Creadores_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "Creadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Productos_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productos_TiposProducto_TipoProductoId",
                        column: x => x.TipoProductoId,
                        principalTable: "TiposProducto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CombinacionNotaCorazon_NotaCorazonId",
                table: "CombinacionNotaCorazon",
                column: "NotaCorazonId");

            migrationBuilder.CreateIndex(
                name: "IX_CombinacionNotaFondo_NotaFondoId",
                table: "CombinacionNotaFondo",
                column: "NotaFondoId");

            migrationBuilder.CreateIndex(
                name: "IX_CombinacionNotaSalida_NotaSalidaId",
                table: "CombinacionNotaSalida",
                column: "NotaSalidaId");

            migrationBuilder.CreateIndex(
                name: "IX_CreacionesFinales_CreadorId",
                table: "CreacionesFinales",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_CreacionesFinales_FormulaId",
                table: "CreacionesFinales",
                column: "FormulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Formulas_CombinacionId",
                table: "Formulas",
                column: "CombinacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Formulas_IntensidadId",
                table: "Formulas",
                column: "IntensidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_FamiliaOlfativaId",
                table: "Notas",
                column: "FamiliaOlfativaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CreacionFinalId",
                table: "Productos",
                column: "CreacionFinalId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CreadorId",
                table: "Productos",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProveedorId",
                table: "Productos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_TipoProductoId",
                table: "Productos",
                column: "TipoProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CombinacionNotaCorazon");

            migrationBuilder.DropTable(
                name: "CombinacionNotaFondo");

            migrationBuilder.DropTable(
                name: "CombinacionNotaSalida");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "CreacionesFinales");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "TiposProducto");

            migrationBuilder.DropTable(
                name: "FamiliasOlfativas");

            migrationBuilder.DropTable(
                name: "Creadores");

            migrationBuilder.DropTable(
                name: "Formulas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Combinaciones");

            migrationBuilder.DropTable(
                name: "Intensidades");
        }
    }
}
