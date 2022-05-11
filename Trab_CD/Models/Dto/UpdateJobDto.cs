using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class UpdateJobDto
    {
        public int IdJob { get; set; }
        [Required]

        public int IdOperation { get; set; }
        [NotMapped]
        [ForeignKey("IdOperation")]
        public UpdateOperationDto Operation { get; set; }
    }
}
