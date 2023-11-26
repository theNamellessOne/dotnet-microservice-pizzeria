using System.ComponentModel.DataAnnotations;

namespace UserService.Models;

public class User
{
    [Key] [Required] public int Id { get; set; }

    [Required] public string? Email { get; set; }

    [Required] public string? Name { get; set; }
    [Required] public string? Surname { get; set; }

    [Required] public string? Password { get; set; }
}