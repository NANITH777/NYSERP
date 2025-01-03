using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class LanguageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LanguageController> _logger;
        public LanguageController(IUnitOfWork unitOfWork, ILogger<LanguageController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<Language> objLanguageList = _unitOfWork.Language
           .GetAll(includeProperties: "Company").ToList();
            return View(objLanguageList);
        }
        public IActionResult Create()
        {
            var languageVM = new LanguageVM
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }).ToList(),
                Language = new Language()
            };

            languageVM.Language.LANCODE = GenerateUniqueLanguageCode();
            return View(languageVM);
        }

        [HttpPost]
        public IActionResult Create(LanguageVM languageVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingLanguage = _unitOfWork.Language.Get(u => u.LANCODE == languageVM.Language.LANCODE);
                    if (existingLanguage != null)
                    {
                        ModelState.AddModelError("Language.LANCODE", "A language with this code already exists.");
                        languageVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COMTEXT,
                            Value = a.COMCODE
                        }).ToList();

                        return View(languageVM);
                    }

                    if (string.IsNullOrEmpty(languageVM.Language.LANCODE))
                    {
                        languageVM.Language.LANCODE = GenerateUniqueLanguageCode();
                    }

                    _unitOfWork.Language.Add(languageVM.Language);
                    _unitOfWork.Save();

                    TempData["success"] = "Language created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred while creating the language: {ex.Message}");
                }
            }

            languageVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            }).ToList();

            return View(languageVM);
        }

        public IActionResult Edit(string? lancode)
        {
            if (string.IsNullOrEmpty(lancode))
            {
                return NotFound();
            }

            var language = _unitOfWork.Language.Get(u => u.LANCODE == lancode);
            if (language == null)
            {
                return NotFound();
            }

            var languageVM = new LanguageVM
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }).ToList(),
                Language = language
            };

            return View(languageVM);
        }

        [HttpPost]
        public IActionResult Edit(LanguageVM languageVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingLanguage = _unitOfWork.Language.Get(u => u.LANCODE == languageVM.Language.LANCODE);
                    if (existingLanguage == null)
                    {
                        return NotFound();
                    }

                    existingLanguage.LANTEXT = languageVM.Language.LANTEXT;
                    _unitOfWork.Language.Update(existingLanguage);
                    _unitOfWork.Save();

                    TempData["success"] = "Language updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating the language");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the language.");
                }
            }

            languageVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            }).ToList();

            return View(languageVM);
        }


        private string GenerateUniqueLanguageCode()
        {
            var lastLanguage = _unitOfWork.Language.GetAll()
                .OrderByDescending(l => l.LANCODE)
                .FirstOrDefault();

            if (lastLanguage == null || string.IsNullOrWhiteSpace(lastLanguage.LANCODE))
            {
                return "L01";
            }

            if (lastLanguage.LANCODE.Length < 3)
            {
                throw new FormatException("LANCODE does not have the expected format.");
            }

            string numericPart = lastLanguage.LANCODE.Substring(2); 

            if (!int.TryParse(numericPart, out int currentNumber))
            {
                throw new FormatException($"LANCODE '{lastLanguage.LANCODE}' contains an invalid numeric part.");
            }

            int nextNumber = currentNumber + 1;
            return $"L{nextNumber:D2}";
        }

        public IActionResult Delete(string lanCode)
        {
            var languageVM = new LanguageVM
            {
                Language = _unitOfWork.Language.Get(u => u.LANCODE == lanCode, includeProperties: "Company"),
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            if (languageVM.Language == null)
            {
                return NotFound();
            }

            return View(languageVM);
        }

        [HttpPost]
        public IActionResult Delete(LanguageVM languageVM)
        {
            try
            {
                var languageToDelete = _unitOfWork.Language.Get(u => u.LANCODE == languageVM.Language.LANCODE);

                if (languageToDelete == null)
                {
                    return NotFound();
                }

                _unitOfWork.Language.Remove(languageToDelete);
                _unitOfWork.Save();

                TempData["success"] = "Language deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData["error"] = "Cannot delete this language because it is being used in other records. Please delete the related records first.";
                return RedirectToAction("Index");
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Language> objLanguageList = _unitOfWork.Language
            .GetAll(includeProperties: "Company")
            .ToList();

            var formattedData = objLanguageList.Select(l => new
            {
                lanCode = l.LANCODE,  
                lanText = l.LANTEXT,  
                companyText = l.Company.COMCODE  
            }).ToList();

            return Json(new { data = formattedData });
        }

        //[HttpDelete]
        //public IActionResult Delete(string lanCode)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(lanCode))
        //        {
        //            _logger.LogWarning($"Delete attempted with empty language code");
        //            return BadRequest(new { success = false, message = "Language Code is required" });
        //        }

        //        var languageToBeDeleted = _unitOfWork.Language.Get(u => u.LANCODE == lanCode);
        //        if (languageToBeDeleted == null)
        //        {
        //            _logger.LogWarning($"Attempted to delete non-existent language: {lanCode}");
        //            return NotFound(new { success = false, message = "Record not found" });
        //        }

        //        _unitOfWork.Language.Remove(languageToBeDeleted);
        //        _unitOfWork.Save();

        //        _logger.LogInformation($"Language deleted successfully: {lanCode}");
        //        return Ok(new { success = true, message = "Language deleted successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error deleting language: {lanCode}");
        //        return StatusCode(500, new
        //        {
        //            success = false,
        //            message = "Failed to delete language",
        //            details = ex.Message
        //        });
        //    }
        //}


        #endregion
    }
}
