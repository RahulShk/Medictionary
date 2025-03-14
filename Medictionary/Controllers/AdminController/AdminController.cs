using Microsoft.AspNetCore.Mvc;
using Medictionary.Models;
using Medictionary.DTOS;
using Microsoft.AspNetCore.Authorization;
using Medictionary.Services.Interfaces;
using Medictionary.Store.Interface;
using Medictionary.Utility;
using Medictionary.Data;
using Microsoft.EntityFrameworkCore;

namespace Medictionary.Controllers.AdminController
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IStore<Industry> _industryStore;
        private readonly IStore<Medicine> _medicineStore;
        private readonly IStore<Image> _imageStore;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IFileService fileService, IStore<Industry> industryStore, IStore<Medicine> medicineStore, IStore<Image> imagestore, ApplicationDbContext applicationDbContext, IWebHostEnvironment environment, ILogger<AdminController> logger)
        {
            _fileService = fileService;
            _industryStore = industryStore;
            _medicineStore = medicineStore;
            _imageStore = imagestore;
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
        public async Task<IActionResult> AddMedicine(MedicineDTO medicineDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(medicineDTO);
            }

            if (medicineDTO.MedicineImageFile == null || medicineDTO.MedicineImageFile.Length == 0)
            {
                ModelState.AddModelError("MedicineImageFile", "The image file is required.");
                return View(medicineDTO);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(medicineDTO.MedicineImageFile.FileName)}";
            var filePath = await _fileService.SaveFileAsync("medicine", fileName, medicineDTO.MedicineImageFile);

            var medicine = new Medicine
            {
                MedicineID = Guid.NewGuid().ToString(),
                Name = medicineDTO.Name,
                Composition = medicineDTO.Composition,
                Manufacturer = medicineDTO.Manufacturer,
                Batch = medicineDTO.Batch,
                ManufacturingDate = medicineDTO.ManufacturingDate,
                ExpiryDate = medicineDTO.ExpiryDate,
                Price = medicineDTO.Price,
                Stock = medicineDTO.Stock,
                IndustryID = medicineDTO.IndustryID,
                MedicineImage = new Image { FilePath = filePath, FileName = Path.GetFileName(filePath) }
            };

            _applicationDbContext.Medicines.Add(medicine);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Medicine", new { id = medicine.IndustryID });
        }

        [HttpGet("medicines/{id}")]
        public async Task<IActionResult> Medicine(string id)
        {
            var medicines = await _applicationDbContext.Medicines
                .Include(m => m.MedicineImage)
                .Where(m => m.IndustryID == id)
                .ToListAsync();
            return View(medicines);
        }

        [HttpGet]
        public async Task<IActionResult> ManageStockiestRequests()
        {
            var requests = await _applicationDbContext.StockiestRequests
                .Include(sr => sr.Stockiest)
                .Include(sr => sr.Industry)
                .Select(sr => new StockiestRequestDTO
                {
                    RequestID = sr.RequestID,
                    StockiestID = sr.StockiestID,
                    IndustryID = sr.IndustryID,
                    RequestDate = sr.RequestDate,
                    Status = sr.Status,
                    StockiestName = sr.Stockiest.UserName,
                    IndustryName = sr.Industry.Name
                })
                .ToListAsync();
            return View(requests);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveStockiestRequest(int requestId)
        {
            var request = await _applicationDbContext.StockiestRequests.FindAsync(requestId);
            if (request != null)
            {
                request.Status = "Approved";
                await _applicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("ManageRequests");
        }

        [HttpPost]
        public async Task<IActionResult> RejectStockiestRequest(int requestId)
        {
            var request = await _applicationDbContext.StockiestRequests.FindAsync(requestId);
            if (request != null)
            {
                request.Status = "Rejected";
                await _applicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("ManageRequests");
        }
    }
}