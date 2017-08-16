using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeyouBoxing.Migrations
{
    public partial class addGenerateData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderSizeGroup_OrderSizeGroupId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderRows_Orders_OrderId",
                table: "OrderRows");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderSummary_Orders_OrderId",
                table: "OrderSummary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderSummary",
                table: "OrderSummary");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderSizeGroupId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderSizeGroupId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "OrderSummary",
                newName: "OrderSummarys");

            migrationBuilder.RenameIndex(
                name: "IX_OrderSummary_OrderId",
                table: "OrderSummarys",
                newName: "IX_OrderSummarys_OrderId");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderSizeGroup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderRows",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GenerateData",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderSummarys",
                table: "OrderSummarys",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSizeGroup_OrderId",
                table: "OrderSizeGroup",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRows_Orders_OrderId",
                table: "OrderRows",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSizeGroup_Orders_OrderId",
                table: "OrderSizeGroup",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSummarys_Orders_OrderId",
                table: "OrderSummarys",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderRows_Orders_OrderId",
                table: "OrderRows");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderSizeGroup_Orders_OrderId",
                table: "OrderSizeGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderSummarys_Orders_OrderId",
                table: "OrderSummarys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderSummarys",
                table: "OrderSummarys");

            migrationBuilder.DropIndex(
                name: "IX_OrderSizeGroup_OrderId",
                table: "OrderSizeGroup");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderSizeGroup");

            migrationBuilder.DropColumn(
                name: "GenerateData",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "OrderSummarys",
                newName: "OrderSummary");

            migrationBuilder.RenameIndex(
                name: "IX_OrderSummarys_OrderId",
                table: "OrderSummary",
                newName: "IX_OrderSummary_OrderId");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderRows",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "OrderSizeGroupId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderSummary",
                table: "OrderSummary",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderSizeGroupId",
                table: "Orders",
                column: "OrderSizeGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderSizeGroup_OrderSizeGroupId",
                table: "Orders",
                column: "OrderSizeGroupId",
                principalTable: "OrderSizeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRows_Orders_OrderId",
                table: "OrderRows",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSummary_Orders_OrderId",
                table: "OrderSummary",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
