// ReSharper disable All

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskListWebApplication.Models.DbModels;

[Table("sprint")]
public class SprintDb
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("projectId")]
    public Guid ProjectId { get; set; }

    [Required]
    [Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Required]
    [Column("start")]
    public DateTime Start { get; set; }

    [Column("end")]
    public DateTime? End { get; set; }

    [Column("comment")]
    public string? Comment { get; set; }

    [Column("files")]
    public List<string> Files { get; set; } = null!;

    //nav property
    [ForeignKey("projectId")]
    public virtual ProjectDb? Project { get; set; }
}