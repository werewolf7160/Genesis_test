﻿// <auto-generated />
using System;
using Genesis_test.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Genesis_test.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20240710094524_order_and_orderline_to_datacontext")]
    partial class order_and_orderline_to_datacontext
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Genesis_test.Model.Beer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BrewerId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Degree")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("Genesis_test.Model.Brewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Brewers");
                });

            modelBuilder.Entity("Genesis_test.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("WholesalerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Genesis_test.Model.OrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BeerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderLine");
                });

            modelBuilder.Entity("Genesis_test.Model.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BeerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Nb")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WholesalerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("Genesis_test.Model.Wholesaler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Wholesalers");
                });

            modelBuilder.Entity("Genesis_test.Model.OrderLine", b =>
                {
                    b.HasOne("Genesis_test.Model.Order", null)
                        .WithMany("Lines")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("Genesis_test.Model.Order", b =>
                {
                    b.Navigation("Lines");
                });
#pragma warning restore 612, 618
        }
    }
}
