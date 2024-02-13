using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SMPT.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cycle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    CycleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Cycle_CycleId",
                        column: x => x.CycleId,
                        principalTable: "Cycle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_StudentState_StateId",
                        column: x => x.StateId,
                        principalTable: "StudentState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordinatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Evidence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlArchivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        name: "FK_Evidence_User_StudentId",
                        column: x => x.StudentId,
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
                name: "CareerStudent",
                columns: table => new
                {
                    CareersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerStudent", x => new { x.CareersId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CareerStudent_Career_CareersId",
                        column: x => x.CareersId,
                        principalTable: "Career",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareerStudent_User_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_CareerStudent_StudentsId",
                table: "CareerStudent",
                column: "StudentsId");

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
                name: "IX_User_CycleId",
                table: "User",
                column: "CycleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_StateId",
                table: "User",
                column: "StateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CareerCycle");

            migrationBuilder.DropTable(
                name: "CareerStudent");

            migrationBuilder.DropTable(
                name: "Evidence");

            migrationBuilder.DropTable(
                name: "Career");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "EvidenceState");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Cycle");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "StudentState");
        }
    }
}
