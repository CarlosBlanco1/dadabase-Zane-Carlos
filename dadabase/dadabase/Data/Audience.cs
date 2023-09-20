using System;
using System.Collections.Generic;

namespace dadabase.Data;

public partial class Audience
{
    public int Id { get; set; }

    public string? Audiencename { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Categorizedaudience> Categorizedaudiences { get; set; } = new List<Categorizedaudience>();

    public virtual ICollection<Deliveredjoke> Deliveredjokes { get; set; } = new List<Deliveredjoke>();
}
