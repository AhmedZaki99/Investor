﻿// <auto-generated />
using System;
using InvestorAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InvestorAPI.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221003173855_ConcatInvoiceTypeAndReturn")]
    partial class ConcatInvoiceTypeAndReturn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InvestorData.Account", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<decimal?>("Balance")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<string>("BusinessId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BusinessTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("DateModified")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("BusinessTypeId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("InvestorData.Address", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressLine1")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("City")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Province")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("InvestorData.Business", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BusinessTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Currency")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("DateModified")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessTypeId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("InvestorData.BusinessType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<bool>("DisableProducts")
                        .HasColumnType("bit");

                    b.Property<bool>("DisableServices")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("NoInventory")
                        .HasColumnType("bit");

                    b.Property<bool>("SalesOnly")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("BusinessTypes");
                });

            modelBuilder.Entity("InvestorData.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BusinessId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("DateModified")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("InvestorData.Contact", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("DateModified")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("LastName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Phone")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("InvestorData.Invoice", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal?>("AmountDue")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<string>("BusinessId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("DateModified")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<int>("InvoiceType")
                        .HasColumnType("int");

                    b.Property<bool>("IsTracked")
                        .HasColumnType("bit");

                    b.Property<DateTime>("IssueDate")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Notes")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<int?>("Number")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PaymentDue")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<string>("TraderId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("TraderId");

                    b.HasIndex("InvoiceType", "TraderId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("InvestorData.Item", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)")
                        .HasComputedColumnSql("[Quantity] * [Price]");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("InvestorData.Payment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<string>("BusinessId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("DateModified")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("Notes")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<int?>("Number")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("PaymentMethodId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<string>("TraderId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("TraderId");

                    b.HasIndex("PaymentType", "TraderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("InvestorData.PaymentMethod", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("InvestorData.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BusinessId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<decimal?>("Cost")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("DateModified")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("ExpenseAccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IncomeAccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InventoryAccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsService")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PurchaseDescription")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<double?>("Quantity")
                        .HasColumnType("float");

                    b.Property<int?>("ReorderPoint")
                        .HasColumnType("int");

                    b.Property<string>("SKU")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("SalesDescription")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<decimal?>("SalesPrice")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<string>("ScaleUnitId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.HasIndex("ExpenseAccountId");

                    b.HasIndex("IncomeAccountId");

                    b.HasIndex("InventoryAccountId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ScaleUnitId");

                    b.HasIndex("IsService", "BusinessId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("InvestorData.ScaleUnit", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BusinessId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("DateModified")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ScaleUnits");
                });

            modelBuilder.Entity("InvestorData.Trader", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BusinessId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContactId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("DateModified")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Notes")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<int>("TraderType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("BusinessId");

                    b.HasIndex("ContactId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("TraderType", "BusinessId");

                    b.ToTable("Traders");
                });

            modelBuilder.Entity("InvestorData.UnitConversion", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("ConversionValue")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("SourceUnitId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TargetUnitId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SourceUnitId");

                    b.HasIndex("TargetUnitId");

                    b.ToTable("UnitConversions");
                });

            modelBuilder.Entity("InvestorData.Account", b =>
                {
                    b.HasOne("InvestorData.Business", "Business")
                        .WithMany("Accounts")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("InvestorData.BusinessType", "BusinessType")
                        .WithMany()
                        .HasForeignKey("BusinessTypeId");

                    b.Navigation("Business");

                    b.Navigation("BusinessType");
                });

            modelBuilder.Entity("InvestorData.Business", b =>
                {
                    b.HasOne("InvestorData.BusinessType", "BusinessType")
                        .WithMany()
                        .HasForeignKey("BusinessTypeId");

                    b.Navigation("BusinessType");
                });

            modelBuilder.Entity("InvestorData.Category", b =>
                {
                    b.HasOne("InvestorData.Business", "Business")
                        .WithMany("Categories")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("Business");
                });

            modelBuilder.Entity("InvestorData.Invoice", b =>
                {
                    b.HasOne("InvestorData.Business", "Business")
                        .WithMany("Invoices")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("InvestorData.Trader", "Trader")
                        .WithMany("Invoices")
                        .HasForeignKey("TraderId");

                    b.Navigation("Business");

                    b.Navigation("Trader");
                });

            modelBuilder.Entity("InvestorData.Item", b =>
                {
                    b.HasOne("InvestorData.Invoice", "Invoice")
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestorData.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("InvestorData.Payment", b =>
                {
                    b.HasOne("InvestorData.Business", "Business")
                        .WithMany("Payments")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("InvestorData.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestorData.Trader", "Trader")
                        .WithMany("Payments")
                        .HasForeignKey("TraderId");

                    b.Navigation("Business");

                    b.Navigation("PaymentMethod");

                    b.Navigation("Trader");
                });

            modelBuilder.Entity("InvestorData.Product", b =>
                {
                    b.HasOne("InvestorData.Business", "Business")
                        .WithMany("Products")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("InvestorData.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("InvestorData.Account", "ExpenseAccount")
                        .WithMany()
                        .HasForeignKey("ExpenseAccountId");

                    b.HasOne("InvestorData.Account", "IncomeAccount")
                        .WithMany()
                        .HasForeignKey("IncomeAccountId");

                    b.HasOne("InvestorData.Account", "InventoryAccount")
                        .WithMany()
                        .HasForeignKey("InventoryAccountId");

                    b.HasOne("InvestorData.ScaleUnit", "ScaleUnit")
                        .WithMany()
                        .HasForeignKey("ScaleUnitId");

                    b.Navigation("Business");

                    b.Navigation("Category");

                    b.Navigation("ExpenseAccount");

                    b.Navigation("IncomeAccount");

                    b.Navigation("InventoryAccount");

                    b.Navigation("ScaleUnit");
                });

            modelBuilder.Entity("InvestorData.ScaleUnit", b =>
                {
                    b.HasOne("InvestorData.Business", "Business")
                        .WithMany("ScaleUnits")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("Business");
                });

            modelBuilder.Entity("InvestorData.Trader", b =>
                {
                    b.HasOne("InvestorData.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("InvestorData.Business", "Business")
                        .WithMany("Traders")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("InvestorData.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.Navigation("Address");

                    b.Navigation("Business");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("InvestorData.UnitConversion", b =>
                {
                    b.HasOne("InvestorData.ScaleUnit", "SourceUnit")
                        .WithMany()
                        .HasForeignKey("SourceUnitId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("InvestorData.ScaleUnit", "TargetUnit")
                        .WithMany()
                        .HasForeignKey("TargetUnitId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("SourceUnit");

                    b.Navigation("TargetUnit");
                });

            modelBuilder.Entity("InvestorData.Business", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Categories");

                    b.Navigation("Invoices");

                    b.Navigation("Payments");

                    b.Navigation("Products");

                    b.Navigation("ScaleUnits");

                    b.Navigation("Traders");
                });

            modelBuilder.Entity("InvestorData.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("InvestorData.Invoice", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("InvestorData.Trader", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
