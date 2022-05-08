using System.ComponentModel.DataAnnotations;

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
        public int IdMachine { get; set; }
    }
}
