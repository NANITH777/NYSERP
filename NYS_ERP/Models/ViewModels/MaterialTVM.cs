using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NYS_ERP.Models.ViewModels
{
    public class MaterialTVM
    {
        public MaterialType MaterialType { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }

    }
}
