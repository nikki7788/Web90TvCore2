﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Web90TvCore2.Models;

namespace Web90TvCore2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190914133629_Mig11-MetaTag_propertis_added_to_newsTble")]
    partial class Mig11MetaTag_propertis_added_to_newsTble
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Web90TvCore2.Models.Advertise", b =>
                {
                    b.Property<int>("AdId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte>("AdvLocation");

                    b.Property<byte>("Flag");

                    b.Property<string>("FromDate")
                        .IsRequired();

                    b.Property<string>("GifPath")
                        .IsRequired();

                    b.Property<string>("Link")
                        .IsRequired();

                    b.Property<string>("ToDate")
                        .IsRequired();

                    b.HasKey("AdId");

                    b.ToTable("Advertises");
                });

            modelBuilder.Entity("Web90TvCore2.Models.ApplicationRoles", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<string>("RoleLevel");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Web90TvCore2.Models.ApplicationUsers", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("BirthDayDate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<byte>("Gender");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserImagePath");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Web90TvCore2.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(800);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Web90TvCore2.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DisLikeCount");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("IP");

                    b.Property<int>("LikeCount");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<int>("NewsId");

                    b.Property<int>("ReplyID");

                    b.Property<string>("commentDate");

                    b.Property<string>("commentTime");

                    b.Property<bool>("status");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Web90TvCore2.Models.News", b =>
                {
                    b.Property<int>("NewsId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abstract")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("IndexImage");

                    b.Property<string>("MetaDescription");

                    b.Property<string>("MetaTag");

                    b.Property<string>("NewsDate");

                    b.Property<byte>("NewsPlace");

                    b.Property<string>("NewsTime");

                    b.Property<byte>("NewsType");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("UserId");

                    b.Property<int>("VisitCount");

                    b.HasKey("NewsId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Web90TvCore2.Models.Poll", b =>
                {
                    b.Property<int>("PollId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("PollEndDate");

                    b.Property<string>("PollStartDate");

                    b.Property<string>("Question");

                    b.HasKey("PollId");

                    b.ToTable("Polls");
                });

            modelBuilder.Entity("Web90TvCore2.Models.PollOption", b =>
                {
                    b.Property<int>("PolloptionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<int>("PollID");

                    b.Property<int>("VouteCount");

                    b.HasKey("PolloptionID");

                    b.HasIndex("PollID");

                    b.ToTable("PollOptions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Web90TvCore2.Models.ApplicationRoles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Web90TvCore2.Models.ApplicationUsers")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Web90TvCore2.Models.ApplicationUsers")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Web90TvCore2.Models.ApplicationRoles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Web90TvCore2.Models.ApplicationUsers")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Web90TvCore2.Models.ApplicationUsers")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Web90TvCore2.Models.Comment", b =>
                {
                    b.HasOne("Web90TvCore2.Models.News", "TblNews")
                        .WithMany()
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Web90TvCore2.Models.News", b =>
                {
                    b.HasOne("Web90TvCore2.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Web90TvCore2.Models.ApplicationUsers", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Web90TvCore2.Models.PollOption", b =>
                {
                    b.HasOne("Web90TvCore2.Models.Poll", "Poll")
                        .WithMany("PollOptions")
                        .HasForeignKey("PollID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
