// ReSharper disable All

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskListWebApplication.Models.DbModels;

[Table("project")]
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
    public List<string> Users { get; set; } = null!;

    //nav property
    public virtual List<SprintDb>? Sprints { get; set; }
}