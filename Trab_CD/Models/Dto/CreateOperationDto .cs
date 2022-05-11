using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class CreateOperationDto
    {
        public string OperationName { get; set; }
        [Required]
        [ForeignKey("IdMachine")]
        public int IdMachine { get; set; }
        public int time { get; set; }
    }
}
