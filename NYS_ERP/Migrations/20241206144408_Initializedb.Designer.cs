﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NYS_ERP.Models;

#nullable disable

namespace NYS_ERP.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241206144408_Initializedb")]
    partial class Initializedb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NYS_ERP.Models.Company", b =>
                {
                    b.Property<string>("COMCODE")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("ADDRESS1")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("ADDRESS2")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("COMTEXT")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("COMCODE");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            COMCODE = "001",
                            ADDRESS1 = "123 Main St",
                            ADDRESS2 = "Suite 100",
                            COMTEXT = "Company A"
                        },
                        new
                        {
                            COMCODE = "002",
                            ADDRESS1 = "456 Elm St",
                            ADDRESS2 = "Apt 5",
                            COMTEXT = "Company B"
                        },
                        new
                        {
                            COMCODE = "003",
                            ADDRESS1 = "789 Oak St",
                            ADDRESS2 = "Floor 2",
                            COMTEXT = "Company C"
                        },
                        new
                        {
                            COMCODE = "004",
                            ADDRESS1 = "101 Pine St",
                            ADDRESS2 = "Building B",
                            COMTEXT = "Company D"
                        });
                });

            modelBuilder.Entity("NYS_ERP.Models.Language", b =>
                {
                    b.Property<string>("LANCODE")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("COMCODE")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("LANTEXT")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("LANCODE");

                    b.HasIndex("COMCODE");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            LANCODE = "001",
                            COMCODE = "001",
                            LANTEXT = "English",
                            RowVersion = new byte[0]
                        },
                        new
                        {
                            LANCODE = "002",
                            COMCODE = "001",
                            LANTEXT = "French",
                            RowVersion = new byte[0]
                        },
                        new
                        {
                            LANCODE = "003",
                            COMCODE = "002",
                            LANTEXT = "Spanish",
                            RowVersion = new byte[0]
                        },
                        new
                        {
                            LANCODE = "004",
                            COMCODE = "002",
                            LANTEXT = "German",
                            RowVersion = new byte[0]
                        });
                });

            modelBuilder.Entity("NYS_ERP.Models.Language", b =>
                {
                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
