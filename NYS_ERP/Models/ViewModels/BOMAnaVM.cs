using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NYS_ERP.Models.ViewModels
{
    public class BOMAnaVM
    {
        public BOMAna BOMAna { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> BOMList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MaterialTypeList { get; set; }
    }
}
