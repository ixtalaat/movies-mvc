using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Web.Models;

public class Movie
{
    public int Id { get; set; }

    [Required, MaxLength(250)]
    public string Title { get; set; } = null!;

    public int Year { get; set; }

    public double Rate { get; set; }

    [Required, MaxLength(2500)]
    public string StoryLine { get; set; } = null!;

    [Required]
    public byte[] Poster { get; set; } = null!;
    public string Trailer { get; set; } = null!;

    public int GenreId { get; set; }

    [ForeignKey(nameof(GenreId))]
    public Genre Genre { get; set; } = null!;
}
