using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobShopWeb.Models
{
    public class Machine
    {
        public int IdMachine { get; set; }
        [Required]
        public string MachineName { get; set; }
        public int IdOperation { get; set; }
        public ICollection<Operation> Operations { get; set; }
    }
}
