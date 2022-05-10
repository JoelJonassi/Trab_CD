using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models
{
    public class Job
    {
        [Key]
        public int IdJob { get; set; }
        public string NameJob { get; set; }
        [Required]
        public int IdOperation { get; set; }
        [ForeignKey("IdOperation")]
        public Operation Operation { get; set; }

        #region Functions
        //Esta tabela deve poder ser criada através de uma função que
        //insere para uma operação específica de um trabalho, a sua máquina e o seu tempo



        #endregion

    }



}
