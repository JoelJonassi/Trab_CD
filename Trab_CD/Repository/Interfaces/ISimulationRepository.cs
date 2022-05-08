using JobShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobShopAPI.Repository.IRepository
{
    public interface ISimulationRepository
    {
        bool SimulationExists(int IdSimulation);
        ICollection<Simulation> GetSimulations();
        Simulation GetSimulation(int IdSimulation);
        bool CreateSimulation(Simulation simulation);
        bool UpdateSimulation(Simulation simulation);
        bool DeleteSimulation(Simulation simulation);
        bool Save();
    }
}
