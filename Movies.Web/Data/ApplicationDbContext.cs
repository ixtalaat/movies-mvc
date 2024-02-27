using Microsoft.EntityFrameworkCore;
using Movies.Web.Models;

namespace Movies.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Drama" },
                new Genre { Id = 4, Name = "Horror" },
                new Genre { Id = 5, Name = "Romance" },
                new Genre { Id = 6, Name = "Thriller" },
                new Genre { Id = 7, Name = "Western" },
                new Genre { Id = 8, Name = "Documentary" },
                new Genre { Id = 9, Name = "Sci-Fi" },
                new Genre { Id = 10, Name = "Fantasy" },
                new Genre { Id = 11, Name = "Animation" },
                new Genre { Id = 12, Name = "Musical" },
                new Genre { Id = 13, Name = "Mystery" },
                new Genre { Id = 14, Name = "War" },
                new Genre { Id = 15, Name = "Crime" },
                new Genre { Id = 16, Name = "Adventure" },
                new Genre { Id = 17, Name = "Family" },
                new Genre { Id = 18, Name = "History" },
                new Genre { Id = 19, Name = "Biography" },
                new Genre { Id = 20, Name = "Sport" }
            );
    }
}
