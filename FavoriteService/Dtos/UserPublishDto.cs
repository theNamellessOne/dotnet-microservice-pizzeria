namespace FavoriteService.Dtos;

public class UserPublishDto : GenericEventDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
}