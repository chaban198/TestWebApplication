// ReSharper disable All

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskListWebApplication.Models.DbModels;

[Table("projects")]
public class ProjectDb
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("users")]
    public List<string> Users { get; set; } = new();

    //nav property
    public virtual ICollection<SprintDb> Posts { get; set; } = null!;
}