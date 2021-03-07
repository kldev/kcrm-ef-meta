﻿// <auto-generated />
using System;
using KCrm.Data.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KCrm.Data.__Migrations.Tags
{
    [DbContext(typeof(TagContext))]
    [Migration("20210131105432_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("tag")
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("KCrm.Core.Entity.Tags.TagEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<Guid?>("TagGroupId")
                        .HasColumnType("uuid")
                        .HasColumnName("tag_group_id");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id")
                        .HasName("pk_tags");

                    b.HasIndex("TagGroupId")
                        .HasDatabaseName("ix_tags_tag_group_id");

                    b.HasIndex("Name", "TagGroupId")
                        .IsUnique()
                        .HasDatabaseName("ix_tags_name_tag_group_id");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("KCrm.Core.Entity.Tags.TagGroupEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id")
                        .HasName("pk_tag_groups");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_tag_groups_name");

                    b.ToTable("tag_groups");
                });

            modelBuilder.Entity("KCrm.Core.Entity.Tags.TagEntity", b =>
                {
                    b.HasOne("KCrm.Core.Entity.Tags.TagGroupEntity", "TagGroupEntity")
                        .WithMany("Tags")
                        .HasForeignKey("TagGroupId")
                        .HasConstraintName("fk_tags_tag_groups_tag_group_entity_id")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("TagGroupEntity");
                });

            modelBuilder.Entity("KCrm.Core.Entity.Tags.TagGroupEntity", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}