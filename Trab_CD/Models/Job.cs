using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//https://stackoverflow.com/questions/21772876/join-two-models-to-get-data-into-a-view
namespace JobShopAPI.Models
{
    //Funcional na base de dados
    public class Job
    {
        [Key]
        public int IdJob { get; set; }

        public string NameJob { get; set; }

        [ForeignKey("IdOperation")]
        public ICollection<JobOperation> JobOperation { get; set; }

        [ForeignKey("IdSimulation")]
        public ICollection<JobSimulation> JobSimulation { get; set; }

        

        #region Functions
        //Esta tabela deve poder ser criada através de uma função que
        //insere para uma operação específica de um trabalho, a sua máquina e o seu tempo



        #endregion

    }



}
