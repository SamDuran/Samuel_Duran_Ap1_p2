﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Samuel_Duran_Ap2_p2_.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 35, nullable: false),
                    Existencia = table.Column<decimal>(type: "TEXT", nullable: false),
                    Costo = table.Column<decimal>(type: "TEXT", nullable: false),
                    ValorInventario = table.Column<decimal>(type: "TEXT", nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false),
                    Ganancia = table.Column<decimal>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "ProductoDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    Cantidad = table.Column<decimal>(type: "TEXT", nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductoDetalles_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductosProducidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmpacadosId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<decimal>(type: "TEXT", nullable: false),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosProducidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductosProducidos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empacados",
                columns: table => new
                {
                    EmpacadosId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Concepto = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empacados", x => x.EmpacadosId);
                    table.ForeignKey(
                        name: "FK_Empacados_ProductosProducidos_EmpacadosId",
                        column: x => x.EmpacadosId,
                        principalTable: "ProductosProducidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductosUtilizados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cantidad = table.Column<decimal>(type: "TEXT", nullable: false),
                    ProductosId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    EmpacadosId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosUtilizados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductosUtilizados_Empacados_EmpacadosId",
                        column: x => x.EmpacadosId,
                        principalTable: "Empacados",
                        principalColumn: "EmpacadosId");
                    table.ForeignKey(
                        name: "FK_ProductosUtilizados_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoDetalles_ProductoId",
                table: "ProductoDetalles",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosProducidos_ProductoId",
                table: "ProductosProducidos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosUtilizados_EmpacadosId",
                table: "ProductosUtilizados",
                column: "EmpacadosId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosUtilizados_ProductoId",
                table: "ProductosUtilizados",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoDetalles");

            migrationBuilder.DropTable(
                name: "ProductosUtilizados");

            migrationBuilder.DropTable(
                name: "Empacados");

            migrationBuilder.DropTable(
                name: "ProductosProducidos");

            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
