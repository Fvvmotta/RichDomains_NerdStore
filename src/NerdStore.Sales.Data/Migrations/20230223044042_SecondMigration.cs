using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NerdStore.Sales.Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItems_Orders_OrderId",
                table: "PedidoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoItems",
                table: "PedidoItems");

            migrationBuilder.RenameTable(
                name: "PedidoItems",
                newName: "OrderItems");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoItems_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "PedidoItems");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "PedidoItems",
                newName: "IX_PedidoItems_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoItems",
                table: "PedidoItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItems_Orders_OrderId",
                table: "PedidoItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
