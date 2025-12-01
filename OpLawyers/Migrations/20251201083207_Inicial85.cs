using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpLawyers.Migrations
{
    /// <inheritdoc />
    public partial class Inicial85 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "553969af-6a9f-467e-a1b8-73dbe329188d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ea5d06c3-5282-4b35-9424-2ce3ec848dea");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fbaaf1da-6970-43ac-ac8d-9a150a030165", "AQAAAAIAAYagAAAAEI40vwdH+nG3iJV5Zk6CjzOFWIkZ67+ouSeCAoHXUyyBJceDsuKWvFUcwbt1c2pQVg==", "f1907cc5-6899-4302-b2ba-a052974d6db0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "7cc299fb-0e13-494a-bde8-cf67dd393edd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "99c395cc-7a4f-4104-9f94-a593f9aab7b3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2721b52e-a839-4118-be5b-552622bed1d2", "AQAAAAIAAYagAAAAEFZy2e46GRLetl2A0SDPrfp6s51nYSXGj00QJb8ux7HGIe1kofHWfgz3UP5TBQHXVw==", "28f11be0-72b4-4dc0-b481-dc3a1d67d7aa" });
        }
    }
}
