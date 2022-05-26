using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models
{
    public class Machine
    {
        [Key]
        public int IdMachine {get; set;}
        [Required]
        public string MachineName { get; set;}
        public int time { get; set; }
        [ForeignKey("IdOperation")]
        public ICollection<MachineOperation> MachineOperation { get; set; }
    }
}
