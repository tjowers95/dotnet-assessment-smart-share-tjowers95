﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Server;

namespace Server.Migrations
{
    [DbContext(typeof(SmartShareContext))]
    [Migration("20190208035602_A2")]
    partial class A2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Server.SmartShareFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DownloadCount")
                        .HasColumnName("download_count");

                    b.Property<string>("Expiration")
                        .IsRequired()
                        .HasColumnName("expiration");

                    b.Property<byte[]>("FileData")
                        .IsRequired()
                        .HasColumnName("file_data");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnName("file_name");

                    b.Property<int>("MaximumDownloads")
                        .HasColumnName("maximum_downloads");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("smart_share_file");
                });
#pragma warning restore 612, 618
        }
    }
}
