using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Infraestructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Usuario_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotoPerfil = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Usuario_Id);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Producto_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Lenguaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usuario_Id = table.Column<int>(type: "int", nullable: false),
                    NumLikes = table.Column<int>(type: "int", nullable: false),
                    NumComentarios = table.Column<int>(type: "int", nullable: false),
                    PromedioDatos = table.Column<int>(type: "int", nullable: false),
                    Promedio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Producto_Id);
                    table.ForeignKey(
                        name: "Fk_Producto_Id_Usuario",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calificacion",
                columns: table => new
                {
                    Calificacion_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Producto_Id = table.Column<int>(type: "int", nullable: false),
                    Usuario_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificacion", x => x.Calificacion_Id);
                    table.ForeignKey(
                        name: "Fk_Calificacion_Id_Producto",
                        column: x => x.Producto_Id,
                        principalTable: "Producto",
                        principalColumn: "Producto_Id");
                    table.ForeignKey(
                        name: "Fk_Calificacion_Id_Usuario",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    Comentario_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usuario_Id = table.Column<int>(type: "int", nullable: false),
                    Producto_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.Comentario_Id);
                    table.ForeignKey(
                        name: "Fk_Comentario_Id_Producto",
                        column: x => x.Producto_Id,
                        principalTable: "Producto",
                        principalColumn: "Producto_Id");
                    table.ForeignKey(
                        name: "Fk_Comentario_Id_Usuario",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    Compra_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Producto_Id = table.Column<int>(type: "int", nullable: false),
                    Usuario_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.Compra_Id);
                    table.ForeignKey(
                        name: "Fk_Compra_Id_Producto",
                        column: x => x.Producto_Id,
                        principalTable: "Producto",
                        principalColumn: "Producto_Id");
                    table.ForeignKey(
                        name: "Fk_Compra_Id_Usuario",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Documento_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Producto_Id = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Documento_Id);
                    table.ForeignKey(
                        name: "Fk_Documentos_Id_Producto",
                        column: x => x.Producto_Id,
                        principalTable: "Producto",
                        principalColumn: "Producto_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    Like_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Producto_Id = table.Column<int>(type: "int", nullable: false),
                    Usuario_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.Like_Id);
                    table.ForeignKey(
                        name: "Fk_Like_Id_Producto",
                        column: x => x.Producto_Id,
                        principalTable: "Producto",
                        principalColumn: "Producto_Id");
                    table.ForeignKey(
                        name: "Fk_Like_Id_Usuario",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_Producto_Id",
                table: "Calificacion",
                column: "Producto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_Usuario_Id",
                table: "Calificacion",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_Producto_Id",
                table: "Comentario",
                column: "Producto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_Usuario_Id",
                table: "Comentario",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_Producto_Id",
                table: "Compra",
                column: "Producto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_Usuario_Id",
                table: "Compra",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_Producto_Id",
                table: "Documento",
                column: "Producto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Like_Producto_Id",
                table: "Like",
                column: "Producto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Like_Usuario_Id",
                table: "Like",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Usuario_Id",
                table: "Producto",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificacion");

            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
