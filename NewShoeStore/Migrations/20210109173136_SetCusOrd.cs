using Microsoft.EntityFrameworkCore.Migrations;

namespace NewShoeStore.Migrations
{
    public partial class SetCusOrd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderShoe_Order_IdOrder",
                table: "OrderShoe");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoe_Category_CategoryId",
                table: "Shoe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Shoe",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdOrder",
                table: "Order",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "IdOrder",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IdOrder",
                table: "Customer",
                column: "IdOrder",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Order_IdOrder",
                table: "Customer",
                column: "IdOrder",
                principalTable: "Order",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderShoe_Order_IdOrder",
                table: "OrderShoe",
                column: "IdOrder",
                principalTable: "Order",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoe_Category_CategoryId",
                table: "Shoe",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Order_IdOrder",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderShoe_Order_IdOrder",
                table: "OrderShoe");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoe_Category_CategoryId",
                table: "Shoe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Customer_IdOrder",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "IdOrder",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "IdOrder",
                table: "Customer");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Shoe",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderShoe_Order_IdOrder",
                table: "OrderShoe",
                column: "IdOrder",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoe_Category_CategoryId",
                table: "Shoe",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
