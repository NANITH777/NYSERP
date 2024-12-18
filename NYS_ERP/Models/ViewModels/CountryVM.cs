using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NYS_ERP.Models.ViewModels
{
    public class CountryVM
    {
        public Country Country { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
    }
}
