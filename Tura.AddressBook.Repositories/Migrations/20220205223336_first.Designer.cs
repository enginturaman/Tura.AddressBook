﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tura.AddressBook.Repositories;

namespace Tura.AddressBook.Repositories.Migrations
{
    [DbContext(typeof(AddressBookContext))]
    [Migration("20220205223336_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Tura.AddressBook.Domain.Entities.LocationDmo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<double>("Lat")
                        .HasColumnType("double precision");

                    b.Property<double>("Lon")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Tura.AddressBook.Domain.Entities.LocationReportDmo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("LocationReports");
                });

            modelBuilder.Entity("Tura.AddressBook.Domain.Entities.LocationReportValueDmo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Lat")
                        .HasColumnType("double precision");

                    b.Property<Guid>("LocationReportId")
                        .HasColumnType("uuid");

                    b.Property<double>("Lon")
                        .HasColumnType("double precision");

                    b.Property<int>("PersonalCount")
                        .HasColumnType("integer");

                    b.Property<int>("PhoneNumberCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LocationReportId");

                    b.ToTable("LocationReportValues");
                });

            modelBuilder.Entity("Tura.AddressBook.Domain.Entities.PersonalContactDmo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PersonelId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("PersonelId");

                    b.ToTable("PersonalContacts");
                });

            modelBuilder.Entity("Tura.AddressBook.Domain.Entities.PersonalDmo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Company")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SurName")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Personals");
                });

            modelBuilder.Entity("Tura.AddressBook.Domain.Entities.LocationReportValueDmo", b =>
                {
                    b.HasOne("Tura.AddressBook.Domain.Entities.LocationReportDmo", "LocationReport")
                        .WithMany("Values")
                        .HasForeignKey("LocationReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocationReport");
                });

            modelBuilder.Entity("Tura.AddressBook.Domain.Entities.PersonalContactDmo", b =>
                {
                    b.HasOne("Tura.AddressBook.Domain.Entities.LocationDmo", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tura.AddressBook.Domain.Entities.PersonalDmo", "Personel")
                        .WithMany("Contacts")
                        .HasForeignKey("PersonelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Personel");
                });

            modelBuilder.Entity("Tura.AddressBook.Domain.Entities.LocationReportDmo", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("Tura.AddressBook.Domain.Entities.PersonalDmo", b =>
                {
                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
