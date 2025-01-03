using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class CountryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<Country> objCountryList = _unitOfWork.Country
           .GetAll(includeProperties: "Company").ToList();
            return View(objCountryList);
        }
        public IActionResult Create()
        {
            CountryVM countryVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }),
                Country = new Country() 
            };

            return View(countryVM);
        }

        [HttpPost]
        public IActionResult Create(CountryVM countryVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingCountry = _unitOfWork.Country.Get(u => u.COUNTRYCODE == countryVM.Country.COUNTRYCODE);
                    if (existingCountry != null)
                    {
                        ModelState.AddModelError("Country.COUNTRYCODE", "A country with this code already exists.");
                        countryVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COMTEXT,
                            Value = a.COMCODE
                        }).ToList();

                        return View(countryVM);
                    }
                    _unitOfWork.Country.Add(countryVM.Country);
                    _unitOfWork.Save();

                    TempData["success"] = "Country created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred while creating the country: {ex.Message}");
                }
            }

            countryVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            }).ToList();

            return View(countryVM);
        }

        public IActionResult Delete(string countryCode)
        {
            var countryVM = new CountryVM
            {
                Country = _unitOfWork.Country.Get(u => u.COUNTRYCODE == countryCode, includeProperties: "Company"),
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            if (countryVM.Country == null)
            {
                return NotFound();
            }

            return View(countryVM);
        }

        public IActionResult Edit(string countrycode)
        {
            if (string.IsNullOrEmpty(countrycode))
            {
                return NotFound();
            }

            CountryVM countryVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }),
                Country = _unitOfWork.Country.Get(u => u.COUNTRYCODE == countrycode)
            };

            if (countryVM.Country == null)
            {
                return NotFound();
            }

            return View(countryVM);
        }

        [HttpPost]
        public IActionResult Edit(CountryVM countryVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var countryFromDb = _unitOfWork.Country.Get(u => u.COUNTRYCODE == countryVM.Country.COUNTRYCODE);

                    if (countryFromDb == null)
                    {
                        return NotFound(); 
                    }

                    countryFromDb.COUNTRYTEXT = countryVM.Country.COUNTRYTEXT;
                    countryFromDb.COMCODE = countryVM.Country.COMCODE; 

                    _unitOfWork.Country.Update(countryFromDb);
                    _unitOfWork.Save();

                    TempData["success"] = "Country updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating the country");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the country.");
                }
            }

            countryVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });
            return View(countryVM);
        }


        [HttpPost]
        public IActionResult Delete(CountryVM countryVM)
        {
            try
            {
                var countryToDelete = _unitOfWork.Country.Get(u => u.COUNTRYCODE == countryVM.Country.COUNTRYCODE); 
                if (countryToDelete == null)
                {
                    return NotFound();
                }

                _unitOfWork.Country.Remove(countryToDelete);
                _unitOfWork.Save();
                TempData["success"] = "Country deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData["error"] = "Cannot delete this Country because it is being used in other records. Please delete the related records first.";
                return RedirectToAction("Index");
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Country> objCountryList = _unitOfWork.Country
            .GetAll(includeProperties: "Company")
            .ToList();

            var formattedData = objCountryList.Select(l => new
            {
                countryCode = l.COUNTRYCODE,  
                countryText = l.COUNTRYTEXT,  
                companyText = l.Company.COMCODE  
            }).ToList();

            return Json(new { data = formattedData });
        }


        #endregion
    }
}
