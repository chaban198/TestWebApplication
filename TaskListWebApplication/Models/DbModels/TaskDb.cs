using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskListWebApplication.Models.Enums;

namespace TaskListWebApplication.Models.DbModels;

public class TaskDb
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("projectId")]
    public Guid SprintId { get; set; }

    [Required]
    [Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Required]
    [Column("status")]
    public UserTaskStatus Status { get; set; }

    [Column("userId")]
    public string? UserId { get; set; }
}