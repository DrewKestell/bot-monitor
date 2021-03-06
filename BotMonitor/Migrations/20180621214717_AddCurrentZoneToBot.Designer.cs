﻿// <auto-generated />
using BotMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BotMonitor.Migrations
{
    [DbContext(typeof(BotContext))]
    [Migration("20180621214717_AddCurrentZoneToBot")]
    partial class AddCurrentZoneToBot
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BotMonitor.Models.Bot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AI")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("AccountPassword")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("AccountUsername")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Ammo")
                        .HasMaxLength(32);

                    b.Property<string>("CurrentState")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("CurrentZone")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Drink")
                        .HasMaxLength(32);

                    b.Property<string>("ExcludedMobs")
                        .HasMaxLength(128);

                    b.Property<string>("Food")
                        .HasMaxLength(32);

                    b.Property<string>("HotSpot")
                        .HasMaxLength(64);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<byte>("Level");

                    b.Property<byte>("MaxLevel");

                    b.Property<byte>("MinLevel");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<string>("RealmName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("Name", "RealmName")
                        .IsUnique();

                    b.ToTable("Bots");
                });

            modelBuilder.Entity("BotMonitor.Models.Instruction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BotId");

                    b.Property<string>("Command");

                    b.HasKey("Id");

                    b.HasIndex("BotId")
                        .IsUnique();

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("BotMonitor.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<bool>("ProfilePrivate");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BotMonitor.Models.Bot", b =>
                {
                    b.HasOne("BotMonitor.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BotMonitor.Models.Instruction", b =>
                {
                    b.HasOne("BotMonitor.Models.Bot", "Bot")
                        .WithMany()
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
