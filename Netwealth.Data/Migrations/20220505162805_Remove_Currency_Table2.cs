using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetWealth.Data.Migrations
{
    public partial class Remove_Currency_Table2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyCountries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyCountries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Reference = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyCountries", x => x.Id);
                });
        }
    }
}
