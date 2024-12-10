using Microsoft.AspNetCore.Mvc;
using Medictionary.Models;
using Medictionary.DTOS;
using Microsoft.AspNetCore.Authorization;
using Medictionary.Services.Interfaces;
using Medictionary.Store.Interface;
using Medictionary.Utility;

namespace Medictionary.Controllers.AdminController
{
    [Authorize(Roles = "ADMIN")]
    public class IndustryController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IStore<Industry> _industryStore;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<IndustryController> _logger;

        public IndustryController(IFileService fileService, IStore<Industry> industryStore, IWebHostEnvironment environment, ILogger<IndustryController> logger)
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

        public IActionResult Add()
        {
            var industryDto = new IndustryDTO(); 
            return View(industryDto);
        }

        [HttpPost]
        public IActionResult Add(IndustryDTO industryDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var industry = IndustryMapper.Map(industryDto);
                    _industryStore.InsertOne(industry);
                    TempData["SuccessMessage"] = "Industry added successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while saving the industry.");
                    ModelState.AddModelError("", "An error occurred while saving the details. Please try again later.");
                }
            }
            return View("Add", industryDto);
        }
    }
}
