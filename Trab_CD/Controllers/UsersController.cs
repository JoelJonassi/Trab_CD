using JobShopAPI.Models;
using JobShopAPI.Models.Dto;
using JobShopAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobShopAPI.Controllers
{
    [Authorize]
    [Route("api/Users")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;

        }


        
        /// <summary>
        /// Iniciar a Sessão do Utilizador
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginUserDto model)
        {
            var user = _userRepo.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }

        /// <summary>
        /// Registar Utilizador
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateUserDto model)
        {
            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.Username);
            if (!ifUserNameUnique)
            {
                return BadRequest(new { message = "Username already exists" });
            }
            var user = _userRepo.Register(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Error while registering" });
            }
            return Ok(user);
        }

        /// <summary>
        /// Alterar palavra passe
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPatch("update")]
        public IActionResult ChangePasswordUser([FromBody] ChangePasswordUserDto model)
        {
            
            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.Username);
            if (ifUserNameUnique)
            {
                var user = _userRepo.ChangePasswordUser(model.Username, model.OldPassword, model.NewPassword);
                return Ok(user);
            }
            return BadRequest(new { message = "Erro na alteração da palavra passe" });
        }

        /// <summary>
        /// Eliminar utilizador
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete("Delete")]
        public IActionResult DeleteUser([FromBody] User model)
        {
            throw new NotImplementedException();
        }

    }
}
