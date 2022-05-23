using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace JobShopWeb.Models.ViewModel
{
    public class SimulationVM
    {
        public IEnumerable<SelectListItem> OperationList { get; set; }

        public IEnumerable<SelectListItem> MachineList { get; set; }

        public IEnumerable<SelectListItem> JobList { get; set; }

        public Simulation Simulation { get; set; }
    }
}
