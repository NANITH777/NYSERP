using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return View(objCompanyList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company obj)
        {
            var existingCompany = _unitOfWork.Company.GetAll()
                .FirstOrDefault(c => c.COMCODE == obj.COMCODE);

            if (existingCompany != null)
            {
                ModelState.AddModelError("COMCODE", "This COMCODE already exists.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        private string GenerateUniqueUnitCode()
        {
            var lastCM = _unitOfWork.Company.GetAll()
                .OrderByDescending(l => l.COMCODE)
                .FirstOrDefault();

            if (lastCM == null)
            {
                return "CM01";
            }

            string numericPart = lastCM.COMCODE.Substring(2);
            int nextNumber = int.Parse(numericPart) + 1;

            return $"CM{nextNumber:D2}";
        }

        public IActionResult Edit(string? comCode)
        {
            if (string.IsNullOrEmpty(comCode)) 
            {
                return NotFound();
            }

            Company? companyFromDb = _unitOfWork.Company.Get(u => u.COMCODE == comCode);

            if (companyFromDb == null)
            {
                return NotFound();
            }

            return View(companyFromDb);
        }


        [HttpPost]
        public IActionResult Edit(Company obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Company updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(string comCode)
        {
            if (string.IsNullOrEmpty(comCode))
            {
                return NotFound();
            }

            Company? companyFromDb = _unitOfWork.Company.Get(u => u.COMCODE == comCode);

            if (companyFromDb == null)
            {
                return NotFound();
            }

            return View(companyFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(string? comCode)
        {
            try
            {
                Company? obj = _unitOfWork.Company.Get(u => u.COMCODE == comCode);
                if (obj == null)
                {
                    return NotFound();
                }
                _unitOfWork.Company.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Company deleted successfully";
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData["error"] = "Cannot delete this Company because it is being used in other records. Please delete the related records first.";
                return RedirectToAction("Index");
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company
                .GetAll()
                .ToList();
            var formattedData = objCompanyList.Select(c => new
            {
                comCode = c.COMCODE,
                comText = c.COMTEXT,
                address1 = c.ADDRESS1,
                address2 = c.ADDRESS2
            }).ToList();
            return Json(new { data = formattedData });
        }
        #endregion
    }
}
