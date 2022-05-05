using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetWealth.Data.Migrations
{
    public partial class Add_Country_Currency_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryCurrency",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCurrency", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryCurrency");
        }
    }
}
