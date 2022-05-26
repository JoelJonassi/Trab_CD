using JobShopAPI.Data;
using JobShopAPI.Models;
using JobShopAPI.Repository.Interfaces;

namespace JobShopAPI.Repository
{
    public class PlanRepository : IPlanRepository
    {
        ///Injeção de dependeçia
        private readonly ApplicationDbContext _db;

        public PlanRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Função que cria uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Create(Plan simulation)
        {
            _db.Plan.Add(simulation);
            return Save();
        }



        /// <summary>
        /// Função que verifica se a simulação já existe
        /// </summary>
        /// <param name="idSimulation"></param>
        /// <returns></returns>
        public bool Exists(int IdSimulation, int IdJob, int IdMachine, int IdOperation)
        {
            return _db.Plan.Any(simu => simu.IdMachine == IdSimulation && simu.IdMachine == IdMachine && simu.IdOperation == IdOperation);
        }

        /// <summary>
        /// Função que verifica se existe plano na base de dados
        /// </summary>
        /// <param name="IdPlan"></param>
        /// <returns></returns>
        public bool Exists(int IdPlan)
        {
            return _db.Plan.Any(simu => simu.IdPlan == IdPlan);
        }

        /// <summary>
        /// Função que mostra todas as simulações
        /// </summary>
        /// <returns></returns>
        public ICollection<Plan> GetAll()
        {
            return _db.Plan.OrderBy(simu => simu.IdMachine).ToList();
        }

        /// <summary>
        /// Função que mostra uma simulação
        /// </summary>
        /// <param name="IdSimulation"></param>
        /// <returns></returns>
        public Plan Get(int IdPlan)
        {
            return _db.Plan.FirstOrDefault(simu => simu.IdPlan == IdPlan);
        }


        /// <summary>
        /// Função que atualiza uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Update(Plan simulation)
        {
            _db.Plan.Update(simulation);
            return Save();
        }

        /// <summary>
        /// Função que apaga uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Delete(Plan simulation)
        {
            _db.Plan.Remove(simulation);
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
