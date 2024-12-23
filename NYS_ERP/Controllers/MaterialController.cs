using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaterialController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var materialList = _unitOfWork.Material.GetAll(includeProperties: "Company,MaterialType,Language,BOM,Rota");
            return View(materialList);
        }

        public IActionResult Upsert(string comCode, string matDocType, string matDocNum, DateTime? matDocFrom, DateTime? matDocUntil, string lanCode)
        {
            MaterialVM materialVM = new()
            {
                Material = new Material(),
                CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
                {
                    Text = c.COMCODE,
                    Value = c.COMCODE
                }),
                MaterialTypeList = _unitOfWork.MaterialType.GetAll().Select(mt => new SelectListItem
                {
                    Text = mt.MATDOCTYPE,
                    Value = mt.MATDOCTYPE
                }),
                LanguageList = _unitOfWork.Language.GetAll().Select(l => new SelectListItem
                {
                    Text = l.LANCODE,
                    Value = l.LANCODE
                }),
                BOMList = _unitOfWork.BOM.GetAll().Select(mt => new SelectListItem
                {
                    Text = mt.BOMDOCTYPE,
                    Value = mt.BOMDOCTYPE
                }),
                RotaList = _unitOfWork.Rota.GetAll().Select(l => new SelectListItem
                {
                    Text = l.ROTDOCTYPE,
                    Value = l.ROTDOCTYPE
                })
            };

            if (!string.IsNullOrEmpty(comCode) && !string.IsNullOrEmpty(matDocType) &&
                !string.IsNullOrEmpty(matDocNum) && matDocFrom.HasValue &&
                matDocUntil.HasValue && !string.IsNullOrEmpty(lanCode))
            {
                var existingRecord = _unitOfWork.Material.Get(m =>
                    m.COMCODE == comCode &&
                    m.MATDOCTYPE == matDocType &&
                    m.MATDOCNUM == matDocNum &&
                    m.MATDOCFROM.Date == matDocFrom.Value.Date &&
                    m.MATDOCUNTIL.Date == matDocUntil.Value.Date &&
                    m.LANCODE == lanCode);

                if (existingRecord != null)
                {
                    materialVM.Material = existingRecord;
                    return View(materialVM);
                }
            }

            return View(materialVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(MaterialVM materialVM)
        {
            if (ModelState.IsValid)
            {
                var existing = _unitOfWork.Material.Get(m =>
                    m.COMCODE == materialVM.Material.COMCODE &&
                    m.MATDOCTYPE == materialVM.Material.MATDOCTYPE &&
                    m.MATDOCNUM == materialVM.Material.MATDOCNUM &&
                    m.MATDOCFROM == materialVM.Material.MATDOCFROM.Date &&
                    m.MATDOCUNTIL == materialVM.Material.MATDOCUNTIL.Date &&
                    m.LANCODE == materialVM.Material.LANCODE);

                try
                {
                    if (existing != null)
                    {   
                        existing.SUPPLYTYPE = materialVM.Material.SUPPLYTYPE;
                        existing.STUNIT = materialVM.Material.STUNIT;
                        existing.NETWEIGHT = materialVM.Material.NETWEIGHT;
                        existing.NWUNIT = materialVM.Material.NWUNIT;
                        existing.BRUTWEIGHT = materialVM.Material.BRUTWEIGHT;
                        existing.BWUNIT = materialVM.Material.BWUNIT;
                        existing.ISBOM = materialVM.Material.ISBOM;
                        existing.BOMDOCTYPE = materialVM.Material.BOMDOCTYPE;
                        existing.BOMDOCNUM = materialVM.Material.BOMDOCNUM;
                        existing.ISROUTE = materialVM.Material.ISROUTE;
                        existing.ROTDOCTYPE = materialVM.Material.ROTDOCTYPE;
                        existing.ROTDOCNUM = materialVM.Material.ROTDOCNUM;
                        existing.ISDELETED = materialVM.Material.ISDELETED;
                        existing.ISPASSIVE = materialVM.Material.ISPASSIVE;
                        existing.MATSTEXT = materialVM.Material.MATSTEXT;
                        existing.MATLTEXT = materialVM.Material.MATLTEXT;
                        existing.RowVersion = materialVM.Material.RowVersion;

                        _unitOfWork.Material.Update(existing);
                        TempData["success"] = "Material updated successfully";
                    }
                    else
                    {
                        materialVM.Material.MATDOCFROM = materialVM.Material.MATDOCFROM.Date;
                        materialVM.Material.MATDOCUNTIL = materialVM.Material.MATDOCUNTIL.Date;
                        _unitOfWork.Material.Add(materialVM.Material);
                        TempData["success"] = "Material created successfully";
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

            materialVM.CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
            {
                Text = c.COMCODE,
                Value = c.COMCODE
            });
            materialVM.MaterialTypeList = _unitOfWork.MaterialType.GetAll().Select(mt => new SelectListItem
            {
                Text = mt.MATDOCTYPE,
                Value = mt.MATDOCTYPE
            });
            materialVM.LanguageList = _unitOfWork.Language.GetAll().Select(l => new SelectListItem
            {
                Text = l.LANCODE,
                Value = l.LANCODE
            });
            materialVM.BOMList = _unitOfWork.BOM.GetAll().Select(mt => new SelectListItem
            {
                Text = mt.BOMDOCTYPE,
                Value = mt.BOMDOCTYPE
            });
            materialVM.RotaList = _unitOfWork.Rota.GetAll().Select(l => new SelectListItem
            {
                Text = l.ROTDOCTYPE,
                Value = l.ROTDOCTYPE
            });

            return View(materialVM);
        }

        public IActionResult Delete(string comCode, string matDocType, string matDocNum, DateTime matDocFrom, DateTime matDocUntil, string lanCode)
        {
            var material = _unitOfWork.Material.Get(m =>
                m.COMCODE == comCode &&
                m.MATDOCTYPE == matDocType &&
                m.MATDOCNUM == matDocNum &&
                m.MATDOCFROM.Date == matDocFrom.Date &&
                m.MATDOCUNTIL.Date == matDocUntil.Date &&
                m.LANCODE == lanCode);

            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string comCode, string matDocType, string matDocNum, DateTime matDocFrom, DateTime matDocUntil, string lanCode)
        {
            var material = _unitOfWork.Material.Get(m =>
                m.COMCODE == comCode &&
                m.MATDOCTYPE == matDocType &&
                m.MATDOCNUM == matDocNum &&
                m.MATDOCFROM.Date == matDocFrom.Date &&
                m.MATDOCUNTIL.Date == matDocUntil.Date &&
                m.LANCODE == lanCode);

            if (material == null)
            {
                return NotFound();
            }

            _unitOfWork.Material.Remove(material);
            _unitOfWork.Save();
            TempData["success"] = "Material deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
