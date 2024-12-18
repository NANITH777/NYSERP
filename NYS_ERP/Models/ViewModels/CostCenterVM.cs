using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NYS_ERP.Models.ViewModels
{
    public class CostCenterVM
    {
        public CostCenter CostCenter { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }

    }
}
