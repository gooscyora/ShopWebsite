using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopWebsite.Migrations
{
    public partial class DeleteSlugFromCars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
