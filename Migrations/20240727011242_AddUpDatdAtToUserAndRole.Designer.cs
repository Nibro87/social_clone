﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SocialClone.Models;

#nullable disable

namespace SocialClone.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240727011242_AddUpDatdAtToUserAndRole")]
    partial class AddUpDatdAtToUserAndRole
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SocialClone.Models.Role", b =>
                {
                    b.Property<string>("RoleName")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("role_name");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("RoleName");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SocialClone.Models.User", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("varchar(25)")
                        .HasColumnName("user_name");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UserPass")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_pass");

                    b.HasKey("UserName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserRoles", b =>
                {
                    b.Property<string>("RoleName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(25)");

                    b.HasKey("RoleName", "UserName");

                    b.HasIndex("UserName");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("UserRoles", b =>
                {
                    b.HasOne("SocialClone.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialClone.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
