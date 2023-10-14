// ReSharper disable All

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace TaskListWebApplication.Data;

public class IdentityReadonlyDbContext : DbContext
{
    protected DbSet<IdentityUser> UsersProtected { get; set; }
    public IQueryable<IdentityUser> Users => UsersProtected.AsNoTracking();

    public IdentityReadonlyDbContext(DbContextOptions<IdentityReadonlyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers", t => t.ExcludeFromMigrations());
    }

    #region SaveChangesBlock

    public override int SaveChanges() => throw new NotSupportedException();

    public override int SaveChanges(bool acceptAllChangesOnSuccess) => throw new NotSupportedException();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) => throw new NotSupportedException();

    public override Task<int> SaveChangesAsync(bool acess, CancellationToken cancellationToken = new()) => throw new NotSupportedException();

    #endregion
}