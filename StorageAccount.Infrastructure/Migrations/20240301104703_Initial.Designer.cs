﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StorageAccount.Infrastructure.DataContext;

#nullable disable

namespace StorageAccount.Infrastructure.Migrations
{
    [DbContext(typeof(FileContext))]
    [Migration("20240301104703_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StorageAccount.Domain.Model.FileOrBlobUpload", b =>
                {
                    b.Property<int>("BlobFileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlobFileId"));

                    b.Property<Guid>("BlobFileGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BlobFileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("BlobFileId");

                    b.ToTable("FileDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
