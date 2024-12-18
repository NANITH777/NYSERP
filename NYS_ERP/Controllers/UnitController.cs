using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UnitController> _logger;
        public UnitController(IUnitOfWork unitOfWork, ILogger<UnitController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<Unit> objUnitList = _unitOfWork.Unit
           .GetAll(includeProperties: "Company").ToList();
            return View(objUnitList);
        }
        public IActionResult Create()
        {
            UnitVM unitVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }).ToList(),

                Unit = new Unit()
            };

            unitVM.Unit.UNITCODE = GenerateUniqueUnitCode();
            return View(unitVM);
        }

        [HttpPost]
        public IActionResult Create(UnitVM unitVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUnit = _unitOfWork.Unit.Get(u => u.UNITCODE == unitVM.Unit.UNITCODE);
                    if (existingUnit != null)
                    {
                        ModelState.AddModelError("Unit.UNITCODE", "A unit with this code already exists.");

                        unitVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COMTEXT,
                            Value = a.COMCODE
                        }).ToList();

                        return View(unitVM);
                    }

                    if (string.IsNullOrEmpty(unitVM.Unit.UNITCODE))
                    {
                        unitVM.Unit.UNITCODE = GenerateUniqueUnitCode();
                    }

                    _unitOfWork.Unit.Add(unitVM.Unit);
                    _unitOfWork.Save();

                    TempData["success"] = "Unit created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred while creating the unit: {ex.Message}");
                }
            }

            unitVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            }).ToList();

            return View(unitVM);
        }

        public IActionResult Edit(string? unitcode)
        {
            if (string.IsNullOrEmpty(unitcode))
            {
                return NotFound();
            }

            UnitVM unitVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }).ToList()
            };

            unitVM.Unit = _unitOfWork.Unit.Get(u => u.UNITCODE == unitcode);
            if (unitVM.Unit == null)
            {
                return NotFound();
            }
            return View(unitVM);
        }

        [HttpPost]
        public IActionResult Edit(UnitVM unitVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var unitFromDb = _unitOfWork.Unit.Get(u => u.UNITCODE == unitVM.Unit.UNITCODE);
                    if (unitFromDb == null)
                    {
                        return NotFound();
                    }

                    unitFromDb.UNITTEXT = unitVM.Unit.UNITTEXT;
                    unitFromDb.ISMAINUNIT = unitVM.Unit.ISMAINUNIT;
                    unitFromDb.MAINUNITCODE = unitVM.Unit.MAINUNITCODE;

                    _unitOfWork.Unit.Update(unitFromDb);
                    _unitOfWork.Save();

                    TempData["success"] = "Unit updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating the unit");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the unit.");
                }
            }

            unitVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            }).ToList();

            return View(unitVM);
        }


        private string GenerateUniqueUnitCode()
        {
            var lastUnit = _unitOfWork.Unit.GetAll()
                .OrderByDescending(l => l.UNITCODE)
                .FirstOrDefault();

            if (lastUnit == null || string.IsNullOrWhiteSpace(lastUnit.UNITCODE))
            {
                return "U01";
            }

            if (lastUnit.UNITCODE.Length < 3)
            {
                throw new FormatException("UNITCODE does not have the expected format.");
            }

            string numericPart = lastUnit.UNITCODE.Substring(2); // Prendre après "UT"

            if (!int.TryParse(numericPart, out int currentNumber))
            {
                throw new FormatException($"UNITCODE '{lastUnit.UNITCODE}' contains an invalid numeric part.");
            }

            int nextNumber = currentNumber + 1;
            return $"U{nextNumber:D2}";
        }



        public IActionResult Delete(string unitCode)
        {
            var unitVM = new UnitVM
            {
                Unit = _unitOfWork.Unit.Get(u => u.UNITCODE == unitCode, includeProperties: "Company"),
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            if (unitVM.Unit == null)
            {
                return NotFound();
            }

            return View(unitVM);
        }

        [HttpPost]
        public IActionResult Delete(UnitVM unitVM)
        {
            var unitToDelete = _unitOfWork.Unit.Get(u => u.UNITCODE == unitVM.Unit.UNITCODE);

            if (unitToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.Unit.Remove(unitToDelete);
            _unitOfWork.Save();

            TempData["success"] = "Unit deleted successfully!";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Unit> objUnitList = _unitOfWork.Unit
            .GetAll(includeProperties: "Company")
            .ToList();

            var formattedData = objUnitList.Select(u => new
            {
                unitCode = u.UNITCODE,  
                unitText = u.UNITTEXT,
                mainUnitCode = u.MAINUNITCODE, 
                isMainUnit = u.ISMAINUNIT,
                companyText = u.Company.COMCODE  
            }).ToList();

            return Json(new { data = formattedData });
        }

        #endregion
    }
}
