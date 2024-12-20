using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Migrations;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class CostCenterController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CostCenterController> _logger;
        public CostCenterController(IUnitOfWork unitOfWork, ILogger<CostCenterController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<CostCenter> objUnitList = _unitOfWork.CostCenter
           .GetAll(includeProperties: "Company").ToList();
            return View(objUnitList);
        }
        public IActionResult Create()
        {
            CostCenterVM costCenterVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }),
                CostCenter = new CostCenter()
            };
            costCenterVM.CostCenter.CCMDOCTYPE = GenerateUniqueUnitCode();
            return View(costCenterVM);
        }

        [HttpPost]
        public IActionResult Create(CostCenterVM costCenterVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingCostCenter = _unitOfWork.CostCenter.Get(u => u.CCMDOCTYPE == costCenterVM.CostCenter.CCMDOCTYPE);
                    if (existingCostCenter != null)
                    {
                        ModelState.AddModelError("CostCenter.CCMDOCTYPE", "A cost center with this code already exists.");

                        costCenterVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COMTEXT,
                            Value = a.COMCODE
                        });
                        return View(costCenterVM);
                    }

                    if (string.IsNullOrEmpty(costCenterVM.CostCenter.CCMDOCTYPE))
                    {
                        costCenterVM.CostCenter.CCMDOCTYPE = GenerateUniqueUnitCode();
                    }

                    _unitOfWork.CostCenter.Add(costCenterVM.CostCenter);
                    _unitOfWork.Save();

                    TempData["success"] = "Cost Center created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred while creating the cost center: {ex.Message}");
                }
            }

            costCenterVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });

            return View(costCenterVM);
        }

        public IActionResult Edit(string? ccCode)
        {
            if (string.IsNullOrEmpty(ccCode))
            {
                return NotFound();
            }

            CostCenterVM costCenterVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            costCenterVM.CostCenter = _unitOfWork.CostCenter.Get(u => u.CCMDOCTYPE == ccCode);
            if (costCenterVM.CostCenter == null)
            {
                return NotFound();
            }
            return View(costCenterVM);
        }

        [HttpPost]
        public IActionResult Edit(CostCenterVM costCenterVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var costCenterFromDb = _unitOfWork.CostCenter.Get(u => u.CCMDOCTYPE == costCenterVM.CostCenter.CCMDOCTYPE);
                    if (costCenterFromDb == null)
                    {
                        return NotFound();
                    }

                    costCenterFromDb.CCMDOCNUM = costCenterVM.CostCenter.CCMDOCNUM;
                    costCenterFromDb.ISPASSIVE = costCenterVM.CostCenter.ISPASSIVE;

                    _unitOfWork.CostCenter.Update(costCenterFromDb);
                    _unitOfWork.Save();

                    TempData["success"] = "Cost Center updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating the cost center");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the cost center.");
                }
            }

            costCenterVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });
            return View(costCenterVM);
        }


        private string GenerateUniqueUnitCode()
        {
            var lastCC = _unitOfWork.CostCenter.GetAll()
                .OrderByDescending(l => l.CCMDOCTYPE)
                .FirstOrDefault();

            if (lastCC == null)
            {
                return "CC01";
            }

            string numericPart = lastCC.CCMDOCTYPE.Substring(3);
            int nextNumber = int.Parse(numericPart) + 1;

            return $"CC{nextNumber:D2}";
        }

        public IActionResult Delete(string ccCode)
        {
            var costCenterVM = new CostCenterVM
            {
                CostCenter = _unitOfWork.CostCenter.Get(u => u.CCMDOCTYPE == ccCode, includeProperties: "Company"),
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            if (costCenterVM.CostCenter == null)
            {
                return NotFound();
            }

            return View(costCenterVM);
        }

        [HttpPost]
        public IActionResult Delete(CostCenterVM costCenterVM)
        {
            var ccToDelete = _unitOfWork.CostCenter.Get(u => u.CCMDOCTYPE == costCenterVM.CostCenter.CCMDOCTYPE);

            if (ccToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.CostCenter.Remove(ccToDelete);
            _unitOfWork.Save();

            TempData["success"] = "Cost Center Type deleted successfully!";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<CostCenter> objMTList = _unitOfWork.CostCenter
            .GetAll(includeProperties: "Company")
            .ToList();

            var formattedData = objMTList.Select(u => new
            {
                ccCode = u.CCMDOCTYPE,  
                ccText = u.CCMDOCNUM,
                isPassive = u.ISPASSIVE,
                companyText = u.Company.COMCODE  
            }).ToList();

            return Json(new { data = formattedData });
        }

        #endregion
    }
}
