using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models
{
    public class Machine
    {
        [Key]
        public int IdMachine {get; set;}
        public string MachineÑame { get; set;}
        [ForeignKey("IdOperation")]
        public ICollection<Operation> Operations { get; set; }
    }
}
