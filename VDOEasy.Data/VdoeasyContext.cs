using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VDOEasy.Data.Models;

namespace VDOEasy.Data;

public partial class VdoeasyContext : DbContext
{
    public VdoeasyContext()
    {
    }

    public VdoeasyContext(DbContextOptions<VdoeasyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MasBranch> MasBranches { get; set; }

    public virtual DbSet<MasMemberType> MasMemberTypes { get; set; }

    public virtual DbSet<MasMoviesType> MasMoviesTypes { get; set; }

    public virtual DbSet<TrnMember> TrnMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MasBranch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__branches__3214EC277F7BF849");

            entity.ToTable("masBranches");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("Thai_CI_AS");
        });

        modelBuilder.Entity<MasMemberType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__masMembe__3214EC271A7B01A6");

            entity.ToTable("masMemberType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .UseCollation("Thai_CI_AS");
        });

        modelBuilder.Entity<MasMoviesType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__masMovie__3214EC27107FA14A");

            entity.ToTable("masMoviesType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .UseCollation("Thai_CI_AS");

            entity.HasMany(d => d.Members).WithMany(p => p.MovieTypes)
                .UsingEntity<Dictionary<string, object>>(
                    "TrnMembersMovieType",
                    r => r.HasOne<TrnMember>().WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__trnMember__Membe__4E88ABD4"),
                    l => l.HasOne<MasMoviesType>().WithMany()
                        .HasForeignKey("MovieTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__trnMember__Movie__4D94879B"),
                    j =>
                    {
                        j.HasKey("MovieTypeId", "MemberId").HasName("PK__trnMembe__BAE891440283D26E");
                        j.ToTable("trnMembersMovieType");
                        j.IndexerProperty<int>("MovieTypeId").HasColumnName("MovieTypeID");
                        j.IndexerProperty<int>("MemberId").HasColumnName("MemberID");
                    });
        });

        modelBuilder.Entity<TrnMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_members");

            entity.ToTable("trnMembers");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .UseCollation("Thai_CI_AS");
            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.BranchId).HasColumnName("BranchID");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .UseCollation("Thai_CI_AS");
            entity.Property(e => e.IdcardNumber)
                .HasMaxLength(13)
                .UseCollation("Thai_CI_AS")
                .HasColumnName("IDCardNumber");
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .UseCollation("Thai_CI_AS");
            entity.Property(e => e.MemberTypeId).HasColumnName("MemberTypeID");
            entity.Property(e => e.ReceiptDate).HasColumnType("datetime");
            entity.Property(e => e.StaffName)
                .HasMaxLength(100)
                .UseCollation("Thai_CI_AS");

            entity.HasOne(d => d.Branch).WithMany(p => p.TrnMembers)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK__trnMember__Branc__4BAC3F29");

            entity.HasOne(d => d.MemberType).WithMany(p => p.TrnMembers)
                .HasForeignKey(d => d.MemberTypeId)
                .HasConstraintName("FK__trnMember__Membe__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
