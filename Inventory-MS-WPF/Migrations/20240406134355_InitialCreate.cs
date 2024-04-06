using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_MS_WPF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrdersView = table.Column<bool>(type: "bit", nullable: false),
                    OrdersAdd = table.Column<bool>(type: "bit", nullable: false),
                    OrdersEdit = table.Column<bool>(type: "bit", nullable: false),
                    OrdersDelete = table.Column<bool>(type: "bit", nullable: false),
                    CustomersView = table.Column<bool>(type: "bit", nullable: false),
                    CustomersAdd = table.Column<bool>(type: "bit", nullable: false),
                    CustomersEdit = table.Column<bool>(type: "bit", nullable: false),
                    CustomersDelete = table.Column<bool>(type: "bit", nullable: false),
                    ProductsView = table.Column<bool>(type: "bit", nullable: false),
                    ProductsAdd = table.Column<bool>(type: "bit", nullable: false),
                    ProductsEdit = table.Column<bool>(type: "bit", nullable: false),
                    ProductsDelete = table.Column<bool>(type: "bit", nullable: false),
                    StoragesView = table.Column<bool>(type: "bit", nullable: false),
                    StoragesAdd = table.Column<bool>(type: "bit", nullable: false),
                    StoragesEdit = table.Column<bool>(type: "bit", nullable: false),
                    StoragesDelete = table.Column<bool>(type: "bit", nullable: false),
                    DefectivesView = table.Column<bool>(type: "bit", nullable: false),
                    DefectivesAdd = table.Column<bool>(type: "bit", nullable: false),
                    DefectivesEdit = table.Column<bool>(type: "bit", nullable: false),
                    DefectivesDelete = table.Column<bool>(type: "bit", nullable: false),
                    CategoriesView = table.Column<bool>(type: "bit", nullable: false),
                    CategoriesAdd = table.Column<bool>(type: "bit", nullable: false),
                    CategoriesEdit = table.Column<bool>(type: "bit", nullable: false),
                    CategoriesDelete = table.Column<bool>(type: "bit", nullable: false),
                    LocationsView = table.Column<bool>(type: "bit", nullable: false),
                    LocationsAdd = table.Column<bool>(type: "bit", nullable: false),
                    LocationsEdit = table.Column<bool>(type: "bit", nullable: false),
                    LocationsDelete = table.Column<bool>(type: "bit", nullable: false),
                    SuppliersView = table.Column<bool>(type: "bit", nullable: false),
                    SuppliersAdd = table.Column<bool>(type: "bit", nullable: false),
                    SuppliersEdit = table.Column<bool>(type: "bit", nullable: false),
                    SuppliersDelete = table.Column<bool>(type: "bit", nullable: false),
                    RolesView = table.Column<bool>(type: "bit", nullable: false),
                    RolesAdd = table.Column<bool>(type: "bit", nullable: false),
                    RolesEdit = table.Column<bool>(type: "bit", nullable: false),
                    RolesDelete = table.Column<bool>(type: "bit", nullable: false),
                    StaffsView = table.Column<bool>(type: "bit", nullable: false),
                    StaffsAdd = table.Column<bool>(type: "bit", nullable: false),
                    StaffsEdit = table.Column<bool>(type: "bit", nullable: false),
                    StaffsDelete = table.Column<bool>(type: "bit", nullable: false),
                    LogsView = table.Column<bool>(type: "bit", nullable: false),
                    LogsAdd = table.Column<bool>(type: "bit", nullable: false),
                    LogsEdit = table.Column<bool>(type: "bit", nullable: false),
                    LogsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    WarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehousePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseVat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.WarehouseID);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK_Staffs_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductSKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductAvailability = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerFirstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerLastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customers_Staffs_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_Logs_Staffs_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Defectives",
                columns: table => new
                {
                    DefectiveID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DateDeclared = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defectives", x => x.DefectiveID);
                    table.ForeignKey(
                        name: "FK_Defectives_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductLocations",
                columns: table => new
                {
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLocations", x => new { x.ProductID, x.LocationID });
                    table.ForeignKey(
                        name: "FK_ProductLocations_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductLocations_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDetailQuantity = table.Column<int>(type: "int", nullable: false),
                    OrderDetailAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.ProductID, x.OrderID });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_StaffID",
                table: "Customers",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Defectives_ProductID",
                table: "Defectives",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_StaffID",
                table: "Logs",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLocations_LocationID",
                table: "ProductLocations",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierID",
                table: "Products",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_RoleID",
                table: "Staffs",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Defectives");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductLocations");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
