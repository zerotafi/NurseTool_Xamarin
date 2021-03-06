﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using NSService.Entities;
using System;

namespace NSService.Migrations
{
    [DbContext(typeof(PatientInfoContext))]
    [Migration("20180422104335_NewTables")]
    partial class NewTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NSService.Entities.BloodPressureData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DiastolicValue");

                    b.Property<DateTime>("ExaminationDate");

                    b.Property<int>("MeanBloodPressure");

                    b.Property<int>("PulseRate");

                    b.Property<int>("SystolicValue");

                    b.HasKey("Id");

                    b.ToTable("BloodPressureData");
                });

            modelBuilder.Entity("NSService.Entities.BodyTemperatureData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExaminationDate");

                    b.Property<int>("TemperatureVAlue");

                    b.HasKey("Id");

                    b.ToTable("BodyTemperatureData");
                });

            modelBuilder.Entity("NSService.Entities.Examination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<string>("ExaminationType")
                        .HasMaxLength(100);

                    b.Property<int>("PatientId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Examinations");
                });

            modelBuilder.Entity("NSService.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int>("Age");

                    b.Property<DateTime>("BirthDate");

                    b.Property<int>("ExternalId");

                    b.Property<string>("Gender");

                    b.Property<string>("Name");

                    b.Property<string>("OriginalHL7Message");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("NSService.Entities.SpOData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SPOValue");

                    b.HasKey("Id");

                    b.ToTable("SpOData");
                });

            modelBuilder.Entity("NSService.Entities.BloodPressureData", b =>
                {
                    b.HasOne("NSService.Entities.Examination", "Examination")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NSService.Entities.BodyTemperatureData", b =>
                {
                    b.HasOne("NSService.Entities.Examination", "Examination")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NSService.Entities.Examination", b =>
                {
                    b.HasOne("NSService.Entities.Patient", "Patient")
                        .WithMany("Examinations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NSService.Entities.SpOData", b =>
                {
                    b.HasOne("NSService.Entities.Examination", "Examination")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
