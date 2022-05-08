using System.ComponentModel.DataAnnotations;

namespace JobShopAPI.Models
{
    public class Machine
    {
        [Key]
        public int IdMachine {get; set;}
        public string MachineÑame { get; set;}
    }
}
