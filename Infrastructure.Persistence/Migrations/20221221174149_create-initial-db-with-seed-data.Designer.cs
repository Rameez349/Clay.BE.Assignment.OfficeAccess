﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(OfficeAccessDbContext))]
    [Migration("20221221174149_create-initial-db-with-seed-data")]
    partial class createinitialdbwithseeddata
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.AccessHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AccessGranted")
                        .HasColumnType("bit");

                    b.Property<int>("DoorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoorId");

                    b.HasIndex("UserId");

                    b.ToTable("AccessHistory", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.AccessLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccessLevel", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "General"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Special"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Door", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Door", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Entrance",
                            OfficeId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Storage",
                            OfficeId = 1
                        });
                });

            modelBuilder.Entity("Domain.Entities.DoorAccessLevel", b =>
                {
                    b.Property<int>("AccessLevelId")
                        .HasColumnType("int");

                    b.Property<int>("DoorId")
                        .HasColumnType("int");

                    b.HasKey("AccessLevelId", "DoorId");

                    b.HasIndex("DoorId");

                    b.ToTable("DoorAccessLevel", (string)null);

                    b.HasData(
                        new
                        {
                            AccessLevelId = 1,
                            DoorId = 1
                        },
                        new
                        {
                            AccessLevelId = 2,
                            DoorId = 2
                        });
                });

            modelBuilder.Entity("Domain.Entities.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Office", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Office One"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Office Two"
                        });
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AllowHistoryView")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AllowHistoryView = true,
                            Name = "Rameez"
                        },
                        new
                        {
                            Id = 2,
                            AllowHistoryView = true,
                            Name = "Darjan"
                        },
                        new
                        {
                            Id = 3,
                            AllowHistoryView = false,
                            Name = "Lucas"
                        });
                });

            modelBuilder.Entity("Domain.Entities.UserAccessLevel", b =>
                {
                    b.Property<int>("AccessLevelId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AccessLevelId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAccessLevel", (string)null);

                    b.HasData(
                        new
                        {
                            AccessLevelId = 1,
                            UserId = 1
                        },
                        new
                        {
                            AccessLevelId = 2,
                            UserId = 1
                        },
                        new
                        {
                            AccessLevelId = 1,
                            UserId = 2
                        },
                        new
                        {
                            AccessLevelId = 2,
                            UserId = 2
                        },
                        new
                        {
                            AccessLevelId = 1,
                            UserId = 3
                        });
                });

            modelBuilder.Entity("Domain.Entities.AccessHistory", b =>
                {
                    b.HasOne("Domain.Entities.Door", "Door")
                        .WithMany()
                        .HasForeignKey("DoorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Door");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Door", b =>
                {
                    b.HasOne("Domain.Entities.Office", "Office")
                        .WithMany("Doors")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("Domain.Entities.DoorAccessLevel", b =>
                {
                    b.HasOne("Domain.Entities.AccessLevel", "AccessLevel")
                        .WithMany("DoorAccessLevels")
                        .HasForeignKey("AccessLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Door", "Door")
                        .WithMany("DoorAccessLevels")
                        .HasForeignKey("DoorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccessLevel");

                    b.Navigation("Door");
                });

            modelBuilder.Entity("Domain.Entities.UserAccessLevel", b =>
                {
                    b.HasOne("Domain.Entities.AccessLevel", "AccessLevel")
                        .WithMany("UserAccessLevels")
                        .HasForeignKey("AccessLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UserAccessLevels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccessLevel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.AccessLevel", b =>
                {
                    b.Navigation("DoorAccessLevels");

                    b.Navigation("UserAccessLevels");
                });

            modelBuilder.Entity("Domain.Entities.Door", b =>
                {
                    b.Navigation("DoorAccessLevels");
                });

            modelBuilder.Entity("Domain.Entities.Office", b =>
                {
                    b.Navigation("Doors");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("UserAccessLevels");
                });
#pragma warning restore 612, 618
        }
    }
}
