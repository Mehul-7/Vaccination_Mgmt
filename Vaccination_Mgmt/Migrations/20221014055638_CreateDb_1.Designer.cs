﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vaccination_Mgmt.Data;

#nullable disable

namespace Vaccination_Mgmt.Migrations
{
    [DbContext(typeof(VacDbContext))]
    [Migration("20221014055638_CreateDb_1")]
    partial class CreateDb_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Vaccination_Mgmt.Models.Dose", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Doses");
                });

            modelBuilder.Entity("Vaccination_Mgmt.Models.DoseMapping", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("DoseId")
                        .HasColumnType("int");

                    b.Property<int>("VaccineId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("DoseId")
                        .IsUnique();

                    b.HasIndex("VaccineId")
                        .IsUnique();

                    b.ToTable("DoseMappings");
                });

            modelBuilder.Entity("Vaccination_Mgmt.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Vaccination_Mgmt.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Vaccination_Mgmt.Models.UserRole", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserID", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Vaccination_Mgmt.Models.Vaccine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("Vaccination_Mgmt.Models.DoseMapping", b =>
                {
                    b.HasOne("Vaccination_Mgmt.Models.Dose", "Dose")
                        .WithOne("DoseMapping")
                        .HasForeignKey("Vaccination_Mgmt.Models.DoseMapping", "DoseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vaccination_Mgmt.Models.User", "User")
                        .WithOne("DoseMapping")
                        .HasForeignKey("Vaccination_Mgmt.Models.DoseMapping", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vaccination_Mgmt.Models.Vaccine", "Vaccine")
                        .WithOne("DoseMapping")
                        .HasForeignKey("Vaccination_Mgmt.Models.DoseMapping", "VaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dose");

                    b.Navigation("User");

                    b.Navigation("Vaccine");
                });

            modelBuilder.Entity("Vaccination_Mgmt.Models.UserRole", b =>
                {
                    b.HasOne("Vaccination_Mgmt.Models.Role", "Role")
                        .WithMany("UserRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vaccination_Mgmt.Models.User", "User")
                        .WithMany("UserRole")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Vaccination_Mgmt.Models.Dose", b =>
                {
                    b.Navigation("DoseMapping")
                        .IsRequired();
                });

            modelBuilder.Entity("Vaccination_Mgmt.Models.Role", b =>
                {
                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Vaccination_Mgmt.Models.User", b =>
                {
                    b.Navigation("DoseMapping")
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Vaccination_Mgmt.Models.Vaccine", b =>
                {
                    b.Navigation("DoseMapping")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
