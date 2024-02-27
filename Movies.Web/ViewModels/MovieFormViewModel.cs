using Movies.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Web.ViewModels;

public class MovieFormViewModel
{
    public int Id { get; set; }
    [Required, StringLength(250)]
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    [Range(1, 10)]
    public double Rate { get; set; }
    [Required, StringLength(2500)]
    public string StoryLine { get; set; } = null!;
    [Display(Name = "Select poster...")]
    public byte[]? Poster { get; set; }
    public string Trailer { get; set; } = null!;
    [Display(Name = "Genre")]
    public int GenreId { get; set; }
    public IEnumerable<Genre>? Genres { get; set; }
}
