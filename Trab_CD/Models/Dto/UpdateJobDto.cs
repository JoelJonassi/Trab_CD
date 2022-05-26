using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class UpdateJobDto {


        public int IdJob { get; set; }

        public string NameJob { get; set; }

        public ICollection<JobOperation> JobOperation { get; set; }

    }
}
