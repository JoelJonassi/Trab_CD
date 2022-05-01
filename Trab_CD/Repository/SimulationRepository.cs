using JobShopAPI.Models;
using JobShopAPI.Repository.IRepository;

namespace JobShopAPI.Repository
{
    public class SimulationRepository : ISimulationRepository
    {

        public ICollection<Simulation> GetSimulation()
        {
            throw new NotImplementedException();
        }
        public Simulation GetSimulations(int IdSimulation)
        {
            throw new NotImplementedException();
        }
        public bool CreateSimulation(Simulation simulation)
        {
            throw new NotImplementedException();
        }
        public bool UpdateSimulation(Simulation simulation)
        {
            throw new NotImplementedException();
        }
        public bool DeleteSimulation(Simulation simulation)
        {
            throw new NotImplementedException();
        }
        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
