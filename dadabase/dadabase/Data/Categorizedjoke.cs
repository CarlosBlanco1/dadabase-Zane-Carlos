using System;
using System.Collections.Generic;

namespace dadabase.Data;

public partial class Categorizedjoke
{
    public int Id { get; set; }

    public int Jokeid { get; set; }

    public int? Jokecategoryid { get; set; }

    public virtual Joke Joke { get; set; } = null!;

    public virtual Jokecategory? Jokecategory { get; set; }
}
