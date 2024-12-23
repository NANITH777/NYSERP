using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Migrations;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;
using System.Reflection.Emit;

namespace NYS_ERP.Controllers
{
    public class BOMAnaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BOMAnaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var bomAnaList = _unitOfWork.BOMAna.GetAll(includeProperties: "Company,BOM,MaterialType");
            return View(bomAnaList);
        }

        public IActionResult Upsert(string comCode, string bomDocType, string bomDocNum, DateTime? bomDocFrom, DateTime? bomDocUntil, string matDocType, string matDocNum, int conTentum)
        {
            BOMAnaVM bomAnaVM = new()
            {
                BOMAna = new BOMAna(),
                CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
                {
                    Text = c.COMCODE,
                    Value = c.COMCODE
                }),
                BOMList = _unitOfWork.BOM.GetAll().Select(bom => new SelectListItem
                {
                    Text = bom.BOMDOCTYPE,
                    Value = bom.BOMDOCTYPE
                }),
                MaterialTypeList = _unitOfWork.MaterialType.GetAll().Select(mt => new SelectListItem
                {
                    Text = mt.MATDOCTYPE,
                    Value = mt.MATDOCTYPE
                }),
            };

            if (!string.IsNullOrEmpty(comCode) && !string.IsNullOrEmpty(bomDocType) &&
                !string.IsNullOrEmpty(bomDocNum) && bomDocFrom.HasValue &&
                bomDocUntil.HasValue  && !string.IsNullOrEmpty(matDocType) &&
                !string.IsNullOrEmpty(matDocNum) && conTentum >= 0)
            {
                var existingRecord = _unitOfWork.BOMAna.Get(c =>
                    c.COMCODE == comCode &&
                    c.BOMDOCTYPE == bomDocType &&
                    c.BOMDOCNUM== bomDocNum &&
                    c.BOMDOCFROM.Date == bomDocFrom.Value.Date &&
                    c.BOMDOCUNTIL.Date == bomDocUntil.Value.Date &&
                    c.MATDOCTYPE == matDocType &&
                    c.MATDOCNUM == matDocNum &&
                    c.CONTENTNUM == conTentum);

                if (existingRecord != null)
                {
                    bomAnaVM.BOMAna = existingRecord;
                    return View(bomAnaVM);
                }
            }

            return View(bomAnaVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BOMAnaVM bomAnaVM)
        {
            if (ModelState.IsValid)
            {
                var existing = _unitOfWork.BOMAna.Get(c =>
                   c.COMCODE == bomAnaVM.BOMAna.COMCODE &&
                   c.BOMDOCTYPE == bomAnaVM.BOMAna.BOMDOCTYPE &&
                   c.BOMDOCNUM == bomAnaVM.BOMAna.BOMDOCNUM &&
                   c.BOMDOCFROM.Date == bomAnaVM.BOMAna.BOMDOCFROM.Date &&
                   c.BOMDOCUNTIL.Date == bomAnaVM.BOMAna.BOMDOCUNTIL.Date &&
                   c.MATDOCTYPE == bomAnaVM.BOMAna.MATDOCTYPE &&
                   c.MATDOCNUM == bomAnaVM.BOMAna.MATDOCNUM &&
                   c.CONTENTNUM == bomAnaVM.BOMAna.CONTENTNUM);


                try
                {
                    if (existing != null)
                    {
                        existing.QUANTITY = bomAnaVM.BOMAna.QUANTITY;
                        existing.ISDELETED = bomAnaVM.BOMAna.ISDELETED;
                        existing.ISPASSIVE = bomAnaVM.BOMAna.ISPASSIVE;
                        existing.DRAWNUM = bomAnaVM.BOMAna.DRAWNUM;
                        existing.COMPONENT = bomAnaVM.BOMAna.COMPONENT;
                        existing.COMPBOMDOCTYPE = bomAnaVM.BOMAna.COMPBOMDOCTYPE;
                        existing.COMPONENT_QUANTITY = bomAnaVM.BOMAna.COMPONENT_QUANTITY;
                        existing.RowVersion = bomAnaVM.BOMAna.RowVersion;

                        _unitOfWork.BOMAna.Update(existing);
                        TempData["success"] = "BOM updated successfully";
                    }
                    else
                    {
                        bomAnaVM.BOMAna.BOMDOCFROM = bomAnaVM.BOMAna.BOMDOCFROM.Date;
                        bomAnaVM.BOMAna.BOMDOCUNTIL = bomAnaVM.BOMAna.BOMDOCUNTIL.Date;
                        _unitOfWork.BOMAna.Add(bomAnaVM.BOMAna);
                        TempData["success"] = "BOM created successfully";
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

            bomAnaVM.CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
            {
                Text = c.COMCODE,
                Value = c.COMCODE
            });
            bomAnaVM.BOMList = _unitOfWork.BOM.GetAll().Select(bom => new SelectListItem
            {
                Text = bom.BOMDOCTYPE,
                Value = bom.BOMDOCTYPE
            });
            bomAnaVM.MaterialTypeList = _unitOfWork.MaterialType.GetAll().Select(mt => new SelectListItem
            {
                Text = mt.MATDOCTYPE,
                Value = mt.MATDOCTYPE
            });

            return View(bomAnaVM);
        }


        public IActionResult Delete(string comCode, string bomDocType, string bomDocNum, DateTime bomDocFrom, DateTime bomDocUntil, string matDocType, string matDocNum, int conTentum)
        {
            var bomAna = _unitOfWork.BOMAna.Get(c =>
                c.COMCODE == comCode &&
                c.BOMDOCTYPE == bomDocType &&
                c.BOMDOCNUM == bomDocNum &&
                c.BOMDOCFROM.Date == bomDocFrom.Date &&
                c.BOMDOCUNTIL.Date == bomDocUntil.Date &&
                c.MATDOCTYPE == matDocType &&
                c.MATDOCNUM == matDocNum &&
                c.CONTENTNUM == conTentum);

            if (bomAna == null)
            {
                return NotFound();
            }

            return View(bomAna);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string comCode, string bomDocType, string bomDocNum, DateTime bomDocFrom, DateTime bomDocUntil, string matDocType, string matDocNum, int conTentum)
        {
            var bomAna = _unitOfWork.BOMAna.Get(c =>
                c.COMCODE == comCode &&
                c.BOMDOCTYPE == bomDocType &&
                c.BOMDOCTYPE == bomDocType &&
                c.BOMDOCFROM.Date == bomDocFrom.Date &&
                c.BOMDOCUNTIL.Date == bomDocUntil.Date &&
                c.MATDOCTYPE == matDocType &&
                c.MATDOCNUM == matDocNum &&
                c.CONTENTNUM == conTentum);

            if (bomAna == null)
            {
                return NotFound();
            }

            _unitOfWork.BOMAna.Remove(bomAna);
            _unitOfWork.Save();
            TempData["success"] = "BOM deleted successfully";
            return RedirectToAction(nameof(Index));
        }


    }
}