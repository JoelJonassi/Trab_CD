using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopWeb.Models
{
    public class Machine
    {
        public int IdOperation { get; set; }
        public string    NameOperation { get; set; }
        public int  IdMachine { get; set; }
        public string    NameMachine { get; set; }
    }
}
