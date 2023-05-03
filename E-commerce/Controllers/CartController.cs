using E_commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDBContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(ApplicationDBContext db,UserManager<ApplicationUser> _userManager)
        {
            this.db = db;
            userManager = _userManager;
        }
        [HttpGet]
        public async Task< IActionResult >Cart()
        {
            var user = await userManager.GetUserAsync(User);
            var res = db.Cart.Include(p => p.Product).Where(u => u.UserId == user.Id).ToList();        

            return View(res);
        }
        [HttpPost]
        public async Task <IActionResult> AddToCart(Cart c,int qty)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == c.ProductId);
            var user = await userManager.GetUserAsync(User);
            var cart = new Cart
            {
                UserId = user.Id,
                ProductId = product.Id,
                Quantity = qty,
            };
            var shopCart=db.Cart.FirstOrDefault(u=>u.UserId==user.Id && u.ProductId==c.ProductId);
            if (qty <= 0)
            {
                qty = 1;
            }
            if (shopCart == null)
            {
                db.Cart.Add(c);
            }
            else
            
                shopCart.Quantity += c.Quantity;
    
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int id)
        {


            var user = await userManager.GetUserAsync(User);


            var shopcart = db.Cart.FirstOrDefault(u => u.UserId == user.Id && u.Id == id);

            if (shopcart != null)
                db.Cart.Remove(shopcart);


            db.SaveChanges();

            return RedirectToAction(nameof(Cart));
        }

    }
}
