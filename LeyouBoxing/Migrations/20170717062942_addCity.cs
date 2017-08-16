using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LeyouBoxing.Migrations
{
    public partial class addCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "OrderBoxings",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderBoxingItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoxNumber = table.Column<int>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    GoodName = table.Column<string>(nullable: true),
                    Money = table.Column<decimal>(nullable: false),
                    OrderBoxingId = table.Column<int>(nullable: false),
                    POprice = table.Column<decimal>(nullable: false),
                    PoQty = table.Column<int>(nullable: false),
                    SKU = table.Column<string>(nullable: true),
                    ShipQty = table.Column<int>(nullable: false),
                    Size = table.Column<string>(nullable: true),
                    Style = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBoxingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderBoxingItem_OrderBoxings_OrderBoxingId",
                        column: x => x.OrderBoxingId,
                        principalTable: "OrderBoxings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderBoxingItem_OrderBoxingId",
                table: "OrderBoxingItem",
                column: "OrderBoxingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderBoxingItem");

            migrationBuilder.DropColumn(
                name: "City",
                table: "OrderBoxings");
        }
    }
}
