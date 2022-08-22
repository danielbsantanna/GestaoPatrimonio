using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "asp_net_roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "1", "1", "Admin", "Administrador" },
                    { "2", "2", "Usuario", "Usuario" }
                });

            migrationBuilder.InsertData(
                table: "asp_net_users",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "1", 0, "068a417d-73d6-4230-bb68-0f16cfae8ffa", "admin@gmail.com", false, false, null, null, null, null, "1234567890", false, "51e6f1cb-3c16-4900-9117-9a7f4e4902fa", false, "Admin" });

            migrationBuilder.InsertData(
                table: "asp_net_user_roles",
                columns: new[] { "user_id", "role_id" },
                values: new object[] { "1", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "asp_net_user_roles",
                keyColumns: new[] { "user_id", "role_id" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "asp_net_users",
                keyColumn: "id",
                keyValue: "1");
        }
    }
}
