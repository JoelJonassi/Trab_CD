using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class CreateMachineDto
    {

        public string MachineName { get; set; }

        public int time { get; set; }

    }
}
