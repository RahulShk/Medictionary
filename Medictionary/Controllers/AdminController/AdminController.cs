using Microsoft.AspNetCore.Mvc;
using Medictionary.Models;
using Medictionary.DTOS;
using Microsoft.AspNetCore.Authorization;
using Medictionary.Services.Interfaces;
using Medictionary.Store.Interface;
using Medictionary.Utility;
using Medictionary.Data;

namespace Medictionary.Controllers.AdminController
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IStore<Industry> _industryStore;
        private readonly IStore<Medicine> _medicineStore;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IFileService fileService, IStore<Industry> industryStore, IStore<Medicine> medicineStore, ApplicationDbContext applicationDbContext, IWebHostEnvironment environment, ILogger<AdminController> logger)
        {
            _fileService = fileService;
            _industryStore = industryStore;
            _medicineStore = medicineStore;
            _applicationDbContext = applicationDbContext;
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

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var industry = _industryStore.FindById(id);

                if (industry == null)
                {
                    return RedirectToAction("Edit");
                }

                var viewmodel = new IndustryDTO
                {
                    Name = industry.Name,
                    Description = industry.Description,
                    Location = industry.Location
                };

                return View(viewmodel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving inudstry details for edit.");
                ModelState.AddModelError("Error", "An error occurred while retrieving the industry details for editing.");
                return RedirectToAction("Edit");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, IndustryDTO viewmodel)
        {
            try
            {
                var industry = _industryStore.FindById(id);

                if (industry == null)
                {
                    return RedirectToAction("Edit");
                }

                if (!ModelState.IsValid)
                {
                    return View(viewmodel);
                }

                industry.Name = viewmodel.Name;
                industry.Description = viewmodel.Description;
                industry.Location = viewmodel.Location;

                
                _industryStore.ReplaceOne(industry);

                return RedirectToAction("Edit");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating industry.");
                ModelState.AddModelError("Error", "An error occurred while updating the industry details.");
                return View(viewmodel);
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(string id)
        {
            try
            {
                var industry = _industryStore.FindById(id);

                if (industry == null)
                {
                    return RedirectToAction("Index");
                }

                _industryStore.DeleteById(id); 

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting industry.");
                ModelState.AddModelError("Error", "An error occurred while deleting the industry.");
                return RedirectToAction("Index");
            }
        }
        
        public IActionResult AddMedicine(string industryId)
        {
            var medicineDto = new MedicineDTO { IndustryID = industryId };
            return View(medicineDto);
        }

        [HttpPost]
        public IActionResult AddMedicine(MedicineDTO medicineDto)
        {
            _logger.LogInformation("AddMedicine POST method called.");
            if (ModelState.IsValid)
            {
                try
                {
                    var medicine = MedicineMapper.Map(medicineDto);
                    _applicationDbContext.Medicines.Add(medicine);
                    _applicationDbContext.SaveChanges();
                    TempData["SuccessMessage"] = "Medicine added successfully.";
                    _logger.LogInformation("Redirecting to Medicine action.");
                    return RedirectToAction("Medicine", new { id = medicine.IndustryID });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while saving the medicine.");
                    ModelState.AddModelError("", "An error occurred while saving the details. Please try again later.");
                }
            }
            else
            {
                _logger.LogWarning("Model state is invalid.");
            }
            return View(medicineDto);
        }

        [HttpGet("medicines/{id}")]
        public IActionResult Medicine(string id)
        {
            var medicines = _applicationDbContext.Medicines.Where(m => m.IndustryID == id).ToList();
            return View(medicines);
        }
    }
}