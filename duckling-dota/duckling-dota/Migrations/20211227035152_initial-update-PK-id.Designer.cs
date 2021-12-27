﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace duckling_dota.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211227035152_initial-update-PK-id")]
    partial class initialupdatePKid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("duckling_dota.Data.Models.DucklingDoter", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("DotaId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DucklingDoters");
                });
#pragma warning restore 612, 618
        }
    }
}
