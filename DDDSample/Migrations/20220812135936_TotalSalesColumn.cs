﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDDSample.Migrations
{
    public partial class TotalSalesColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalSales",
                table: "Customer",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSales",
                table: "Customer");
        }
    }
}
