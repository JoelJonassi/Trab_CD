using JobShopAPI.Data;
using JobShopAPI.Models;
using JobShopAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace JobShopAPI.Repository
{
    public class SimulationRepository : ISimulationRepository
    {
        //Injeção de dependeçia
        private readonly ApplicationDbContext _db;

        public SimulationRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Função que cria uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CreateSimulation(Simulation simulation)
        {
            _db.Simulations.Add(simulation);
            return Save();
        }


        /// <summary>
        /// Função que verifica se a simulação já existe
        /// </summary>
        /// <param name="idSimulation"></param>
        /// <returns></returns>
        public bool SimulationExists(int idSimulation)
        {
            return _db.Simulations.Any(simu => simu.IdSimulation == idSimulation);
        }

        /// <summary>
        /// Função que mostra todas as simulações
        /// </summary>
        /// <returns></returns>
        public ICollection<Simulation> GetSimulations()
        {
            return _db.Simulations.OrderBy(simu => simu.IdSimulation).ToList();
        }

        /// <summary>
        /// Função que mostra uma simulação
        /// </summary>
        /// <param name="IdSimulation"></param>
        /// <returns></returns>
        public Simulation GetSimulation(int IdSimulation)
        {
            return _db.Simulations.FirstOrDefault(simu => simu.IdSimulation == IdSimulation);
        }


        /// <summary>
        /// Função que atualiza uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateSimulation(Simulation simulation)
        {
            _db.Simulations.Update(simulation);
            return Save();
        }

        /// <summary>
        /// Função que apaga uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteSimulation(Simulation simulation)
        {
           _db.Simulations.Remove(simulation);
            return Save();
        }

        /// <summary>
        /// Função que guarda uma simulação
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Save()
        {
           return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
