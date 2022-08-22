using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AjusteFKNota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_nota_grupo_grupo_id",
                table: "nota");

            migrationBuilder.DeleteData(
                table: "asp_net_user_roles",
                keyColumns: new[] { "user_id", "role_id" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "asp_net_users",
                keyColumn: "id",
                keyValue: "1");

            migrationBuilder.AlterColumn<int>(
                name: "grupo_id",
                table: "nota",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_entrada",
                table: "nota",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_emissao",
                table: "nota",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "fk_nota_grupo_grupo_id",
                table: "nota",
                column: "grupo_id",
                principalTable: "grupo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_nota_grupo_grupo_id",
                table: "nota");

            migrationBuilder.AlterColumn<int>(
                name: "grupo_id",
                table: "nota",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_entrada",
                table: "nota",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_emissao",
                table: "nota",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "asp_net_users",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "1", 0, "068a417d-73d6-4230-bb68-0f16cfae8ffa", "admin@gmail.com", false, false, null, null, null, null, "1234567890", false, "51e6f1cb-3c16-4900-9117-9a7f4e4902fa", false, "Admin" });

            migrationBuilder.InsertData(
                table: "asp_net_user_roles",
                columns: new[] { "user_id", "role_id" },
                values: new object[] { "1", "1" });

            migrationBuilder.AddForeignKey(
                name: "fk_nota_grupo_grupo_id",
                table: "nota",
                column: "grupo_id",
                principalTable: "grupo",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
