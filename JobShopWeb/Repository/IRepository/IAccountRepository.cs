


using JobShopWeb.Models;
using JobShopWeb.Repository.IRepository;
using System.Threading.Tasks;

namespace JobShopAPI.Repository.Interfaces
{
    public interface IAccountRepository : IRepository<User>
    {
        Task<User> LoginAsync(string url, User objToCreate);

        Task<bool> RegisterAsync(string url, CreateUserDto objToRegister, string token = "");

        Task<bool> UpdateAsync(string url, UpdateUserDto objToUpdate, string token = "");

        Task<bool> DeleteAsync(string url, DeleteUserDto objToUpdate, string token = "");


    }
}
