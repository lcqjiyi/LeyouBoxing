﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LeyouBoxing;

namespace LeyouBoxing.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170717062942_addCity")]
    partial class addCity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LeyouBoxing.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("GenerateData");

                    b.Property<DateTime>("InputDate");

                    b.Property<string>("JcNo");

                    b.Property<string>("Name");

                    b.Property<string>("Style");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LeyouBoxing.Model.OrderBoxing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<int>("Hashcode");

                    b.Property<int>("LeyouNo");

                    b.Property<int>("OrderId");

                    b.Property<int>("QidiNo");

                    b.Property<string>("ShopName");

                    b.Property<int>("TotalBox");

                    b.Property<int>("TotalQty");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderBoxings");
                });

            modelBuilder.Entity("LeyouBoxing.Model.OrderBoxingItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BoxNumber");

                    b.Property<string>("Color");

                    b.Property<string>("GoodName");

                    b.Property<decimal>("Money");

                    b.Property<int>("OrderBoxingId");

                    b.Property<decimal>("POprice");

                    b.Property<int>("PoQty");

                    b.Property<string>("SKU");

                    b.Property<int>("ShipQty");

                    b.Property<string>("Size");

                    b.Property<string>("Style");

                    b.HasKey("Id");

                    b.HasIndex("OrderBoxingId");

                    b.ToTable("OrderBoxingItem");
                });

            modelBuilder.Entity("LeyouBoxing.Model.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApprovalNo");

                    b.Property<int>("BoxNumber");

                    b.Property<string>("Color");

                    b.Property<string>("EndTime");

                    b.Property<string>("GoodName");

                    b.Property<int>("LeyouNo");

                    b.Property<decimal>("Money");

                    b.Property<int>("OrderId");

                    b.Property<string>("OrderNo");

                    b.Property<decimal>("POprice");

                    b.Property<string>("PoInfo");

                    b.Property<int>("PoQty");

                    b.Property<int>("QidiNo");

                    b.Property<string>("Quarterly");

                    b.Property<string>("SKU");

                    b.Property<int>("ShipQty");

                    b.Property<string>("ShopName");

                    b.Property<string>("Size");

                    b.Property<string>("StartTime");

                    b.Property<string>("Style");

                    b.Property<string>("Suppliercode");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("LeyouBoxing.Model.OrderRow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<int>("LeyouNo");

                    b.Property<int>("OrderId");

                    b.Property<int>("QidiNo");

                    b.Property<int>("Qty1");

                    b.Property<int>("Qty2");

                    b.Property<int>("Qty3");

                    b.Property<int>("Qty4");

                    b.Property<int>("Qty5");

                    b.Property<int>("Qty6");

                    b.Property<int>("Qty7");

                    b.Property<int>("Qty8");

                    b.Property<int>("Qty9");

                    b.Property<string>("ShopName");

                    b.Property<string>("Sylte");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderRows");
                });

            modelBuilder.Entity("LeyouBoxing.Model.OrderSizeGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nmae");

                    b.Property<int>("OrderId");

                    b.Property<string>("Size1");

                    b.Property<string>("Size2");

                    b.Property<string>("Size3");

                    b.Property<string>("Size4");

                    b.Property<string>("Size5");

                    b.Property<string>("Size6");

                    b.Property<string>("Size7");

                    b.Property<string>("Size8");

                    b.Property<string>("Size9");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("OrderSizeGroup");
                });

            modelBuilder.Entity("LeyouBoxing.Model.OrderBoxing", b =>
                {
                    b.HasOne("LeyouBoxing.Model.Order")
                        .WithMany("OrderSummarys")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LeyouBoxing.Model.OrderBoxingItem", b =>
                {
                    b.HasOne("LeyouBoxing.Model.OrderBoxing")
                        .WithMany("OrderBoxingItems")
                        .HasForeignKey("OrderBoxingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LeyouBoxing.Model.OrderItem", b =>
                {
                    b.HasOne("LeyouBoxing.Model.Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LeyouBoxing.Model.OrderRow", b =>
                {
                    b.HasOne("LeyouBoxing.Model.Order")
                        .WithMany("OrderRows")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LeyouBoxing.Model.OrderSizeGroup", b =>
                {
                    b.HasOne("LeyouBoxing.Model.Order")
                        .WithOne("OrderSizeGroup")
                        .HasForeignKey("LeyouBoxing.Model.OrderSizeGroup", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
