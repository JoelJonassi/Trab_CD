using FastMember;
using JobShopWeb.Models;
using JobShopWeb.Models.ViewModel;
using JobShopWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using OfficeOpenXml;

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
            _job = job;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index() {
            ProductionTableServiceVM jobs = new ProductionTableServiceVM()
            {
                JobList = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken")),
            };
            return View(jobs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            Job obj = new Job();

            if (id == null)
            {
                //this will be true for Insert/Create
                return View(obj);
            }
            //Flow will come here for update
            obj = await _job.GetAsync(UriAPI.JobsApiPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Job obj)
        {
            if (ModelState.IsValid)
            {
                
                if (obj.IdJob == 0)
                {
                    await _job.CreateAsync(UriAPI.JobsApiPath, obj, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _job.UpdateAsync(UriAPI.JobsApiPath + obj.IdJob, obj, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllJobs()
        {
            return Json(new { data = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken")) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Job"></param>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public async Task<IActionResult> DoPlan(int Job, int Operation)
        {
            return Json(new { data = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken")) });
        }

        /// <summary>
        /// Função para fazer Download do ficheiro da tabela de produção 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ExportToExcellProductionTable()
        {
            // LicenseContext of the ExcelPackage class:
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // Lincença não comercial para utilizar o excell
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
          
            IEnumerable<Job> jobList = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken"));

            DataTable dt = new DataTable();
            using(var reader = ObjectReader.Create(jobList))
            {
                dt.Load(reader);
            }

            byte[] fileContents;
            using (var package = new ExcelPackage(new FileInfo("MyWorkbook.xlsx")))
            {
                var workSheet = package.Workbook.Worksheets.Add("jobList");
                workSheet.Cells["A1"].LoadFromDataTable(dt, true);
                fileContents = package.GetAsByteArray();
            }
            if(fileContents == null || fileContents.Length == 0)
            {
                return NotFound();
            }
            return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "ProductionTable.xlsx"
                );

        }
    }
}

