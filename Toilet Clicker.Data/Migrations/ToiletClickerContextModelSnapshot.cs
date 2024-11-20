﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Toilet_Clicker.Data;

#nullable disable

namespace Toilet_Clicker.Data.Migrations
{
    [DbContext(typeof(ToiletClickerContext))]
    partial class ToiletClickerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Toilet_Clicker.Core.Domain.FileToDatabase", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LocationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ToiletID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.ToTable("FilesToDatabase");
                });

            modelBuilder.Entity("Toilet_Clicker.Core.Domain.Location", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LocationDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationType")
                        .HasColumnType("int");

                    b.Property<DateTime>("LocationWasMade")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Toilet_Clicker.Core.Domain.Toilet", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Power")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("Score")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("Speed")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("ToiletName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ToiletWasBorn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Toilets");
                });
#pragma warning restore 612, 618
        }
    }
}
