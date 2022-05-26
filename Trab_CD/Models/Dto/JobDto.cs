using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class JobDto
    {


        public int IdJob { get; set; }

        public string NameJob { get; set; }


        public List<int> Operations { get; set; }

    }
}
