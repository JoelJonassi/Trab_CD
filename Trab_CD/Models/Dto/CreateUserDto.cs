using System.ComponentModel.DataAnnotations;

namespace JobShopAPI.Models.Dto
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
