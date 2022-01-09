﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WavoProjects.Api.DatabaseModels;

namespace WavoProjects.Api.Migrations
{
    [DbContext(typeof(WavoContext))]
    partial class WavoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("WavoProjects.Api.Models.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Priority");

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
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int?>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int?>("SortOrder")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("PriorityId");

                    b.HasIndex("TeamId");

                    b.ToTable("Project");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2021, 12, 26, 10, 48, 38, 792, DateTimeKind.Local).AddTicks(8980),
                            Description = "T-Shirt Cannon Robot",
                            Name = "T-Shirt Cannon Robot",
                            PriorityId = 1,
                            SortOrder = 1,
                            TeamId = 3,
                            UpdatedOn = new DateTime(2021, 12, 26, 10, 48, 38, 792, DateTimeKind.Local).AddTicks(9490)
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2021, 12, 26, 10, 48, 38, 793, DateTimeKind.Local).AddTicks(570),
                            Description = "Sponsor Thank Yous",
                            Name = "Sponsor Thank Yous",
                            PriorityId = 1,
                            SortOrder = 2,
                            TeamId = 1,
                            UpdatedOn = new DateTime(2021, 12, 26, 10, 48, 38, 793, DateTimeKind.Local).AddTicks(580)
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2021, 12, 26, 10, 48, 38, 793, DateTimeKind.Local).AddTicks(580),
                            Description = "Chairman's Presentation",
                            Name = "Chairman's Presentation",
                            PriorityId = 1,
                            SortOrder = 3,
                            TeamId = 2,
                            UpdatedOn = new DateTime(2021, 12, 26, 10, 48, 38, 793, DateTimeKind.Local).AddTicks(580)
                        },
                        new
                        {
                            Id = 4,
                            CreatedOn = new DateTime(2021, 12, 26, 10, 48, 38, 793, DateTimeKind.Local).AddTicks(590),
                            Description = "Organize Electrical Toolbox",
                            Name = "Organize Electrical Toolbox",
                            PriorityId = 1,
                            SortOrder = 4,
                            TeamId = 4,
                            UpdatedOn = new DateTime(2021, 12, 26, 10, 48, 38, 793, DateTimeKind.Local).AddTicks(590)
                        },
                        new
                        {
                            Id = 5,
                            CreatedOn = new DateTime(2021, 12, 26, 10, 48, 38, 793, DateTimeKind.Local).AddTicks(590),
                            Description = "Make New Router Vacuum Table",
                            Name = "Make New Router Vacuum Table",
                            PriorityId = 1,
                            SortOrder = 5,
                            TeamId = 6,
                            UpdatedOn = new DateTime(2021, 12, 26, 10, 48, 38, 793, DateTimeKind.Local).AddTicks(590)
                        },
                        new
                        {
                            Id = 6,
                            CreatedOn = new DateTime(2021, 12, 26, 10, 48, 38, 793, DateTimeKind.Local).AddTicks(600),
                            Description = "Develop Path Planning",
                            Name = "Develop Path Planning",
                            PriorityId = 1,
                            SortOrder = 6,
                            TeamId = 4,
                            UpdatedOn = new DateTime(2021, 12, 26, 10, 48, 38, 793, DateTimeKind.Local).AddTicks(600)
                        });
                });

            modelBuilder.Entity("WavoProjects.Api.Models.QueryModels.PriorityProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int?>("PriorityViewId")
                        .HasColumnType("int");

                    b.Property<int?>("SortOrder")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("PriorityViewId");

                    b.HasIndex("TeamId");

                    b.ToTable("PriorityProject", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("WavoProjects.Api.Models.QueryModels.PriorityTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PriorityTeam", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("WavoProjects.Api.Models.QueryModels.PriorityView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PriorityView", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("WavoProjects.Api.Models.Snapshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("SnapshotTakenOn")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Snapshot");
                });

            modelBuilder.Entity("WavoProjects.Api.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("char(7)");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Team");

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
