using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models
{
    //Não existem operações vázias
    //Operação é uma máquina com um tempo
    //Primeira operação do job
    //
    public class Operation
    {
        [Key]
        public int IdOperation { get; set; }
        public string OperationName { get; set; }
        [Required]
        public int IdMachine { get; set; }
        [ForeignKey("IdMachine")]
        public ICollection<Machine> Machines { get; set; }
        public int time { get; set; }
    }
}
