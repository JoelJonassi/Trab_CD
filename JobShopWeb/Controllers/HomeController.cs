using JobShopAPI.Repository.Interfaces;
using JobShopWeb.Models;
using JobShopWeb.Models.ViewModel;
using JobShopWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobShopWeb.Controllers
{
    public class HomeController : Controller
    {
       private readonly ILogger<HomeController> _logger;
       private readonly ISimulationRepository _simu;
        private readonly IAccountRepository _account;


        /// <summary>
        /// Dependecy Injection
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger, ISimulationRepository simu, IAccountRepository account) { 
           _logger = logger;
            _simu = simu;
            _account = account;

        }

      public async Task<IActionResult> Index()
        {
            IndexVM listOfParksAndTrails = new IndexVM()
            {
                //JobList = await _job.GetAllAsync(UriAPI.ProductionTableService),
                SimulationList = await _simu.GetAllAsync(UriAPI.SimulationsAPIPath, HttpContext.Session.GetString("JWToken")),
            };
            return View(listOfParksAndTrails);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            User obj = new User();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User obj)
        {
            User objUser = await _account.LoginAsync(UriAPI.AccountPath+"authenticate/", obj);
            if(objUser.Token == null)
            {
                TempData["alert"] = "Verifique a senha ou password!";
                return View();

                
            }
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, objUser.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, objUser.Role));

            var principal = new ClaimsPrincipal(identity);
            //Inicia a sessão do utilizador automaticamente
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            //Definir a sessão
            HttpContext.Session.SetString("JWToken", objUser.Token);

            TempData["alert"] = "Bem Vindo " + objUser.Username;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User obj)
        {
           bool result = await _account.RegisterAsync(UriAPI.AccountPath + "register/", obj);
            if (result == false)
            {
                return View();
            }
            //Caso o utiulizador tenha se registado deve ser redirecionado a página inicial.
            TempData["alert"] = "Registado com Sucesso";
            return RedirectToAction("Login");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            //Fechar a sessão
            HttpContext.Session.SetString("JWToken", "");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            
            return View();
        }
    }
}
