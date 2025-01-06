using Microsoft.AspNetCore.Mvc;
using Medictionary.DTOS;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;

namespace Medictionary.Controllers
{
    public class IdentityController : Controller
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityController(ILogger<IdentityController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(loginDTO);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        // [HttpPost]
        // public async Task<IActionResult> Register(RegisterDTO registerDTO)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var user = new MongoIdentityUser<string> { UserName = registerDTO.Email, Email = registerDTO.Email };
        //         var result = await _userManager.CreateAsync(user, registerDTO.Password);
        //         var role =await _roleManager.FindByNameAsync("User");
        //         if(role == null){
        //             var newRole = new MongoDbIdentityRole("USER");
        //             await _roleManager.CreateAsync(newRole);
        //         }
        //         await _userManager.AddToRoleAsync(user, "USER");
        //         if (result.Succeeded)
        //         {
        //             _logger.LogInformation("User created a new account with password.");

        //             await _signInManager.SignInAsync(user, isPersistent: false);
        //             return RedirectToAction("Index","Home");
        //         }
        //         foreach (var error in result.Errors)
        //         {
        //             ModelState.AddModelError(string.Empty, error.Description);
        //         }
        //     }
        //     return View();
        // }

        public IActionResult LogOut(){
            HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return RedirectToAction("Login","Identity");
        }
    }
}