using Food.DTOs;
using Food.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodWebbApp.Controllers
{
    public class LoginRegistrationController : Controller
    {
        private readonly IFoodRepo _foodRepo;

        public LoginRegistrationController(IFoodRepo foodRepo)
        {
            _foodRepo = foodRepo;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login(string UserName, string Password)
        {

            var myUser = _foodRepo.GetUsers()
                .Where(x => x.USERNAME.Equals(UserName, StringComparison.OrdinalIgnoreCase)
                            && x.PASSWORD.Equals(Password))
                .FirstOrDefault();

            if (myUser != null)
            {

                HttpContext.Session.SetString("UserSession",myUser.USERNAME);
                HttpContext.Session.SetInt32("UserID", myUser.USERID);
            }
           

            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public ActionResult UserDetails()
        {
            var current = HttpContext.Session.GetInt32("UserID");
            var user = _foodRepo.GetUsers().Where(x => x.USERID == current);
            var chu = user.FirstOrDefault();
            return View(chu);
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var product = _foodRepo.GetUsers().FirstOrDefault(p => p.USERID == id);

            return View(product);
        }
        [HttpPost]
        public IActionResult Update(UserDto model, IFormFile file)
        {    
                 var user = _foodRepo.GetUsers().FirstOrDefault(p => p.USERID == model.USERID);
                if (user == null)
                {
                    return NotFound();
                }

                // Update user details
                user.NAME = model.NAME;
                user.USERNAME = model.USERNAME;
                user.ADDRESS = model.ADDRESS;
                user.POSTCODE = model.POSTCODE;
                user.EMAIL = model.EMAIL;
                user.MOBILE = model.MOBILE;

                // Handle image upload
                if (file != null && file.Length > 0)
                {
                    var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/IMAGE");
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(webRootPath, fileName);

                    // Save the new image
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Update the user with the new image URL
                    user.IMAGEURL = $"/IMAGE/{fileName}";
                }
                else
                {
                    // Retain the existing image if no new file is uploaded
                    user.IMAGEURL = user.IMAGEURL;
                }

                _foodRepo.UpdatDate(user);
                return RedirectToAction("Index", "Home");
            

           
        }
        // GET: LoginRegistrationController/Create
        public ActionResult Add()
        {

            return View();
        }

        // POST: LoginRegistrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(UserDto userDto, IFormFile IMAGE)
        {
           
                if (IMAGE != null && IMAGE.Length > 0)
                {
                   
                    var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/IMAGE");
                    Directory.CreateDirectory(Path.Combine(webRootPath, "IMAGE"));
                    var fileName = Path.GetFileName(IMAGE.FileName);
                    var path = Path.Combine(webRootPath,fileName);
                    userDto.IMAGEURL = "/IMAGE/" + fileName; // Store the relative path
                   
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        IMAGE.CopyTo(stream);
                    }
                    userDto.IMAGEURL=$"/IMAGE/{fileName}";
                }

                _foodRepo.AddUser(userDto);
                return RedirectToAction("Index", "Home");
          
        }



        // GET: LoginRegistrationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginRegistrationController/Edit/5
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

        // GET: LoginRegistrationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginRegistrationController/Delete/5
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
