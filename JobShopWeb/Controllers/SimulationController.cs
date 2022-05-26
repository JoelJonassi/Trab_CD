using FastMember;
using JobShopWeb.Models;
using JobShopWeb.Models.ViewModel;
using JobShopWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JobShopWeb.Controllers
{
    [Authorize]
    public class SimulationController : Controller
    {
        private readonly ISimulationRepository _simu;
        private readonly IJobRepository _job;
        private readonly IMachineRepository _machine;
        private readonly IOperationRepository _operation;

        public SimulationController(ISimulationRepository sim, IJobRepository job, IMachineRepository machine, IOperationRepository operation)
        {
           _simu = sim;
           _job = job;
           _operation = operation;
           _machine = machine;
        }

       public IActionResult Index()
       {
            return View(new Simulation() { });
       }

       public async Task <IActionResult> GetAllSimulations()
       {
            return Json(new { data = await _simu.GetAllAsync(UriAPI.SimulationsAPIPath, HttpContext.Session.GetString("JWToken")) });
       }

        [Authorize]
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<Job> jobList = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken"));
            IEnumerable<Operation> operationList = await _operation.GetAllAsync(UriAPI.OperationsApiPath, HttpContext.Session.GetString("JWToken"));
            IEnumerable<Machine> machineList = await _machine.GetAllAsync(UriAPI.MachinesApiPath, HttpContext.Session.GetString("JWToken"));
            
            SimulationVM objVM = new SimulationVM()
            {                         
                JobList = jobList.Select(i => new SelectListItem
                {
                    Text = i.NameJob,
                    Value = i.IdJob.ToString()
                }),
                Simulation = new Simulation(),


            };

            if (id == null)
            {
                //this will be true for Insert/Create
                return View(objVM);
            }
            //string idString = id.+ "?simulationId=" + id
            //Flow will come here for update
            objVM.Simulation = await _simu.GetAsync(UriAPI.SimulationsAPIPath+id+"?simulationId=" , id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if (objVM.Simulation == null)
            {
                return NotFound();
            }
            return View(objVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SimulationVM obj)
        {
            if (ModelState.IsValid)
            {

                if (obj.Simulation.IdSimulation == 0)
                {
                    await _simu.CreateAsync(UriAPI.SimulationsAPIPath, obj.Simulation, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _simu.UpdateAsync(UriAPI.SimulationsAPIPath + obj.Simulation.IdSimulation, obj.Simulation, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<Job> jobList = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken"));
                IEnumerable<Machine> machineList = await _machine.GetAllAsync(UriAPI.MachinesApiPath, HttpContext.Session.GetString("JWToken"));
                IEnumerable<Operation> operationList = await _operation.GetAllAsync(UriAPI.OperationsApiPath, HttpContext.Session.GetString("JWToken"));


                SimulationVM objVM = new SimulationVM()
                {
                    JobList = jobList.Select(i => new SelectListItem
                    {
                        Text = i.NameJob,
                        Value = i.IdJob.ToString()
                    }),
                    Simulation = new Simulation(),

                };
                /*
                 SimulationVM objVM1 = new SimulationVM()
                 {

                     OperationList = operationList.Select(i => new SelectListItem
                     {
                         Text = i.OperationName,
                         Value = i.IdOperation.ToString()
                     }),
                     Simulation = new Simulation(),

                 };*/
                /*SimulationVM objVM2 = new SimulationVM()
                {

                    MachineList = machineList.Select(i => new SelectListItem
                    {
                        Text = i.MachineName,
                        Value = i.IdMachine.ToString()
                    }),
                    Simulation = new Simulation(),

                };*/

                return View(objVM);
            }
        }
        public async Task<IActionResult> GetAllJobsProductionTable()
        {
            return Json(new { data = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken")) });
        }

        public async Task<IActionResult> GetAllOperations()
        {
            return Json(new { data = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken")) });
        }

        public async Task<IActionResult> DoPlan(int Job, int Operation)
        {
            return Json(new { data = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken")) });
        }
        
        /// <summary>
        /// fazer download do ficheiro com os dados da simulação
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ExportToExcellSimulationTable()
        {
            // LicenseContext of the ExcelPackage class:
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //Sua lista que está enviando apra View.

            IEnumerable<Simulation> simulationList = await _simu.GetAllAsync(UriAPI.SimulationsAPIPath, HttpContext.Session.GetString("JWToken"));
            //JobList = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken")),

            DataTable dt = new DataTable();
            using (var reader = ObjectReader.Create(simulationList))
            {
                dt.Load(reader);
            }

            byte[] fileContents;
            using (var package = new ExcelPackage(new FileInfo("MyWorkbook.xlsx")))
            {
                var workSheet = package.Workbook.Worksheets.Add("simulationList");
                workSheet.Cells["A1"].LoadFromDataTable(dt, true);
                fileContents = package.GetAsByteArray();
            }
            if (fileContents == null || fileContents.Length == 0)
            {
                return NotFound();
            }
            return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "SimulationTable.xlsx"
                );
            //return View(JobList);

        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _simu.DeleteAsync(UriAPI.SimulationsAPIPath + id + "?simulationId=", id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new { success = true, message = "Apagado com Sucesso" });
            }
            return Json(new { success = false, message = "Falha na remoção" });
        }
    }


}


/*

//jQuery.ajax

[Authorize(Roles = "Admin")]
public async Task<IActionResult> Upsert(int? id)
{
    IEnumerable<Job> jobList = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken"));
    IEnumerable<Operation> operationList = await _operation.GetAllAsync(UriAPI.OperationsApiPath, HttpContext.Session.GetString("JWToken"));
    IEnumerable<Machine> machineList = await _machine.GetAllAsync(UriAPI.MachinesApiPath, HttpContext.Session.GetString("JWToken"));

    SimulationVM objVM = new SimulationVM()
    {
        JobList = jobList.Select(i => new SelectListItem
        {
            Text = i.NameJob,
            Value = i.IdJob.ToString()
        }),
        Simulation = new Simulation(),

    };

           /* SimulationVM objVM1 = new SimulationVM()
            {
                
                OperationList = operationList.Select(i => new SelectListItem
                {
                    Text = i.OperationName,
                    Value = i.IdOperation.ToString()
                }),
                Simulation = new Simulation(),

            };*/
/*SimulationVM objVM2 = new SimulationVM()
{

    MachineList = machineList.Select(i => new SelectListItem
    {
        Text = i.MachineName,
        Value = i.IdMachine.ToString()
    }),
    Simulation = new Simulation(),

};*/