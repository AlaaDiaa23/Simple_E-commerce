using E_commerce.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerce.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser>_userManager,SignInManager<ApplicationUser>_signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var res =await signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Id = model.Id,
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email
                };
                user.Id = Guid.NewGuid().ToString();

                var res=await userManager.CreateAsync(user,model.Password);
                if (res.Succeeded)
                {
                    Claim claim=new Claim("user", "user"); 
                    await userManager.AddClaimAsync(user, claim);

                        
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }
        public async Task< IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index","Home");
        }
    }
}
