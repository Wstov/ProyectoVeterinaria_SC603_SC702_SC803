using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoVeterinaria.Controllers
{
    public class ContabilidadController : Controller
    {
        // GET: ContabilidadController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ContabilidadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContabilidadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContabilidadController/Create
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

        // GET: ContabilidadController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContabilidadController/Edit/5
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

        // GET: ContabilidadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContabilidadController/Delete/5
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
