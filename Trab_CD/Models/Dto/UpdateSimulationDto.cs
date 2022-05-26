using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class UpdateSimulationDto {

        public int IdSimulation { get; set; }

        [Required]
        public string NameSimulation { get; set; }

        public int Id { get; set; }

        [ForeignKey("Id")]
        public User User { get; set; }

        public List<Job> Job { get; set; }
    }
}
