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
using System;
using System.IO;
using System.Diagnostics;

namespace Medictionary.Controllers
{
    public class StockiestController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IStore<Industry> _industryStore;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<StockiestController> _logger;

        public StockiestController(IFileService fileService, IStore<Industry> industryStore, ApplicationDbContext applicationDbContext, IWebHostEnvironment environment, UserManager<User> userManager, ILogger<StockiestController> logger)
        {
            _fileService = fileService;
            _industryStore = industryStore;
            _applicationDbContext = applicationDbContext;
            _environment = environment;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Dashboard()
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
                var industries = _industryStore.FilterBy(x => true).ToList();

                if (industries == null || !industries.Any())
                {
                    return View("EmptyIndustryList");
                }

                return View(industries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving industry data.");
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult AddMedicine()
        {
            var industries = _industryStore.FilterBy(x => true).ToList();
            ViewBag.Industries = industries;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicine(MedicineDTO medicineDTO)
        {
            if (!ModelState.IsValid)
            {
                var industries = _industryStore.FilterBy(x => true).ToList();
                ViewBag.Industries = industries;
                return View(medicineDTO);
            }

            string filePath = null;
            if (medicineDTO.MedicineImageFile != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(medicineDTO.MedicineImageFile.FileName)}";
                filePath = await _fileService.SaveFileAsync("medicine", fileName, medicineDTO.MedicineImageFile);
            }

            var medicine = new Medicine
            {
                MedicineID = Guid.NewGuid().ToString(),
                Name = medicineDTO.Name,
                Composition = medicineDTO.Composition,
                Manufacturer = medicineDTO.Manufacturer,
                Batch = medicineDTO.Batch,
                ManufacturingDate = medicineDTO.ManufacturingDate,
                ExpiryDate = medicineDTO.ExpiryDate,
                IndustryID = medicineDTO.IndustryID,
                Price = medicineDTO.Price,
                Stock = medicineDTO.Stock,
                CreatedBy = User?.Identity?.Name,
                CreatedDate = DateTime.UtcNow,
                MedicineImage = filePath != null ? new Image { FilePath = filePath, FileName = Path.GetFileName(filePath) } : null
            };

            _applicationDbContext.Medicines.Add(medicine);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> AddToStock(string MedicineID, int Quantity)
        {
            if (string.IsNullOrEmpty(MedicineID) || Quantity <= 0)
            {
                return BadRequest("Invalid medicine ID or quantity.");
            }

            var medicine = await _applicationDbContext.Medicines.FindAsync(MedicineID);
            if (medicine == null)
            {
                return NotFound("Medicine not found.");
            }

            if (medicine.Stock < Quantity)
            {
                return BadRequest("Not enough stock available in the industry.");
            }

            // Get the logged-in user's ID
            var stockiestID = _userManager.GetUserId(User);

            // Check if the stockiest has an approved request for the industry
            var approvedRequest = await _applicationDbContext.StockiestRequests
                .FirstOrDefaultAsync(sr => sr.StockiestID == stockiestID && sr.IndustryID == medicine.IndustryID && sr.Status == "Approved");

            if (approvedRequest == null)
            {
                ViewBag.Message = "You are not approved to add stock for this industry.";
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            // Update the stock in the industry's inventory
            medicine.Stock -= Quantity;

            // Check if the medicine already exists in the stockiest's inventory
            var existingStock = await _applicationDbContext.StockiestMedicines
                .FirstOrDefaultAsync(sm => sm.StockiestID == stockiestID && sm.MedicineID == MedicineID);

            if (existingStock != null)
            {
                // Update the quantity if the medicine already exists
                existingStock.Quantity += Quantity;
                existingStock.UpdatedBy = User?.Identity?.Name;
                existingStock.UpdatedDate = DateTime.UtcNow;
            }
            else
            {
                // Add the medicine to the stockiest's inventory
                var stockiestMedicine = new StockiestMedicine
                {
                    StockiestID = stockiestID,
                    MedicineID = MedicineID,
                    Quantity = Quantity,
                    AddedDate = DateTime.UtcNow,
                    CreatedBy = User?.Identity?.Name,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = User?.Identity?.Name,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                };
                _applicationDbContext.StockiestMedicines.Add(stockiestMedicine);
            }

            // Create a new transaction record
            var transactionRecord = new StockiestTransactionRecord
            {
                StockiestID = stockiestID,
                MedicineID = MedicineID,
                Quantity = Quantity,
                MRP = medicine.Price,
                Date = DateTime.UtcNow,
                CreatedBy = User?.Identity?.Name,
                CreatedDate = DateTime.UtcNow
            };

            // Save the transaction record
            _applicationDbContext.StockiestTransactionRecords.Add(transactionRecord);

            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult GetMedicines(string industryId)
        {
            var medicines = _applicationDbContext.Medicines
                .Where(m => m.IndustryID == industryId)
                .Include(m => m.MedicineImage)
                .ToList();

            return PartialView("_MedicineDetails", medicines);
        }

        public async Task<IActionResult> Inventory()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var stockDetails = await _applicationDbContext.StockiestMedicines
                    .Include(sm => sm.Medicine)
                    .Where(sm => sm.StockiestID == userId)
                    .ToListAsync();

                return View(stockDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving stock details.");
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult RequestAssociation()
        {
            var industries = _applicationDbContext.Industries.ToList();
            ViewBag.Industries = industries;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RequestAssociation(string industryId)
        {
            var stockiestID = _userManager.GetUserId(User);

            var request = new StockiestRequestDTO
            {
                StockiestID = stockiestID,
                IndustryID = industryId,
                RequestDate = DateTime.UtcNow,
                Status = "Pending"
            };

            var stockiestRequest = new StockiestRequest
            {
                StockiestID = request.StockiestID,
                IndustryID = request.IndustryID,
                RequestDate = request.RequestDate,
                Status = request.Status
            };

            _applicationDbContext.StockiestRequests.Add(stockiestRequest);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> Transactions()
        {
            var transactions = await _applicationDbContext.StockiestTransactionRecords
                .Include(tr => tr.Medicine)
                .ThenInclude(m => m.Industry)
                .ToListAsync();
            return View(transactions);
        }
    }
}