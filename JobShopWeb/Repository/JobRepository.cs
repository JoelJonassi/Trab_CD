using JobShopWeb.Models;
using JobShopWeb.Repository.IRepository;
using System.Net.Http;

namespace JobShopWeb.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private readonly IHttpClientFactory _clienteFactory;

        public JobRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clienteFactory = clientFactory;

        }
    }
}
