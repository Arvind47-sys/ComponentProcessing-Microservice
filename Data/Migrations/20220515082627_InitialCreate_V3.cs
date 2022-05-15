using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComponentProcessing_Microservice.Data.Migrations
{
    public partial class InitialCreate_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ContactNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    DefectiveComponentType = table.Column<string>(type: "TEXT", nullable: false),
                    DefectiveComponentName = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessRequest_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcessingCharge = table.Column<double>(type: "REAL", nullable: false),
                    PackagingAndDeliveryCharge = table.Column<double>(type: "REAL", nullable: false),
                    DateOfDelivery = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcessRequestId = table.Column<int>(type: "INTEGER", nullable: true),
                    RequestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessResponse_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessResponse_ProcessRequest_ProcessRequestId",
                        column: x => x.ProcessRequestId,
                        principalTable: "ProcessRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessRequest_AppUserId",
                table: "ProcessRequest",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessResponse_AppUserId",
                table: "ProcessResponse",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessResponse_ProcessRequestId",
                table: "ProcessResponse",
                column: "ProcessRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessResponse");

            migrationBuilder.DropTable(
                name: "ProcessRequest");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
