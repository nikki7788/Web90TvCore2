using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Web90TvCore2.Migrations
{
    public partial class Mig11MetaTag_propertis_added_to_newsTble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTag",
                table: "News",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "News");

            migrationBuilder.DropColumn(
                name: "MetaTag",
                table: "News");
        }
    }
}
