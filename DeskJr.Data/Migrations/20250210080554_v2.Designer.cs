﻿// <auto-generated />
using System;
using DeskJr.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DeskJr.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250210080554_v2")]
    partial class v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DeskJr.Entity.Models.Employee", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Base64Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DayOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeRole")
                        .HasColumnType("int");

                    b.Property<Guid?>("EmployeeTitleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeTitleId");

                    b.HasIndex("TeamId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.EmployeeOptions", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.ToTable("EmployeeOptions");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.EmployeeTitle", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TitleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("TitleName")
                        .IsUnique();

                    b.ToTable("EmployeeTitles");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.Holiday", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.Leave", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ApprovedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConfirmDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LeaveTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RequestComments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RequestingEmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusOfLeave")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("LeaveTypeId");

                    b.HasIndex("RequestingEmployeeId");

                    b.ToTable("Leaves");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.LeaveType", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("LeaveTypes");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.Log", b =>
                {
                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatusCode")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.ToTable("Logs", null, t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("DeskJr.Entity.Models.Setting", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.Survey", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.SurveyQuestion", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SurveyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyQuestions");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.SurveyQuestionOptions", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SurveyQuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("SurveyQuestionId");

                    b.ToTable("SurveyQuestionOptions");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.Team", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UpTeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("ManagerId");

                    b.HasIndex("UpTeamId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.Employee", b =>
                {
                    b.HasOne("DeskJr.Entity.Models.EmployeeTitle", "EmployeeTitle")
                        .WithMany()
                        .HasForeignKey("EmployeeTitleId");

                    b.HasOne("DeskJr.Entity.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("EmployeeTitle");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.Leave", b =>
                {
                    b.HasOne("DeskJr.Entity.Models.Employee", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("DeskJr.Entity.Models.LeaveType", "LeaveType")
                        .WithMany()
                        .HasForeignKey("LeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeskJr.Entity.Models.Employee", "RequestingEmployee")
                        .WithMany()
                        .HasForeignKey("RequestingEmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ApprovedBy");

                    b.Navigation("LeaveType");

                    b.Navigation("RequestingEmployee");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.SurveyQuestion", b =>
                {
                    b.HasOne("DeskJr.Entity.Models.Survey", "Survey")
                        .WithMany("SurveyQuestions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.SurveyQuestionOptions", b =>
                {
                    b.HasOne("DeskJr.Entity.Models.SurveyQuestion", "SurveyQuestion")
                        .WithMany("SurveyQuestionOptions")
                        .HasForeignKey("SurveyQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SurveyQuestion");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.Team", b =>
                {
                    b.HasOne("DeskJr.Entity.Models.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("DeskJr.Entity.Models.Team", "UpTeam")
                        .WithMany()
                        .HasForeignKey("UpTeamId");

                    b.Navigation("Manager");

                    b.Navigation("UpTeam");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.Survey", b =>
                {
                    b.Navigation("SurveyQuestions");
                });

            modelBuilder.Entity("DeskJr.Entity.Models.SurveyQuestion", b =>
                {
                    b.Navigation("SurveyQuestionOptions");
                });
#pragma warning restore 612, 618
        }
    }
}
