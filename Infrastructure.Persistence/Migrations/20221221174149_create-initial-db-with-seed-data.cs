using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class createinitialdbwithseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowHistoryView = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Door",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Door", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Door_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAccessLevel",
                columns: table => new
                {
                    AccessLevelId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccessLevel", x => new { x.AccessLevelId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserAccessLevel_AccessLevel_AccessLevelId",
                        column: x => x.AccessLevelId,
                        principalTable: "AccessLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAccessLevel_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DoorId = table.Column<int>(type: "int", nullable: false),
                    AccessGranted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessHistory_Door_DoorId",
                        column: x => x.DoorId,
                        principalTable: "Door",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoorAccessLevel",
                columns: table => new
                {
                    DoorId = table.Column<int>(type: "int", nullable: false),
                    AccessLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorAccessLevel", x => new { x.AccessLevelId, x.DoorId });
                    table.ForeignKey(
                        name: "FK_DoorAccessLevel_AccessLevel_AccessLevelId",
                        column: x => x.AccessLevelId,
                        principalTable: "AccessLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoorAccessLevel_Door_DoorId",
                        column: x => x.DoorId,
                        principalTable: "Door",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccessLevel",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "General" },
                    { 2, "Special" }
                });

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Office One" },
                    { 2, "Office Two" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AllowHistoryView", "Name" },
                values: new object[,]
                {
                    { 1, true, "Rameez" },
                    { 2, true, "Darjan" },
                    { 3, false, "Lucas" }
                });

            migrationBuilder.InsertData(
                table: "Door",
                columns: new[] { "Id", "Name", "OfficeId" },
                values: new object[,]
                {
                    { 1, "Entrance", 1 },
                    { 2, "Storage", 1 }
                });

            migrationBuilder.InsertData(
                table: "UserAccessLevel",
                columns: new[] { "AccessLevelId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "DoorAccessLevel",
                columns: new[] { "AccessLevelId", "DoorId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "DoorAccessLevel",
                columns: new[] { "AccessLevelId", "DoorId" },
                values: new object[] { 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistory_DoorId",
                table: "AccessHistory",
                column: "DoorId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistory_UserId",
                table: "AccessHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Door_OfficeId",
                table: "Door",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_DoorAccessLevel_DoorId",
                table: "DoorAccessLevel",
                column: "DoorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessLevel_UserId",
                table: "UserAccessLevel",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessHistory");

            migrationBuilder.DropTable(
                name: "DoorAccessLevel");

            migrationBuilder.DropTable(
                name: "UserAccessLevel");

            migrationBuilder.DropTable(
                name: "Door");

            migrationBuilder.DropTable(
                name: "AccessLevel");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Office");
        }
    }
}
