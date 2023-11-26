namespace UserService.Dtos;

public class UserReadDto
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string? Name { get; set; }
    public required string? Surname { get; set; }
}