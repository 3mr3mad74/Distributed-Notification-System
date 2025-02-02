﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CommunicationChannels")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Metadata")
                        .HasColumnType("longtext");

                    b.Property<string>("NotificationType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CreatedDate");

                    b.ToTable("Notifications", (string)null);

                    b.HasDiscriminator<string>("NotificationType").HasValue("Notification");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.EmailNotification", b =>
                {
                    b.HasBaseType("Domain.Entities.Notification");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasDiscriminator().HasValue("Email");
                });

            modelBuilder.Entity("Domain.Entities.RealTimeNotification", b =>
                {
                    b.HasBaseType("Domain.Entities.Notification");

                    b.Property<bool>("IsRead")
                        .HasColumnType("tinyint(1)");

                    b.HasDiscriminator().HasValue("RealTime");
                });

            modelBuilder.Entity("Domain.Entities.SMSNotification", b =>
                {
                    b.HasBaseType("Domain.Entities.Notification");

                    b.Property<string>("ToPhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasDiscriminator().HasValue("SMS");
                });
#pragma warning restore 612, 618
        }
    }
}
