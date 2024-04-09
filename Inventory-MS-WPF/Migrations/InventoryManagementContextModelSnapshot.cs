﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Inventory_MS_WPF.Data;

#nullable disable

namespace Inventory_MS_WPF.Migrations
{
    [DbContext(typeof(InventoryManagementContext))]
    partial class InventoryManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Inventory_MS_WPF.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Customer", b =>
                {
                    b.Property<Guid>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerFirstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerLastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StaffID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CustomerID");

                    b.HasIndex("StaffID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Defective", b =>
                {
                    b.Property<Guid>("DefectiveID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateDeclared")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("DefectiveID");

                    b.HasIndex("ProductID");

                    b.ToTable("Defectives");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Location", b =>
                {
                    b.Property<Guid>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Log", b =>
                {
                    b.Property<Guid>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LogCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StaffID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LogID");

                    b.HasIndex("StaffID");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Order", b =>
                {
                    b.Property<Guid>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeliveryStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OrderTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.OrderDetail", b =>
                {
                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("OrderDetailAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OrderDetailQuantity")
                        .HasColumnType("int");

                    b.HasKey("ProductID", "OrderID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductAvailability")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.Property<string>("ProductSKU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SupplierID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("SupplierID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.ProductLocation", b =>
                {
                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LocationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.HasKey("ProductID", "LocationID");

                    b.HasIndex("LocationID");

                    b.ToTable("ProductLocations");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Role", b =>
                {
                    b.Property<Guid>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("CategoriesAdd")
                        .HasColumnType("bit");

                    b.Property<bool>("CategoriesDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("CategoriesEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("CategoriesView")
                        .HasColumnType("bit");

                    b.Property<bool>("CustomersAdd")
                        .HasColumnType("bit");

                    b.Property<bool>("CustomersDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("CustomersEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("CustomersView")
                        .HasColumnType("bit");

                    b.Property<bool>("DefectivesAdd")
                        .HasColumnType("bit");

                    b.Property<bool>("DefectivesDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("DefectivesEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("DefectivesView")
                        .HasColumnType("bit");

                    b.Property<bool>("LocationsAdd")
                        .HasColumnType("bit");

                    b.Property<bool>("LocationsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("LocationsEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("LocationsView")
                        .HasColumnType("bit");

                    b.Property<bool>("LogsAdd")
                        .HasColumnType("bit");

                    b.Property<bool>("LogsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("LogsEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("LogsView")
                        .HasColumnType("bit");

                    b.Property<bool>("OrdersAdd")
                        .HasColumnType("bit");

                    b.Property<bool>("OrdersDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("OrdersEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("OrdersView")
                        .HasColumnType("bit");

                    b.Property<bool>("ProductsAdd")
                        .HasColumnType("bit");

                    b.Property<bool>("ProductsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("ProductsEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("ProductsView")
                        .HasColumnType("bit");

                    b.Property<string>("RoleDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RolesAdd")
                        .HasColumnType("bit");

                    b.Property<bool>("RolesDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("RolesEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("RolesView")
                        .HasColumnType("bit");

                    b.Property<bool>("StaffsAdd")
                        .HasColumnType("bit");

                    b.Property<bool>("StaffsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("StaffsEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("StaffsView")
                        .HasColumnType("bit");

                    b.Property<bool>("StoragesAdd")
                        .HasColumnType("bit");

                    b.Property<bool>("StoragesDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("StoragesEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("StoragesView")
                        .HasColumnType("bit");

                    b.Property<bool>("SuppliersAdd")
                        .HasColumnType("bit");

                    b.Property<bool>("SuppliersDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("SuppliersEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("SuppliersView")
                        .HasColumnType("bit");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Staff", b =>
                {
                    b.Property<Guid>("StaffID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StaffAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaffID");

                    b.HasIndex("RoleID");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Supplier", b =>
                {
                    b.Property<Guid>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SupplierAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Warehouse", b =>
                {
                    b.Property<Guid>("WarehouseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WarehouseAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehouseEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehouseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehousePhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("WarehouseVat")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("WarehouseID");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Customer", b =>
                {
                    b.HasOne("Inventory_MS_WPF.Models.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Defective", b =>
                {
                    b.HasOne("Inventory_MS_WPF.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Log", b =>
                {
                    b.HasOne("Inventory_MS_WPF.Models.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Order", b =>
                {
                    b.HasOne("Inventory_MS_WPF.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.OrderDetail", b =>
                {
                    b.HasOne("Inventory_MS_WPF.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Inventory_MS_WPF.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Product", b =>
                {
                    b.HasOne("Inventory_MS_WPF.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Inventory_MS_WPF.Models.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.ProductLocation", b =>
                {
                    b.HasOne("Inventory_MS_WPF.Models.Location", "Location")
                        .WithMany("ProductLocations")
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Inventory_MS_WPF.Models.Product", "Product")
                        .WithMany("ProductLocations")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Staff", b =>
                {
                    b.HasOne("Inventory_MS_WPF.Models.Role", "Role")
                        .WithMany("Staffs")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Location", b =>
                {
                    b.Navigation("ProductLocations");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Product", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("ProductLocations");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Role", b =>
                {
                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("Inventory_MS_WPF.Models.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
