using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WavoProjects.Api.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_PriorityView",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PriorityView", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriorityTeam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityTeam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Snapshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SnapshotTakenOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snapshots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriorityProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    PriorityId = table.Column<int>(type: "int", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    StartedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    PriorityViewId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriorityProject__PriorityView_PriorityViewId",
                        column: x => x.PriorityViewId,
                        principalTable: "_PriorityView",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriorityProject_PriorityTeam_TeamId",
                        column: x => x.TeamId,
                        principalTable: "PriorityTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    PriorityId = table.Column<int>(type: "int", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    StartedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Unassigned" },
                    { 2, "High" },
                    { 3, "Medium" },
                    { 4, "Low" },
                    { 5, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Color", "Name" },
                values: new object[,]
                {
                    { 1, "#ef0000", "General" },
                    { 2, "#00ff2d", "Business" },
                    { 3, "#4700d8", "CAD/Design" },
                    { 4, "#ffab00", "Programming" },
                    { 5, "#0dcaf0", "Media" },
                    { 6, "#039aff", "Build" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedOn", "Description", "Name", "PriorityId", "SortOrder", "StartedOn", "TeamId", "UpdatedOn" },
                values: new object[,]
                {
                    { 2, new DateTimeOffset(new DateTime(2021, 4, 9, 21, 17, 48, 540, DateTimeKind.Unspecified).AddTicks(8363), new TimeSpan(0, -4, 0, 0, 0)), "Sponsor Thank Yous", "Sponsor Thank Yous", 1, 2, null, 1, new DateTimeOffset(new DateTime(2021, 4, 9, 21, 17, 48, 540, DateTimeKind.Unspecified).AddTicks(8373), new TimeSpan(0, -4, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2021, 4, 9, 21, 17, 48, 540, DateTimeKind.Unspecified).AddTicks(8378), new TimeSpan(0, -4, 0, 0, 0)), "Chairman's Presentation", "Chairman's Presentation", 1, 3, null, 2, new DateTimeOffset(new DateTime(2021, 4, 9, 21, 17, 48, 540, DateTimeKind.Unspecified).AddTicks(8380), new TimeSpan(0, -4, 0, 0, 0)) },
                    { 1, new DateTimeOffset(new DateTime(2021, 4, 9, 21, 17, 48, 540, DateTimeKind.Unspecified).AddTicks(7441), new TimeSpan(0, -4, 0, 0, 0)), "T-Shirt Cannon Robot", "T-Shirt Cannon Robot", 1, 1, null, 3, new DateTimeOffset(new DateTime(2021, 4, 9, 21, 17, 48, 540, DateTimeKind.Unspecified).AddTicks(7977), new TimeSpan(0, -4, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2021, 4, 9, 21, 17, 48, 540, DateTimeKind.Unspecified).AddTicks(8384), new TimeSpan(0, -4, 0, 0, 0)), "Organize Electrical Toolbox", "Organize Electrical Toolbox", 1, 4, null, 4, new DateTimeOffset(new DateTime(2021, 4, 9, 21, 17, 48, 540, DateTimeKind.Unspecified).AddTicks(8387), new TimeSpan(0, -4, 0, 0, 0)) },
                    { 6, new DateTimeOffset(new DateTime(2021, 4, 9, 21, 17, 48, 540, DateTimeKind.Unspecified).AddTicks(8397), new TimeSpan(0, -4, 0, 0, 0)), "Develop Path Planning", "Develop Path Planning", 1, 6, null, 4, new DateTimeOffset(new DateTime(2021, 4, 9, 21, 17, 48, 540, DateTimeKind.Unspecified).AddTicks(8400), new TimeSpan(0, -4, 0, 0, 0)) },
                    { 5, new DateTimeOffset(new DateTime(2021, 4, 9, 21, 17, 48, 540, DateTimeKind.Unspecified).AddTicks(8391), new TimeSpan(0, -4, 0, 0, 0)), "Make New Router Vacuum Table", "Make New Router Vacuum Table", 1, 5, null, 6, new DateTimeOffset(new DateTime(2021, 4, 9, 21, 17, 48, 540, DateTimeKind.Unspecified).AddTicks(8393), new TimeSpan(0, -4, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriorityProject_PriorityViewId",
                table: "PriorityProject",
                column: "PriorityViewId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityProject_TeamId",
                table: "PriorityProject",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PriorityId",
                table: "Projects",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TeamId",
                table: "Projects",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriorityProject");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Snapshots");

            migrationBuilder.DropTable(
                name: "_PriorityView");

            migrationBuilder.DropTable(
                name: "PriorityTeam");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
