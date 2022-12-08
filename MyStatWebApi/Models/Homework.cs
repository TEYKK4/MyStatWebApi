using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStatWebApi.Models;

public class Homework
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(1024)]
    public string Description { get; set; } = null!;
    
    [Required]
    public DateTime DateTime { get; set; }
    
    [Required]
    [ForeignKey("UserId")]
    public User? User { get; set; }
    public int UserId { get; set; }
}