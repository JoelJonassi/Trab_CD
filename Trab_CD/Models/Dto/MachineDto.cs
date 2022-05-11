using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class MachineDto
    {
        public int IdMachine { get; set; }
        [Required]
        public string MachineName { get; set; }
        [ForeignKey("IdOperation")]
        public ICollection<Operation> Operations { get; set; }
    }
}
