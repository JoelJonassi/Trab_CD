using JobShopWeb.Models;
using JobShopWeb.Repository.IRepository;
using System.Net.Http;

namespace JobShopWeb.Repository
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        private readonly IHttpClientFactory _clienteFactory;

        public PlanRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clienteFactory = clientFactory;

        }
    }
}
