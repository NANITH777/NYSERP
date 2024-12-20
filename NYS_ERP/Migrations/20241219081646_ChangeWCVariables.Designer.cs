﻿// <auto-generated />
using System;
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
    [Migration("20241219081646_ChangeWCVariables")]
    partial class ChangeWCVariables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NYS_ERP.Models.BOM", b =>
                {
                    b.Property<string>("BOMDOCTYPE")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("BOMDOCNUM")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("COMCODE")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)");

                    b.Property<int?>("ISPASSIVE")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("BOMDOCTYPE");

                    b.HasIndex("COMCODE");

                    b.ToTable("BOMs");
                });

            modelBuilder.Entity("NYS_ERP.Models.City", b =>
                {
                    b.Property<string>("CITYCODE")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("CITYTEXT")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("COMCODE")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("COUNTRYCODE")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("CITYCODE");

                    b.HasIndex("COMCODE");

                    b.HasIndex("COUNTRYCODE");

                    b.ToTable("Cities");
                });

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

            modelBuilder.Entity("NYS_ERP.Models.CostCenter", b =>
                {
                    b.Property<string>("DOCTYPE")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("COMCODE")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("DOCTYPETEXT")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("ISPASSIVE")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("DOCTYPE");

                    b.HasIndex("COMCODE");

                    b.ToTable("CostCenters");
                });

            modelBuilder.Entity("NYS_ERP.Models.Country", b =>
                {
                    b.Property<string>("COUNTRYCODE")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("COMCODE")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("COUNTRYTEXT")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("COUNTRYCODE");

                    b.HasIndex("COMCODE");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            COUNTRYCODE = "FRA",
                            COMCODE = "001",
                            COUNTRYTEXT = "France"
                        },
                        new
                        {
                            COUNTRYCODE = "USA",
                            COMCODE = "002",
                            COUNTRYTEXT = "United States"
                        },
                        new
                        {
                            COUNTRYCODE = "TUR",
                            COMCODE = "003",
                            COUNTRYTEXT = "Turkey"
                        },
                        new
                        {
                            COUNTRYCODE = "DEU",
                            COMCODE = "004",
                            COUNTRYTEXT = "Germany"
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

            modelBuilder.Entity("NYS_ERP.Models.MaterialType", b =>
                {
                    b.Property<string>("MATDOCTYPE")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("COMCODE")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<int>("ISPASSIVE")
                        .HasColumnType("int");

                    b.Property<string>("MATDOCNUM")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("MATDOCTYPE");

                    b.HasIndex("COMCODE");

                    b.ToTable("MaterialTypes");
                });

            modelBuilder.Entity("NYS_ERP.Models.Operation", b =>
                {
                    b.Property<string>("DOCTYPE")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("COMCODE")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("DOCTYPETEXT")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int?>("ISPASSIVE")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("DOCTYPE");

                    b.HasIndex("COMCODE");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("NYS_ERP.Models.Rota", b =>
                {
                    b.Property<string>("ROTDOCTYPE")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("COMCODE")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<int>("ISPASSIVE")
                        .HasColumnType("int");

                    b.Property<string>("ROTDOCNUM")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("ROTDOCTYPE");

                    b.HasIndex("COMCODE");

                    b.ToTable("Rotas");
                });

            modelBuilder.Entity("NYS_ERP.Models.Unit", b =>
                {
                    b.Property<string>("UNITCODE")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("COMCODE")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<int>("ISMAINUNIT")
                        .HasColumnType("int");

                    b.Property<string>("MAINUNITCODE")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("UNITTEXT")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("UNITCODE");

                    b.HasIndex("COMCODE");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("NYS_ERP.Models.WorkCenter", b =>
                {
                    b.Property<string>("WCMDOCTYPE")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("COMCODE")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<int>("ISPASSIVE")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("WCMDOCNUM")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("WCMDOCTYPE");

                    b.HasIndex("COMCODE");

                    b.ToTable("WorkCenters");
                });

            modelBuilder.Entity("NYS_ERP.Models.BOM", b =>
                {
                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("NYS_ERP.Models.City", b =>
                {
                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("COUNTRYCODE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("NYS_ERP.Models.CostCenter", b =>
                {
                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("NYS_ERP.Models.Country", b =>
                {
                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
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

            modelBuilder.Entity("NYS_ERP.Models.MaterialType", b =>
                {
                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("NYS_ERP.Models.Operation", b =>
                {
                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("NYS_ERP.Models.Rota", b =>
                {
                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("NYS_ERP.Models.Unit", b =>
                {
                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("NYS_ERP.Models.WorkCenter", b =>
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
