namespace UserService.Dtos;

public class UserPublishDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Event { get; set; }
}