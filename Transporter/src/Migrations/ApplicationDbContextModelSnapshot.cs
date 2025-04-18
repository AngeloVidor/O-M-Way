﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using src.Infrastructure.Data;

#nullable disable

namespace Transporter.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("src.Application.Models.VerificationCodeModel", b =>
                {
                    b.Property<long>("VerificationCode_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("VerificationCode_ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("VerificationCode_ID");

                    b.ToTable("VerificationCodes");
                });

            modelBuilder.Entity("src.Domain.Entities.Employee", b =>
                {
                    b.Property<long>("Employee_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Employee_ID"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Transporter_ID")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Employee_ID");

                    b.HasIndex("Transporter_ID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("src.Domain.Entities.Location", b =>
                {
                    b.Property<long>("Location_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Location_ID"));

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<long>("Transporter_ID")
                        .HasColumnType("bigint");

                    b.HasKey("Location_ID");

                    b.HasIndex("Transporter_ID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("src.Domain.Entities.PendingLocation", b =>
                {
                    b.Property<long>("PendingLocation_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PendingLocation_ID"));

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<long>("Transporter_ID")
                        .HasColumnType("bigint");

                    b.HasKey("PendingLocation_ID");

                    b.ToTable("PendingLocations");
                });

            modelBuilder.Entity("src.Domain.Entities.PendingRegistration", b =>
                {
                    b.Property<long>("PendingTransporter_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PendingTransporter_ID"));

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Cnpj_Validated")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCnpj_Valid")
                        .HasColumnType("bit");

                    b.Property<long>("LocationPendingLocation_ID")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PendingTransporter_ID");

                    b.HasIndex("LocationPendingLocation_ID");

                    b.ToTable("TransporterPreRegistrations");
                });

            modelBuilder.Entity("src.Domain.Entities.TransporterCompany", b =>
                {
                    b.Property<long>("Transporter_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Transporter_ID"));

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("Location_ID")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Transporter_ID");

                    b.HasIndex("Location_ID");

                    b.ToTable("Transporters");
                });

            modelBuilder.Entity("src.Domain.Entities.Employee", b =>
                {
                    b.HasOne("src.Domain.Entities.TransporterCompany", "TransporterCompany")
                        .WithMany("Employees")
                        .HasForeignKey("Transporter_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransporterCompany");
                });

            modelBuilder.Entity("src.Domain.Entities.Location", b =>
                {
                    b.HasOne("src.Domain.Entities.TransporterCompany", "Transporter")
                        .WithMany()
                        .HasForeignKey("Transporter_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transporter");
                });

            modelBuilder.Entity("src.Domain.Entities.PendingRegistration", b =>
                {
                    b.HasOne("src.Domain.Entities.PendingLocation", "Location")
                        .WithMany()
                        .HasForeignKey("LocationPendingLocation_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("src.Domain.Entities.TransporterCompany", b =>
                {
                    b.HasOne("src.Domain.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("Location_ID");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("src.Domain.Entities.TransporterCompany", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
