using FavoriteService.Models;

namespace FavoriteService.Data.Repositories;

public interface IUserRepository
{
    bool SaveChanges();
    void CreateUser(User user);
    bool ExternalUserExists(int externalUserId);
    User? GetUserById(int externalId);
    IEnumerable<User> GetAllUsers();
}