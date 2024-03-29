﻿using System.ComponentModel.DataAnnotations;

namespace Movies.Web.Models;

public class Genre
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;
}
