using Food.DTOs;
using Food.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodWebbApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdmin _admin;

        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            var products = _admin.GetProducts();

            if (products == null)
            {
                products = new List<ProductDTO>(); // Initialize with an empty list if null
            }

            return View(products);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            var product = _admin.GetProductById(id); 
           
          
            return View(product);
        }

        // GET: AdminController/Create
        public ActionResult AddProduct()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductDTO productDTO, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ADDPROIMG");
                Directory.CreateDirectory(path);

                var fileName = Path.GetFileName(file.FileName);
                var filepath = Path.Combine(path, fileName);

                // Save the file to the specified path
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Set the IMAGEURL property to the relative path of the image
                productDTO.IMAGEURL = $"/ADDPROIMG/{fileName}";
            }

            // Add the product using the repository
            _admin.AddProduct(productDTO);

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _admin.GetProducts().FirstOrDefault(p => p.PRODUCTID == id);
            
            return View(product);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ProductDTO productDTO, IFormFile file)
        {
            if (file != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ADDPROIMG",file.FileName);
               // Directory.CreateDirectory(path);

                //var fileName = Path.GetFileName(file.FileName);
                //var filepath = Path.Combine(path, fileName);

                // Save the file to the specified path
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Set the IMAGEURL property to the relative path of the image
                productDTO.IMAGEURL = $"/ADDPROIMG/{file.FileName}";
            }

            _admin.UpdatDate(productDTO);
              //  _admin.UpdateProduct(productDTO);
                return RedirectToAction("Index","Admin");
            
            
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
           var obj = _admin.GetProducts().FirstOrDefault(m => m.PRODUCTID == id);
            _admin.DeleteData(id);
            return RedirectToAction("Index", "Admin");
        }

        // POST: AdminController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            
               // Delete the product by ID
                return RedirectToAction("Index", "Admin");
            
          
        }
    }
}
