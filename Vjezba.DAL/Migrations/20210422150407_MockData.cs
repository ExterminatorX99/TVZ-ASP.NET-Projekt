using Microsoft.EntityFrameworkCore.Migrations;

namespace Vjezba.DAL.Migrations
{
    public partial class MockData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Zagreb" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "New York" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name" },
                values: new object[] { 3, "London" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ID", "Address", "CityID", "Email", "FirstName", "Gender", "LastName", "PhoneNumber" },
                values: new object[] { 1, "Negdje 12", 1, "kkos1@tvz.hr", "Kristijan", "M", "Kos", "091-0911-091" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
