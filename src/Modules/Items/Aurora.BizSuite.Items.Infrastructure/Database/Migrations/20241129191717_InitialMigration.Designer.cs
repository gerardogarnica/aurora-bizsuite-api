﻿// <auto-generated />
using System;
using Aurora.BizSuite.Items.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aurora.BizSuite.Items.Infrastructure.Database.Migrations
{
    [DbContext(typeof(ItemsDbContext))]
    [Migration("20241129191717_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Items")
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Brands.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BrandId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LogoUri")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK_Brand");

                    b.ToTable("Brands", "Items");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Categories.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CategoryId");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ParentPath")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK_Category");

                    b.HasIndex("ParentId")
                        .HasDatabaseName("IX_Category_ParentId");

                    b.ToTable("Categories", "Items");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ItemId");

                    b.Property<string>("AlternativeCode")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<string>("Tags")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK_Item");

                    b.HasIndex("BrandId")
                        .HasDatabaseName("IX_Item_BrandId");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("IX_Item_CategoryId");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("UK_Item");

                    b.ToTable("Items", "Items");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.ItemCategory", b =>
                {
                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ItemId", "CategoryId")
                        .HasName("PK_ItemCategory");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("IX_ItemCategory_CategoryId");

                    b.HasIndex("ItemId")
                        .HasDatabaseName("IX_ItemCategory_ItemId");

                    b.ToTable("ItemCategories", "Items");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.ItemDescription", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ItemDescriptionId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id")
                        .HasName("PK_ItemDescription");

                    b.HasIndex("ItemId")
                        .HasDatabaseName("IX_ItemDescription_ItemId");

                    b.HasIndex("ItemId", "Type")
                        .IsUnique()
                        .HasDatabaseName("UK_ItemDescription");

                    b.ToTable("ItemDescriptions", "Items");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.ItemResource", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ItemResourceId");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id")
                        .HasName("PK_ItemResource");

                    b.HasIndex("ItemId")
                        .HasDatabaseName("IX_ItemResource_ItemId");

                    b.ToTable("ItemResources", "Items");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.ItemUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ItemUnitId");

                    b.Property<bool>("AvailableForDispatch")
                        .HasColumnType("bit");

                    b.Property<bool>("AvailableForReceipt")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEditable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("bit");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UnitId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UnitId");

                    b.Property<bool>("UseDecimals")
                        .HasColumnType("bit");

                    b.HasKey("Id")
                        .HasName("PK_ItemUnit");

                    b.HasIndex("ItemId")
                        .HasDatabaseName("IX_ItemUnit_ItemId");

                    b.HasIndex("UnitId")
                        .HasDatabaseName("IX_ItemUnit_UnitId");

                    b.HasIndex("ItemId", "UnitId")
                        .IsUnique()
                        .HasDatabaseName("UK_ItemUnit");

                    b.ToTable("ItemUnits", "Items");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.RelatedItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RelatedItemId");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RelatedItemId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RelatedId");

                    b.HasKey("Id")
                        .HasName("PK_RelatedItem");

                    b.HasIndex("ItemId")
                        .HasDatabaseName("IX_RelatedItem_ItemId");

                    b.ToTable("RelatedItems", "Items");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Units.UnitOfMeasurement", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UnitId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK_UnitOfMeasurement");

                    b.ToTable("Units", "Items");
                });

            modelBuilder.Entity("Aurora.Framework.Infrastructure.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("InboxId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Error")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK_InboxMessages");

                    b.ToTable("InboxMessages", "Items");
                });

            modelBuilder.Entity("Aurora.Framework.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OutboxId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Error")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK_OutboxMessages");

                    b.ToTable("OutboxMessages", "Items");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Categories.Category", b =>
                {
                    b.HasOne("Aurora.BizSuite.Items.Domain.Categories.Category", null)
                        .WithMany("Childs")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.Item", b =>
                {
                    b.HasOne("Aurora.BizSuite.Items.Domain.Brands.Brand", "Brand")
                        .WithMany("Items")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aurora.BizSuite.Items.Domain.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.ItemCategory", b =>
                {
                    b.HasOne("Aurora.BizSuite.Items.Domain.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aurora.BizSuite.Items.Domain.Items.Item", null)
                        .WithMany("Categories")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.ItemDescription", b =>
                {
                    b.HasOne("Aurora.BizSuite.Items.Domain.Items.Item", null)
                        .WithMany("Descriptions")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.ItemResource", b =>
                {
                    b.HasOne("Aurora.BizSuite.Items.Domain.Items.Item", null)
                        .WithMany("Resources")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.ItemUnit", b =>
                {
                    b.HasOne("Aurora.BizSuite.Items.Domain.Items.Item", null)
                        .WithMany("Units")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aurora.BizSuite.Items.Domain.Units.UnitOfMeasurement", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.RelatedItem", b =>
                {
                    b.HasOne("Aurora.BizSuite.Items.Domain.Items.Item", null)
                        .WithMany("RelatedItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Brands.Brand", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Categories.Category", b =>
                {
                    b.Navigation("Childs");
                });

            modelBuilder.Entity("Aurora.BizSuite.Items.Domain.Items.Item", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Descriptions");

                    b.Navigation("RelatedItems");

                    b.Navigation("Resources");

                    b.Navigation("Units");
                });
#pragma warning restore 612, 618
        }
    }
}