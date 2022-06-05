using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopAPI.Models.Dto
{
    public class CreateOperationDto
    {
        public string OperationName { get; set; }

    }
}
