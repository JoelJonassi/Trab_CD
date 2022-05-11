using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class UpdateMachineDto
    {

        [Key]
        public int IdMachine { get; set; }
        [Required]
        public string MachineName { get; set; }
        [Required]
        public int IdOperation { get; set; }
    }
}
