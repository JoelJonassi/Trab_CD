using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class CreateMachineDto
    {
        [Required]
        public string MachineName { get; set; }
        [Required]
        [ForeignKey("IdOperation")]
        public int IdOperation{ get; set; }
    }
}
