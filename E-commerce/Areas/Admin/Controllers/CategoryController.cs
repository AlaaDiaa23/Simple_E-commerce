using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("AdminRole")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepo;
        private readonly ApplicationDBContext db;

        public CategoryController(ICategoryRepository categoryRepo,ApplicationDBContext db)
        {
            this.categoryRepo = categoryRepo;
            this.db = db;
        }
        public IActionResult Index()
        {
            return View(categoryRepo.GettAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category, List<IFormFile> files)
        {
            //if (ModelState.IsValid)
           // {
                foreach (var item in files)
                {
                    if (files.Count > 0)
                    {
                        string imageName = Guid.NewGuid().ToString() + ".jpg";
                        string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", imageName);

                        using (var steam = System.IO.File.Create(path))
                        {
                            await item.CopyToAsync(steam);
                        }
                        category.Image = imageName;
                    }

                //}

            
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(categoryRepo.GetCategory(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category,List<IFormFile>files)
        {
            //if (ModelState.IsValid)
            //{
            foreach (var item in files)
            {
                if (files.Count > 0)
                {
                    string imageName = Guid.NewGuid().ToString() + ".jpg";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", imageName);

                    using (var steam = System.IO.File.Create(path))
                    {
                        await item.CopyToAsync(steam);
                    }
                    category.Image = imageName;
                }

                //}


                db.Categories.Update(category);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(category);
        }  
        [HttpGet]
        public IActionResult Delete(int id)
        {

            return View(categoryRepo.GetCategory(id));
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            if (category != null)
            {
                categoryRepo.Delete(category);
                return RedirectToAction(nameof(Index));

            }
            return View(category);
        }
    }
}
