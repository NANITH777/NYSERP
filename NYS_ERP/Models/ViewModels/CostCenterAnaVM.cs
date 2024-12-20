using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NYS_ERP.Models.ViewModels
{
    public class CostCenterAnaVM
    {
        public CostCenterAna CostCenterAna { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CostCenterList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> LanguageList { get; set; }
    }
}
