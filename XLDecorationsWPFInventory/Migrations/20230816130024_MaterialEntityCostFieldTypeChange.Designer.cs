﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XLDecorationsWPFInventory.Data;

#nullable disable

namespace XLDecorationsWPFInventory.Migrations;

    [DbContext(typeof(AppDbContext))]
    [Migration("20230816130024_MaterialEntityCostFieldTypeChange")]
    partial class MaterialEntityCostFieldTypeChange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("XLDecorationsWPFInventory.Data.Models.CustomerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("XLDecorationsWPFInventory.Data.Models.MaterialTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MaterialTypes");
                });

            modelBuilder.Entity("XLDecorationsWPFInventory.Data.Models.MaterialsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Cost")
                        .HasColumnType("REAL");

                    b.Property<int>("MaterialTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Qty")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MaterialTypeId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("XLDecorationsWPFInventory.Data.Models.OrderItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("XLDecorationsWPFInventory.Data.Models.OrderItemMaterialEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Qty")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrderItemId");

                    b.ToTable("OrderItemsMaterials");
                });

            modelBuilder.Entity("XLDecorationsWPFInventory.Data.Models.OrdersEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("XLDecorationsWPFInventory.Data.Models.MaterialsEntity", b =>
                {
                    b.HasOne("XLDecorationsWPFInventory.Data.Models.MaterialTypeEntity", "MaterialType")
                        .WithMany()
                        .HasForeignKey("MaterialTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaterialType");
                });

            modelBuilder.Entity("XLDecorationsWPFInventory.Data.Models.OrderItemEntity", b =>
                {
                    b.HasOne("XLDecorationsWPFInventory.Data.Models.OrdersEntity", "Orders")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("XLDecorationsWPFInventory.Data.Models.OrderItemMaterialEntity", b =>
                {
                    b.HasOne("XLDecorationsWPFInventory.Data.Models.OrderItemEntity", "OrderItem")
                        .WithMany()
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("XLDecorationsWPFInventory.Data.Models.OrdersEntity", b =>
                {
                    b.HasOne("XLDecorationsWPFInventory.Data.Models.CustomerEntity", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });
#pragma warning restore 612, 618
        }
    }
