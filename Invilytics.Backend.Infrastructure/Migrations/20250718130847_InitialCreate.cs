using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invilytics.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PASSWORD_HASH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INVESTMENT_TYPES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVESTMENT_TYPES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INVESTMENT_TYPES_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INVESTMENT_SECTORS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    INVESTMENT_TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVESTMENT_SECTORS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INVESTMENT_SECTORS_INVESTMENT_TYPES_INVESTMENT_TYPE_ID",
                        column: x => x.INVESTMENT_TYPE_ID,
                        principalTable: "INVESTMENT_TYPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INVESTMENT_SECTORS_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STOCKS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SYMBOL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    INVESTMENT_SECTOR_ID = table.Column<int>(type: "int", nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOCKS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STOCKS_INVESTMENT_SECTORS_INVESTMENT_SECTOR_ID",
                        column: x => x.INVESTMENT_SECTOR_ID,
                        principalTable: "INVESTMENT_SECTORS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STOCKS_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dividends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReceiveTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dividends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dividends_STOCKS_StockId",
                        column: x => x.StockId,
                        principalTable: "STOCKS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dividends_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PORTFOLIOS",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    STOCK_ID = table.Column<int>(type: "int", nullable: false),
                    VALUE_INVESTED = table.Column<decimal>(type: "decimal(19,6)", precision: 19, scale: 6, nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PORTFOLIOS", x => new { x.USER_ID, x.STOCK_ID });
                    table.ForeignKey(
                        name: "FK_PORTFOLIOS_STOCKS_STOCK_ID",
                        column: x => x.STOCK_ID,
                        principalTable: "STOCKS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PORTFOLIOS_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PURCHASES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STOCK_ID = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(19,6)", precision: 19, scale: 6, nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    PURCHASE_TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PURCHASES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PURCHASES_STOCKS_STOCK_ID",
                        column: x => x.STOCK_ID,
                        principalTable: "STOCKS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PURCHASES_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SALES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STOCK_ID = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(19,6)", precision: 19, scale: 6, nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    SALE_TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SALES_STOCKS_STOCK_ID",
                        column: x => x.STOCK_ID,
                        principalTable: "STOCKS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SALES_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STOCK_QUOTES_HISTORY",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STOCK_ID = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(19,6)", precision: 19, scale: 6, nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOCK_QUOTES_HISTORY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STOCK_QUOTES_HISTORY_STOCKS_STOCK_ID",
                        column: x => x.STOCK_ID,
                        principalTable: "STOCKS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STOCK_QUOTES_HISTORY_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dividends_StockId",
                table: "Dividends",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Dividends_UserId",
                table: "Dividends",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_INVESTMENT_SECTORS_INVESTMENT_TYPE_ID",
                table: "INVESTMENT_SECTORS",
                column: "INVESTMENT_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_INVESTMENT_SECTORS_USER_ID",
                table: "INVESTMENT_SECTORS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_INVESTMENT_TYPES_USER_ID",
                table: "INVESTMENT_TYPES",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PORTFOLIOS_STOCK_ID",
                table: "PORTFOLIOS",
                column: "STOCK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASES_STOCK_ID",
                table: "PURCHASES",
                column: "STOCK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASES_USER_ID",
                table: "PURCHASES",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SALES_STOCK_ID",
                table: "SALES",
                column: "STOCK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SALES_USER_ID",
                table: "SALES",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STOCK_QUOTES_HISTORY_STOCK_ID",
                table: "STOCK_QUOTES_HISTORY",
                column: "STOCK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STOCK_QUOTES_HISTORY_USER_ID",
                table: "STOCK_QUOTES_HISTORY",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STOCKS_INVESTMENT_SECTOR_ID",
                table: "STOCKS",
                column: "INVESTMENT_SECTOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STOCKS_USER_ID",
                table: "STOCKS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_EMAIL",
                table: "USERS",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_ID",
                table: "USERS",
                column: "ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dividends");

            migrationBuilder.DropTable(
                name: "PORTFOLIOS");

            migrationBuilder.DropTable(
                name: "PURCHASES");

            migrationBuilder.DropTable(
                name: "SALES");

            migrationBuilder.DropTable(
                name: "STOCK_QUOTES_HISTORY");

            migrationBuilder.DropTable(
                name: "STOCKS");

            migrationBuilder.DropTable(
                name: "INVESTMENT_SECTORS");

            migrationBuilder.DropTable(
                name: "INVESTMENT_TYPES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
