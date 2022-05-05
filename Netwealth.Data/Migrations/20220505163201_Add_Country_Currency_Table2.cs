using Microsoft.EntityFrameworkCore.Migrations;

namespace NetWealth.Data.Migrations
{
    public partial class Add_Country_Currency_Table2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryCurrency",
                table: "CountryCurrency");

            migrationBuilder.RenameTable(
                name: "CountryCurrency",
                newName: "CountryCurrencies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryCurrencies",
                table: "CountryCurrencies",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryCurrencies",
                table: "CountryCurrencies");

            migrationBuilder.RenameTable(
                name: "CountryCurrencies",
                newName: "CountryCurrency");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryCurrency",
                table: "CountryCurrency",
                column: "Id");
        }
    }
}
