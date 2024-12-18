using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class MaterialTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MaterialTypeController> _logger;
        public MaterialTypeController(IUnitOfWork unitOfWork, ILogger<MaterialTypeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<MaterialType> objUnitList = _unitOfWork.MaterialType
           .GetAll(includeProperties: "Company").ToList();
            return View(objUnitList);
        }
        public IActionResult Create()
        {
            MaterialTVM materialTVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }),
                MaterialType = new MaterialType()
            };
            materialTVM.MaterialType.DOCTYPE = GenerateUniqueUnitCode();
            return View(materialTVM);
        }

        [HttpPost]
        public IActionResult Create(MaterialTVM MaterialTVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingMaterial = _unitOfWork.MaterialType.Get(u => u.DOCTYPE == MaterialTVM.MaterialType.DOCTYPE);
                    if (existingMaterial != null)
                    {
                        ModelState.AddModelError("MaterialType.DOCTYPE", "A document type with this code already exists.");

                        MaterialTVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COMTEXT,
                            Value = a.COMCODE
                        });
                        return View(MaterialTVM);
                    }

                    if (string.IsNullOrEmpty(MaterialTVM.MaterialType.DOCTYPE))
                    {
                        MaterialTVM.MaterialType.DOCTYPE = GenerateUniqueUnitCode();
                    }

                    _unitOfWork.MaterialType.Add(MaterialTVM.MaterialType);
                    _unitOfWork.Save();

                    TempData["success"] = "Material created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred while creating the material: {ex.Message}");
                }
            }

            MaterialTVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });

            return View(MaterialTVM);
        }

        public IActionResult Edit(string? mtCode)
        {
            if (string.IsNullOrEmpty(mtCode))
            {
                return NotFound();
            }

            MaterialTVM MaterialTVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            MaterialTVM.MaterialType = _unitOfWork.MaterialType.Get(u => u.DOCTYPE == mtCode);
            if (MaterialTVM.MaterialType == null)
            {
                return NotFound();
            }
            return View(MaterialTVM);
        }

        [HttpPost]
        public IActionResult Edit(MaterialTVM MaterialTVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MaterialFromDb = _unitOfWork.MaterialType.Get(u => u.DOCTYPE == MaterialTVM.MaterialType.DOCTYPE);
                    if (MaterialFromDb == null)
                    {
                        return NotFound();
                    }

                    MaterialFromDb.DOCTYPETEXT = MaterialTVM.MaterialType.DOCTYPETEXT;
                    MaterialFromDb.ISPASSIVE = MaterialTVM.MaterialType.ISPASSIVE;

                    _unitOfWork.MaterialType.Update(MaterialFromDb);
                    _unitOfWork.Save();

                    TempData["success"] = "Material updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating the material");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the material.");
                }
            }

            MaterialTVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });
            return View(MaterialTVM);
        }


        private string GenerateUniqueUnitCode()
        {
            var lastMT = _unitOfWork.MaterialType.GetAll()
                .OrderByDescending(l => l.DOCTYPE)
                .FirstOrDefault();

            if (lastMT == null)
            {
                return "MT01";
            }

            string numericPart = lastMT.DOCTYPE.Substring(3);
            int nextNumber = int.Parse(numericPart) + 1;

            return $"MT{nextNumber:D2}";
        }


        public IActionResult Delete(string mtCode)
        {
            var materialTVM = new MaterialTVM
            {
                MaterialType = _unitOfWork.MaterialType.Get(u => u.DOCTYPE == mtCode, includeProperties: "Company"),
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            if (materialTVM.MaterialType == null)
            {
                return NotFound();
            }

            return View(materialTVM);
        }

        [HttpPost]
        public IActionResult Delete(MaterialTVM materialTVM)
        {
            var mtToDelete = _unitOfWork.MaterialType.Get(u => u.DOCTYPE == materialTVM.MaterialType.DOCTYPE);

            if (mtToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.MaterialType.Remove(mtToDelete);
            _unitOfWork.Save();

            TempData["success"] = "Material Type deleted successfully!";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<MaterialType> objMTList = _unitOfWork.MaterialType
            .GetAll(includeProperties: "Company")
            .ToList();

            var formattedData = objMTList.Select(u => new
            {
                mtCode = u.DOCTYPE,  
                mtText = u.DOCTYPETEXT,
                isPassive = u.ISPASSIVE,
                companyText = u.Company.COMCODE  
            }).ToList();

            return Json(new { data = formattedData });
        }

        #endregion
    }
}
