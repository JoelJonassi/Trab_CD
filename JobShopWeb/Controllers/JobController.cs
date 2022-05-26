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
        public async Task<IActionResult> Index() {
            ProductionTableServiceVM listOfParksAndTrails = new ProductionTableServiceVM()
            {
                JobList = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken")),
            };
            return View(listOfParksAndTrails);


        }

        public async Task<IActionResult> GetAllJobs()
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

        public async Task<IActionResult> ExportToExcellProductionTable()
        {
            // LicenseContext of the ExcelPackage class:
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //Sua lista que está enviando apra View.

            IEnumerable<Job> jobList = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken"));
            //JobList = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken")),
           
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
                //return View(JobList);

        }
    }
}

