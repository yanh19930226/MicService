﻿// <auto-generated />
using System;
using MicService.Project.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MicService.Project.Api.Migrations
{
    [DbContext(typeof(ProjectContext))]
    partial class ProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MicService.Project.Api.Domain.AggregatesModel.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Area")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("BrokerageOptions")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Company")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("FinMoney")
                        .HasColumnType("int");

                    b.Property<string>("FormatBPFile")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Income")
                        .HasColumnType("int");

                    b.Property<bool>("OnPlatform")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("OriginBPFile")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Province")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("int");

                    b.Property<int>("ReferenceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisterTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Revenue")
                        .HasColumnType("int");

                    b.Property<bool>("ShowSecurityInfo")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("SourceId")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Valuation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MicService.Project.Api.Domain.AggregatesModel.ProjectContributor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ContributorType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsCloser")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectContributors");
                });

            modelBuilder.Entity("MicService.Project.Api.Domain.AggregatesModel.ProjectProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Key")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectProperties");
                });

            modelBuilder.Entity("MicService.Project.Api.Domain.AggregatesModel.ProjectViewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectViewers");
                });

            modelBuilder.Entity("MicService.Project.Api.Domain.AggregatesModel.ProjectVisibleRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.ToTable("ProjectVisibleRules");
                });

            modelBuilder.Entity("MicService.Project.Api.Domain.AggregatesModel.ProjectContributor", b =>
                {
                    b.HasOne("MicService.Project.Api.Domain.AggregatesModel.Project", "Project")
                        .WithMany("Contributors")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MicService.Project.Api.Domain.AggregatesModel.ProjectProperty", b =>
                {
                    b.HasOne("MicService.Project.Api.Domain.AggregatesModel.Project", "Project")
                        .WithMany("Properties")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MicService.Project.Api.Domain.AggregatesModel.ProjectViewer", b =>
                {
                    b.HasOne("MicService.Project.Api.Domain.AggregatesModel.Project", "Project")
                        .WithMany("Viewers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MicService.Project.Api.Domain.AggregatesModel.ProjectVisibleRule", b =>
                {
                    b.HasOne("MicService.Project.Api.Domain.AggregatesModel.Project", "Project")
                        .WithOne("VisibleRule")
                        .HasForeignKey("MicService.Project.Api.Domain.AggregatesModel.ProjectVisibleRule", "ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}