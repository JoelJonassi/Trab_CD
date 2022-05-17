using System.ComponentModel.DataAnnotations;

namespace JobShopWeb.Models
{
    public class Simulation
    {
        public int IdSimulation { get; set; }
        [Required]
        public string NameSimulation { get; set; }

        public int IdJob { get; set; }
        public Job Job { get; set; }

        public int IdOperation { get; set; }
        public Operation Operation { get; set; }

        public int IdMachine { get; set; }

        public Machine Machine { get; set; }
    }
}
