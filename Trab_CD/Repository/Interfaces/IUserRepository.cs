using JobShopAPI.Models;

namespace JobShopAPI.Repository.IRepository
{
    public interface IUserRepository
    {



        bool UserExists(int idUser);

        bool UserExists(string userName);

        ICollection<User> GetUsers();

        User GetUser(int userId);

        User GetUser(string username);

        bool IsUniqueUser(string username);

        User Authenticate(string username, string password);

        User Register(string username, string password, string role);

        bool DeleteUser(User user);

        bool UpdateUser(string username);

        bool ChangePasswordUser(string username, string oldPassword, string newPassword);

        bool Save();
    }
}
