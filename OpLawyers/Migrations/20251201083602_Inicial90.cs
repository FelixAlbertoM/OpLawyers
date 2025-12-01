using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpLawyers.Migrations
{
    /// <inheritdoc />
    public partial class Inicial90 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "876ef10d-ea39-41bb-b651-da69c2abf564", "AQAAAAIAAYagAAAAEE7YOvt6n0BwugR1uHk9kWo2CZpjy4MfPJEztCaZrf/aDiblMCM2dwbfHO7yEkhCpA==", "" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "user-001", 0, "6cd4d486-6bdf-4e4e-b551-900f677c30d2", "cliente@correo.com", true, false, null, "CLIENTE@CORREO.COM", "CLIENTE@CORREO.COM", "AQAAAAIAAYagAAAAEA72xweU8/4xME6+52sJS1WwZDNhBicrcihwCsr43UbM0KKchWLj6+RKGGTWckgzzA==", null, false, "", false, "cliente@correo.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "user-001" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "user-001" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-001");

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
    }
}
