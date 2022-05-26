using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class UpdateOperationDto
    {
        public int IdOperation { get; set; }
        public string OperationName { get; set; }
        [Required]
        public List<Machine> Machines { get; set; }
    }
}
