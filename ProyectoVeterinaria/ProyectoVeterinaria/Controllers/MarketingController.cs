using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoVeterinaria.Controllers
{
    public class MarketingController : Controller
    {
        // GET: MarketingController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MarketingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MarketingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarketingController/Create
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

        // GET: MarketingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MarketingController/Edit/5
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

        // GET: MarketingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MarketingController/Delete/5
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
