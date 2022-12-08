using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyStatWebApi.Models;

[Index(nameof(Login), IsUnique = true)]
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(128)]
    public string Name { get; set; } = null!;
    
    [Required]
    [MinLength(2)]
    [MaxLength(128)]
    public string Surname { get; set; } = null!;
    
    [Required]
    [MinLength(5)]
    [MaxLength(128)]
    public string Login { get; set; } = null!;
    
    [Required]
    [MinLength(8)]
    [MaxLength(128)]
    public string Password { get; set; } = null!;
    
    public ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
}