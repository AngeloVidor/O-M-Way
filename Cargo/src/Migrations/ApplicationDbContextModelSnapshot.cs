﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using src.Infrastructure.Data;

#nullable disable

namespace src.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("src.Domain.Entities.Address", b =>
                {
                    b.Property<long>("Address_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Address_ID"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Address_ID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("src.Domain.Entities.DriverSnapshot", b =>
                {
                    b.Property<long>("Snapshot_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Snapshot_ID"));

                    b.Property<long>("Employee_ID")
                        .HasColumnType("bigint");

                    b.Property<long>("Transporter_ID")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Snapshot_ID");

                    b.ToTable("DriverSnapshots");
                });

            modelBuilder.Entity("src.Domain.Entities.Load", b =>
                {
                    b.Property<long>("Load_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Load_ID"));

                    b.Property<DateTime>("DeliveredAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeliveryDeadline")
                        .HasColumnType("datetime2");

                    b.Property<long>("Driver_ID")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDelivered")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LoadingFinishedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LoadingStartedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Transporter_ID")
                        .HasColumnType("bigint");

                    b.HasKey("Load_ID");

                    b.ToTable("Loads");
                });

            modelBuilder.Entity("src.Domain.Entities.Product", b =>
                {
                    b.Property<long>("Product_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Product_ID"));

                    b.Property<long>("Address_ID")
                        .HasColumnType("bigint");

                    b.Property<long>("Load_ID")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecipientCPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Product_ID");

                    b.HasIndex("Address_ID");

                    b.HasIndex("Load_ID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("src.Domain.Entities.Product", b =>
                {
                    b.HasOne("src.Domain.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("Address_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("src.Domain.Entities.Load", "Load")
                        .WithMany("Products")
                        .HasForeignKey("Load_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Load");
                });

            modelBuilder.Entity("src.Domain.Entities.Load", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
