using System;
using System.Collections.Generic;

namespace dadabase.Data;

public partial class Categorizedaudience
{
    public int Id { get; set; }

    public int Audienceid { get; set; }

    public int? Audiencecategoryid { get; set; }

    public virtual Audience Audience { get; set; } = null!;

    public virtual Audiencecategory? Audiencecategory { get; set; }
}
