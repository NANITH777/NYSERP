using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NYS_ERP.Models.ViewModels
{
    public class WorkCenterAnaVM
    {
        public WorkCenterAna WorkCenterAna { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> WorkCenterList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> LanguageList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CostCenterList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> OperationList { get; set; }
    }
}
