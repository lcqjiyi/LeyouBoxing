using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeyouBoxing.Migrations
{
    public partial class add_OrderBoxingItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBoxingItem_OrderBoxings_OrderBoxingId",
                table: "OrderBoxingItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderBoxingItem",
                table: "OrderBoxingItem");

            migrationBuilder.RenameTable(
                name: "OrderBoxingItem",
                newName: "OrderBoxingItems");

            migrationBuilder.RenameIndex(
                name: "IX_OrderBoxingItem_OrderBoxingId",
                table: "OrderBoxingItems",
                newName: "IX_OrderBoxingItems_OrderBoxingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderBoxingItems",
                table: "OrderBoxingItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBoxingItems_OrderBoxings_OrderBoxingId",
                table: "OrderBoxingItems",
                column: "OrderBoxingId",
                principalTable: "OrderBoxings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBoxingItems_OrderBoxings_OrderBoxingId",
                table: "OrderBoxingItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderBoxingItems",
                table: "OrderBoxingItems");

            migrationBuilder.RenameTable(
                name: "OrderBoxingItems",
                newName: "OrderBoxingItem");

            migrationBuilder.RenameIndex(
                name: "IX_OrderBoxingItems_OrderBoxingId",
                table: "OrderBoxingItem",
                newName: "IX_OrderBoxingItem_OrderBoxingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderBoxingItem",
                table: "OrderBoxingItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBoxingItem_OrderBoxings_OrderBoxingId",
                table: "OrderBoxingItem",
                column: "OrderBoxingId",
                principalTable: "OrderBoxings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
