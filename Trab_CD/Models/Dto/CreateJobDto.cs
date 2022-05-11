using System.ComponentModel.DataAnnotations;

namespace JobShopAPI.Models.Dto
{
    public class CreateJobDto
    {

        public string NameJob { get; set; }
        [Required]
        public int IdOperation { get; set; }
    }
}
