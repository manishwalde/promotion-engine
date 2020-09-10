﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi.Data;

namespace webapi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200910171828_Added_Promotion")]
    partial class Added_Promotion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("webapi.Domain.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("ForPrice")
                        .HasColumnType("REAL");

                    b.Property<int>("NumberOfUnit")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PromotionType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SkuIds")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("webapi.Domain.Sku", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Price")
                        .HasColumnType("REAL");

                    b.Property<char>("SkuId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Skus");
                });
#pragma warning restore 612, 618
        }
    }
}