using JobShopWeb.Models;
using JobShopWeb.Repository.IRepository;
using System.Net.Http;

namespace JobShopWeb.Repository
{
    public class SimulationRepository : Repository<Simulation>, ISimulationRepository
    {
        private readonly IHttpClientFactory _clienteFactory;

        public SimulationRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clienteFactory = clientFactory;

        }

    }
}
