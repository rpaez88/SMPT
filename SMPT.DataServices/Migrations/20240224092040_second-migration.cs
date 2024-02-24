using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SMPT.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Cycle_CycleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CycleId",
                table: "User");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("4f01473d-e10b-4112-b7f5-04f2a8aa9b7e"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("6956ff8a-062f-410d-9bb9-164ca945f559"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("836097ef-eb02-4909-84c0-6833094c7b25"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("5eb4b3f7-3bc9-4e30-9fd0-6953a4b50b9a"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("1592cf34-75ba-4698-a83c-68f8c5b9bcef"));

            migrationBuilder.DropColumn(
                name: "CycleId",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "User",
                type: "nvarchar(200)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StudentState",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "StudentState",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Role",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Role",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Role",
                type: "nvarchar(200)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EvidenceState",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "EvidenceState",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UrlArchivo",
                table: "Evidence",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observaciones",
                table: "Evidence",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Evidence",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cycle",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Career",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Career",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Area",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CycleStudent",
                columns: table => new
                {
                    CyclesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CycleStudent", x => new { x.CyclesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CycleStudent_Cycle_CyclesId",
                        column: x => x.CyclesId,
                        principalTable: "Cycle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CycleStudent_User_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EvidenceState",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("3f017e94-8b45-4b3d-8570-d27bae91f354"), new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9557), "", "Nueva", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9561) },
                    { new Guid("578b3c6a-4e05-4151-a89c-4fbf97e4c99d"), new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9587), "", "Rechazada", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9591) },
                    { new Guid("dafcb2bb-2c76-4636-849e-3e4e361f2cbd"), new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9573), "", "Aceptada", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9576) }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Alias", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0b474721-5dce-415b-b76c-21d7ee44c48a"), "area-manager", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(8849), "Rol con privilegios de lectura y escritura en el área correspondiente de la aplicación.", "Responsable de Área", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(8853) },
                    { new Guid("271b2d02-b0c0-4b2b-a963-a416961df8d3"), "student", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(8864), "Rol con escritura y lectura en sus datos de evidencias.", "Estudiante", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(8868) },
                    { new Guid("5b11bb5a-0e71-4b2d-b501-22ed7de32e3a"), "admin", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(8769), "Rol dedicado a la administración de la aplicación.", "Administrador", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(8806) },
                    { new Guid("853f234a-ac3d-41f2-ae0a-2167bb187a28"), "coordinator", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(8831), "Rol con privilegios de lectura en toda la aplicación.", "Coordinador", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(8835) }
                });

            migrationBuilder.InsertData(
                table: "StudentState",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("20db964b-739c-4973-b78c-18a62d712139"), new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9313), "", "Baja", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9316) },
                    { new Guid("26f48afe-d0f2-440a-96ef-111fb8390a53"), new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9284), "", "Egresado", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9287) },
                    { new Guid("cbf87dd6-776d-4b3e-aefb-92a265e68279"), new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9214), "", "Pasante", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9219) },
                    { new Guid("daa6f5bb-ad6f-419f-9111-4a62e920ef01"), new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9298), "", "Titulado", new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9302) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Code", "CreatedDate", "Discriminator", "Email", "IsActive", "Name", "Password", "RoleId", "UpdatedDate" },
                values: new object[] { new Guid("116ea908-c367-46fe-af50-be26778a89b1"), 0L, new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9800), "User", "cuvalles@udg.mx", true, "Administrador", "AQAAAAIAAYagAAAAEKWWe3U/k3WqjRYIdoJfV6stmwBxj4PVGKCDJV6ScS3t0OnFaBx/YNtY5/i7+WGXDw==", new Guid("5b11bb5a-0e71-4b2d-b501-22ed7de32e3a"), new DateTime(2024, 2, 24, 3, 20, 36, 860, DateTimeKind.Local).AddTicks(9804) });

            migrationBuilder.CreateIndex(
                name: "IX_CycleStudent_StudentsId",
                table: "CycleStudent",
                column: "StudentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CycleStudent");

            migrationBuilder.DeleteData(
                table: "EvidenceState",
                keyColumn: "Id",
                keyValue: new Guid("3f017e94-8b45-4b3d-8570-d27bae91f354"));

            migrationBuilder.DeleteData(
                table: "EvidenceState",
                keyColumn: "Id",
                keyValue: new Guid("578b3c6a-4e05-4151-a89c-4fbf97e4c99d"));

            migrationBuilder.DeleteData(
                table: "EvidenceState",
                keyColumn: "Id",
                keyValue: new Guid("dafcb2bb-2c76-4636-849e-3e4e361f2cbd"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("0b474721-5dce-415b-b76c-21d7ee44c48a"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("271b2d02-b0c0-4b2b-a963-a416961df8d3"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("853f234a-ac3d-41f2-ae0a-2167bb187a28"));

            migrationBuilder.DeleteData(
                table: "StudentState",
                keyColumn: "Id",
                keyValue: new Guid("20db964b-739c-4973-b78c-18a62d712139"));

            migrationBuilder.DeleteData(
                table: "StudentState",
                keyColumn: "Id",
                keyValue: new Guid("26f48afe-d0f2-440a-96ef-111fb8390a53"));

            migrationBuilder.DeleteData(
                table: "StudentState",
                keyColumn: "Id",
                keyValue: new Guid("cbf87dd6-776d-4b3e-aefb-92a265e68279"));

            migrationBuilder.DeleteData(
                table: "StudentState",
                keyColumn: "Id",
                keyValue: new Guid("daa6f5bb-ad6f-419f-9111-4a62e920ef01"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("116ea908-c367-46fe-af50-be26778a89b1"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("5b11bb5a-0e71-4b2d-b501-22ed7de32e3a"));

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Role");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "User",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 8);

            migrationBuilder.AddColumn<Guid>(
                name: "CycleId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StudentState",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "StudentState",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Role",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EvidenceState",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "EvidenceState",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UrlArchivo",
                table: "Evidence",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observaciones",
                table: "Evidence",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Evidence",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cycle",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Career",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Career",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Area",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1592cf34-75ba-4698-a83c-68f8c5b9bcef"), new DateTime(2024, 2, 10, 18, 33, 13, 919, DateTimeKind.Local).AddTicks(6771), "Rol dedicado a la administración de la aplicación.", "Administrador", new DateTime(2024, 2, 10, 18, 33, 13, 919, DateTimeKind.Local).AddTicks(6869) },
                    { new Guid("4f01473d-e10b-4112-b7f5-04f2a8aa9b7e"), new DateTime(2024, 2, 10, 18, 33, 13, 919, DateTimeKind.Local).AddTicks(6942), "Rol con escritura y lectura en sus datos de evidencias.", "Alumno", new DateTime(2024, 2, 10, 18, 33, 13, 919, DateTimeKind.Local).AddTicks(6944) },
                    { new Guid("6956ff8a-062f-410d-9bb9-164ca945f559"), new DateTime(2024, 2, 10, 18, 33, 13, 919, DateTimeKind.Local).AddTicks(6923), "Rol con privilegios de lectura en toda la aplicación.", "Coordinador", new DateTime(2024, 2, 10, 18, 33, 13, 919, DateTimeKind.Local).AddTicks(6925) },
                    { new Guid("836097ef-eb02-4909-84c0-6833094c7b25"), new DateTime(2024, 2, 10, 18, 33, 13, 919, DateTimeKind.Local).AddTicks(6933), "Rol con privilegios de lectura y escritura en el área correspondiente de la aplicación.", "ResponsableArea", new DateTime(2024, 2, 10, 18, 33, 13, 919, DateTimeKind.Local).AddTicks(6935) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Code", "CreatedDate", "Discriminator", "Email", "IsActive", "Name", "Password", "RoleId", "UpdatedDate" },
                values: new object[] { new Guid("5eb4b3f7-3bc9-4e30-9fd0-6953a4b50b9a"), 0L, new DateTime(2024, 2, 10, 18, 33, 13, 919, DateTimeKind.Local).AddTicks(7197), "User", "cuvalles@udg.mx", true, "Administrador", "AQAAAAIAAYagAAAAEKWWe3U/k3WqjRYIdoJfV6stmwBxj4PVGKCDJV6ScS3t0OnFaBx/YNtY5/i7+WGXDw==", new Guid("1592cf34-75ba-4698-a83c-68f8c5b9bcef"), new DateTime(2024, 2, 10, 18, 33, 13, 919, DateTimeKind.Local).AddTicks(7200) });

            migrationBuilder.CreateIndex(
                name: "IX_User_CycleId",
                table: "User",
                column: "CycleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Cycle_CycleId",
                table: "User",
                column: "CycleId",
                principalTable: "Cycle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
