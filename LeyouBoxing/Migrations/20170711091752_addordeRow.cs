using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeyouBoxing.Migrations
{
    public partial class addordeRow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderRow_Orders_OrderId",
                table: "OrderRow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderRow",
                table: "OrderRow");

            migrationBuilder.RenameTable(
                name: "OrderRow",
                newName: "OrderRows");

            migrationBuilder.RenameIndex(
                name: "IX_OrderRow_OrderId",
                table: "OrderRows",
                newName: "IX_OrderRows_OrderId");

            migrationBuilder.RenameColumn(
                name: "Sylte",
                table: "OrderItems",
                newName: "Style");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderRows",
                table: "OrderRows",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRows_Orders_OrderId",
                table: "OrderRows",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderRows_Orders_OrderId",
                table: "OrderRows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderRows",
                table: "OrderRows");

            migrationBuilder.RenameTable(
                name: "OrderRows",
                newName: "OrderRow");

            migrationBuilder.RenameIndex(
                name: "IX_OrderRows_OrderId",
                table: "OrderRow",
                newName: "IX_OrderRow_OrderId");

            migrationBuilder.RenameColumn(
                name: "Style",
                table: "OrderItems",
                newName: "Sylte");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderRow",
                table: "OrderRow",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRow_Orders_OrderId",
                table: "OrderRow",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
