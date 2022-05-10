using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models
{
    public class Simulation
    {
        [Key]
        public int IdSimulation { get; set; }
        public string NameSimulation { get; set; }
        [Required]
        public int IdJob { get; set; }
        [ForeignKey("IdJob")]
        public Job Job { get; set; }

    }
}
