using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace JobShopWeb.Models.ViewModel
{
    public class ProductionTableServiceVM
    {

        public IEnumerable<Operation> OperationList { get; set; }

        public IEnumerable<Machine> MachineList { get; set; }

        public IEnumerable<Job> JobList { get; set; }
    }
}
