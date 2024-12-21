using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class CostCenterAnaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CostCenterAnaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var costCenterAnaList = _unitOfWork.CostCenterAna.GetAll(includeProperties: "Company,CostCenter,Language");
            return View(costCenterAnaList);
        }

        public IActionResult Upsert(string comCode, string ccmDocType, string ccmDocNum, DateTime ccmDocFrom, DateTime ccmDocUntil, string lanCode)
        {
            CostCenterAnaVM costCenterAnaVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
                {
                    Text = c.COMCODE,
                    Value = c.COMCODE
                }),
                CostCenterList = _unitOfWork.CostCenter.GetAll().Select(cc => new SelectListItem
                {
                    Text = cc.CCMDOCTYPE,
                    Value = cc.CCMDOCTYPE
                }),
                LanguageList = _unitOfWork.Language.GetAll().Select(l => new SelectListItem
                {
                    Text = l.LANCODE,
                    Value = l.LANCODE
                }),
                CostCenterAna = new CostCenterAna()
            };

            if (!string.IsNullOrEmpty(comCode) && !string.IsNullOrEmpty(ccmDocType) &&
                !string.IsNullOrEmpty(ccmDocNum) && !string.IsNullOrEmpty(lanCode))
            {
                costCenterAnaVM.CostCenterAna = _unitOfWork.CostCenterAna.Get(c =>
                    c.COMCODE == comCode &&
                    c.CCMDOCTYPE == ccmDocType &&
                    c.CCMDOCNUM == ccmDocNum &&
                    c.CCMDOCFROM == ccmDocFrom &&
                    c.CCMDOCUNTIL == ccmDocUntil &&
                    c.LANCODE == lanCode);

                if (costCenterAnaVM.CostCenterAna == null)
                {
                    return NotFound();
                }
            }

            return View(costCenterAnaVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CostCenterAnaVM costCenterAnaVM)
        {
            if (ModelState.IsValid)
            {
                var existing = _unitOfWork.CostCenterAna.Get(c =>
                    c.COMCODE == costCenterAnaVM.CostCenterAna.COMCODE &&
                    c.CCMDOCTYPE == costCenterAnaVM.CostCenterAna.CCMDOCTYPE &&
                    c.CCMDOCNUM == costCenterAnaVM.CostCenterAna.CCMDOCNUM &&
                    c.CCMDOCFROM == costCenterAnaVM.CostCenterAna.CCMDOCFROM &&
                    c.CCMDOCUNTIL == costCenterAnaVM.CostCenterAna.CCMDOCUNTIL &&
                    c.LANCODE == costCenterAnaVM.CostCenterAna.LANCODE);

                if (existing != null)
                {
                    // Copier toutes les propriétés non-clés
                    existing.MAINCCMDOCTYPE = costCenterAnaVM.CostCenterAna.MAINCCMDOCTYPE;
                    existing.MAINCCMDOCNUM = costCenterAnaVM.CostCenterAna.MAINCCMDOCNUM;
                    existing.ISDELETED = costCenterAnaVM.CostCenterAna.ISDELETED;
                    existing.ISPASSIVE = costCenterAnaVM.CostCenterAna.ISPASSIVE;
                    existing.CCMSTEXT = costCenterAnaVM.CostCenterAna.CCMSTEXT;
                    existing.CCMLTEXT = costCenterAnaVM.CostCenterAna.CCMLTEXT;

                    _unitOfWork.CostCenterAna.Update(existing);
                    TempData["success"] = "Cost Center Analysis updated successfully";
                }
                else
                {
                    _unitOfWork.CostCenterAna.Add(costCenterAnaVM.CostCenterAna);
                    TempData["success"] = "Cost Center Analysis created successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            costCenterAnaVM.CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
            {
                Text = c.COMCODE,
                Value = c.COMCODE
            });
            costCenterAnaVM.CostCenterList = _unitOfWork.CostCenter.GetAll().Select(cc => new SelectListItem
            {
                Text = cc.CCMDOCTYPE,
                Value = cc.CCMDOCTYPE
            });
            costCenterAnaVM.LanguageList = _unitOfWork.Language.GetAll().Select(l => new SelectListItem
            {
                Text = l.LANCODE,
                Value = l.LANCODE
            });

            return View(costCenterAnaVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string comCode, string ccmDocType, string ccmDocNum,
     DateTime ccmDocFrom, DateTime ccmDocUntil, string lanCode)
        {
            try
            {
                // Validation des paramètres
                if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(ccmDocType) ||
                    string.IsNullOrEmpty(ccmDocNum) || string.IsNullOrEmpty(lanCode))
                {
                    return Json(new { success = false, message = "Paramètres invalides pour la suppression." });
                }

                // Normaliser les dates pour ne garder que la partie Date sans l'heure
                ccmDocFrom = ccmDocFrom.Date;
                ccmDocUntil = ccmDocUntil.Date;

                var costCenterAna = _unitOfWork.CostCenterAna.Get(c =>
                    c.COMCODE == comCode &&
                    c.CCMDOCTYPE == ccmDocType &&
                    c.CCMDOCNUM == ccmDocNum &&
                    c.CCMDOCFROM.Date == ccmDocFrom &&
                    c.CCMDOCUNTIL.Date == ccmDocUntil &&
                    c.LANCODE == lanCode);

                if (costCenterAna == null)
                {
                    return Json(new { success = false, message = "Enregistrement non trouvé." });
                }

                // Supprimer l'enregistrement
                _unitOfWork.CostCenterAna.Remove(costCenterAna);
                _unitOfWork.Save();

                return Json(new { success = true, message = "Suppression réussie." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erreur: {ex.Message}" });
            }
        }

    }
}