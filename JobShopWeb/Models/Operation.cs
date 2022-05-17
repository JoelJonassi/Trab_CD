using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobShopWeb.Models
{
    public class Operation
    {
        public int IdOperation { get; set; }
        public string OperationName { get; set; }
        [Required]
        public int IdMachine { get; set; }
        public ICollection<Machine> Machines { get; set; }
        public int time { get; set; }
    }
}
