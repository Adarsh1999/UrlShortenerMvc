﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UrlShortenerMvc.Data;

#nullable disable

namespace UrlShortenerMvc.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250529160633_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UrlShortenerMvc.Models.Click", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Agent")
                        .HasColumnType("text");

                    b.Property<DateTime>("At")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Referer")
                        .HasColumnType("text");

                    b.Property<int>("ShortUrlId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ShortUrlId");

                    b.ToTable("Clicks");
                });

            modelBuilder.Entity("UrlShortenerMvc.Models.ShortUrl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClickCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ShortCode")
                        .IsUnique();

                    b.ToTable("ShortUrls");
                });

            modelBuilder.Entity("UrlShortenerMvc.Models.Click", b =>
                {
                    b.HasOne("UrlShortenerMvc.Models.ShortUrl", "ShortUrl")
                        .WithMany("Clicks")
                        .HasForeignKey("ShortUrlId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShortUrl");
                });

            modelBuilder.Entity("UrlShortenerMvc.Models.ShortUrl", b =>
                {
                    b.Navigation("Clicks");
                });
#pragma warning restore 612, 618
        }
    }
}
