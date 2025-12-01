using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpLawyers.Migrations
{
    /// <inheritdoc />
    public partial class Inicial91 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "admin-001" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "user-001" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-001");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-001");

            migrationBuilder.UpdateData(
                table: "Administradores",
                keyColumn: "AdministradorId",
                keyValue: 1,
                column: "UsuarioId",
                value: "1");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEFckMSwoaIoT+i8P1dhWC0texCWXJM9vYogzvLShiaAto2Zb6owofXi1mYEpVcRZXA==", null, false, "", false, "admin" },
                    { "2", 0, "", "user@example.com", true, false, null, "USER@EXAMPLE.COM", "USER", "AQAAAAIAAYagAAAAEK+QewBCKQMQoqUkUtmVEdI7MpR/XuYJ/0mXH0qKCf+PspVsJ7/naNTI3TqhdJ4b7Q==", null, false, "", false, "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.UpdateData(
                table: "Administradores",
                keyColumn: "AdministradorId",
                keyValue: 1,
                column: "UsuarioId",
                value: "admin-001");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "admin-001", 0, "876ef10d-ea39-41bb-b651-da69c2abf564", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEE7YOvt6n0BwugR1uHk9kWo2CZpjy4MfPJEztCaZrf/aDiblMCM2dwbfHO7yEkhCpA==", null, false, "", false, "admin@admin.com" },
                    { "user-001", 0, "6cd4d486-6bdf-4e4e-b551-900f677c30d2", "cliente@correo.com", true, false, null, "CLIENTE@CORREO.COM", "CLIENTE@CORREO.COM", "AQAAAAIAAYagAAAAEA72xweU8/4xME6+52sJS1WwZDNhBicrcihwCsr43UbM0KKchWLj6+RKGGTWckgzzA==", null, false, "", false, "cliente@correo.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "admin-001" },
                    { "2", "user-001" }
                });
        }
    }
}
