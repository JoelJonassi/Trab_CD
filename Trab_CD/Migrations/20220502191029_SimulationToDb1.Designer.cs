﻿// <auto-generated />
using System;
using JobShopAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JobShopAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220502191029_SimulationToDb1")]
    partial class SimulationToDb1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("JobShopAPI.Models.Job", b =>
                {
                    b.Property<int>("IdJob")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdJob"), 1L, 1);

                    b.Property<string>("NameJob")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SimulationIdSimulation")
                        .HasColumnType("int");

                    b.HasKey("IdJob");

                    b.HasIndex("SimulationIdSimulation");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("JobShopAPI.Models.Machine", b =>
                {
                    b.Property<int>("IdMachine")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMachine"), 1L, 1);

                    b.Property<string>("MachineÑame")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdMachine");

                    b.ToTable("Machine");
                });

            modelBuilder.Entity("JobShopAPI.Models.Operation", b =>
                {
                    b.Property<int>("IdOperation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdOperation"), 1L, 1);

                    b.Property<int>("IdMachine")
                        .HasColumnType("int");

                    b.Property<int?>("JobIdJob")
                        .HasColumnType("int");

                    b.Property<string>("OperationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdOperation");

                    b.HasIndex("JobIdJob");

                    b.ToTable("Operation");
                });

            modelBuilder.Entity("JobShopAPI.Models.Simulation", b =>
                {
                    b.Property<int>("IdSimulation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSimulation"), 1L, 1);

                    b.Property<int>("MachineIdMachine")
                        .HasColumnType("int");

                    b.Property<int>("OperationIdOperation")
                        .HasColumnType("int");

                    b.HasKey("IdSimulation");

                    b.HasIndex("MachineIdMachine");

                    b.HasIndex("OperationIdOperation");

                    b.ToTable("Simulations");
                });

            modelBuilder.Entity("JobShopAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JobShopAPI.Models.Job", b =>
                {
                    b.HasOne("JobShopAPI.Models.Simulation", null)
                        .WithMany("Jobs")
                        .HasForeignKey("SimulationIdSimulation");
                });

            modelBuilder.Entity("JobShopAPI.Models.Operation", b =>
                {
                    b.HasOne("JobShopAPI.Models.Job", null)
                        .WithMany("Operations")
                        .HasForeignKey("JobIdJob");
                });

            modelBuilder.Entity("JobShopAPI.Models.Simulation", b =>
                {
                    b.HasOne("JobShopAPI.Models.Machine", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineIdMachine")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobShopAPI.Models.Operation", "Operation")
                        .WithMany()
                        .HasForeignKey("OperationIdOperation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Machine");

                    b.Navigation("Operation");
                });

            modelBuilder.Entity("JobShopAPI.Models.Job", b =>
                {
                    b.Navigation("Operations");
                });

            modelBuilder.Entity("JobShopAPI.Models.Simulation", b =>
                {
                    b.Navigation("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
