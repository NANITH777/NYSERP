using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NYS_ERP.Models;
using NYS_ERP.Models.ViewModels;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Controllers
{
    public class OperationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OperationController> _logger;
        public OperationController(IUnitOfWork unitOfWork, ILogger<OperationController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<Operation> objUnitList = _unitOfWork.Operation
           .GetAll(includeProperties: "Company").ToList();
            return View(objUnitList);
        }
        public IActionResult Create()
        {
            OperationVM operationVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                }),
                Operation = new Operation()
            };

            operationVM.Operation.OPRDOCTYPE = GenerateUniqueUnitCode();

            return View(operationVM);
        }

        [HttpPost]
        public IActionResult Create(OperationVM operationVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingOperation = _unitOfWork.Operation.Get(u => u.OPRDOCTYPE == operationVM.Operation.OPRDOCTYPE);
                    if (existingOperation != null)
                    {
                        ModelState.AddModelError("Operation.DOCTYPE", "An operation with this code already exists.");

                        operationVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                        {
                            Text = a.COMTEXT,
                            Value = a.COMCODE
                        });
                        return View(operationVM);
                    }

                    if (string.IsNullOrEmpty(operationVM.Operation.OPRDOCTYPE))
                    {
                        operationVM.Operation.OPRDOCTYPE = GenerateUniqueUnitCode();
                    }

                    _unitOfWork.Operation.Add(operationVM.Operation);
                    _unitOfWork.Save();

                    TempData["success"] = "Operation created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating the operation");
                    ModelState.AddModelError(string.Empty, $"An error occurred while creating the operation: {ex.Message}");
                }
            }

            operationVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });

            return View(operationVM);
        }


        public IActionResult Edit(string? opCode)
        {
            if (string.IsNullOrEmpty(opCode))
            {
                return NotFound();
            }

            OperationVM operationVM = new()
            {
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            operationVM.Operation = _unitOfWork.Operation.Get(u => u.OPRDOCTYPE == opCode);
            if (operationVM.Operation == null)
            {
                return NotFound();
            }
            return View(operationVM);
        }

        [HttpPost]
        public IActionResult Edit(OperationVM operationVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var operationFromDb = _unitOfWork.Operation.Get(u => u.OPRDOCTYPE == operationVM.Operation.OPRDOCTYPE);
                    if (operationFromDb == null)
                    {
                        return NotFound();
                    }

                    operationFromDb.OPRDOCNUM = operationVM.Operation.OPRDOCNUM;
                    operationFromDb.ISPASSIVE = operationVM.Operation.ISPASSIVE;

                    _unitOfWork.Operation.Update(operationFromDb);
                    _unitOfWork.Save();

                    TempData["success"] = "Operation updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating the operation");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the operation.");
                }
            }

            operationVM.CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
            {
                Text = a.COMTEXT,
                Value = a.COMCODE
            });
            return View(operationVM);
        }

        private string GenerateUniqueUnitCode()
        {
            var lastOperation = _unitOfWork.Operation.GetAll()
                .OrderByDescending(l => l.OPRDOCTYPE)
                .FirstOrDefault();

            if (lastOperation == null)
            {
                return "OP01";
            }

            string numericPart = lastOperation.OPRDOCTYPE.Substring(2);
            int nextNumber = int.Parse(numericPart) + 1;

            return $"OP{nextNumber:D2}";
        }

        public IActionResult Delete(string opCode)
        {
            var operationVM = new OperationVM
            {
                Operation = _unitOfWork.Operation.Get(u => u.OPRDOCTYPE == opCode, includeProperties: "Company"),
                CompanyList = _unitOfWork.Company.GetAll().Select(a => new SelectListItem
                {
                    Text = a.COMTEXT,
                    Value = a.COMCODE
                })
            };

            if (operationVM.Operation == null)
            {
                return NotFound();
            }

            return View(operationVM);
        }

        [HttpPost]
        public IActionResult Delete(OperationVM operationVM)
        {
            var opToDelete = _unitOfWork.Operation.Get(u => u.OPRDOCTYPE == operationVM.Operation.OPRDOCTYPE);

            if (opToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.Operation.Remove(opToDelete);
            _unitOfWork.Save();

            TempData["success"] = "Operation Type deleted successfully!";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Operation> objMTList = _unitOfWork.Operation
            .GetAll(includeProperties: "Company")
            .ToList();

            var formattedData = objMTList.Select(u => new
            {
                opCode = u.OPRDOCTYPE,  
                opText = u.OPRDOCNUM,
                isPassive = u.ISPASSIVE,
                companyText = u.Company.COMCODE  
            }).ToList();

            return Json(new { data = formattedData });
        }

        #endregion
    }
}
