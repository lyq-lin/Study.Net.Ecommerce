﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using User.Infrastructure.DbContexts;

#nullable disable

namespace User.Infrastructure.Migrations
{
    [DbContext(typeof(UserDbContext))]
    partial class UserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("User.Domain.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("JwtVersion")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("bytea");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("T_User", (string)null);
                });

            modelBuilder.Entity("User.Domain.Entity.UserAccessFail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailCount")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LockEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<bool>("isLockOut")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("T_UserAccessFail", (string)null);
                });

            modelBuilder.Entity("User.Domain.Entity.UserLoginHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("T_UserLoginHistory", (string)null);
                });

            modelBuilder.Entity("User.Domain.Entity.User", b =>
                {
                    b.OwnsOne("User.Domain.ValueObject.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("PhoneNumberValue")
                                .IsRequired()
                                .HasMaxLength(20)
                                .IsUnicode(false)
                                .HasColumnType("character varying(20)");

                            b1.Property<int>("RegionNumber")
                                .HasMaxLength(5)
                                .IsUnicode(false)
                                .HasColumnType("integer");

                            b1.HasKey("UserId");

                            b1.ToTable("T_User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("User.Domain.Entity.UserAccessFail", b =>
                {
                    b.HasOne("User.Domain.Entity.User", "User")
                        .WithOne("UserAccessFail")
                        .HasForeignKey("User.Domain.Entity.UserAccessFail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("User.Domain.Entity.UserLoginHistory", b =>
                {
                    b.OwnsOne("User.Domain.ValueObject.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<long>("UserLoginHistoryId")
                                .HasColumnType("bigint");

                            b1.Property<string>("PhoneNumberValue")
                                .IsRequired()
                                .HasMaxLength(20)
                                .IsUnicode(false)
                                .HasColumnType("character varying(20)");

                            b1.Property<int>("RegionNumber")
                                .HasColumnType("integer");

                            b1.HasKey("UserLoginHistoryId");

                            b1.ToTable("T_UserLoginHistory");

                            b1.WithOwner()
                                .HasForeignKey("UserLoginHistoryId");
                        });

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("User.Domain.Entity.User", b =>
                {
                    b.Navigation("UserAccessFail")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
