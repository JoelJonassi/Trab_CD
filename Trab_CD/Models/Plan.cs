
using JobShopAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace JobShopAPI.Models
{
    public class Plan
    {
        [Key]
        public int IdPlan { get; set; }
        public int IdSimulation { get; set; }
        [ForeignKey("IdSimulation")]
        public Simulation Simulation { get; set; }

        public int IdJob { get; set; }
        [ForeignKey("IdJob")]
        public Job Job{ get; set; }

        public int IdOperation { get; set; }
        [ForeignKey("IdOperation")]
        public Operation Operation { get; set; }

        public int IdMachine { get; set; }
        [ForeignKey("IdMachine")]
        public Machine Machine { get; set; }

        public int initialTime { get; set; }

        public int finalTime { get; set; }
    }
}
