using JobShopAPI.Models;

namespace JobShopAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User GetUser(int userId);

        User GetUser(string username);

        bool IsUniqueUser(string username);

        User Authenticate(string username, string password);

        User Register(string username, string password);

        bool DeleteUser(User user);

        bool UpdateUser(string username);

        bool ChangePasswordUser(string username, string oldPassword, string newPassword);

        bool Save();
    }
}
