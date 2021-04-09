using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WavoProjects.Api.Migrations
{
    public partial class AddSortOrderToProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedOn", "Description", "Name", "PriorityId", "SortOrder", "StartedOn", "TeamId", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(6285), new TimeSpan(0, -4, 0, 0, 0)), "T-Shirt Cannon Robot", "T-Shirt Cannon Robot", 1, 1, null, 3, new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(6932), new TimeSpan(0, -4, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7405), new TimeSpan(0, -4, 0, 0, 0)), "Sponsor Thank Yous", "Sponsor Thank Yous", 1, 2, null, 1, new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7417), new TimeSpan(0, -4, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7422), new TimeSpan(0, -4, 0, 0, 0)), "Chairman's Presentation", "Chairman's Presentation", 1, 3, null, 2, new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7424), new TimeSpan(0, -4, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7428), new TimeSpan(0, -4, 0, 0, 0)), "Organize Electrical Toolbox", "Organize Electrical Toolbox", 1, 4, null, 4, new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7430), new TimeSpan(0, -4, 0, 0, 0)) },
                    { 6, new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7439), new TimeSpan(0, -4, 0, 0, 0)), "Develop Path Planning", "Develop Path Planning", 1, 6, null, 4, new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7441), new TimeSpan(0, -4, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Color", "Name" },
                values: new object[] { 6, "#039aff", "Build" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedOn", "Description", "Name", "PriorityId", "SortOrder", "StartedOn", "TeamId", "UpdatedOn" },
                values: new object[] { 5, new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7433), new TimeSpan(0, -4, 0, 0, 0)), "Make New Router Vacuum Table", "Make New Router Vacuum Table", 1, 5, null, 6, new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7436), new TimeSpan(0, -4, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
