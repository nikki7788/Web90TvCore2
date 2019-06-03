using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Web90TvCore2.Migrations
{
    public partial class Mig9created_Advertise_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advertises",
                columns: table => new
                {
                    AdId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvLocation = table.Column<byte>(nullable: false),
                    Flag = table.Column<byte>(nullable: false),
                    FromDate = table.Column<string>(nullable: false),
                    GifPath = table.Column<string>(nullable: false),
                    Link = table.Column<string>(nullable: false),
                    ToDate = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertises", x => x.AdId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertises");
        }
    }
}
