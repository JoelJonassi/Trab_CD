using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models
{
    public class Simulation
    {
        [Key]
        public int IdSimulation { get; set; }

        [Required]
        public string NameSimulation { get; set; }

        [ForeignKey("IdJob")]
        public ICollection<JobSimulation> JobSimulation { get; set; }

        public int Id { get; set; }
        [ForeignKey("Id")]
        public User User { get; set; }
    }
}
