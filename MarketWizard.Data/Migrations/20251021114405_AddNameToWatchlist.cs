using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketWizard.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToWatchlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Watchlists",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Watchlists");
        }
    }
}
