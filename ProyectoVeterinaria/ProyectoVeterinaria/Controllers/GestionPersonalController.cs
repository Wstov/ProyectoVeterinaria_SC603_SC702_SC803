using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoVeterinaria.Controllers
{
    public class GestionPersonalController : Controller
    {
        // GET: GestionPersonalController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GestionPersonalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GestionPersonalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GestionPersonalController/Create
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

        // GET: GestionPersonalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GestionPersonalController/Edit/5
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

        // GET: GestionPersonalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GestionPersonalController/Delete/5
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
