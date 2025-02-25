﻿// <auto-generated />
using System;
using FinanceTracker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinanceTracker.Migrations
{
    [DbContext(typeof(FinanceContext))]
    partial class FinanceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("FinanceTracker.Classes.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<int>("Category")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsIncome")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 2000m,
                            Category = 4,
                            Date = new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Salary",
                            IsIncome = true,
                            PaymentMethod = "Bank Transfer"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 500m,
                            Category = 0,
                            Date = new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Groceries",
                            IsIncome = false,
                            PaymentMethod = "Credit Card"
                        },
                        new
                        {
                            Id = 3,
                            Amount = 1200m,
                            Category = 1,
                            Date = new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Rent",
                            IsIncome = false,
                            PaymentMethod = "Bank Transfer"
                        },
                        new
                        {
                            Id = 4,
                            Amount = 150m,
                            Category = 2,
                            Date = new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Electricity Bill",
                            IsIncome = false,
                            PaymentMethod = "Debit Card"
                        },
                        new
                        {
                            Id = 5,
                            Amount = 75m,
                            Category = 3,
                            Date = new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Movie Night",
                            IsIncome = false,
                            PaymentMethod = "Cash"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
