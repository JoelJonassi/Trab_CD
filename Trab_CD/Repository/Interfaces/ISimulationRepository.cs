using JobShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobShopAPI.Repository.IRepository
{
    public interface ISimulationRepository
    {
        ICollection<Simulation> GetSimulation();
        Simulation GetSimulations(int IdSimulation);
        bool CreateSimulation(Simulation simulation);
        bool UpdateSimulation(Simulation simulation);
        bool DeleteSimulation(Simulation simulation);
        bool Save();
    }
}
