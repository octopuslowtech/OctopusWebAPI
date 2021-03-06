// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OctopusWebAPI.Data;

#nullable disable

namespace OctopusWebAPI.Migrations
{
    [DbContext(typeof(MyOctpDBContext))]
    [Migration("20220725024035_inital")]
    partial class inital
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OctopusWebAPI.Entities.Account", b =>
                {
                    b.Property<string>("UserTiktok")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("Backup")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DateBackup")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateRun")
                        .HasColumnType("datetime2");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserTiktok");

                    b.HasIndex("UserID");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("OctopusWebAPI.Entities.AccountTDS", b =>
                {
                    b.Property<string>("UserTDS")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Xu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTDS");

                    b.HasIndex("UserID");

                    b.ToTable("AccountTDS", (string)null);
                });

            modelBuilder.Entity("OctopusWebAPI.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TokenId");

                    b.HasIndex("UserID");

                    b.ToTable("RefreshToken", (string)null);
                });

            modelBuilder.Entity("OctopusWebAPI.Entities.User", b =>
                {
                    b.Property<string>("UserID")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserID");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("OctopusWebAPI.Entities.Account", b =>
                {
                    b.HasOne("OctopusWebAPI.Entities.User", "User")
                        .WithMany("Account")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OctopusWebAPI.Entities.AccountTDS", b =>
                {
                    b.HasOne("OctopusWebAPI.Entities.User", "User")
                        .WithMany("AccountTDS")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OctopusWebAPI.Entities.RefreshToken", b =>
                {
                    b.HasOne("OctopusWebAPI.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OctopusWebAPI.Entities.User", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("AccountTDS");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
