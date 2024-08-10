using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoVeterinaria.Controllers
{
    public class GestionExpedienteController : Controller
    {
        // GET: GestionExpedienteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GestionExpedienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GestionExpedienteController/RegistroExpediente
        public ActionResult RegistroExpediente()
        {
            return View();
        }

        // POST: GestionExpedienteController/Create
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

        // GET: GestionExpedienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GestionExpedienteController/Edit/5
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

        // GET: GestionExpedienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GestionExpedienteController/Delete/5
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
