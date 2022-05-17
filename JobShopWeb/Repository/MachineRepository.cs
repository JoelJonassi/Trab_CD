using JobShopWeb.Models;
using JobShopWeb.Repository.IRepository;
using System.Net.Http;

namespace JobShopWeb.Repository
{
    public class MachineRepository : Repository<Machine>, IMachineRepository
    {
        private readonly IHttpClientFactory _clienteFactory;

        public MachineRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clienteFactory = clientFactory;

        }
    }
}
