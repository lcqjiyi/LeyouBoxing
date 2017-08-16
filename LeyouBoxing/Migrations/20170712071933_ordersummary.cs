using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LeyouBoxing.Migrations
{
    public partial class ordersummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderSummary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hashcode = table.Column<int>(nullable: false),
                    LeyouNo = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    QidiNo = table.Column<int>(nullable: false),
                    ShopName = table.Column<string>(nullable: true),
                    TotalQty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSummary_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderSummary_OrderId",
                table: "OrderSummary",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderSummary");
        }
    }
}
