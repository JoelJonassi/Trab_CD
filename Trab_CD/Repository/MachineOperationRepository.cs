using JobShopAPI.Data;
using JobShopAPI.Models;
using JobShopAPI.Repository.Interfaces;

namespace JobShopAPI.Repository
{
    public class MachineOperationRepository : IMachineOperationRepository
    {

        ///Injeção de dependeçia
        private readonly ApplicationDbContext _db;

        public MachineOperationRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Função que cria uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Create(MachineOperation simulation)
        {
            _db.MachineOperation.Add(simulation);
            return Save();
        }



        /// <summary>
        /// Função que verifica se a simulação já existe
        /// </summary>
        /// <param name="idSimulation"></param>
        /// <returns></returns>
        public bool Exists(int IdSimulation)
        {
            return _db.MachineOperation.Any(simu => simu.IdMachine == IdSimulation);
        }

        /// <summary>
        /// Função que mostra todas as simulações
        /// </summary>
        /// <returns></returns>
        public ICollection<MachineOperation> GetAll()
        {
            return _db.MachineOperation.OrderBy(simu => simu.IdMachine).ToList();
        }

        /// <summary>
        /// Função que mostra uma simulação
        /// </summary>
        /// <param name="IdSimulation"></param>
        /// <returns></returns>
        public MachineOperation Get(int IdSimulation)
        {
            return _db.MachineOperation.FirstOrDefault(simu => simu.IdMachine == IdSimulation);
        }


        /// <summary>
        /// Função que atualiza uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Update(MachineOperation simulation)
        {
            _db.MachineOperation.Update(simulation);
            return Save();
        }

        /// <summary>
        /// Função que apaga uma simulação
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Delete(MachineOperation simulation)
        {
            _db.MachineOperation.Remove(simulation);
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
