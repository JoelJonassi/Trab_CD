using System.Collections.Generic;

namespace JobShopWeb.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<Simulation> SimulationList { get; set; }

        public IEnumerable<Operation> OperationList { get; set; }

        public IEnumerable<Machine> MachineList { get; set; }

        public IEnumerable<Job> JobList { get; set; }
    }
}
