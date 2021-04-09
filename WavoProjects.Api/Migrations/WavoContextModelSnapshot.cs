﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WavoProjects.Api.Models;

namespace WavoProjects.Api.Migrations
{
    [DbContext(typeof(WavoContext))]
    partial class WavoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WavoProjects.Api.Models.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Priorities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Unassigned"
                        },
                        new
                        {
                            Id = 2,
                            Name = "High"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Medium"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Low"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Completed"
                        });
                });

            modelBuilder.Entity("WavoProjects.Api.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int?>("SortOrder")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("StartedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("PriorityId");

                    b.HasIndex("TeamId");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(6285), new TimeSpan(0, -4, 0, 0, 0)),
                            Description = "T-Shirt Cannon Robot",
                            Name = "T-Shirt Cannon Robot",
                            PriorityId = 1,
                            SortOrder = 1,
                            TeamId = 3,
                            UpdatedOn = new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(6932), new TimeSpan(0, -4, 0, 0, 0))
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7405), new TimeSpan(0, -4, 0, 0, 0)),
                            Description = "Sponsor Thank Yous",
                            Name = "Sponsor Thank Yous",
                            PriorityId = 1,
                            SortOrder = 2,
                            TeamId = 1,
                            UpdatedOn = new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7417), new TimeSpan(0, -4, 0, 0, 0))
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7422), new TimeSpan(0, -4, 0, 0, 0)),
                            Description = "Chairman's Presentation",
                            Name = "Chairman's Presentation",
                            PriorityId = 1,
                            SortOrder = 3,
                            TeamId = 2,
                            UpdatedOn = new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7424), new TimeSpan(0, -4, 0, 0, 0))
                        },
                        new
                        {
                            Id = 4,
                            CreatedOn = new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7428), new TimeSpan(0, -4, 0, 0, 0)),
                            Description = "Organize Electrical Toolbox",
                            Name = "Organize Electrical Toolbox",
                            PriorityId = 1,
                            SortOrder = 4,
                            TeamId = 4,
                            UpdatedOn = new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7430), new TimeSpan(0, -4, 0, 0, 0))
                        },
                        new
                        {
                            Id = 5,
                            CreatedOn = new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7433), new TimeSpan(0, -4, 0, 0, 0)),
                            Description = "Make New Router Vacuum Table",
                            Name = "Make New Router Vacuum Table",
                            PriorityId = 1,
                            SortOrder = 5,
                            TeamId = 6,
                            UpdatedOn = new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7436), new TimeSpan(0, -4, 0, 0, 0))
                        },
                        new
                        {
                            Id = 6,
                            CreatedOn = new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7439), new TimeSpan(0, -4, 0, 0, 0)),
                            Description = "Develop Path Planning",
                            Name = "Develop Path Planning",
                            PriorityId = 1,
                            SortOrder = 6,
                            TeamId = 4,
                            UpdatedOn = new DateTimeOffset(new DateTime(2021, 4, 9, 12, 37, 25, 823, DateTimeKind.Unspecified).AddTicks(7441), new TimeSpan(0, -4, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("WavoProjects.Api.Models.QueryModels.PriorityProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int?>("PriorityViewId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("StartedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("PriorityViewId");

                    b.HasIndex("TeamId");

                    b.ToTable("PriorityProject");
                });

            modelBuilder.Entity("WavoProjects.Api.Models.QueryModels.PriorityTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PriorityTeam");
                });

            modelBuilder.Entity("WavoProjects.Api.Models.QueryModels.PriorityView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("_PriorityView");
                });

            modelBuilder.Entity("WavoProjects.Api.Models.Snapshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("SnapshotTakenOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Snapshots");
                });

            modelBuilder.Entity("WavoProjects.Api.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "#ef0000",
                            Name = "General"
                        },
                        new
                        {
                            Id = 2,
                            Color = "#00ff2d",
                            Name = "Business"
                        },
                        new
                        {
                            Id = 3,
                            Color = "#4700d8",
                            Name = "CAD/Design"
                        },
                        new
                        {
                            Id = 4,
                            Color = "#ffab00",
                            Name = "Programming"
                        },
                        new
                        {
                            Id = 5,
                            Color = "#0dcaf0",
                            Name = "Media"
                        },
                        new
                        {
                            Id = 6,
                            Color = "#039aff",
                            Name = "Build"
                        });
                });

            modelBuilder.Entity("WavoProjects.Api.Models.Project", b =>
                {
                    b.HasOne("WavoProjects.Api.Models.Priority", "Priority")
                        .WithMany("Projects")
                        .HasForeignKey("PriorityId");

                    b.HasOne("WavoProjects.Api.Models.Team", "Team")
                        .WithMany("Projects")
                        .HasForeignKey("TeamId");

                    b.Navigation("Priority");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("WavoProjects.Api.Models.QueryModels.PriorityProject", b =>
                {
                    b.HasOne("WavoProjects.Api.Models.QueryModels.PriorityView", null)
                        .WithMany("Projects")
                        .HasForeignKey("PriorityViewId");

                    b.HasOne("WavoProjects.Api.Models.QueryModels.PriorityTeam", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("WavoProjects.Api.Models.Priority", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("WavoProjects.Api.Models.QueryModels.PriorityView", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("WavoProjects.Api.Models.Team", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
