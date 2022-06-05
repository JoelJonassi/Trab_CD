using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class CreateSimulationDto
    {

        [Required]
        public string NameSimulation { get; set; }

        public int Id { get; set; }
    }

}
