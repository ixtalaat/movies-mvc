﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movies.Web.Data;

#nullable disable

namespace Movies.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240227034916_SeedGenreData")]
    partial class SeedGenreData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Movies.Web.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Comedy"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Drama"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Romance"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Thriller"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Western"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Documentary"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Sci-Fi"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Animation"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Musical"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Mystery"
                        },
                        new
                        {
                            Id = 14,
                            Name = "War"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Crime"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Family"
                        },
                        new
                        {
                            Id = 18,
                            Name = "History"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Biography"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Sport"
                        });
                });

            modelBuilder.Entity("Movies.Web.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Poster")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<string>("StoryLine")
                        .IsRequired()
                        .HasMaxLength(2500)
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Movies.Web.Models.Movie", b =>
                {
                    b.HasOne("Movies.Web.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });
#pragma warning restore 612, 618
        }
    }
}
