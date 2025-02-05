using Microsoft.AspNetCore.Mvc;
using Medictionary.Models;
using Medictionary.DTOS;
using Microsoft.AspNetCore.Authorization;
using Medictionary.Services.Interfaces;
using Medictionary.Store.Interface;
using Medictionary.Utility;
using Medictionary.Controllers.AdminController;
using Medictionary.Data;

namespace Medictionary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IStore<Industry> _industryStore;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IFileService fileService, IStore<Industry> industryStore, IWebHostEnvironment environment, ILogger<HomeController> logger)
        {
            _fileService = fileService;
            _industryStore = industryStore;
            _environment = environment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var industry = _industryStore.FilterBy(x => true).ToList();
                return View(industry);
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
    }
}