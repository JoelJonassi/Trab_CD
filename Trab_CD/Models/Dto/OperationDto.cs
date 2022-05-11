using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class OperationDto
    {

        public int IdOperation { get; set; }
        public string OperationName { get; set; }
        [Required]
        public int IdMachine { get; set; }
        [ForeignKey("IdMachine")]
        public ICollection<Machine> Machines { get; set; }
        public int time { get; set; }
    }
}
