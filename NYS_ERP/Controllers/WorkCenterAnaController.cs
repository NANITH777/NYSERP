using Microsoft.AspNetCore.Mvc;
using NYS_ERP.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Upsert(string comcode = "", string wcmdoctype = "", string wcmdocnum = "", DateTime? wcmdocfrom = null,
            DateTime? wcmdocuntil = null, string lancode = "", string oprdoctype = "")
        {
            WorkCenterAna workCenterAna = new();

            ViewBag.CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
            {
                Text = c.COMCODE,
                Value = c.COMCODE
            });

            ViewBag.WorkCenterList = _unitOfWork.WorkCenter.GetAll().Select(w => new SelectListItem
            {
                Text = w.WCMDOCTYPE,
                Value = w.WCMDOCTYPE
            });

            ViewBag.CostCenterList = _unitOfWork.CostCenter.GetAll().Select(cc => new SelectListItem
            {
                Text = cc.CCMDOCTYPE,
                Value = cc.CCMDOCTYPE
            });

            ViewBag.LanguageList = _unitOfWork.Language.GetAll().Select(l => new SelectListItem
            {
                Text = l.LANCODE,
                Value = l.LANCODE
            });

            ViewBag.OperationList = _unitOfWork.Operation.GetAll().Select(o => new SelectListItem
            {
                Text = o.OPRDOCTYPE,
                Value = o.OPRDOCTYPE
            });

            if (string.IsNullOrEmpty(comcode) || string.IsNullOrEmpty(wcmdoctype) || string.IsNullOrEmpty(wcmdocnum) ||
                !wcmdocfrom.HasValue || !wcmdocuntil.HasValue || string.IsNullOrEmpty(lancode) || string.IsNullOrEmpty(oprdoctype))
            {
                return View(workCenterAna);
            }

            workCenterAna = _unitOfWork.WorkCenterAna.Get(w =>
                w.COMCODE == comcode &&
                w.WCMDOCTYPE == wcmdoctype &&
                w.WCMDOCNUM == wcmdocnum &&
                w.WCMDOCFROM == wcmdocfrom &&
                w.WCMDOCUNTIL == wcmdocuntil &&
                w.LANCODE == lancode &&
                w.OPRDOCTYPE == oprdoctype);

            if (workCenterAna == null)
            {
                return NotFound();
            }

            return View(workCenterAna);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(WorkCenterAna workCenterAna)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.WorkCenterAna.Get(w =>
                    w.COMCODE == workCenterAna.COMCODE &&
                    w.WCMDOCTYPE == workCenterAna.WCMDOCTYPE &&
                    w.WCMDOCNUM == workCenterAna.WCMDOCNUM &&
                    w.WCMDOCFROM == workCenterAna.WCMDOCFROM &&
                    w.WCMDOCUNTIL == workCenterAna.WCMDOCUNTIL &&
                    w.LANCODE == workCenterAna.LANCODE &&
                    w.OPRDOCTYPE == workCenterAna.OPRDOCTYPE) == null)
                {
                    _unitOfWork.WorkCenterAna.Add(workCenterAna);
                    TempData["success"] = "Work Center Analysis created successfully";
                }
                else
                {
                    _unitOfWork.WorkCenterAna.Update(workCenterAna);
                    TempData["success"] = "Work Center Analysis updated successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(workCenterAna);
        }

        public IActionResult Delete(string comcode, string wcmdoctype, string wcmdocnum, DateTime wcmdocfrom,
            DateTime wcmdocuntil, string lancode, string oprdoctype)
        {
            var workCenterAna = _unitOfWork.WorkCenterAna.Get(w =>
                w.COMCODE == comcode &&
                w.WCMDOCTYPE == wcmdoctype &&
                w.WCMDOCNUM == wcmdocnum &&
                w.WCMDOCFROM == wcmdocfrom &&
                w.WCMDOCUNTIL == wcmdocuntil &&
                w.LANCODE == lancode &&
                w.OPRDOCTYPE == oprdoctype);

            if (workCenterAna == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.WorkCenterAna.Remove(workCenterAna);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}