using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Upsert(string comCode, string ccmDocType, string ccmDocNum, DateTime? ccmDocFrom, DateTime? ccmDocUntil, string lanCode)
        {
            CostCenterAnaVM costCenterAnaVM = new()
            {
                CostCenterAna = new CostCenterAna(),
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
                })
            };

            if (!string.IsNullOrEmpty(comCode) && !string.IsNullOrEmpty(ccmDocType) &&
                !string.IsNullOrEmpty(ccmDocNum) && ccmDocFrom.HasValue &&
                ccmDocUntil.HasValue && !string.IsNullOrEmpty(lanCode))
            {
                var existingRecord = _unitOfWork.CostCenterAna.Get(c =>
                    c.COMCODE == comCode &&
                    c.CCMDOCTYPE == ccmDocType &&
                    c.CCMDOCNUM == ccmDocNum &&
                    c.CCMDOCFROM.Date == ccmDocFrom.Value.Date &&
                    c.CCMDOCUNTIL.Date == ccmDocUntil.Value.Date &&
                    c.LANCODE == lanCode);

                if (existingRecord != null)
                {
                    costCenterAnaVM.CostCenterAna = existingRecord;
                    return View(costCenterAnaVM);
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
                    c.CCMDOCFROM == costCenterAnaVM.CostCenterAna.CCMDOCFROM.Date &&
                    c.CCMDOCUNTIL == costCenterAnaVM.CostCenterAna.CCMDOCUNTIL.Date &&
                    c.LANCODE == costCenterAnaVM.CostCenterAna.LANCODE);

                try
                {
                    if (existing != null)
                    {
                        existing.MAINCCMDOCTYPE = costCenterAnaVM.CostCenterAna.MAINCCMDOCTYPE;
                        existing.MAINCCMDOCNUM = costCenterAnaVM.CostCenterAna.MAINCCMDOCNUM;
                        existing.ISDELETED = costCenterAnaVM.CostCenterAna.ISDELETED;
                        existing.ISPASSIVE = costCenterAnaVM.CostCenterAna.ISPASSIVE;
                        existing.CCMSTEXT = costCenterAnaVM.CostCenterAna.CCMSTEXT;
                        existing.CCMLTEXT = costCenterAnaVM.CostCenterAna.CCMLTEXT;
                        existing.RowVersion = costCenterAnaVM.CostCenterAna.RowVersion;

                        _unitOfWork.CostCenterAna.Update(existing);
                        TempData["success"] = "Cost Center Analysis updated successfully";
                    }
                    else
                    {
                        costCenterAnaVM.CostCenterAna.CCMDOCFROM = costCenterAnaVM.CostCenterAna.CCMDOCFROM.Date;
                        costCenterAnaVM.CostCenterAna.CCMDOCUNTIL = costCenterAnaVM.CostCenterAna.CCMDOCUNTIL.Date;
                        _unitOfWork.CostCenterAna.Add(costCenterAnaVM.CostCenterAna);
                        TempData["success"] = "Cost Center Analysis created successfully";
                    }

                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["error"] = "The record has been modified by another user. Please reload and try again.";
                }
                catch (Exception ex)
                {
                    TempData["error"] = "An error occurred: " + ex.Message;
                }
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


        public IActionResult Delete(string comCode, string ccmDocType, string ccmDocNum,
            DateTime ccmDocFrom, DateTime ccmDocUntil, string lanCode)
        {
            var costCenterAna = _unitOfWork.CostCenterAna.Get(c =>
                c.COMCODE == comCode &&
                c.CCMDOCTYPE == ccmDocType &&
                c.CCMDOCNUM == ccmDocNum &&
                c.CCMDOCFROM.Date == ccmDocFrom.Date &&
                c.CCMDOCUNTIL.Date == ccmDocUntil.Date &&
                c.LANCODE == lanCode);

            if (costCenterAna == null)
            {
                return NotFound();
            }

            return View(costCenterAna);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string comCode, string ccmDocType, string ccmDocNum,
            DateTime ccmDocFrom, DateTime ccmDocUntil, string lanCode)
        {
            var costCenterAna = _unitOfWork.CostCenterAna.Get(c =>
                c.COMCODE == comCode &&
                c.CCMDOCTYPE == ccmDocType &&
                c.CCMDOCNUM == ccmDocNum &&
                c.CCMDOCFROM.Date == ccmDocFrom.Date &&
                c.CCMDOCUNTIL.Date == ccmDocUntil.Date &&
                c.LANCODE == lanCode);

            if (costCenterAna == null)
            {
                return NotFound();
            }

            _unitOfWork.CostCenterAna.Remove(costCenterAna);
            _unitOfWork.Save();
            TempData["success"] = "Cost Center Analysis deleted successfully";
            return RedirectToAction(nameof(Index));
        }


    }
}