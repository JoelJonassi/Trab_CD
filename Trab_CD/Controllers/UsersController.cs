using AutoMapper;
using JobShopAPI.Models;
using JobShopAPI.Models.Dto;
using JobShopAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobShopAPI.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/Users")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper, IUserRepository userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;

        }

        /// <summary>
        /// Buscar todos os utilizadores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var objList = _userRepo.GetUsers();
            var objDto = new List<UserDto>();

            foreach (var item in objList)
            {
                objDto.Add(_mapper.Map<UserDto>(item));
            }
            return Ok(objList);
        }

        /// <summary>
        /// Buscar utilizador pelo username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("username")]
        [ProducesResponseType(200, Type = typeof(List<UserDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetUser(string username)
        {
            var obj = _userRepo.GetUser(username);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<UserDto>(obj);
            return Ok(objDto);
        }

        [HttpGet("idUser")]
        [ProducesResponseType(200, Type = typeof(List<UserDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetUser(int idUser)
        {
            var obj = _userRepo.GetUser(idUser);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<UserDto>(obj);
            return Ok(objDto);
        }


     

        /// <summary>
        /// Registar Utilizador
        /// Quem pode criar um utilizador é o administrador
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateUserDto model)
        {
            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.Username);
            if (!ifUserNameUnique)
            {
                return BadRequest(new { message = "Username already exists" });
            }
            var user = _userRepo.Register(model.Username, model.Password, model.Role);
            if (user == null)
            {
                return BadRequest(new { message = "Error while registering" });
            }
            return Ok(user);
        }

        /// <summary>
        /// Iniciar a Sessão do Utilizador
        /// O utilizador apenas vai poder autenticar-se, não pode criar conta
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
        /// Alterar palavra passe
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPatch("update")]
        public IActionResult ChangePasswordUser([FromBody] ChangePasswordUserDto model)
        {
            
            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.Username);
            if (!ifUserNameUnique)
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
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepo.UserExists(userId))
            {
                return NotFound();
            }
            else
            {
                var userOb = _userRepo.GetUser(userId);

                if (!_userRepo.DeleteUser(userOb))
                {
                    ModelState.AddModelError("", $"something went wrong when deleting the record {userOb.Username}");
                    return StatusCode(500, ModelState);
                }

            }
            return NoContent();
        }

    }
}
