﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(XitasoranteDBContext))]
    partial class XitasoranteDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Persistence.DatabaseInventory+DBIngredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3f29b668-20bf-4836-b4b9-7f38c0ab4ee7"),
                            Amount = 100.0,
                            Name = "flour",
                            Unit = 0
                        },
                        new
                        {
                            Id = new Guid("9b2507c0-78fc-4571-8728-78c4557f41d2"),
                            Amount = 1.0,
                            Name = "tomato",
                            Unit = 1
                        },
                        new
                        {
                            Id = new Guid("ddd74ae5-1345-495f-85ce-50e3f9733770"),
                            Amount = 75.0,
                            Name = "cheese",
                            Unit = 0
                        },
                        new
                        {
                            Id = new Guid("3d39b5bc-8b25-4bcf-97eb-b7c7071ec457"),
                            Amount = 200.0,
                            Name = "pasta",
                            Unit = 0
                        },
                        new
                        {
                            Id = new Guid("6cd66f6d-4899-45e4-9a31-c68fdb2c4dfa"),
                            Amount = 100.0,
                            Name = "pancetta",
                            Unit = 0
                        },
                        new
                        {
                            Id = new Guid("42ac06dd-ade7-49e4-b5c3-7c6df9ca394e"),
                            Amount = 50.0,
                            Name = "Parmesan cheese",
                            Unit = 0
                        },
                        new
                        {
                            Id = new Guid("d1b73503-1bd8-48e4-945c-4d6806c8565a"),
                            Amount = 2.0,
                            Name = "eggs",
                            Unit = 1
                        },
                        new
                        {
                            Id = new Guid("87d3a2b4-67ab-4773-8301-9c9cf88daa96"),
                            Amount = 200.0,
                            Name = "spaghetti",
                            Unit = 0
                        },
                        new
                        {
                            Id = new Guid("842c6c80-2799-4e36-bef7-7e7f72fc3121"),
                            Amount = 1.0,
                            Name = "tomatoes",
                            Unit = 3
                        },
                        new
                        {
                            Id = new Guid("9da8f35a-e6b3-4c30-b1e5-82c73820fae3"),
                            Amount = 50.0,
                            Name = "olives",
                            Unit = 0
                        },
                        new
                        {
                            Id = new Guid("5b97465e-c4d5-4a10-8af4-8872d8c8984c"),
                            Amount = 25.0,
                            Name = "capers",
                            Unit = 0
                        },
                        new
                        {
                            Id = new Guid("5e8d07cf-b63b-4de9-bbf2-ff9bf628d44b"),
                            Amount = 4.0,
                            Name = "anchovy fillets",
                            Unit = 1
                        },
                        new
                        {
                            Id = new Guid("0c257fe9-6a05-4180-8cbd-a0b2fb7762b5"),
                            Amount = 20.0,
                            Name = "red pepper flakes",
                            Unit = 1
                        },
                        new
                        {
                            Id = new Guid("222f5dc0-fe84-44fa-a28b-9a374cbf95cb"),
                            Amount = 12.0,
                            Name = "ladyfingers",
                            Unit = 1
                        },
                        new
                        {
                            Id = new Guid("5c52832a-d88c-4de0-a3af-b844e3183221"),
                            Amount = 250.0,
                            Name = "mascarpone cheese",
                            Unit = 0
                        },
                        new
                        {
                            Id = new Guid("71ad94e1-161d-42ea-8ebe-ab36a9f0b12b"),
                            Amount = 250.0,
                            Name = "heavy cream",
                            Unit = 2
                        },
                        new
                        {
                            Id = new Guid("01de28c6-c36b-4af4-827c-0a24d9e06a6b"),
                            Amount = 50.0,
                            Name = "sugar",
                            Unit = 0
                        },
                        new
                        {
                            Id = new Guid("bc0a5d53-5b84-49cb-b7f0-89d03c0c6589"),
                            Amount = 100.0,
                            Name = "espresso",
                            Unit = 2
                        },
                        new
                        {
                            Id = new Guid("f8829d14-56fb-4d45-9b9d-87e1cbdca91d"),
                            Amount = 10.0,
                            Name = "cocoa powder",
                            Unit = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
