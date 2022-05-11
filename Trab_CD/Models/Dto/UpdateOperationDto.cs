using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class UpdateOperationDto
    {
        [Required]
        public int IdOperation { get; set; }
        [Required]
        public string OperationName { get; set; }
        [Required]
        public int IdMachine { get; set; }
        public int time { get; set; }
    }
}
