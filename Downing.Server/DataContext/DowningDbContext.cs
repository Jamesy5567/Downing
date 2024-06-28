using System;
using System.Collections.Generic;
using Downing.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Downing.Server.DataContext;

public partial class DowningDbContext : DbContext
{
    public DowningDbContext(DbContextOptions<DowningDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Companie__3214EC07EEDA80B0");

            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.CompanyName).HasMaxLength(100);
            entity.Property(e => e.SharePrice).HasColumnType("money");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
