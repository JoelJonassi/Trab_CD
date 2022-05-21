using System.ComponentModel.DataAnnotations;

namespace JobShopAPI.Models.Dto
{
    public class ChangePasswordUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string OldPassword { get; set; }
    }
}
