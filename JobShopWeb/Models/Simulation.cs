using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopWeb.Models
{
    public class Simulation
    {
        public int IdUser { get; set; }

        public int IdSimulation { get; set; } 

        public string NameSimulation { get; set; }

        public int IdJob { get; set; }

        public string NameJob { get; set; }
    }
}
