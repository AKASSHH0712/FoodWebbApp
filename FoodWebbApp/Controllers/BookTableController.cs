using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodWebbApp.Controllers
{
    public class BookTableController : Controller
    {
        // GET: BookTableController
        public ActionResult BookTable()
        {
            return View();
        }

        // GET: BookTableController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookTableController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookTableController/Create
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

        // GET: BookTableController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookTableController/Edit/5
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

        // GET: BookTableController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookTableController/Delete/5
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
