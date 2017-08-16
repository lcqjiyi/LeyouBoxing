using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeyouBoxing.Migrations
{
    public partial class updateOrderBoxing2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderSummarys_Orders_OrderId",
                table: "OrderSummarys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderSummarys",
                table: "OrderSummarys");

            migrationBuilder.RenameTable(
                name: "OrderSummarys",
                newName: "OrderBoxings");

            migrationBuilder.RenameIndex(
                name: "IX_OrderSummarys_OrderId",
                table: "OrderBoxings",
                newName: "IX_OrderBoxings_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderBoxings",
                table: "OrderBoxings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBoxings_Orders_OrderId",
                table: "OrderBoxings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBoxings_Orders_OrderId",
                table: "OrderBoxings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderBoxings",
                table: "OrderBoxings");

            migrationBuilder.RenameTable(
                name: "OrderBoxings",
                newName: "OrderSummarys");

            migrationBuilder.RenameIndex(
                name: "IX_OrderBoxings_OrderId",
                table: "OrderSummarys",
                newName: "IX_OrderSummarys_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderSummarys",
                table: "OrderSummarys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSummarys_Orders_OrderId",
                table: "OrderSummarys",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
