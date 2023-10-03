﻿// <auto-generated />
using System;
using BLG.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BLG.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231003151408_thisisaddedmigration23")]
    partial class thisisaddedmigration23
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BLG.Domin.CategoryBlogAgg.category", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("urlhadle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("category");
                });

            modelBuilder.Entity("BLG.Domin.PostBlogAgg.Postblog", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cotent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isvisible")
                        .HasColumnType("bit");

                    b.Property<string>("shorttitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("urlhandler")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("postblog");
                });

            modelBuilder.Entity("BLG.Domin.uploadImage.uploadimg", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("fileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tiltle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("time")
                        .HasColumnType("datetime2");

                    b.Property<string>("urlhandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("uplaodimages", (string)null);
                });

            modelBuilder.Entity("Postblogcategory", b =>
                {
                    b.Property<Guid>("Categoriesid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("postsid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Categoriesid", "postsid");

                    b.HasIndex("postsid");

                    b.ToTable("Postblogcategory");
                });

            modelBuilder.Entity("Postblogcategory", b =>
                {
                    b.HasOne("BLG.Domin.CategoryBlogAgg.category", null)
                        .WithMany()
                        .HasForeignKey("Categoriesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BLG.Domin.PostBlogAgg.Postblog", null)
                        .WithMany()
                        .HasForeignKey("postsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
