using System.ComponentModel.DataAnnotations;

namespace JobShopAPI.Models.Dto
{
    public class CreateUserDto
    {

        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
