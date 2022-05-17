using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace JobShopWeb.Models.ViewModel
{
    public class ProductionTableServiceVM
    {

        public IEnumerable<SelectListItem> OperationList { get; set; }

        public IEnumerable<SelectListItem> MachineList { get; set; }

        public IEnumerable<Job> JobList { get; set; }
    }
}
