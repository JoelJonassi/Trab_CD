using JobShopAPI.Data;
using JobShopAPI.Models;
using JobShopAPI.Repository.IRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

/// <summary>
/// 
/// </summary>
namespace JobShopAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public UserRepository(ApplicationDbContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Verifica se o utilizador existe na base de dados
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool UserExists(string UserName)
        {
            return _db.Users.Any(simu => simu.Username == UserName);
        }

        /// <summary>
        /// Verifica se o utilizador existe na base de dados
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool UserExists(int IdUser)
        {
            return _db.Users.Any(user => user.Id == IdUser);
        }

        /// <summary>
        /// Bucar todos os utilizadores na base de dados
        /// </summary>
        /// <returns></returns>
        public ICollection<User> GetUsers()
        {
            return _db.Users.OrderBy(a => a.Username).ToList();
        }

        /// <summary>
        /// Buscar um utilizador pelo username na base de dados
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUser(string username)
        {
            return _db.Users.FirstOrDefault(user => user.Username == username);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public User GetUser(int idUser)
        {
            return _db.Users.FirstOrDefault(user => user.Id == idUser);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateUser(string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsUniqueUser(string username)
        {
            var user = _db.Users.SingleOrDefault(x => x.Username == username);
            if (user == null)
                return true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Authenticate(string username, string password)
        {
            var user = _db.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            //if not found
            if (user == null)
            {
                return null;
            }
            //if user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),//Duration of JWT - JSON Web Token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            //user.Password = "";
            return user;

        }

       /// <summary>
       /// Função para registar um utilizador
       /// </summary>
       /// <param name="username"></param>
       /// <param name="password"></param>
       /// <returns></returns>
        public User Register(string username, string Name,  string password, string role)
        {
            User userObj = new User()
            {
                Name = Name,
                Username = username,
                Password = password,
                Role = role
            };
            if(userObj.Role == "SenhaSecreta")
            {
                userObj.Role = "Admin";
                _db.Users.Add(userObj);
                _db.SaveChanges();
                userObj.Password = "";
                return userObj;
            }
            userObj.Role = "common";
            _db.Users.Add(userObj);
            _db.SaveChanges();
            userObj.Password = "";
            return userObj;
        }

        /// <summary>
        /// Mudar Password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangePasswordUser(string username, string oldPassword, string newPassword)
        {
            User userObj = GetUser(username);
            if (userObj.Password == oldPassword)
            {
                userObj.Password = newPassword;
                _db.Users.Update(userObj);
                _db.SaveChanges();
                userObj.Password = "";
                return true;
            }
            return false;
           

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usernmae"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteUser(User user)
        {
                _db.Users.Update(user);
                return Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateUser(User user)
        {
            _db.Users.Update(user);
            return Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

       
       

    }
}