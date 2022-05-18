using JobShopWeb.Models;
using JobShopWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobShopWeb.Controllers
{
    [Authorize]
    public class SimulationController : Controller
    {
        private readonly ISimulationRepository _simu;

        public SimulationController(ISimulationRepository sim)
        {
            _simu = sim;
        }
        public IActionResult Index()
        {
            return View(new Simulation() { });
        }

       public async Task <IActionResult> GetAllSimulations()
        {
            return Json(new { data = await _simu.GetAllAsync(UriAPI.SimulationsAPIPath, HttpContext.Session.GetString("JWToken")) });
        }
    }
}

//jQuery.ajax