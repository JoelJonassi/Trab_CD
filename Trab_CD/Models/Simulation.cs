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
       
        public int IdJob { get; set; }
        [ForeignKey("IdJob")]
        public Job Job { get; set; }
        
        public int IdOperation { get; set; }
        [ForeignKey("IdOperation")]
        public Operation Operation { get; set; }
        
        public int IdMachine { get; set; }
        [ForeignKey("IdMachine")]
        public Machine Machine { get; set; }

       /* public int Id { get; set; }
        [ForeignKey("Id")]
        public User user { get; set; }*/
    }
}
