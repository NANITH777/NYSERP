using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NYS_ERP.Models.ViewModels
{
    public class MaterialVM
    {
        public Material Material { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> MaterialTypeList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> LanguageList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> BOMList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RotaList { get; set; }
    }
}
