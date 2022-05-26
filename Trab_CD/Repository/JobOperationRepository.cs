using JobShopAPI.Data;
using JobShopAPI.Models;
using JobShopAPI.Repository.Interfaces;

namespace JobShopAPI.Repository
{
    public class JobOperationRepository : IJobOperationRepository
    {
        //Injeção de dependeçia
        private readonly ApplicationDbContext _db;

        public JobOperationRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Função que cria uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Create(JobOperation simulation)
        {
            _db.JobOperation.Add(simulation);
            return Save();
        }



        /// <summary>
        /// Função que verifica se a simulação já existe
        /// </summary>
        /// <param name="idSimulation"></param>
        /// <returns></returns>
        public bool Exists(int IdSimulation)
        {
            return _db.JobOperation.Any(simu => simu.IdJob == IdSimulation);
        }

        /// <summary>
        /// Função que mostra todas as simulações
        /// </summary>
        /// <returns></returns>
        public ICollection<JobOperation> GetAll()
        {
            return _db.JobOperation.OrderBy(simu => simu.IdJob).ToList();
        }

        /// <summary>
        /// Função que mostra uma simulação
        /// </summary>
        /// <param name="IdSimulation"></param>
        /// <returns></returns>
        public JobOperation Get(int IdSimulation)
        {
            return _db.JobOperation.FirstOrDefault(simu => simu.IdJob == IdSimulation);
        }


        /// <summary>
        /// Função que atualiza uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Update(JobOperation simulation)
        {
            _db.JobOperation.Update(simulation);
            return Save();
        }

        /// <summary>
        /// Função que apaga uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Delete(JobOperation simulation)
        {
            _db.JobOperation.Remove(simulation);
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
