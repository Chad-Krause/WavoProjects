using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace WavoProjects.Api.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Snapshot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    SnapshotTakenOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snapshot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Color = table.Column<string>(type: "char(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    PriorityId = table.Column<int>(type: "int", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    StartedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Priority",
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
                table: "Team",
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
                table: "Project",
                columns: new[] { "Id", "CreatedOn", "Description", "Name", "PriorityId", "SortOrder", "StartedOn", "TeamId", "UpdatedOn" },
                values: new object[,]
                {
                    { 2, new DateTime(2021, 4, 12, 17, 36, 40, 866, DateTimeKind.Local).AddTicks(928), "Sponsor Thank Yous", "Sponsor Thank Yous", 1, 2, null, 1, new DateTime(2021, 4, 12, 17, 36, 40, 866, DateTimeKind.Local).AddTicks(939) },
                    { 3, new DateTime(2021, 4, 12, 17, 36, 40, 866, DateTimeKind.Local).AddTicks(942), "Chairman's Presentation", "Chairman's Presentation", 1, 3, null, 2, new DateTime(2021, 4, 12, 17, 36, 40, 866, DateTimeKind.Local).AddTicks(945) },
                    { 1, new DateTime(2021, 4, 12, 17, 36, 40, 865, DateTimeKind.Local).AddTicks(9543), "T-Shirt Cannon Robot", "T-Shirt Cannon Robot", 1, 1, null, 3, new DateTime(2021, 4, 12, 17, 36, 40, 865, DateTimeKind.Local).AddTicks(9977) },
                    { 4, new DateTime(2021, 4, 12, 17, 36, 40, 866, DateTimeKind.Local).AddTicks(1008), "Organize Electrical Toolbox", "Organize Electrical Toolbox", 1, 4, null, 4, new DateTime(2021, 4, 12, 17, 36, 40, 866, DateTimeKind.Local).AddTicks(1011) },
                    { 6, new DateTime(2021, 4, 12, 17, 36, 40, 866, DateTimeKind.Local).AddTicks(1019), "Develop Path Planning", "Develop Path Planning", 1, 6, null, 4, new DateTime(2021, 4, 12, 17, 36, 40, 866, DateTimeKind.Local).AddTicks(1021) },
                    { 5, new DateTime(2021, 4, 12, 17, 36, 40, 866, DateTimeKind.Local).AddTicks(1014), "Make New Router Vacuum Table", "Make New Router Vacuum Table", 1, 5, null, 6, new DateTime(2021, 4, 12, 17, 36, 40, 866, DateTimeKind.Local).AddTicks(1016) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_PriorityId",
                table: "Project",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_TeamId",
                table: "Project",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Snapshot");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
