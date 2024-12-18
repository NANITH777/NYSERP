using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class WorkCenterController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<WorkCenterController> _logger;
        public WorkCenterController(IUnitOfWork unitOfWork, ILogger<WorkCenterController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<WorkCenter> objUnitList = _unitOfWork.WorkCenter
           .GetAll(includeProperties: "Company").ToList();
            return View(objUnitList);
        }
        public IActionResult Create()
        {
            WorkCenterVM workCenterVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }),
                WorkCenter = new WorkCenter()
            };
            workCenterVM.WorkCenter.DOCTYPE = GenerateUniqueUnitCode();
            return View(workCenterVM);
        }

        [HttpPost]
        public IActionResult Create(WorkCenterVM workCenterVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingWorkCenter = _unitOfWork.WorkCenter.Get(u => u.DOCTYPE == workCenterVM.WorkCenter.DOCTYPE);
                    if (existingWorkCenter != null)
                    {
                        ModelState.AddModelError("WorkCenter.DOCTYPE", "A work center with this code already exists.");

                        workCenterVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COMTEXT,
                            Value = a.COMCODE
                        });
                        return View(workCenterVM);
                    }

                    if (string.IsNullOrEmpty(workCenterVM.WorkCenter.DOCTYPE))
                    {
                        workCenterVM.WorkCenter.DOCTYPE = GenerateUniqueUnitCode();
                    }

                    _unitOfWork.WorkCenter.Add(workCenterVM.WorkCenter);
                    _unitOfWork.Save();

                    TempData["success"] = "Work Center created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating the work center");
                    ModelState.AddModelError(string.Empty, $"An error occurred while creating the work center: {ex.Message}");
                }
            }

            workCenterVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });

            return View(workCenterVM);
        }

        public IActionResult Edit(string? wcCode)
        {
            if (string.IsNullOrEmpty(wcCode))
            {
                return NotFound();
            }

            WorkCenterVM workCenterVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            workCenterVM.WorkCenter = _unitOfWork.WorkCenter.Get(u => u.DOCTYPE == wcCode);
            if (workCenterVM.WorkCenter == null)
            {
                return NotFound();
            }
            return View(workCenterVM);
        }

        [HttpPost]
        public IActionResult Edit(WorkCenterVM workCenterVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var workCenterFromDb = _unitOfWork.WorkCenter.Get(u => u.DOCTYPE == workCenterVM.WorkCenter.DOCTYPE);
                    if (workCenterFromDb == null)
                    {
                        return NotFound();
                    }

                    workCenterFromDb.DOCTYPETEXT = workCenterVM.WorkCenter.DOCTYPETEXT;
                    workCenterFromDb.ISPASSIVE = workCenterVM.WorkCenter.ISPASSIVE;

                    _unitOfWork.WorkCenter.Update(workCenterFromDb);
                    _unitOfWork.Save();

                    TempData["success"] = "Work Center updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating the work center");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the work center.");
                }
            }

            workCenterVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });
            return View(workCenterVM);
        }
        private string GenerateUniqueUnitCode()
        {
            var lastWC = _unitOfWork.WorkCenter.GetAll()
                .OrderByDescending(l => l.DOCTYPE)
                .FirstOrDefault();

            if (lastWC == null)
            {
                return "WC01";
            }

            string numericPart = lastWC.DOCTYPE.Substring(2);
            int nextNumber = int.Parse(numericPart) + 1;

            return $"WC{nextNumber:D2}";
        }

        public IActionResult Delete(string wcCode)
        {
            var workCenterVM = new WorkCenterVM
            {
                WorkCenter = _unitOfWork.WorkCenter.Get(u => u.DOCTYPE == wcCode, includeProperties: "Company"),
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            if (workCenterVM.WorkCenter == null)
            {
                return NotFound();
            }

            return View(workCenterVM);
        }

        [HttpPost]
        public IActionResult Delete(WorkCenterVM workCenterVM)
        {
            var ccToDelete = _unitOfWork.WorkCenter.Get(u => u.DOCTYPE == workCenterVM.WorkCenter.DOCTYPE);

            if (ccToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.WorkCenter.Remove(ccToDelete);
            _unitOfWork.Save();

            TempData["success"] = "Work Center Type deleted successfully!";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<WorkCenter> objMTList = _unitOfWork.WorkCenter
            .GetAll(includeProperties: "Company")
            .ToList();

            var formattedData = objMTList.Select(u => new
            {
                wcCode = u.DOCTYPE,  
                wcText = u.DOCTYPETEXT,
                isPassive = u.ISPASSIVE,
                companyText = u.Company.COMCODE  
            }).ToList();

            return Json(new { data = formattedData });
        }

        #endregion
    }
}
