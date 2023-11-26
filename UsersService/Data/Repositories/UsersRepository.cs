using UserService.Models;

namespace UserService.Data.Repositories;

public class UsersRepository(AppDbContext dbContext) : IUsersRepository
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

    public User GetUserById(int id)
    {
        return dbContext.Users.FirstOrDefault(user => user.Id == id)!;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return dbContext.Users.ToList();
    }
}