using JobShopAPI.Models;

namespace JobShopAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int userId);
        bool IsUniqueUser(string username);
        User Authenticate(string username, string password);
        User Register(string username, string password);
        User DeleteUser(string usernmae);
        bool UpdateUser(string username);
        bool Save();
    }
}
