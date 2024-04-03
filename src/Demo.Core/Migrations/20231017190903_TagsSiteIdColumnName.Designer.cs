﻿// <auto-generated />
using Demo.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Demo.Core.Migrations
{
    [DbContext(typeof(DemoDbContext))]
    [Migration("20231017190903_TagsSiteIdColumnName")]
    partial class TagsSiteIdColumnName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("Demo.Core.Entities.Site", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("sites", (string)null);
                });

            modelBuilder.Entity("Demo.Core.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<int>("SiteId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("site_id");

                    b.Property<string>("Unit")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("unit");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("tags", (string)null);
                });

            modelBuilder.Entity("Demo.Core.Entities.Tag", b =>
                {
                    b.HasOne("Demo.Core.Entities.Site", "Site")
                        .WithMany("Tags")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("Demo.Core.Entities.Site", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}