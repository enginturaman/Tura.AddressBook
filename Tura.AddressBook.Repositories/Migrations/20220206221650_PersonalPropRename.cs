using Microsoft.EntityFrameworkCore.Migrations;

namespace Tura.AddressBook.Repositories.Migrations
{
    public partial class PersonalPropRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SurName",
                table: "Personals",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "Personals",
                newName: "Firm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Personals",
                newName: "SurName");

            migrationBuilder.RenameColumn(
                name: "Firm",
                table: "Personals",
                newName: "Company");
        }
    }
}
