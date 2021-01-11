﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PromoCodeProject.Models;

namespace PromoCodeProject.Migrations
{
    [DbContext(typeof(PromoDbContext))]
    [Migration("20210107183007_clientSecretsTable")]
    partial class clientSecretsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PromoCodeProject.Models.ClientSecrets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppKey")
                        .HasMaxLength(1000);

                    b.Property<string>("AppValue")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("ClientSecrets");
                });

            modelBuilder.Entity("PromoCodeProject.Models.PromoCodes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activated");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("PromoCode")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("PromoCodes");
                });
#pragma warning restore 612, 618
        }
    }
}