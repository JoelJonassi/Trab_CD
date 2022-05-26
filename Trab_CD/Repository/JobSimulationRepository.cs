using JobShopAPI.Data;
using JobShopAPI.Models;
using JobShopAPI.Repository.Interfaces;

namespace JobShopAPI.Repository
{
    public class JobSimulationRepository : IJobSimulationRepository
    {
        ///Injeção de dependeçia
        private readonly ApplicationDbContext _db;

        public JobSimulationRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Função que cria uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Create(JobSimulation simulation)
        {
            _db.JobSimulation.Add(simulation);
            return Save();
        }



        /// <summary>
        /// Função que verifica se a simulação já existe
        /// </summary>
        /// <param name="idSimulation"></param>
        /// <returns></returns>
        public bool Exists(int IdSimulation)
        {
            return _db.JobSimulation.Any(simu => simu.IdJob == IdSimulation);
        }

        /// <summary>
        /// Função que mostra todas as simulações
        /// </summary>
        /// <returns></returns>
        public ICollection<JobSimulation> GetAll()
        {
            return _db.JobSimulation.OrderBy(simu => simu.IdJob).ToList();
        }

        /// <summary>
        /// Função que mostra uma simulação
        /// </summary>
        /// <param name="IdSimulation"></param>
        /// <returns></returns>
        public JobSimulation Get(int IdSimulation)
        {
            return _db.JobSimulation.FirstOrDefault(simu => simu.IdJob == IdSimulation);
        }


        /// <summary>
        /// Função que atualiza uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Update(JobSimulation simulation)
        {
            _db.JobSimulation.Update(simulation);
            return Save();
        }

        /// <summary>
        /// Função que apaga uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Delete(JobSimulation simulation)
        {
            _db.JobSimulation.Remove(simulation);
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
