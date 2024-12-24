using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;
using System.Reflection.Emit;

namespace NYS_ERP.Controllers
{
    public class RotaAnaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RotaAnaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var rotaAnaList = _unitOfWork.RotaAna.GetAll(includeProperties: "Company,Rota,MaterialType,WorkCenter,BOM,Operation");
            return View(rotaAnaList);
        }

        public IActionResult Upsert(string comCode, string rotDocType, string rotDocNum, DateTime? rotDocFrom, 
            DateTime? rotDocUntil, string matDocType, string matDocNum, int oprNum, string bomDocType, string bomDocNum, int conTentum)
        {
            RotaAnaVM rotaAnaVM = new()
            {
                RotaAna = new RotaAna(),
                CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
                {
                    Text = c.COMCODE,
                    Value = c.COMCODE
                }),
                RotaList = _unitOfWork.Rota.GetAll().Select(wc => new SelectListItem
                {
                    Text = wc.ROTDOCTYPE,
                    Value = wc.ROTDOCTYPE
                }),
                MaterialTypeList = _unitOfWork.MaterialType.GetAll().Select(mt => new SelectListItem
                {
                    Text = mt.MATDOCTYPE,
                    Value = mt.MATDOCTYPE
                }),
                WorkCenterList = _unitOfWork.WorkCenter.GetAll().Select(wc => new SelectListItem
                {
                    Text = wc.WCMDOCTYPE,
                    Value = wc.WCMDOCTYPE
                }),
                BOMList = _unitOfWork.BOM.GetAll().Select(bm => new SelectListItem
                {
                    Text = bm.BOMDOCTYPE,
                    Value = bm.BOMDOCTYPE
                }),
                OperationList = _unitOfWork.Operation.GetAll().Select(o => new SelectListItem
                {
                    Text = o.OPRDOCTYPE,
                    Value = o.OPRDOCTYPE
                })
                
            };

            if (!string.IsNullOrEmpty(comCode) && !string.IsNullOrEmpty(rotDocType) &&
                !string.IsNullOrEmpty(rotDocNum) && rotDocFrom.HasValue &&
                rotDocUntil.HasValue && !string.IsNullOrEmpty(matDocType) && 
                !string.IsNullOrEmpty(matDocNum) && oprNum >= 0 && !string.IsNullOrEmpty(bomDocType) &&
                !string.IsNullOrEmpty(bomDocNum) && conTentum >= 0)
            {
                var existingRecord = _unitOfWork.RotaAna.Get(r =>
                    r.COMCODE == comCode &&
                    r.ROTDOCTYPE == rotDocType &&
                    r.ROTDOCNUM == rotDocNum &&
                    r.ROTDOCFROM.Date == rotDocFrom.Value.Date &&
                    r.ROTDOCUNTIL.Date == rotDocUntil.Value.Date &&
                    r.MATDOCTYPE == matDocType &&
                    r.MATDOCNUM == matDocNum &&
                    r.CONTENTNUM == oprNum &&
                    r.BOMDOCTYPE == bomDocType &&
                    r.BOMDOCNUM == bomDocNum &&
                    r.CONTENTNUM == conTentum);

                if (existingRecord != null)
                {
                    rotaAnaVM.RotaAna = existingRecord;
                    return View(rotaAnaVM);
                }
            }

            return View(rotaAnaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(RotaAnaVM rotaAnaVM)
        {
            if (ModelState.IsValid)
            {
                var existing = _unitOfWork.RotaAna.Get(r =>
                    r.COMCODE == rotaAnaVM.RotaAna.COMCODE &&
                    r.ROTDOCTYPE == rotaAnaVM.RotaAna.ROTDOCTYPE &&
                    r.ROTDOCNUM == rotaAnaVM.RotaAna.ROTDOCNUM &&
                    r.ROTDOCFROM == rotaAnaVM.RotaAna.ROTDOCFROM.Date &&
                    r.ROTDOCUNTIL == rotaAnaVM.RotaAna.ROTDOCUNTIL.Date &&
                    r.MATDOCTYPE == rotaAnaVM.RotaAna.MATDOCTYPE &&
                    r.MATDOCNUM == rotaAnaVM.RotaAna.MATDOCNUM &&
                    r.CONTENTNUM == rotaAnaVM.RotaAna.CONTENTNUM &&
                    r.BOMDOCTYPE == rotaAnaVM.RotaAna.BOMDOCTYPE &&
                    r.BOMDOCNUM == rotaAnaVM.RotaAna.BOMDOCNUM &&
                    r.CONTENTNUM == rotaAnaVM.RotaAna.CONTENTNUM);

                try
                {
                    if (existing != null)
                    {
                        existing.QUANTITY = rotaAnaVM.RotaAna.QUANTITY;
                        existing.ISDELETED = rotaAnaVM.RotaAna.ISDELETED;
                        existing.ISPASSIVE = rotaAnaVM.RotaAna.ISPASSIVE;
                        existing.DRAWNUM = rotaAnaVM.RotaAna.DRAWNUM;
                        existing.WCMDOCTYPE = rotaAnaVM.RotaAna.WCMDOCTYPE;
                        existing.WCMDOCNUM = rotaAnaVM.RotaAna.WCMDOCNUM;
                        existing.OPRDOCTYPE = rotaAnaVM.RotaAna.OPRDOCTYPE;
                        existing.SETUPTIME = rotaAnaVM.RotaAna.SETUPTIME;
                        existing.MACHINETIME = rotaAnaVM.RotaAna.MACHINETIME;
                        existing.LABOURTIME = rotaAnaVM.RotaAna.LABOURTIME;
                        existing.COMPONENT_QUANTITY = rotaAnaVM.RotaAna.COMPONENT_QUANTITY;
                        existing.RowVersion = rotaAnaVM.RotaAna.RowVersion;

                        _unitOfWork.RotaAna.Update(existing);
                        TempData["success"] = "Rota Analysis updated successfully";
                    }
                    else
                    {
                        rotaAnaVM.RotaAna.ROTDOCFROM = rotaAnaVM.RotaAna.ROTDOCFROM.Date;
                        rotaAnaVM.RotaAna.ROTDOCUNTIL = rotaAnaVM.RotaAna.ROTDOCUNTIL.Date;
                        _unitOfWork.RotaAna.Add(rotaAnaVM.RotaAna);
                        TempData["success"] = "Rota Analysis created successfully";
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

            rotaAnaVM.CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
            {
                Text = c.COMCODE,
                Value = c.COMCODE
            });
            rotaAnaVM.RotaList = _unitOfWork.Rota.GetAll().Select(wc => new SelectListItem
            {
                Text = wc.ROTDOCTYPE,
                Value = wc.ROTDOCTYPE
            });
            rotaAnaVM.MaterialTypeList = _unitOfWork.MaterialType.GetAll().Select(cc => new SelectListItem
            {
                Text = cc.MATDOCTYPE,
                Value = cc.MATDOCTYPE
            });
            rotaAnaVM.WorkCenterList = _unitOfWork.WorkCenter.GetAll().Select(cc => new SelectListItem
            {
                Text = cc.WCMDOCTYPE,
                Value = cc.WCMDOCTYPE
            });
            rotaAnaVM.BOMList = _unitOfWork.BOM.GetAll().Select(l => new SelectListItem
            {
                Text = l.BOMDOCTYPE,
                Value = l.BOMDOCTYPE
            });
            rotaAnaVM.OperationList = _unitOfWork.Operation.GetAll().Select(o => new SelectListItem
            {
                Text = o.OPRDOCTYPE,
                Value = o.OPRDOCTYPE
            });
           
            return View(rotaAnaVM);
        }

        public IActionResult Delete(string comCode, string rotDocType, string rotDocNum, DateTime rotDocFrom,
            DateTime rotDocUntil, string matDocType, string matDocNum, int oprNum, string bomDocType, string bomDocNum, int conTentum)
        {
            var rotaAna = _unitOfWork.RotaAna.Get(r =>
                r.COMCODE == comCode &&
                r.ROTDOCTYPE == rotDocType &&
                r.ROTDOCNUM == rotDocNum &&
                r.ROTDOCFROM.Date == rotDocFrom.Date &&
                r.ROTDOCUNTIL.Date == rotDocUntil.Date &&
                r.MATDOCTYPE == matDocType &&
                r.MATDOCNUM == matDocNum &&
                r.CONTENTNUM == oprNum &&
                r.BOMDOCTYPE == bomDocType &&
                r.BOMDOCNUM == bomDocNum &&
                r.CONTENTNUM == conTentum);

            if (rotaAna == null)
            {
                return NotFound();
            }

            return View(rotaAna);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string comCode, string rotDocType, string rotDocNum, DateTime rotDocFrom,
            DateTime rotDocUntil, string matDocType, string matDocNum, int oprNum, string bomDocType, string bomDocNum, int conTentum)
        {
            var rotaAna = _unitOfWork.RotaAna.Get(r =>
               r.COMCODE == comCode &&
               r.ROTDOCTYPE == rotDocType &&
               r.ROTDOCNUM == rotDocNum &&
               r.ROTDOCFROM.Date == rotDocFrom.Date &&
               r.ROTDOCUNTIL.Date == rotDocUntil.Date &&
               r.MATDOCTYPE == matDocType &&
               r.MATDOCNUM == matDocNum &&
               r.CONTENTNUM == oprNum &&
               r.BOMDOCTYPE == bomDocType &&
               r.BOMDOCNUM == bomDocNum &&
               r.CONTENTNUM == conTentum);

            if (rotaAna == null)
            {
                return NotFound();
            }

            _unitOfWork.RotaAna.Remove(rotaAna);
            _unitOfWork.Save();
            TempData["success"] = "Rota Analysis deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
