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
    [Migration("20241223135549_UpdateMat2")]
    partial class UpdateMat2
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

            modelBuilder.Entity("NYS_ERP.Models.BOMAna", b =>
                {
                    b.Property<string>("COMCODE")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("BOMDOCTYPE")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("BOMDOCNUM")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BOMDOCFROM")
                        .HasColumnType("DATE");

                    b.Property<DateTime>("BOMDOCUNTIL")
                        .HasColumnType("DATE");

                    b.Property<string>("MATDOCTYPE")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("MATDOCNUM")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("CONTENTNUM")
                        .HasColumnType("int");

                    b.Property<string>("COMPBOMDOCNUM")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("COMPBOMDOCTYPE")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("COMPONENT")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<decimal?>("COMPONENT_QUANTITY")
                        .IsRequired()
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("DRAWNUM")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int?>("ISDELETED")
                        .HasColumnType("int");

                    b.Property<int?>("ISPASSIVE")
                        .HasColumnType("int");

                    b.Property<decimal?>("QUANTITY")
                        .IsRequired()
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("COMCODE", "BOMDOCTYPE", "BOMDOCNUM", "BOMDOCFROM", "BOMDOCUNTIL", "MATDOCTYPE", "MATDOCNUM", "CONTENTNUM");

                    b.HasIndex("BOMDOCTYPE");

                    b.HasIndex("MATDOCTYPE");

                    b.ToTable("BOMAnas");
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
                    b.Property<string>("CCMDOCTYPE")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("CCMDOCNUM")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

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

                    b.HasKey("CCMDOCTYPE");

                    b.HasIndex("COMCODE");

                    b.ToTable("CostCenters");
                });

            modelBuilder.Entity("NYS_ERP.Models.CostCenterAna", b =>
                {
                    b.Property<string>("COMCODE")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("CCMDOCTYPE")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("CCMDOCNUM")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime>("CCMDOCFROM")
                        .HasColumnType("DATE");

                    b.Property<DateTime>("CCMDOCUNTIL")
                        .HasColumnType("DATE");

                    b.Property<string>("LANCODE")
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("CCMLTEXT")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("CCMSTEXT")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<bool>("ISDELETED")
                        .HasColumnType("bit");

                    b.Property<bool>("ISPASSIVE")
                        .HasColumnType("bit");

                    b.Property<string>("MAINCCMDOCNUM")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("MAINCCMDOCTYPE")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("VARCHAR");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("COMCODE", "CCMDOCTYPE", "CCMDOCNUM", "CCMDOCFROM", "CCMDOCUNTIL", "LANCODE");

                    b.HasIndex("CCMDOCTYPE");

                    b.HasIndex("LANCODE");

                    b.ToTable("CostCenterAnas");
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

            modelBuilder.Entity("NYS_ERP.Models.Material", b =>
                {
                    b.Property<string>("COMCODE")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("MATDOCTYPE")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("MATDOCNUM")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("MATDOCFROM")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MATDOCUNTIL")
                        .HasColumnType("datetime2");

                    b.Property<string>("LANCODE")
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("BOMDOCNUM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BOMDOCTYPE")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)");

                    b.Property<decimal?>("BRUTWEIGHT")
                        .HasColumnType("decimal(12,3)");

                    b.Property<string>("BWUNIT")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("ISBOM")
                        .HasColumnType("int");

                    b.Property<bool>("ISDELETED")
                        .HasColumnType("bit");

                    b.Property<bool>("ISPASSIVE")
                        .HasColumnType("bit");

                    b.Property<int>("ISROUTE")
                        .HasColumnType("int");

                    b.Property<string>("MATLTEXT")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("MATSTEXT")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("NETWEIGHT")
                        .HasColumnType("decimal(12,3)");

                    b.Property<string>("NWUNIT")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("ROTDOCNUM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ROTDOCTYPE")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("STUNIT")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("SUPPLYTYPE")
                        .HasColumnType("int");

                    b.HasKey("COMCODE", "MATDOCTYPE", "MATDOCNUM", "MATDOCFROM", "MATDOCUNTIL", "LANCODE");

                    b.HasIndex("BOMDOCTYPE");

                    b.HasIndex("LANCODE");

                    b.HasIndex("MATDOCTYPE");

                    b.HasIndex("ROTDOCTYPE");

                    b.ToTable("Materials");
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
                    b.Property<string>("OPRDOCTYPE")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("COMCODE")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)");

                    b.Property<int?>("ISPASSIVE")
                        .HasColumnType("int");

                    b.Property<string>("OPRDOCNUM")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("OPRDOCTYPE");

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

            modelBuilder.Entity("NYS_ERP.Models.WorkCenterAna", b =>
                {
                    b.Property<string>("COMCODE")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("WCMDOCTYPE")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("WCMDOCNUM")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime>("WCMDOCFROM")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WCMDOCUNTIL")
                        .HasColumnType("datetime2");

                    b.Property<string>("LANCODE")
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("OPRDOCTYPE")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("CCMDOCNUM")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("CCMDOCTYPE")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<bool>("ISDELETED")
                        .HasColumnType("bit");

                    b.Property<bool>("ISPASSIVE")
                        .HasColumnType("bit");

                    b.Property<string>("MAINWCMDOCNUM")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("MAINWCMDOCTYPE")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("WCMLTEXT")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("WCMSTEXT")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("WORKTIME")
                        .HasPrecision(3, 2)
                        .HasColumnType("decimal(3,2)");

                    b.HasKey("COMCODE", "WCMDOCTYPE", "WCMDOCNUM", "WCMDOCFROM", "WCMDOCUNTIL", "LANCODE", "OPRDOCTYPE");

                    b.HasIndex("CCMDOCTYPE");

                    b.HasIndex("LANCODE");

                    b.HasIndex("OPRDOCTYPE");

                    b.HasIndex("WCMDOCTYPE");

                    b.ToTable("WorkCenterAnas");
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

            modelBuilder.Entity("NYS_ERP.Models.BOMAna", b =>
                {
                    b.HasOne("NYS_ERP.Models.BOM", "BOM")
                        .WithMany()
                        .HasForeignKey("BOMDOCTYPE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.MaterialType", "MaterialType")
                        .WithMany()
                        .HasForeignKey("MATDOCTYPE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BOM");

                    b.Navigation("Company");

                    b.Navigation("MaterialType");
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

            modelBuilder.Entity("NYS_ERP.Models.CostCenterAna", b =>
                {
                    b.HasOne("NYS_ERP.Models.CostCenter", "CostCenter")
                        .WithMany()
                        .HasForeignKey("CCMDOCTYPE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LANCODE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("CostCenter");

                    b.Navigation("Language");
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

            modelBuilder.Entity("NYS_ERP.Models.Material", b =>
                {
                    b.HasOne("NYS_ERP.Models.BOM", "BOM")
                        .WithMany()
                        .HasForeignKey("BOMDOCTYPE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LANCODE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.MaterialType", "MaterialType")
                        .WithMany()
                        .HasForeignKey("MATDOCTYPE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.Rota", "Rota")
                        .WithMany()
                        .HasForeignKey("ROTDOCTYPE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BOM");

                    b.Navigation("Company");

                    b.Navigation("Language");

                    b.Navigation("MaterialType");

                    b.Navigation("Rota");
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

            modelBuilder.Entity("NYS_ERP.Models.WorkCenterAna", b =>
                {
                    b.HasOne("NYS_ERP.Models.CostCenter", "CostCenter")
                        .WithMany()
                        .HasForeignKey("CCMDOCTYPE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("COMCODE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LANCODE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.Operation", "Operation")
                        .WithMany()
                        .HasForeignKey("OPRDOCTYPE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NYS_ERP.Models.WorkCenter", "WorkCenter")
                        .WithMany()
                        .HasForeignKey("WCMDOCTYPE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("CostCenter");

                    b.Navigation("Language");

                    b.Navigation("Operation");

                    b.Navigation("WorkCenter");
                });
#pragma warning restore 612, 618
        }
    }
}
