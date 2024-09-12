using Food.DTOs;
using Food.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FoodWebbApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly IAdmin _admin;

        public MenuController(IAdmin admin)
        {
            _admin = admin;
        }
        // GET: MenuController
        public ActionResult Menu()
        {
            var obj = _admin.GetProducts();
            var Model = new ProductDTO
            {


                FastFood = obj.Where(p =>p.CATEGORYNAME !=null && p.CATEGORYNAME.Trim().Equals("Fast Food", StringComparison.OrdinalIgnoreCase)),
                SouthIndian = obj.Where(p => p.CATEGORYNAME.Equals("South Indian", StringComparison.OrdinalIgnoreCase)),
                Chinese = obj.Where(p => p.CATEGORYNAME.Equals("Chinese", StringComparison.OrdinalIgnoreCase)),
                Italian = obj.Where(p => p.CATEGORYNAME.Equals("Italian", StringComparison.OrdinalIgnoreCase)),
                Mexican = obj.Where(p => p.CATEGORYNAME.Equals("Mexican", StringComparison.OrdinalIgnoreCase)),
                Desserts = obj.Where(p => p.CATEGORYNAME.Equals("Desserts", StringComparison.OrdinalIgnoreCase)),
                Beverages = obj.Where(p => p.CATEGORYNAME.Equals("Beverages", StringComparison.OrdinalIgnoreCase)),
                NorthIndian = obj.Where(p => p.CATEGORYNAME.Equals("North Indian", StringComparison.OrdinalIgnoreCase)),
                Continental = obj.Where(p => p.CATEGORYNAME.Equals("Continental", StringComparison.OrdinalIgnoreCase)),
                Seafood = obj.Where(p => p.CATEGORYNAME.Equals("Seafood", StringComparison.OrdinalIgnoreCase)),


            };
            return View(Model);


            // Categorize products

        }



        // GET: MenuController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenuController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuController/Create
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

        // GET: MenuController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuController/Edit/5
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

        // GET: MenuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuController/Delete/5
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
