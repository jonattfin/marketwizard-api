using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketWizard.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreTracingProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Watchlists",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Watchlists",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PortfolioAsset",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "PortfolioAsset",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AssetPriceHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AssetPriceHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Asset",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Asset",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PortfolioAsset");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PortfolioAsset");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AssetPriceHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AssetPriceHistory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Asset");
        }
    }
}
