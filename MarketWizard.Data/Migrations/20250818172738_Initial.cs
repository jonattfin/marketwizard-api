using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MarketWizard.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioAsset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    NumberOfShares = table.Column<double>(type: "double precision", nullable: false),
                    PricePerShare = table.Column<double>(type: "double precision", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioAsset_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioAsset_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PortfolioNews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Time = table.Column<string>(type: "text", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    Headline = table.Column<string>(type: "text", nullable: false),
                    Provider = table.Column<string>(type: "text", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioNews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioNews_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioNews_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "Description", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("313089d6-cebc-44db-8d6d-10788c512656"), "Description 2", "Asset 2", "DECK" },
                    { new Guid("d82dcdad-f6cc-4b94-a8ec-e26abe09e590"), "Description 1", "Asset 1", "ASML" }
                });

            migrationBuilder.InsertData(
                table: "Portfolios",
                columns: new[] { "Id", "Description", "ImageUrl", "Name" },
                values: new object[] { new Guid("54f53f92-1043-4ca4-b32a-4db5a2d1013f"), "Description 1", "image", "Portfolio 1" });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioAsset_AssetId",
                table: "PortfolioAsset",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioAsset_PortfolioId",
                table: "PortfolioAsset",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioNews_AssetId",
                table: "PortfolioNews",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioNews_PortfolioId",
                table: "PortfolioNews",
                column: "PortfolioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioAsset");

            migrationBuilder.DropTable(
                name: "PortfolioNews");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Portfolios");
        }
    }
}
