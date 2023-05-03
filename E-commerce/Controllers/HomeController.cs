using E_commerce.Models;
using E_commerce.RepositoryModels;
using E_commerce.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace E_commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactRepo contactRepo;
        private readonly ApplicationDBContext db;

        public HomeController(ILogger<HomeController> logger,IContactRepo contactRepo,ApplicationDBContext db)
        {
            _logger = logger;
            this.contactRepo = contactRepo;
            this.db = db;
        }

        public IActionResult Index()
        {
            var items = new IndexVM
            {
                Products = db.Products.Take(5).ToList(),
                Categories = db.Categories.ToList()

            };
            return View(items);
        }
       
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact c)
        {
            if (ModelState.IsValid)
            {
                contactRepo.Create(c);
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public IActionResult ShopDetail(int ?id )
        {
            var item = db.Products.Include(a => a.Category).FirstOrDefault(p => p.Id == id);
            return View(item);
        }
        public async Task<List<Product>> GetPage(IQueryable<Product>result,int pagenumber)
        {

            const int pagesize = 2;
            decimal rowcount = await db.Products.CountAsync();
            var pagecount = Math.Ceiling(rowcount/ pagesize);
            if (pagenumber > pagecount)
            {
                pagenumber = 1;
            }
            pagenumber = pagenumber <= 0 ? 1 : pagenumber;
            int skipcount = (pagenumber - 1) * pagesize;
            var pagedata=await result.Skip(skipcount).Take(pagesize).ToListAsync();
            ViewBag.curr = pagenumber;
            ViewBag.pagecount = pagecount;
            return pagedata;



        }
        public async Task< IActionResult> Shop(int page=1) {

            var products = db.Products;
            var model = await GetPage(products, page);
            return View(model);
        }
        [HttpPost]
        public IActionResult Search(string name)
        {
            var items = db.Products.Where(p => p.Name == name).ToList();
            return View(items);
        }
        public IActionResult ProductCat(int id)
        {
            var items = db.Products.Where(p => p.CatId == id).ToList();
            return View(items);
        }
        public IActionResult CheckOut() {
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}