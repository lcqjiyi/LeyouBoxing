using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeyouBoxing.Migrations
{
    public partial class Addboxnumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoxNumber",
                table: "OrderItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShipQty",
                table: "OrderItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoxNumber",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ShipQty",
                table: "OrderItems");
        }
    }
}
