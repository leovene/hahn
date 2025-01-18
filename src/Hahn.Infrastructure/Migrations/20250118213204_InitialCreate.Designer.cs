﻿// <auto-generated />
using System;
using Hahn.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hahn.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250118213204_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hahn.Domain.Entities.ArtObjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArtistDisplayName")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Culture")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Dimensions")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsPublicDomain")
                        .HasColumnType("bit");

                    b.Property<string>("Medium")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("MetadataDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ObjectDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int");

                    b.Property<string>("ObjectURL")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Period")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("ArtObjects");
                });

            modelBuilder.Entity("Hahn.Domain.Entities.ConstituentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtObjectId")
                        .HasColumnType("int");

                    b.Property<int>("ConstituentId")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Role")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ULANURL")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("WikidataURL")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("ArtObjectId");

                    b.ToTable("Constituents");
                });

            modelBuilder.Entity("Hahn.Domain.Entities.DepartmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Hahn.Domain.Entities.ImageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtObjectId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("ArtObjectId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Hahn.Domain.Entities.TagEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AATURL")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("ArtObjectId")
                        .HasColumnType("int");

                    b.Property<string>("Term")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("WikidataURL")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("ArtObjectId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Hahn.Domain.Entities.ArtObjectEntity", b =>
                {
                    b.HasOne("Hahn.Domain.Entities.DepartmentEntity", null)
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .HasPrincipalKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hahn.Domain.Entities.ConstituentEntity", b =>
                {
                    b.HasOne("Hahn.Domain.Entities.ArtObjectEntity", "ArtObject")
                        .WithMany("Constituents")
                        .HasForeignKey("ArtObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArtObject");
                });

            modelBuilder.Entity("Hahn.Domain.Entities.ImageEntity", b =>
                {
                    b.HasOne("Hahn.Domain.Entities.ArtObjectEntity", "ArtObject")
                        .WithMany("Images")
                        .HasForeignKey("ArtObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArtObject");
                });

            modelBuilder.Entity("Hahn.Domain.Entities.TagEntity", b =>
                {
                    b.HasOne("Hahn.Domain.Entities.ArtObjectEntity", "ArtObject")
                        .WithMany("Tags")
                        .HasForeignKey("ArtObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArtObject");
                });

            modelBuilder.Entity("Hahn.Domain.Entities.ArtObjectEntity", b =>
                {
                    b.Navigation("Constituents");

                    b.Navigation("Images");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}