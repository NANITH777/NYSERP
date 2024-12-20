using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NYS_ERP.Controllers
{
    public class CostCenterAnaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CostCenterAnaController> _logger;

        public CostCenterAnaController(IUnitOfWork unitOfWork, ILogger<CostCenterAnaController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var costCenterList = _unitOfWork.CostCenterAna.GetAll(includeProperties: "Company,CostCenter,Language").ToList();
            return View(costCenterList);
        }

        public IActionResult Create()
        {
            var costCenterAnaVM = new CostCenterAnaVM
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
                {
                    Text = c.COMTEXT,
                    Value = c.COMCODE
                }),
                CostCenterList = _unitOfWork.CostCenter.GetAll().Select(cc => new SelectListItem
                {
                    Text = cc.CCMDOCNUM,
                    Value = cc.CCMDOCTYPE
                }),
                LanguageList = _unitOfWork.Language.GetAll().Select(l => new SelectListItem
                {
                    Text = l.LANTEXT,
                    Value = l.LANCODE
                }),
                CostCenterAna = new CostCenterAna()
            };

            return View(costCenterAnaVM);
        }

        [HttpPost]
        public IActionResult Create(CostCenterAnaVM costCenterAnaVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.CostCenterAna.Add(costCenterAnaVM.CostCenterAna);
                    _unitOfWork.Save();

                    TempData["success"] = "Cost Center Ana created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating Cost Center Ana");
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the Cost Center Ana.");
                }
            }

            // Repopulate dropdowns
            costCenterAnaVM.CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
            {
                Text = c.COMTEXT,
                Value = c.COMCODE
            });
            costCenterAnaVM.CostCenterList = _unitOfWork.CostCenter.GetAll().Select(cc => new SelectListItem
            {
                Text = cc.CCMDOCNUM,
                Value = cc.CCMDOCTYPE
            });
            costCenterAnaVM.LanguageList = _unitOfWork.Language.GetAll().Select(l => new SelectListItem
            {
                Text = l.LANTEXT,
                Value = l.LANCODE
            });

            return View(costCenterAnaVM);
        }

        public IActionResult Edit(string comCode, string ccmDocType, string ccmDocNum)
        {
            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(ccmDocType) || string.IsNullOrEmpty(ccmDocNum))
            {
                return NotFound();
            }

            var costCenterAna = _unitOfWork.CostCenterAna.Get(u =>
                u.COMCODE == comCode &&
                u.CCMDOCTYPE == ccmDocType &&
                u.CCMDOCNUM == ccmDocNum);

            if (costCenterAna == null)
            {
                return NotFound();
            }

            var costCenterAnaVM = new CostCenterAnaVM
            {
                CostCenterAna = costCenterAna,
                CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
                {
                    Text = c.COMTEXT,
                    Value = c.COMCODE
                }),
                CostCenterList = _unitOfWork.CostCenter.GetAll().Select(cc => new SelectListItem
                {
                    Text = cc.CCMDOCNUM,
                    Value = cc.CCMDOCTYPE
                }),
                LanguageList = _unitOfWork.Language.GetAll().Select(l => new SelectListItem
                {
                    Text = l.LANTEXT,
                    Value = l.LANCODE
                })
            };

            return View(costCenterAnaVM);
        }

        [HttpPost]
        public IActionResult Edit(CostCenterAnaVM costCenterAnaVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.CostCenterAna.Update(costCenterAnaVM.CostCenterAna);
                    _unitOfWork.Save();

                    TempData["success"] = "Cost Center updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating Cost Center ");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the Cost Center.");
                }
            }

            // Repopulate dropdowns
            costCenterAnaVM.CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
            {
                Text = c.COMTEXT,
                Value = c.COMCODE
            });
            costCenterAnaVM.CostCenterList = _unitOfWork.CostCenter.GetAll().Select(cc => new SelectListItem
            {
                Text = cc.CCMDOCNUM,
                Value = cc.CCMDOCTYPE
            });
            costCenterAnaVM.LanguageList = _unitOfWork.Language.GetAll().Select(l => new SelectListItem
            {
                Text = l.LANTEXT,
                Value = l.LANCODE
            });

            return View(costCenterAnaVM);
        }

        public IActionResult Delete(string comCode, string ccmDocType, string ccmDocNum)
        {
            var costCenterAna = _unitOfWork.CostCenterAna.Get(u =>
                u.COMCODE == comCode &&
                u.CCMDOCTYPE == ccmDocType &&
                u.CCMDOCNUM == ccmDocNum);

            if (costCenterAna == null)
            {
                return NotFound();
            }

            return View(costCenterAna);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string comCode, string ccmDocType, string ccmDocNum)
        {
            var costCenterAna = _unitOfWork.CostCenterAna.Get(u =>
                u.COMCODE == comCode &&
                u.CCMDOCTYPE == ccmDocType &&
                u.CCMDOCNUM == ccmDocNum);

            if (costCenterAna == null)
            {
                return NotFound();
            }

            _unitOfWork.CostCenterAna.Remove(costCenterAna);
            _unitOfWork.Save();

            TempData["success"] = "Cost Center deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
