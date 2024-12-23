using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class WorkCenterAnaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkCenterAnaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var workCenterAnaList = _unitOfWork.WorkCenterAna.GetAll(includeProperties: "Company,WorkCenter,CostCenter,Operation,Language");
            return View(workCenterAnaList);
        }

        public IActionResult Upsert(string comCode, string wcMDocType, string wcMDocNum, DateTime? wcMDocFrom, DateTime? wcMDocUntil, string lanCode, string opRDocType)
        {
            WorkCenterAnaVM workCenterAnaVM = new()
            {
                WorkCenterAna = new WorkCenterAna(),
                CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
                {
                    Text = c.COMCODE,
                    Value = c.COMCODE
                }),
                WorkCenterList = _unitOfWork.WorkCenter.GetAll().Select(wc => new SelectListItem
                {
                    Text = wc.WCMDOCTYPE,
                    Value = wc.WCMDOCTYPE
                }),
                CostCenterList = _unitOfWork.CostCenter.GetAll().Select(cc => new SelectListItem
                {
                    Text = cc.CCMDOCTYPE,
                    Value = cc.CCMDOCTYPE
                }),
                OperationList = _unitOfWork.Operation.GetAll().Select(o => new SelectListItem
                {
                    Text = o.OPRDOCTYPE,
                    Value = o.OPRDOCTYPE
                }),
                LanguageList = _unitOfWork.Language.GetAll().Select(l => new SelectListItem
                {
                    Text = l.LANCODE,
                    Value = l.LANCODE
                })
            };

            if (!string.IsNullOrEmpty(comCode) && !string.IsNullOrEmpty(wcMDocType) &&
                !string.IsNullOrEmpty(wcMDocNum) && wcMDocFrom.HasValue &&
                wcMDocUntil.HasValue && !string.IsNullOrEmpty(lanCode) && !string.IsNullOrEmpty(opRDocType))
            {
                var existingRecord = _unitOfWork.WorkCenterAna.Get(w =>
                    w.COMCODE == comCode &&
                    w.WCMDOCTYPE == wcMDocType &&
                    w.WCMDOCNUM == wcMDocNum &&
                    w.WCMDOCFROM.Date == wcMDocFrom.Value.Date &&
                    w.WCMDOCUNTIL.Date == wcMDocUntil.Value.Date &&
                    w.LANCODE == lanCode &&
                    w.OPRDOCTYPE == opRDocType);

                if (existingRecord != null)
                {
                    workCenterAnaVM.WorkCenterAna = existingRecord;
                    return View(workCenterAnaVM);
                }
            }

            return View(workCenterAnaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(WorkCenterAnaVM workCenterAnaVM)
        {
            if (ModelState.IsValid)
            {
                var existing = _unitOfWork.WorkCenterAna.Get(w =>
                    w.COMCODE == workCenterAnaVM.WorkCenterAna.COMCODE &&
                    w.WCMDOCTYPE == workCenterAnaVM.WorkCenterAna.WCMDOCTYPE &&
                    w.WCMDOCNUM == workCenterAnaVM.WorkCenterAna.WCMDOCNUM &&
                    w.WCMDOCFROM == workCenterAnaVM.WorkCenterAna.WCMDOCFROM.Date &&
                    w.WCMDOCUNTIL == workCenterAnaVM.WorkCenterAna.WCMDOCUNTIL.Date &&
                    w.LANCODE == workCenterAnaVM.WorkCenterAna.LANCODE && 
                    w.OPRDOCTYPE == workCenterAnaVM.WorkCenterAna.OPRDOCTYPE);

                try
                {
                    if (existing != null)
                    {
                        existing.MAINWCMDOCTYPE = workCenterAnaVM.WorkCenterAna.MAINWCMDOCTYPE;
                        existing.MAINWCMDOCNUM = workCenterAnaVM.WorkCenterAna.MAINWCMDOCNUM;
                        existing.ISDELETED = workCenterAnaVM.WorkCenterAna.ISDELETED;
                        existing.ISPASSIVE = workCenterAnaVM.WorkCenterAna.ISPASSIVE;
                        existing.WCMSTEXT = workCenterAnaVM.WorkCenterAna.WCMSTEXT;
                        existing.WCMLTEXT = workCenterAnaVM.WorkCenterAna.WCMLTEXT;
                        existing.RowVersion = workCenterAnaVM.WorkCenterAna.RowVersion;

                        _unitOfWork.WorkCenterAna.Update(existing);
                        TempData["success"] = "Work Center Analysis updated successfully";
                    }
                    else
                    {
                        workCenterAnaVM.WorkCenterAna.WCMDOCFROM = workCenterAnaVM.WorkCenterAna.WCMDOCFROM.Date;
                        workCenterAnaVM.WorkCenterAna.WCMDOCUNTIL = workCenterAnaVM.WorkCenterAna.WCMDOCUNTIL.Date;
                        _unitOfWork.WorkCenterAna.Add(workCenterAnaVM.WorkCenterAna);
                        TempData["success"] = "Work Center Analysis created successfully";
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

            workCenterAnaVM.CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
            {
                Text = c.COMCODE,
                Value = c.COMCODE
            });
            workCenterAnaVM.WorkCenterList = _unitOfWork.WorkCenter.GetAll().Select(wc => new SelectListItem
            {
                Text = wc.WCMDOCTYPE,
                Value = wc.WCMDOCTYPE
            });
            workCenterAnaVM.CostCenterList = _unitOfWork.CostCenter.GetAll().Select(cc => new SelectListItem
            {
                Text = cc.CCMDOCTYPE,
                Value = cc.CCMDOCTYPE
            });
            workCenterAnaVM.OperationList = _unitOfWork.Operation.GetAll().Select(o => new SelectListItem
            {
                Text = o.OPRDOCTYPE,
                Value = o.OPRDOCTYPE
            });
            workCenterAnaVM.LanguageList = _unitOfWork.Language.GetAll().Select(l => new SelectListItem
            {
                Text = l.LANCODE,
                Value = l.LANCODE
            });

            return View(workCenterAnaVM);
        }

        public IActionResult Delete(string comCode, string wcMDocType, string wcMDocNum,
            DateTime wcMDocFrom, DateTime wcMDocUntil, string lanCode, string opRDocType)
        {
            var workCenterAna = _unitOfWork.WorkCenterAna.Get(w =>
                w.COMCODE == comCode &&
                w.WCMDOCTYPE == wcMDocType &&
                w.WCMDOCNUM == wcMDocNum &&
                w.WCMDOCFROM.Date == wcMDocFrom.Date &&
                w.WCMDOCUNTIL.Date == wcMDocUntil.Date &&
                w.LANCODE == lanCode &&
                w.OPRDOCTYPE == opRDocType);

            if (workCenterAna == null)
            {
                return NotFound();
            }

            return View(workCenterAna);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string comCode, string wcMDocType, string wcMDocNum,
            DateTime wcMDocFrom, DateTime wcMDocUntil, string lanCode, string opRDocType)
        {
            var workCenterAna = _unitOfWork.WorkCenterAna.Get(w =>
                w.COMCODE == comCode &&
                w.WCMDOCTYPE == wcMDocType &&
                w.WCMDOCNUM == wcMDocNum &&
                w.WCMDOCFROM.Date == wcMDocFrom.Date &&
                w.WCMDOCUNTIL.Date == wcMDocUntil.Date &&
                w.LANCODE == lanCode &&
                w.OPRDOCTYPE == opRDocType);

            if (workCenterAna == null)
            {
                return NotFound();
            }

            _unitOfWork.WorkCenterAna.Remove(workCenterAna);
            _unitOfWork.Save();
            TempData["success"] = "Work Center Analysis deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
