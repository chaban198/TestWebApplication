using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskListWebApplication.Models.Enums;

namespace TaskListWebApplication.Models.DbModels;

[Table("tasks")]
public class TaskDb
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("sprintId")]
    public Guid SprintId { get; set; }

    [Required]
    [Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Required]
    [Column("status")]
    public UserTaskStatus Status { get; set; }

    [Column("user")]
    public string? User { get; set; }

    //nav property
    public virtual SprintDb Sprint { get; set; } = null!;
}