using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.Models.Extensions;

namespace WavoProjects.Api.Migrations
{
    public partial class AddSystemVersioningToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddTemporalTableSupport("Project", "ProjectHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
