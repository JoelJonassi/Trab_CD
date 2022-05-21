using JobShopWeb.Models;
using JobShopWeb.Models.ViewModel;
using JobShopWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobShopWeb.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private readonly IJobRepository _job;
        private readonly IOperationRepository _operation;
        private readonly IMachineRepository _machine;


        public JobController(IJobRepository job, IMachineRepository machine, IOperationRepository operation)
        {
            _operation = operation;
            _machine = machine;
        }
        public async Task<IActionResult> Index() {
            ProductionTableServiceVM listOfParksAndTrails = new ProductionTableServiceVM()
            {
                OperationList = await _operation.GetAllAsync(UriAPI.MachinesApiPath, HttpContext.Session.GetString("JWToken")),
                JobList = await _job.GetAllAsync(UriAPI.SimulationsAPIPath, HttpContext.Session.GetString("JWToken")),
            };
            return View(listOfParksAndTrails);


        }

        public async Task<IActionResult> GetAllJobs()
        {
            return Json(new { data = await _job.GetAllAsync(UriAPI.MachinesApiPath, HttpContext.Session.GetString("JWToken")) });
        }
    }
}

