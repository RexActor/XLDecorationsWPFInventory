using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XLDecorationsWPFInventory.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderItemsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "OrderItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaterialQuantity",
                table: "OrderItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MaterialId",
                table: "OrderItems",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Materials_MaterialId",
                table: "OrderItems",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Materials_MaterialId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_MaterialId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "MaterialQuantity",
                table: "OrderItems");
        }
    }
}
