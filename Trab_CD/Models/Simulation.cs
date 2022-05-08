using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models
{
    public class Simulation
    {
        [Key]
        public int IdSimulation { get; set; }   
        public Machine Machine { get; set; }
        public Operation Operation{ get; set; }
        public ICollection<Job> Jobs { get; set; }

    }
}
