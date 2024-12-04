using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.BizSuite.Items.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Items");

            migrationBuilder.CreateTable(
                name: "Brands",
                schema: "Items",
                columns: table => new
                {
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LogoUri = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "Items",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ParentPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Items",
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "InboxMessages",
                schema: "Items",
                columns: table => new
                {
                    InboxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    OccurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxMessages", x => x.InboxId);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "Items",
                columns: table => new
                {
                    OutboxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    OccurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.OutboxId);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "Items",
                columns: table => new
                {
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasurement", x => x.UnitId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "Items",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AlternativeCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Items",
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Items",
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                schema: "Items",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategory", x => new { x.ItemId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ItemCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Items",
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemCategories_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Items",
                        principalTable: "Items",
                        principalColumn: "ItemId");
                });

            migrationBuilder.CreateTable(
                name: "ItemDescriptions",
                schema: "Items",
                columns: table => new
                {
                    ItemDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDescription", x => x.ItemDescriptionId);
                    table.ForeignKey(
                        name: "FK_ItemDescriptions_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Items",
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemResources",
                schema: "Items",
                columns: table => new
                {
                    ItemResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemResource", x => x.ItemResourceId);
                    table.ForeignKey(
                        name: "FK_ItemResources_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Items",
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemUnits",
                schema: "Items",
                columns: table => new
                {
                    ItemUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    AvailableForReceipt = table.Column<bool>(type: "bit", nullable: false),
                    AvailableForDispatch = table.Column<bool>(type: "bit", nullable: false),
                    UseDecimals = table.Column<bool>(type: "bit", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemUnit", x => x.ItemUnitId);
                    table.ForeignKey(
                        name: "FK_ItemUnits_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Items",
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "Items",
                        principalTable: "Units",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelatedItems",
                schema: "Items",
                columns: table => new
                {
                    RelatedItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedItem", x => x.RelatedItemId);
                    table.ForeignKey(
                        name: "FK_RelatedItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Items",
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                schema: "Items",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategory_CategoryId",
                schema: "Items",
                table: "ItemCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategory_ItemId",
                schema: "Items",
                table: "ItemCategories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDescription_ItemId",
                schema: "Items",
                table: "ItemDescriptions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "UK_ItemDescription",
                schema: "Items",
                table: "ItemDescriptions",
                columns: new[] { "ItemId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemResource_ItemId",
                schema: "Items",
                table: "ItemResources",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_BrandId",
                schema: "Items",
                table: "Items",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoryId",
                schema: "Items",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "UK_Item",
                schema: "Items",
                table: "Items",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnit_ItemId",
                schema: "Items",
                table: "ItemUnits",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnit_UnitId",
                schema: "Items",
                table: "ItemUnits",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "UK_ItemUnit",
                schema: "Items",
                table: "ItemUnits",
                columns: new[] { "ItemId", "UnitId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItem_ItemId",
                schema: "Items",
                table: "RelatedItems",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboxMessages",
                schema: "Items");

            migrationBuilder.DropTable(
                name: "ItemCategories",
                schema: "Items");

            migrationBuilder.DropTable(
                name: "ItemDescriptions",
                schema: "Items");

            migrationBuilder.DropTable(
                name: "ItemResources",
                schema: "Items");

            migrationBuilder.DropTable(
                name: "ItemUnits",
                schema: "Items");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "Items");

            migrationBuilder.DropTable(
                name: "RelatedItems",
                schema: "Items");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "Items");

            migrationBuilder.DropTable(
                name: "Items",
                schema: "Items");

            migrationBuilder.DropTable(
                name: "Brands",
                schema: "Items");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Items");
        }
    }
}
