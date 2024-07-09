using System;
using System.Collections.Generic;

namespace VDOEasy.Data.Models;

public partial class TrnMember
{
    public int Id { get; set; }

    public int? BranchId { get; set; }

    public DateTime? IssueDate { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateTime? Birthdate { get; set; }

    public string? Address { get; set; }

    public string? IdcardNumber { get; set; }

    public int? MemberTypeId { get; set; }

    public string? StaffName { get; set; }

    public DateTime? ReceiptDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual MasBranch? Branch { get; set; }

    public virtual MasMemberType? MemberType { get; set; }

    public virtual ICollection<MasMoviesType> MovieTypes { get; set; } = new List<MasMoviesType>();
}
