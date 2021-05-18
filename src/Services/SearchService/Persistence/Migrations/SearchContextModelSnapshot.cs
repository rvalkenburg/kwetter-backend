﻿// <auto-generated />
using System;
using Kwetter.Services.SearchService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kwetter.Services.SearchService.Persistence.Migrations
{
    [DbContext(typeof(SearchContext))]
    partial class SearchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kwetter.Services.SearchService.Domain.Entities.Follow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FollowerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FollowerId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Follow");
                });

            modelBuilder.Entity("Kwetter.Services.SearchService.Domain.Entities.Profile", b =>
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

            modelBuilder.Entity("Kwetter.Services.SearchService.Domain.Entities.Follow", b =>
                {
                    b.HasOne("Kwetter.Services.SearchService.Domain.Entities.Profile", "Follower")
                        .WithMany()
                        .HasForeignKey("FollowerId");

                    b.HasOne("Kwetter.Services.SearchService.Domain.Entities.Profile", "Profile")
                        .WithMany("Followers")
                        .HasForeignKey("ProfileId");

                    b.Navigation("Follower");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Kwetter.Services.SearchService.Domain.Entities.Profile", b =>
                {
                    b.Navigation("Followers");
                });
#pragma warning restore 612, 618
        }
    }
}