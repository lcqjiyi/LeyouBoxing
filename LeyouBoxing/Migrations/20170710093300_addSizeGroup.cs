using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LeyouBoxing.Migrations
{
    public partial class addSizeGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderSizeGroupId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderRow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Color = table.Column<string>(nullable: true),
                    LeyouNo = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true),
                    QidiNo = table.Column<int>(nullable: false),
                    Qty1 = table.Column<int>(nullable: false),
                    Qty2 = table.Column<int>(nullable: false),
                    Qty3 = table.Column<int>(nullable: false),
                    Qty4 = table.Column<int>(nullable: false),
                    Qty5 = table.Column<int>(nullable: false),
                    Qty6 = table.Column<int>(nullable: false),
                    Qty7 = table.Column<int>(nullable: false),
                    Qty8 = table.Column<int>(nullable: false),
                    Qty9 = table.Column<int>(nullable: false),
                    ShopName = table.Column<string>(nullable: true),
                    Sylte = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderRow_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderSizeGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nmae = table.Column<string>(nullable: true),
                    Size1 = table.Column<string>(nullable: true),
                    Size2 = table.Column<string>(nullable: true),
                    Size3 = table.Column<string>(nullable: true),
                    Size4 = table.Column<string>(nullable: true),
                    Size5 = table.Column<string>(nullable: true),
                    Size6 = table.Column<string>(nullable: true),
                    Size7 = table.Column<string>(nullable: true),
                    Size8 = table.Column<string>(nullable: true),
                    Size9 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSizeGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderSizeGroupId",
                table: "Orders",
                column: "OrderSizeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRow_OrderId",
                table: "OrderRow",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderSizeGroup_OrderSizeGroupId",
                table: "Orders",
                column: "OrderSizeGroupId",
                principalTable: "OrderSizeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderSizeGroup_OrderSizeGroupId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderRow");

            migrationBuilder.DropTable(
                name: "OrderSizeGroup");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderSizeGroupId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderSizeGroupId",
                table: "Orders");
        }
    }
}
