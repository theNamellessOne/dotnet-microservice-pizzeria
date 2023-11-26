using FavoriteService.Models;

namespace FavoriteService.Data.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public bool SaveChanges()
    {
        return dbContext.SaveChanges() >= 0;
    }

    public void CreateUser(User user)
    {
        if (user == null) throw new ArgumentNullException();
        dbContext.Users.Add(user);
    }

    public bool ExternalUserExists(int externalUserId)
    {
        return dbContext.Users.Any(user => user.ExternalId == externalUserId);
    }

    public User? GetUserById(int externalId)
    {
        return dbContext.Users.FirstOrDefault(user => user.ExternalId == externalId);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return dbContext.Users.ToList();
    }
}