using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("AdminRole")]

    public class HomeController : Controller
    {
        private readonly IContactRepo contactRepo;

        public HomeController(IContactRepo contactRepo)
        {
            this.contactRepo = contactRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowContact()
        {
            return View(contactRepo.GetContacts());
        }
    }
}
