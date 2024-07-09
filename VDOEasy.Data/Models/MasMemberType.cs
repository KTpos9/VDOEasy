using System;
using System.Collections.Generic;

namespace VDOEasy.Data.Models;

public partial class MasMemberType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<TrnMember> TrnMembers { get; set; } = new List<TrnMember>();
}
