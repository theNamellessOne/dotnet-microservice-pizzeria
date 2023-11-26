﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PizzaService.Data;

#nullable disable

namespace PizzaService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231125101151_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PizzaService.Models.Pizza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("BasePrice")
                        .HasColumnType("double");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Pizza");
                });

            modelBuilder.Entity("PizzaService.Models.PizzaBorderOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PizzaId")
                        .HasColumnType("int");

                    b.Property<double>("PriceModifier")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("PizzaId");

                    b.ToTable("PizzaBorderOption");
                });

            modelBuilder.Entity("PizzaService.Models.PizzaSizeOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PizzaId")
                        .HasColumnType("int");

                    b.Property<double>("PriceModifier")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("PizzaId");

                    b.ToTable("PizzaSizeOption");
                });

            modelBuilder.Entity("PizzaService.Models.PizzaBorderOption", b =>
                {
                    b.HasOne("PizzaService.Models.Pizza", "Pizza")
                        .WithMany("BorderOptions")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("PizzaService.Models.PizzaSizeOption", b =>
                {
                    b.HasOne("PizzaService.Models.Pizza", "Pizza")
                        .WithMany("SizeOptions")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("PizzaService.Models.Pizza", b =>
                {
                    b.Navigation("BorderOptions");

                    b.Navigation("SizeOptions");
                });
#pragma warning restore 612, 618
        }
    }
}
