using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SMPT.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cycle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cycle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvidenceState",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvidenceState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(200)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentState",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(200)", maxLength: 20, nullable: true),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Area_User_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Career",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CoordinatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Career", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Career_User_CoordinatorId",
                        column: x => x.CoordinatorId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CareerCycle",
                columns: table => new
                {
                    CareersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CyclesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerCycle", x => new { x.CareersId, x.CyclesId });
                    table.ForeignKey(
                        name: "FK_CareerCycle_Career_CareersId",
                        column: x => x.CareersId,
                        principalTable: "Career",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareerCycle_Cycle_CyclesId",
                        column: x => x.CyclesId,
                        principalTable: "Cycle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CycleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CareerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Career_CareerId",
                        column: x => x.CareerId,
                        principalTable: "Career",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Student_Cycle_CycleId",
                        column: x => x.CycleId,
                        principalTable: "Cycle",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Student_StudentState_StateId",
                        column: x => x.StateId,
                        principalTable: "StudentState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Student_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Evidence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    UrlArchivo = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evidence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evidence_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evidence_EvidenceState_StateId",
                        column: x => x.StateId,
                        principalTable: "EvidenceState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evidence_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "EvidenceState",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("3506207e-220f-4bae-a912-f06ba8c6d9f6"), new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8721), "", "Aceptada", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8723) },
                    { new Guid("9dced51b-4708-48ed-9131-94b0f76f0ec5"), new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8712), "", "Nueva", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8714) },
                    { new Guid("f7565740-e47a-4027-9d09-60cf062ee605"), new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8730), "", "Rechazada", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8732) }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Alias", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("01f7ce23-aade-489c-8278-6a4ee5b3c65a"), "coordinator", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8121), "Rol con privilegios de lectura en toda la aplicación.", "Coordinador", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8123) },
                    { new Guid("405fff9b-45c5-4045-afd4-97323675a4ed"), "student", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8139), "Rol con escritura y lectura en sus datos de evidencias.", "Estudiante", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8142) },
                    { new Guid("60d49817-4745-402e-902a-d7e279026946"), "admin", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8069), "Rol dedicado a la administración de la aplicación.", "Administrador", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8098) },
                    { new Guid("d507ef5a-0b98-4c6f-8960-78fd4b03d2bf"), "area-manager", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8130), "Rol con privilegios de lectura y escritura en el área correspondiente de la aplicación.", "Responsable de Área", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8132) }
                });

            migrationBuilder.InsertData(
                table: "StudentState",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("3552e990-906d-4b57-b9a4-7027d82d4c38"), new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8515), "", "Egresado", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8517) },
                    { new Guid("3a02215c-62cb-4ab8-bcf6-beb5de690330"), new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8525), "", "Titulado", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8526) },
                    { new Guid("506b4867-ffe2-483d-9b77-70e64136bc7f"), new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8503), "", "Pasante", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8506) },
                    { new Guid("f0d4444a-87b5-46cd-8cb5-ae331e3e028b"), new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8533), "", "Baja", new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8535) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Code", "CreatedDate", "Email", "IsActive", "Name", "Password", "RoleId", "UpdatedDate" },
                values: new object[] { new Guid("937ae2ed-fbd7-4625-bdcb-b754f108cd2b"), 0L, new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8864), "cuvalles@udg.mx", true, "Administrador", "AQAAAAIAAYagAAAAEKWWe3U/k3WqjRYIdoJfV6stmwBxj4PVGKCDJV6ScS3t0OnFaBx/YNtY5/i7+WGXDw==", new Guid("60d49817-4745-402e-902a-d7e279026946"), new DateTime(2024, 2, 25, 16, 23, 53, 585, DateTimeKind.Local).AddTicks(8867) });

            migrationBuilder.CreateIndex(
                name: "IX_Area_ManagerId",
                table: "Area",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Career_CoordinatorId",
                table: "Career",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CareerCycle_CyclesId",
                table: "CareerCycle",
                column: "CyclesId");

            migrationBuilder.CreateIndex(
                name: "IX_Evidence_AreaId",
                table: "Evidence",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Evidence_StateId",
                table: "Evidence",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Evidence_StudentId",
                table: "Evidence",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_CareerId",
                table: "Student",
                column: "CareerId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_CycleId",
                table: "Student",
                column: "CycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_StateId",
                table: "Student",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_UserId",
                table: "Student",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CareerCycle");

            migrationBuilder.DropTable(
                name: "Evidence");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "EvidenceState");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Career");

            migrationBuilder.DropTable(
                name: "Cycle");

            migrationBuilder.DropTable(
                name: "StudentState");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
