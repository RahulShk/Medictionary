using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Medictionary.DTOS;
using Microsoft.AspNetCore.Authentication;
using Medictionary.Models;
using Medictionary.Services.Interfaces;
using Medictionary.Store.Interface;
using System;
using System.IO;

namespace Medictionary.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityController> _logger;
        private readonly IFileService _fileService;
        private readonly IStore<Image> _imageStore;

        public IdentityController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityController> logger,
            IFileService fileService,
            IStore<Image> imageStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _fileService = fileService;
            _imageStore = imageStore;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDTO);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");

                var user = await _userManager.FindByNameAsync(loginDTO.Username);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (roles.Contains("Stockiest"))
                {
                    return RedirectToAction("Dashboard", "Stockiest");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginDTO);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDTO);
            }

            if (registerDTO.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The image file is required.");
                return View(registerDTO);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(registerDTO.ImageFile.FileName)}";
            var filePath = await _fileService.SaveFileAsync("user", fileName, registerDTO.ImageFile);

            var image = new Image
            {
                ImageId = Guid.NewGuid().ToString(),
                FileName = registerDTO.ImageFile.FileName,
                FilePath = filePath,
                CreatedBy = User?.Identity?.Name,
                CreatedDate = DateTime.UtcNow
            };
            _imageStore.InsertOne(image);

            var user = new User
            {
                UserId = Guid.NewGuid().ToString(),
                UserName = registerDTO.Username,
                Email = registerDTO.Username,
                Name = registerDTO.Name,
                Address = registerDTO.Address,
                ContactNo = registerDTO.ContactNo,
                ProfileImage = image
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerDTO);
            }

            if (!await _roleManager.RoleExistsAsync("USER"))
            {
                await _roleManager.CreateAsync(new IdentityRole("USER"));
            }
            
            await _userManager.AddToRoleAsync(user, "USER");

            _logger.LogInformation("User created a new account with password.");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }
    }
}