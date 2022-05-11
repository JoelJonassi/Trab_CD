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
        public int IdOperation { get; set; }
        [ForeignKey("IdOperation")]
        public ICollection<Operation> Operations { get; set; }
    }
}
