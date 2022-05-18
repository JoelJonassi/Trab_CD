using JobShopWeb.Models;
using JobShopWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobShopWeb.Controllers
{
    [Authorize]
    public class ProductionTableServiceController : Controller
    {
        private readonly IJobRepository _job;


        public ProductionTableServiceController(IJobRepository job)
        {
            _job = job;
        }
        public IActionResult Index()
        {
            return View(new Job() { });
        }

        public async Task<IActionResult> GetAllJobs()
        {
            return Json(new { data = await _job.GetAllAsync(UriAPI.ProductionTableServicePath, HttpContext.Session.GetString("JWToken")) });
        }
    }
}

