﻿// <auto-generated />
using System;
using BinanceStatistic.DAL.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BinanceStatistic.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("BinanceStatistic.DAL.Entities.Currency", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("BinanceStatistic.DAL.Entities.Statistic", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrencyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Long")
                        .HasColumnType("int");

                    b.Property<int>("Short")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("BinanceStatistic.DAL.Entities.Subscribe", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Minutes")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subscribes");
                });

            modelBuilder.Entity("BinanceStatistic.DAL.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TelegramId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BinanceStatistic.DAL.Entities.UserSubscribe", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubscribeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SubscribeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSubscribes");
                });

            modelBuilder.Entity("BinanceStatistic.DAL.Entities.Statistic", b =>
                {
                    b.HasOne("BinanceStatistic.DAL.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("BinanceStatistic.DAL.Entities.UserSubscribe", b =>
                {
                    b.HasOne("BinanceStatistic.DAL.Entities.Subscribe", "Subscribe")
                        .WithMany("UserSubscribes")
                        .HasForeignKey("SubscribeId");

                    b.HasOne("BinanceStatistic.DAL.Entities.User", "User")
                        .WithMany("UserSubscribes")
                        .HasForeignKey("UserId");

                    b.Navigation("Subscribe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BinanceStatistic.DAL.Entities.Subscribe", b =>
                {
                    b.Navigation("UserSubscribes");
                });

            modelBuilder.Entity("BinanceStatistic.DAL.Entities.User", b =>
                {
                    b.Navigation("UserSubscribes");
                });
#pragma warning restore 612, 618
        }
    }
}
