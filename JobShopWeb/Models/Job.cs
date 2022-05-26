using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShopWeb.Models
{
    public class Job
    {

        public int IdJob { get; set; }

        public string NameJob { get; set; }

        public int IdOperation { get; set; }

        public string NameOperation { get; set; }


    }
}
