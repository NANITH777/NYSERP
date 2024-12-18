using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class RotaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RotaController> _logger;
        public RotaController(IUnitOfWork unitOfWork, ILogger<RotaController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<Rota> objUnitList = _unitOfWork.Rota
           .GetAll(includeProperties: "Company").ToList();
            return View(objUnitList);
        }
        public IActionResult Create()
        {
            RotaVM RotaVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }),
                Rota = new Rota()
            };
            RotaVM.Rota.DOCTYPE = GenerateUniqueUnitCode();
            return View(RotaVM);
        }

        [HttpPost]
        public IActionResult Create(RotaVM RotaVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingRota = _unitOfWork.Rota.Get(u => u.DOCTYPE == RotaVM.Rota.DOCTYPE);
                    if (existingRota != null)
                    {
                        ModelState.AddModelError("Rota.DOCTYPE", "A document type with this code already exists.");

                        RotaVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COMTEXT,
                            Value = a.COMCODE
                        });
                        return View(RotaVM);
                    }

                    if (string.IsNullOrEmpty(RotaVM.Rota.DOCTYPE))
                    {
                        RotaVM.Rota.DOCTYPE = GenerateUniqueUnitCode();
                    }

                    _unitOfWork.Rota.Add(RotaVM.Rota);
                    _unitOfWork.Save();

                    TempData["success"] = "Rota created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred while creating the Rota: {ex.Message}");
                }
            }

            RotaVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });

            return View(RotaVM);
        }


        public IActionResult Edit(string? rotaCode)
        {
            if (string.IsNullOrEmpty(rotaCode))
            {
                return NotFound();
            }

            RotaVM RotaVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            RotaVM.Rota = _unitOfWork.Rota.Get(u => u.DOCTYPE == rotaCode);
            if (RotaVM.Rota == null)
            {
                return NotFound();
            }
            return View(RotaVM);
        }

        [HttpPost]
        public IActionResult Edit(RotaVM RotaVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var RotaFromDb = _unitOfWork.Rota.Get(u => u.DOCTYPE == RotaVM.Rota.DOCTYPE);
                    if (RotaFromDb == null)
                    {
                        return NotFound();
                    }

                    RotaFromDb.DOCTYPETEXT = RotaVM.Rota.DOCTYPETEXT;
                    RotaFromDb.ISPASSIVE = RotaVM.Rota.ISPASSIVE;

                    _unitOfWork.Rota.Update(RotaFromDb);
                    _unitOfWork.Save();

                    TempData["success"] = "Rota updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating the Rota");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the Rota.");
                }
            }

            RotaVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });
            return View(RotaVM);
        }

        private string GenerateUniqueUnitCode()
        {
            var lastRT = _unitOfWork.Rota.GetAll()
                .OrderByDescending(l => l.DOCTYPE)
                .FirstOrDefault();

            if (lastRT == null)
            {
                return "RT01";
            }

            string numericPart = lastRT.DOCTYPE.Substring(3);
            int nextNumber = int.Parse(numericPart) + 1;

            return $"RT{nextNumber:D2}";
        }


        public IActionResult Delete(string rotaCode)
        {
            var RotaVM = new RotaVM
            {
                Rota = _unitOfWork.Rota.Get(u => u.DOCTYPE == rotaCode, includeProperties: "Company"),
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            if (RotaVM.Rota == null)
            {
                return NotFound();
            }

            return View(RotaVM);
        }

        [HttpPost]
        public IActionResult Delete(RotaVM RotaVM)
        {
            var RTToDelete = _unitOfWork.Rota.Get(u => u.DOCTYPE == RotaVM.Rota.DOCTYPE);

            if (RTToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.Rota.Remove(RTToDelete);
            _unitOfWork.Save();

            TempData["success"] = "Rota Type deleted successfully!";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Rota> objMTList = _unitOfWork.Rota
            .GetAll(includeProperties: "Company")
            .ToList();

            var formattedData = objMTList.Select(u => new
            {
                rotaCode = u.DOCTYPE,  
                rotaText = u.DOCTYPETEXT,
                isPassive = u.ISPASSIVE,
                companyText = u.Company.COMCODE  
            }).ToList();

            return Json(new { data = formattedData });
        }

        #endregion
    }
}
