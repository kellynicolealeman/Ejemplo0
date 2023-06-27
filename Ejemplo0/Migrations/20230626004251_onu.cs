using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ejemplo0.Migrations
{
    /// <inheritdoc />
    public partial class onu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grados",
                columns: table => new
                {
                    IdGrados = table.Column<int>(type: "int", nullable: false),
                    NombreGrado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Seccion = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grados", x => x.IdGrados);
                });

            migrationBuilder.CreateTable(
                name: "Studiantes",
                columns: table => new
                {
                    IdStudiante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreStudiante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGrado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studiantes", x => x.IdStudiante);
                    table.ForeignKey(
                        name: "FK_Studiantes_Grados_IdGrado",
                        column: x => x.IdGrado,
                        principalTable: "Grados",
                        principalColumn: "IdGrados",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Grados",
                columns: new[] { "IdGrados", "NombreGrado", "Seccion" },
                values: new object[,]
                {
                    { 1, "Primer", "A" },
                    { 2, "Segundo", "B" }
                });

            migrationBuilder.InsertData(
                table: "Studiantes",
                columns: new[] { "IdStudiante", "DateOfBirth", "IdGrado", "NombreStudiante" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Manolo Sanchez" },
                    { 2, new DateTime(2016, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Manue Pedrolo" },
                    { 3, new DateTime(2017, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Kequito Dulce" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Studiantes_IdGrado",
                table: "Studiantes",
                column: "IdGrado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Studiantes");

            migrationBuilder.DropTable(
                name: "Grados");
        }
    }
}
