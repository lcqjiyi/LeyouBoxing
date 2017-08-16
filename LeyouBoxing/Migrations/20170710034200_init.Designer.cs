using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LeyouBoxing;

namespace LeyouBoxing.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170710034200_init")]
    partial class init
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

                    b.Property<DateTime>("InputDate");

                    b.Property<string>("JcNo");

                    b.Property<string>("Name");

                    b.Property<string>("Style");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LeyouBoxing.Model.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApprovalNo");

                    b.Property<string>("Color");

                    b.Property<string>("EndTime");

                    b.Property<string>("GoodName");

                    b.Property<int>("LeyouNo");

                    b.Property<int?>("OrderId");

                    b.Property<string>("OrderNo");

                    b.Property<decimal>("POprice");

                    b.Property<string>("PoInfo");

                    b.Property<int>("PoQty");

                    b.Property<int>("QidiNo");

                    b.Property<string>("Quarterly");

                    b.Property<string>("SKU");

                    b.Property<string>("ShopName");

                    b.Property<string>("Size");

                    b.Property<string>("StartTime");

                    b.Property<string>("Suppliercode");

                    b.Property<string>("Sylte");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("LeyouBoxing.Model.OrderItem", b =>
                {
                    b.HasOne("LeyouBoxing.Model.Order")
                        .WithMany("OrderItem")
                        .HasForeignKey("OrderId");
                });
        }
    }
}
