using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class BOMController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BOMController> _logger;
        public BOMController(IUnitOfWork unitOfWork, ILogger<BOMController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<BOM> objUnitList = _unitOfWork.BOM
           .GetAll(includeProperties: "Company").ToList();
            return View(objUnitList);
        }

        public IActionResult Create()
        {
            BOMVM BOMVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }),
                BOM = new BOM()
            };
            BOMVM.BOM.BOMDOCTYPE = GenerateUniqueUnitCode();
            return View(BOMVM);
        }

        [HttpPost]
        public IActionResult Create(BOMVM BOMVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingBOM = _unitOfWork.BOM.Get(u => u.BOMDOCTYPE == BOMVM.BOM.BOMDOCTYPE);
                    if (existingBOM != null)
                    {
                        ModelState.AddModelError("BOM.BOMDOCTYPE", "A document type with this code already exists.");

                        BOMVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COMTEXT,
                            Value = a.COMCODE
                        });
                        return View(BOMVM);
                    }

                    if (string.IsNullOrEmpty(BOMVM.BOM.BOMDOCTYPE))
                    {
                        BOMVM.BOM.BOMDOCTYPE = GenerateUniqueUnitCode();
                    }

                    _unitOfWork.BOM.Add(BOMVM.BOM);
                    _unitOfWork.Save();

                    TempData["success"] = "BOM created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating the BOM");
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the BOM.");
                }
            }

            BOMVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });
            return View(BOMVM);
        }

        public IActionResult Edit(string? BOMCode)
        {
            if (string.IsNullOrEmpty(BOMCode))
            {
                return NotFound();
            }

            BOMVM BOMVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            BOMVM.BOM = _unitOfWork.BOM.Get(u => u.BOMDOCTYPE == BOMCode);
            if (BOMVM.BOM == null)
            {
                return NotFound();
            }
            return View(BOMVM);
        }

        [HttpPost]
        public IActionResult Edit(BOMVM BOMVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var BOMFromDb = _unitOfWork.BOM.Get(u => u.BOMDOCTYPE == BOMVM.BOM.BOMDOCTYPE);
                    if (BOMFromDb == null)
                    {
                        return NotFound();
                    }

                    BOMFromDb.BOMDOCNUM = BOMVM.BOM.BOMDOCNUM;
                    BOMFromDb.ISPASSIVE = BOMVM.BOM.ISPASSIVE;

                    _unitOfWork.BOM.Update(BOMFromDb);
                    _unitOfWork.Save();

                    TempData["success"] = "BOM updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating the BOM");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the BOM.");
                }
            }

            BOMVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });
            return View(BOMVM);
        }



        private string GenerateUniqueUnitCode()
        {
            var lastBOM = _unitOfWork.BOM.GetAll()
                .OrderByDescending(l => l.BOMDOCTYPE)
                .FirstOrDefault();

            if (lastBOM == null)
            {
                return "UR01"; 
            }

            string numericPart = lastBOM.BOMDOCTYPE.Substring(2); 
            int nextNumber = int.Parse(numericPart) + 1;

            return $"UR{nextNumber:D2}"; 
        }

        public IActionResult Delete(string BOMCode)
        {
            var BOMVM = new BOMVM
            {
                BOM = _unitOfWork.BOM.Get(u => u.BOMDOCTYPE == BOMCode, includeProperties: "Company"),
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            if (BOMVM.BOM == null)
            {
                return NotFound();
            }

            return View(BOMVM);
        }

        [HttpPost]
        public IActionResult Delete(BOMVM BOMVM)
        {
            var ccToDelete = _unitOfWork.BOM.Get(u => u.BOMDOCTYPE == BOMVM.BOM.BOMDOCTYPE);

            if (ccToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.BOM.Remove(ccToDelete);
            _unitOfWork.Save();

            TempData["success"] = "BOM Type deleted successfully!";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<BOM> objMTList = _unitOfWork.BOM
            .GetAll(includeProperties: "Company")
            .ToList();

            var formattedData = objMTList.Select(u => new
            {
                BOMCode = u.BOMDOCTYPE,  
                BOMText = u.BOMDOCNUM,
                isPassive = u.ISPASSIVE,
                companyText = u.Company.COMCODE  
            }).ToList();

            return Json(new { data = formattedData });
        }

        #endregion
    }
}
