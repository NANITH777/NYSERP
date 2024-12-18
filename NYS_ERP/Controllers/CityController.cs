using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class CityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CityController> _logger;

        public CityController(IUnitOfWork unitOfWork, ILogger<CityController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<City> objCityList = _unitOfWork.City
                .GetAll(includeProperties: "Company,Country")
                .ToList();
            return View(objCityList);
        }

        public IActionResult Create()
        {
            CityVM cityVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }),
                CountryList = _unitOfWork.Country.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COUNTRYTEXT,
                    Value = a.COUNTRYCODE
                }),
                City = new City()
            };
            return View(cityVM);
        }

        [HttpPost]
        public IActionResult Create(CityVM cityVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingCity = _unitOfWork.City.Get(u =>
                        u.CITYCODE == cityVM.City.CITYCODE );

                    if (existingCity != null)
                    {
                        ModelState.AddModelError("City.CITYCODE", "A city with this code already exists in the selected country.");

                        cityVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COMTEXT,
                            Value = a.COMCODE
                        });
                        cityVM.CountryList = _unitOfWork.Country.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COUNTRYTEXT,
                            Value = a.COUNTRYCODE
                        });

                        return View(cityVM);
                    }

                    _unitOfWork.City.Add(cityVM.City);
                    _unitOfWork.Save();

                    TempData["success"] = "City created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating the city");
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the city.");
                }
            }

            cityVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });
            cityVM.CountryList = _unitOfWork.Country.GetAll().Select(a => new SelectListItem
            {
                Text = a.COUNTRYTEXT,
                Value = a.COUNTRYCODE
            });

            return View(cityVM);
        }

        public IActionResult Edit(string? cityCode)
        {
            if (string.IsNullOrEmpty(cityCode))
            {
                return NotFound();
            }

            CityVM cityVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }),
                CountryList = _unitOfWork.Country.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COUNTRYTEXT,
                    Value = a.COUNTRYCODE
                })
            };

            cityVM.City = _unitOfWork.City.Get(u => u.CITYCODE == cityCode);

            if (cityVM.City == null)
            {
                return NotFound();
            }

            return View(cityVM);
        }

        [HttpPost]
        public IActionResult Edit(CityVM cityVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cityFromDb = _unitOfWork.City.Get(u => u.CITYCODE == cityVM.City.CITYCODE);

                    if (cityFromDb == null)
                    {
                        return NotFound();
                    }

                    var existingCity = _unitOfWork.City.Get(u =>
                        u.CITYTEXT == cityVM.City.CITYTEXT &&
                        u.COUNTRYCODE == cityVM.City.COUNTRYCODE &&
                        u.CITYCODE != cityVM.City.CITYCODE);

                    if (existingCity != null)
                    {
                        ModelState.AddModelError("City.CITYTEXT", "A city with this name already exists in the selected country.");

                        cityVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COMTEXT,
                            Value = a.COMCODE
                        });
                        cityVM.CountryList = _unitOfWork.Country.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COUNTRYTEXT,
                            Value = a.COUNTRYCODE
                        });

                        return View(cityVM);
                    }

                    cityFromDb.CITYTEXT = cityVM.City.CITYTEXT;
                    cityFromDb.COUNTRYCODE = cityVM.City.COUNTRYCODE;

                    _unitOfWork.City.Update(cityFromDb);
                    _unitOfWork.Save();

                    TempData["success"] = "City updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating the city");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the city.");
                }
            }

            cityVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });
            cityVM.CountryList = _unitOfWork.Country.GetAll().Select(a => new SelectListItem
            {
                Text = a.COUNTRYTEXT,
                Value = a.COUNTRYCODE
            });

            return View(cityVM);
        }

        private string GenerateUniqueCityCode()
        {
            var lastCity = _unitOfWork.City.GetAll()
                .OrderByDescending(c => c.CITYCODE)
                .FirstOrDefault();

            if (lastCity == null)
            {
                return "CITY001";
            }

            string numericPart = lastCity.CITYCODE.Substring(4);
            int nextNumber = int.Parse(numericPart) + 1;
            return $"CITY{nextNumber:D3}";
        }


        public IActionResult Delete(string cityCode)
        {
            var cityVM = new CityVM
            {
                City = _unitOfWork.City.Get(u => u.CITYCODE == cityCode, includeProperties: "Company,Country"),
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }),
                CountryList = _unitOfWork.Country.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COUNTRYTEXT,
                    Value = a.COUNTRYCODE
                })
            };

            if (cityVM.City == null)
            {
                return NotFound();
            }

            return View(cityVM);
        }

        [HttpPost]
        public IActionResult Delete(CityVM cityVM)
        {
            var cityToDelete = _unitOfWork.City.Get(u => u.CITYCODE == cityVM.City.CITYCODE);

            if (cityToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.City.Remove(cityToDelete);
            _unitOfWork.Save();

            TempData["success"] = "City deleted successfully!";
            return RedirectToAction("Index");
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<City> objCityList = _unitOfWork.City
                .GetAll(includeProperties: "Company,Country")
                .ToList();

            var formattedData = objCityList.Select(l => new
            {
                cityCode = l.CITYCODE,
                cityText = l.CITYTEXT,
                companyText = l.Company.COMTEXT,
                countryText = l.Country.COUNTRYTEXT
            }).ToList();

            return Json(new { data = formattedData });
        }

        #endregion
    }
}
