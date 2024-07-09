using System;
using System.Collections.Generic;

namespace VDOEasy.Data.Models;

public partial class MasMoviesType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<TrnMember> Members { get; set; } = new List<TrnMember>();
}
