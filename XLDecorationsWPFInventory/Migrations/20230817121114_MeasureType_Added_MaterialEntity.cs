using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XLDecorationsWPFInventory.Migrations;

    /// <inheritdoc />
    public partial class MeasureType_Added_MaterialEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaterialMeasureTypeId",
                table: "Materials",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialMeasureTypeId",
                table: "Materials",
                column: "MaterialMeasureTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MeasureTypes_MaterialMeasureTypeId",
                table: "Materials",
                column: "MaterialMeasureTypeId",
                principalTable: "MeasureTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MeasureTypes_MaterialMeasureTypeId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_MaterialMeasureTypeId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "MaterialMeasureTypeId",
                table: "Materials");
        }
    }
