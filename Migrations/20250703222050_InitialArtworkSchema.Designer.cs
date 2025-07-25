﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using gfcf14_art_backend.Data;

#nullable disable

namespace gfcf14_art_backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250703222050_InitialArtworkSchema")]
    partial class InitialArtworkSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("gfcf14_art_backend.Models.Artwork", b =>
                {
                    b.Property<string>("Date")
                        .HasColumnType("text")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Date");

                    b.ToTable("artworks");
                });

            modelBuilder.Entity("gfcf14_art_backend.Models.Link", b =>
                {
                    b.Property<string>("ArtworkDate")
                        .HasColumnType("text")
                        .HasColumnName("artwork_date");

                    b.Property<string>("Type")
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<string>("Url")
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("ArtworkDate", "Type", "Url");

                    b.ToTable("artwork_links");
                });

            modelBuilder.Entity("gfcf14_art_backend.Models.Link", b =>
                {
                    b.HasOne("gfcf14_art_backend.Models.Artwork", "Artwork")
                        .WithMany("Links")
                        .HasForeignKey("ArtworkDate")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artwork");
                });

            modelBuilder.Entity("gfcf14_art_backend.Models.Artwork", b =>
                {
                    b.Navigation("Links");
                });
#pragma warning restore 612, 618
        }
    }
}
