using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoLot.Dal.EfStructures.Migrations
{
    public partial class FkOrderInventoryToCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Inventory",
                schema: "dbo",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Inventory",
                schema: "dbo",
                table: "Orders",
                column: "CarId",
                principalSchema: "dbo",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Inventory",
                schema: "dbo",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Inventory",
                schema: "dbo",
                table: "Orders",
                column: "CarId",
                principalSchema: "dbo",
                principalTable: "Inventory",
                principalColumn: "Id");
        }
    }
}
