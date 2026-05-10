using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZadanieDomowe7.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentManufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FoundationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentManufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PCs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Warranty = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ComponentManufacturersId = table.Column<int>(type: "int", nullable: false),
                    ComponentTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Components_ComponentManufacturers_ComponentManufacturersId",
                        column: x => x.ComponentManufacturersId,
                        principalTable: "ComponentManufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Components_ComponentTypes_ComponentTypeId",
                        column: x => x.ComponentTypeId,
                        principalTable: "ComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PCComponents",
                columns: table => new
                {
                    PCId = table.Column<int>(type: "int", nullable: false),
                    ComponentCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCComponents", x => new { x.PCId, x.ComponentCode });
                    table.ForeignKey(
                        name: "FK_PCComponents_Components_ComponentCode",
                        column: x => x.ComponentCode,
                        principalTable: "Components",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PCComponents_PCs_PCId",
                        column: x => x.PCId,
                        principalTable: "PCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ComponentManufacturers",
                columns: new[] { "Id", "Abbreviation", "FoundationDate", "FullName" },
                values: new object[,]
                {
                    { 1, "Intel", new DateTime(1968, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intel Corporation" },
                    { 2, "AMD", new DateTime(1969, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advanced Micro Devices" },
                    { 3, "Kingston", new DateTime(1987, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kingston Technology" },
                    { 4, "Corsair", new DateTime(1994, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Corsair Components" },
                    { 5, "Samsung", new DateTime(1969, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung Electronics" }
                });

            migrationBuilder.InsertData(
                table: "ComponentTypes",
                columns: new[] { "Id", "Abbreviation", "Name" },
                values: new object[,]
                {
                    { 1, "CPU", "Processor" },
                    { 2, "RAM", "Memory" },
                    { 3, "GPU", "Graphics Card" },
                    { 4, "SSD", "Solid State Drive" },
                    { 5, "PSU", "Power Supply Unit" }
                });

            migrationBuilder.InsertData(
                table: "PCs",
                columns: new[] { "Id", "CreatedAt", "Name", "Stock", "Warranty", "Weight" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), "Gaming Beast X", 5, 36, 12.5m },
                    { 2, new DateTime(2026, 4, 15, 13, 30, 0, 0, DateTimeKind.Unspecified), "Office Mini Pro", 12, 24, 4.2m },
                    { 3, new DateTime(2026, 5, 1, 10, 15, 0, 0, DateTimeKind.Unspecified), "Workstation Ultra", 3, 60, 15.8m },
                    { 4, new DateTime(2026, 4, 20, 14, 45, 0, 0, DateTimeKind.Unspecified), "Server Standard", 2, 60, 20.0m }
                });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Code", "ComponentManufacturersId", "ComponentTypeId", "Description", "Name" },
                values: new object[,]
                {
                    { "CPU001", 1, 1, "High-end desktop processor", "Intel Core i9-13900K" },
                    { "CPU002", 2, 1, "High-performance desktop CPU", "AMD Ryzen 9 7950X" },
                    { "GPU001", 1, 3, "Flagship graphics card", "NVIDIA RTX 4090" },
                    { "GPU002", 2, 3, "High-end gaming GPU", "AMD Radeon RX 7900 XTX" },
                    { "PSU001", 4, 5, "Gold modular PSU", "Corsair RM1000e 1000W" },
                    { "RAM001", 3, 2, "Gaming memory module", "Kingston Fury 32GB DDR5" },
                    { "RAM002", 4, 2, "Professional memory", "Corsair Vengeance 16GB DDR5" },
                    { "SSD001", 5, 4, "NVMe SSD for gaming", "Samsung 990 Pro 2TB" },
                    { "SSD002", 4, 4, "Fast NVMe storage", "Corsair MP600 1TB" }
                });

            migrationBuilder.InsertData(
                table: "PCComponents",
                columns: new[] { "ComponentCode", "PCId", "Amount" },
                values: new object[,]
                {
                    { "CPU001", 1, 1 },
                    { "GPU001", 1, 1 },
                    { "PSU001", 1, 1 },
                    { "RAM001", 1, 2 },
                    { "SSD001", 1, 1 },
                    { "CPU002", 2, 1 },
                    { "RAM002", 2, 1 },
                    { "SSD002", 2, 1 },
                    { "CPU001", 3, 1 },
                    { "GPU002", 3, 1 },
                    { "RAM001", 3, 4 },
                    { "SSD001", 3, 2 },
                    { "CPU002", 4, 2 },
                    { "RAM002", 4, 4 },
                    { "SSD002", 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentManufacturersId",
                table: "Components",
                column: "ComponentManufacturersId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentTypeId",
                table: "Components",
                column: "ComponentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PCComponents_ComponentCode",
                table: "PCComponents",
                column: "ComponentCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PCComponents");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "PCs");

            migrationBuilder.DropTable(
                name: "ComponentManufacturers");

            migrationBuilder.DropTable(
                name: "ComponentTypes");
        }
    }
}
