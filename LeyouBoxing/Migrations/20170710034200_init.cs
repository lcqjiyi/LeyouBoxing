using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LeyouBoxing.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InputDate = table.Column<DateTime>(nullable: false),
                    JcNo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Style = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApprovalNo = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    GoodName = table.Column<string>(nullable: true),
                    LeyouNo = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true),
                    OrderNo = table.Column<string>(nullable: true),
                    POprice = table.Column<decimal>(nullable: false),
                    PoInfo = table.Column<string>(nullable: true),
                    PoQty = table.Column<int>(nullable: false),
                    QidiNo = table.Column<int>(nullable: false),
                    Quarterly = table.Column<string>(nullable: true),
                    SKU = table.Column<string>(nullable: true),
                    ShopName = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true),
                    Suppliercode = table.Column<string>(nullable: true),
                    Sylte = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
