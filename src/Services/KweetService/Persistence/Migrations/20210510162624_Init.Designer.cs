﻿// <auto-generated />
using System;
using Kwetter.Services.KweetService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kwetter.Services.KweetService.Persistence.Migrations
{
    [DbContext(typeof(KweetContext))]
    [Migration("20210510162624_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kwetter.Services.KweetService.Domain.Entities.Follow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FollowerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FollowerId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("Kwetter.Services.KweetService.Domain.Entities.HashTag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("KweetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KweetId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Kwetter.Services.KweetService.Domain.Entities.Kweet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Kweets");
                });

            modelBuilder.Entity("Kwetter.Services.KweetService.Domain.Entities.Like", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("KweetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("KweetId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Kwetter.Services.KweetService.Domain.Entities.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Kwetter.Services.KweetService.Domain.Entities.Follow", b =>
                {
                    b.HasOne("Kwetter.Services.KweetService.Domain.Entities.Profile", "Follower")
                        .WithMany()
                        .HasForeignKey("FollowerId");

                    b.HasOne("Kwetter.Services.KweetService.Domain.Entities.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.Navigation("Follower");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Kwetter.Services.KweetService.Domain.Entities.HashTag", b =>
                {
                    b.HasOne("Kwetter.Services.KweetService.Domain.Entities.Kweet", "Kweet")
                        .WithMany()
                        .HasForeignKey("KweetId");

                    b.Navigation("Kweet");
                });

            modelBuilder.Entity("Kwetter.Services.KweetService.Domain.Entities.Kweet", b =>
                {
                    b.HasOne("Kwetter.Services.KweetService.Domain.Entities.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Kwetter.Services.KweetService.Domain.Entities.Like", b =>
                {
                    b.HasOne("Kwetter.Services.KweetService.Domain.Entities.Kweet", "Kweet")
                        .WithMany("Likes")
                        .HasForeignKey("KweetId");

                    b.HasOne("Kwetter.Services.KweetService.Domain.Entities.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.Navigation("Kweet");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Kwetter.Services.KweetService.Domain.Entities.Kweet", b =>
                {
                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
