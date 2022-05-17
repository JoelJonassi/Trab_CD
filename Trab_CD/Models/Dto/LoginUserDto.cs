using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class LoginUserDto
    {

        public string Username { get; set; }

        public string Password { get; set; }

    }
}
