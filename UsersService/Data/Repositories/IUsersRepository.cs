using UserService.Models;

namespace UserService.Data.Repositories;

public interface IUsersRepository
{
    bool SaveChanges();
    void CreateUser(User user);
    User GetUserById(int id);
    IEnumerable<User> GetAllUsers();
}