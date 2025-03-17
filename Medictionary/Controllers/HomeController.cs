using Microsoft.AspNetCore.Mvc;
using Medictionary.Models;
using Medictionary.Services.Interfaces;
using Medictionary.Store.Interface;
using Medictionary.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Medictionary.Helpers;

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
            var industry = _applicationDbContext.Industries.Find(industryId);

            if (industry == null)
            {
                return BadRequest("Invalid industry ID.");
            }

            var medicines = _applicationDbContext.Medicines
                .Where(m => m.IndustryID == industryId)
                .Include(m => m.MedicineImage)
                .ToList();

            ViewBag.IndustryName = industry.Name;

            return PartialView("_MedicineDetails", medicines);
        }
        
        [HttpGet]
        public async Task<IActionResult> Stockiest()
        {
            var stockiests = await _userManager.GetUsersInRoleAsync("Stockiest");
            return View(stockiests);
        }
        
        [HttpPost]
        public IActionResult AddToCart(string medicineId)
        {
            if (string.IsNullOrEmpty(medicineId))
            {
                return BadRequest("Invalid medicine ID.");
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<string>>("Cart") ?? new List<string>();
            cart.Add(medicineId);
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return Ok(new { success = true, message = "Product added to cart." });
        }

        [HttpGet]
        public IActionResult Cart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<string>>("Cart") ?? new List<string>();
            var medicines = _applicationDbContext.Medicines
                .Where(m => cart.Contains(m.MedicineID))
                .Include(m => m.MedicineImage)
                .ToList();

            return View(medicines);
        }
        // [HttpGet]
        // public async Task<IActionResult> GetStockiests(string medicineId)
        // {
        //     if (string.IsNullOrEmpty(medicineId))
        //     {
        //         return BadRequest("Invalid medicine ID.");
        //     }

        //     var stockiests = await _applicationDbContext.StockiestMedicines
        //         .Where(sm => sm.MedicineID == medicineId)
        //         .Include(sm => sm.Stockiest)
        //         .Select(sm => sm.Stockiest)
        //         .ToListAsync();

        //     return View("StockiestDetails", stockiests);
        // }

        [HttpGet]
        public async Task<IActionResult> GetStockiests(string industryId)
        {
            var industry = _applicationDbContext.Industries.Find(industryId);

            if (industry == null)
            {
                return BadRequest("Invalid industry ID.");
            }

            var approvedStockiests = await _applicationDbContext.StockiestRequests
                .Where(sr => sr.IndustryID == industryId && sr.Status == "Approved")
                .Include(sr => sr.Stockiest)
                .ToListAsync();

            ViewBag.IndustryName = industry.Name;

            return PartialView("_StockiestDetails", approvedStockiests);
        }
    }
}