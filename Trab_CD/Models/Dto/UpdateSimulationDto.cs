using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class UpdateSimulationDto {
        
        public int IdSimulation { get; set; }

        [Required]
        public string NameSimulation { get; set; }

        [Required]
        public int IdJob { get; set; }

        [Required]
        public int IdOperation { get; set; }

        [Required]
        public int IdMachine { get; set; }
    }
}
