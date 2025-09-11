using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketWizard.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTraceableProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Watchlists",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Watchlists",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Portfolios",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Portfolios",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PortfolioAsset",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PortfolioAsset",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AssetPriceHistory",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AssetPriceHistory",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Asset",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Asset",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PortfolioAsset");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PortfolioAsset");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AssetPriceHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AssetPriceHistory");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Asset");
        }
    }
}
