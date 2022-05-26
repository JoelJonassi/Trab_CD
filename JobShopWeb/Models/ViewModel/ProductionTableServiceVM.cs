using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace JobShopWeb.Models.ViewModel
{
    public class ProductionTableServiceVM
    {
        public IEnumerable<Job> JobList { get; set; }
        public IEnumerable<Plan> PlanList { get; set; }
    }
}
