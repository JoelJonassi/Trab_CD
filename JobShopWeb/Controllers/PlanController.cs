﻿using FastMember;
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
    public class PlanController : Controller
    {
        private readonly IJobRepository _job;
        private readonly IOperationRepository _operation;
        private readonly IMachineRepository _machine;
        private readonly IPlanRepository _plan;


        public PlanController(IPlanRepository plan, IJobRepository job, IMachineRepository machine, IOperationRepository operation)
        {
            _operation = operation;
            _machine = machine;
            _job = job;
            _plan = plan;
        }
        public async Task<IActionResult> Index()
        {
            ProductionTableServiceVM listOfParksAndTrails = new ProductionTableServiceVM()
            {
                PlanList = await _plan.GetAllAsync(UriAPI.PlansApiPath, HttpContext.Session.GetString("JWToken")),
            };
            return View(listOfParksAndTrails);


        }

        public async Task<IActionResult> GetAllPlans()
        {
            return Json(new { data = await _job.GetAllAsync(UriAPI.PlansApiPath, HttpContext.Session.GetString("JWToken")) });
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
        /// Função para fazer download do ficheiro
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ExportToExcellProductionPlan()
        {
            // LicenseContext of the ExcelPackage class:
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //Sua lista que está enviando apra View.

            IEnumerable<Plan> planList = await _plan.GetAllAsync(UriAPI.PlansApiPath, HttpContext.Session.GetString("JWToken"));
            //JobList = await _job.GetAllAsync(UriAPI.JobsApiPath, HttpContext.Session.GetString("JWToken")),

            DataTable dt = new DataTable();
            using (var reader = ObjectReader.Create(planList))
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
            if (fileContents == null || fileContents.Length == 0)
            {
                return NotFound();
            }
            return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "ProductionPlan.xlsx"
                );
            //return View(JobList);

        }
        [HttpGet]
        public IActionResult NotImplemented()
        {
            return View();
        }

    }
}
