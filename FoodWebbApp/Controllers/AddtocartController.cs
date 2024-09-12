using Food.DTOs;
using Food.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FoodWebApp.Controllers
{
    public class AddtocartController : Controller
    {
        private readonly IAddtocart _addtocart;

        public AddtocartController(IAddtocart addtocart)
        {
            _addtocart = addtocart;
        }
        [HttpGet]
        public IActionResult Cart()
        {
            var currentUserId = HttpContext.Session.GetInt32("UserID");
            if (currentUserId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var cartItems = _addtocart.GetCartData().Where(x => x.USERID == currentUserId);
            return View(cartItems); // Return the view with cart data
        }
        [HttpGet]
        public JsonResult GetCartDataJson()
        {
            var currentUserId = HttpContext.Session.GetInt32("UserID");
            if (currentUserId == null)
            {
                return Json(new { success = false });
            }

            var cartItems = _addtocart.GetCartData().Where(x => x.USERID == currentUserId);
            return Json(cartItems);
        }

        [HttpPost]
        public ActionResult IsProductInCart(AddtoCartDTO addtocartDTO)
        {
            var currentUserId = HttpContext.Session.GetInt32("UserID");
            if (currentUserId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var isInCart = _addtocart.GetCartData().FirstOrDefault(x => x.USERID == currentUserId && x.PRODUCTID == addtocartDTO.PRODUCTID);
            TempData["OrderDetails"] = JsonConvert.SerializeObject(addtocartDTO);

            if (isInCart == null)
            {
                return RedirectToAction("AddToCart");
            }
            else
            {
                return RedirectToAction("EditQuantity");
            }
        }

        [HttpPost]
        public JsonResult UpdateQuantity(int cartId, int quantity)
        {
            try
            {
                var currentUserId = HttpContext.Session.GetInt32("UserID");
                if (currentUserId == null)
                {
                    return Json(new { success = false });
                }

                bool result = _addtocart.UpdateCartQuantity(currentUserId.Value, cartId, quantity);
                if (result)
                {
                    var cartItems = _addtocart.GetCartData().Where(x => x.USERID == currentUserId);
                    var updatedItem = cartItems.FirstOrDefault(item => item.CARTID == cartId);
                    decimal totalCartPrice = cartItems.Sum(item => item.PRICE * item.QUANTITY);

                    return Json(new
                    {
                        success = true,
                        updatedItemPrice = updatedItem.PRICE * quantity,
                    });
                }

                return Json(new { success = false });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult AddToCart()
        {
            var orderDetailsJson = TempData["OrderDetails"] as string;
            if (!string.IsNullOrEmpty(orderDetailsJson))
            {
                var dto = JsonConvert.DeserializeObject<AddtoCartDTO>(orderDetailsJson);
                if (dto != null)
                {
                    _addtocart.AddToCart(dto);
                }
            }
            return RedirectToAction("Index", "Home"); // Redirect to the home page
        }
        public IActionResult GetCartData()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId != null)
            {
                // Assuming GetCartByUserId is a method that fetches cart items for the user
                var cartItems = _addtocart.GetCartData().Where(x=> x.USERID == userId);
                return View(cartItems);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult EditQuantity()
        {
            var orderDetailsJson = TempData["OrderDetails"] as string;
            if (!string.IsNullOrEmpty(orderDetailsJson))
            {
                var dto = JsonConvert.DeserializeObject<AddtoCartDTO>(orderDetailsJson);
                if (dto != null)
                {
                    _addtocart.EditQuantity(dto.QUANTITY, dto.PRODUCTID);
                    _addtocart.EditPrice(dto.PRICE, dto.PRODUCTID);
                }
            }
            return RedirectToAction("Index", "Home"); // Redirect to the home page
        }
    }
}
