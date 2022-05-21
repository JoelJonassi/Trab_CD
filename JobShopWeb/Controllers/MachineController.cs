using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobShopWeb.Controllers
{
    public class MachineController : Controller
    {
        // GET: MachineController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MachineController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MachineController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MachineController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MachineController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MachineController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MachineController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MachineController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
