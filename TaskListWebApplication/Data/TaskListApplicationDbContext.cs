using Microsoft.EntityFrameworkCore;
using TaskListWebApplication.Models.DbModels;
using TaskListWebApplication.Models.Enums;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace TaskListWebApplication.Data;

public class TaskListApplicationDbContext : DbContext
{
    public DbSet<ProjectDb> Projects { get; set; }
    public DbSet<SprintDb> Sprints { get; set; }
    public DbSet<TaskDb> Tasks { get; set; }

    public TaskListApplicationDbContext(DbContextOptions<TaskListApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<UserTaskStatus>();
    }
}