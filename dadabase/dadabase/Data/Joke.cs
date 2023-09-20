using System;
using System.Collections.Generic;

namespace dadabase.Data;

public partial class Joke
{
    public int Id { get; set; }

    public string? Jokename { get; set; }

    public string? Joketext { get; set; }

    public virtual ICollection<Categorizedjoke> Categorizedjokes { get; set; } = new List<Categorizedjoke>();

    public virtual ICollection<Deliveredjoke> Deliveredjokes { get; set; } = new List<Deliveredjoke>();
}
