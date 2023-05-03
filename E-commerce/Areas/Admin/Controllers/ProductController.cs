using E_commerce.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("AdminRole")]

    public class ProductController : Controller
    {
        private readonly ApplicationDBContext db;

        public ProductController(ApplicationDBContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var items = db.Products.Include(p => p.Category).ToList();
            return View(items);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["category"] = new SelectList(db.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product, List<IFormFile> files)
        {
            if (ModelState.IsValid)
           {
            foreach (var item in files)
            {
                if (files.Count > 0)
                {
                    string imageName = Guid.NewGuid().ToString() + ".jpg";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Products", imageName);

                    using (var steam = System.IO.File.Create(path))
                    {
                        await item.CopyToAsync(steam);
                    }
                    product.Image = imageName;
                }

                }


                db.Products.Add(product);
                db.SaveChanges();
                ViewData["category"] = new SelectList(db.Categories, "Id", "Name");

                return RedirectToAction("Index");

            }
            return View(product);
        }


        [HttpGet]
        public IActionResult Edit(int ?id)
        {
            var item = db.Products.Find(id);
            ViewData["category"] = new SelectList(db.Categories, "Id", "Name");
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Product product, IFormFile files)
        {
            if (ModelState.IsValid)
            {
           
                if (files!=null)
                {
                    string imageName = Guid.NewGuid().ToString() + ".jpg";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Products", imageName);

                    using (var steam = System.IO.File.Create(path))
                    {
                        await files.CopyToAsync(steam);
                    }
                    product.Image = imageName;
                }
                else
                {
                    product.Image = product.Image;
                }

                
                db.Products.Update(product);
                db.SaveChanges();
                ViewData["category"] = new SelectList(db.Categories, "Id", "Name");

                return RedirectToAction("Index");

            }
          return View(product);

      }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Remove(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }
    }
}
