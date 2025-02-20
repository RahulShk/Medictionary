using Microsoft.AspNetCore.Mvc;
using Medictionary.Models;
using Medictionary.DTOS;
using Microsoft.AspNetCore.Authorization;
using Medictionary.Services.Interfaces;
using Medictionary.Store.Interface;
using Medictionary.Utility;
using Medictionary.Controllers.AdminController;
using Medictionary.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Medictionary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IStore<Industry> _industryStore;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IFileService fileService, IStore<Industry> industryStore, ApplicationDbContext applicationDbContext, IWebHostEnvironment environment, UserManager<User> userManager, ILogger<HomeController> logger)
        {
            _fileService = fileService;
            _industryStore = industryStore;
            _applicationDbContext = applicationDbContext;
            _environment = environment;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                _logger.LogInformation($"User ID retrieved: {userId}");

                var user = await _userManager.GetUserAsync(User);
                _logger.LogInformation($"Retrieved user: {user?.UserName}");

                if (user == null)
                {
                    _logger.LogWarning("User not found in database.");
                    return RedirectToAction("Login", "Identity");
                }

                _logger.LogInformation($"User Found: {user.Name}, Profile Image: {(user.ProfileImage != null ? user.ProfileImage.FilePath : "No Image")}");

                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving industry data.");
                ModelState.AddModelError("Error", "An error occurred while retrieving industry data.");
                return View("Error");
            }
        }

        public IActionResult Industry()
        {
            try
            {
                // Fetch all industries from the store
                var industries = _industryStore.FilterBy(x => true).ToList();

                // Check if the result is empty
                if (industries == null || !industries.Any())
                {
                    // Optionally return a different view or message
                    return View("EmptyIndustryList"); // Create a view for empty lists, if needed
                }

                // Return the industries to the view
                return View(industries);
            }
            catch (Exception ex)
            {
                // Log the exception (use a logger here)
                Console.WriteLine(ex.Message); // Replace with actual logging

                // Return an error view
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult GetMedicines(string industryId)
        {
            var medicines = _applicationDbContext.Medicines.Where(m => m.IndustryID == industryId)
                .ToList();

            return PartialView("_MedicineDetails", medicines);
        }
    }
}