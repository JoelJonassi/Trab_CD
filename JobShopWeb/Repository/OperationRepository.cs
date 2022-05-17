using JobShopWeb.Models;
using JobShopWeb.Repository.IRepository;
using System.Net.Http;

namespace JobShopWeb.Repository
{
    public class OperationRepository : Repository<Operation>, IOperationRepository
    {
        private readonly IHttpClientFactory _clienteFactory;

        public OperationRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clienteFactory = clientFactory;

        }
    }
}
