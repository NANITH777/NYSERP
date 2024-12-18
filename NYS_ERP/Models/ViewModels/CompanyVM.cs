using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NYS_ERP.Models.ViewModels
{
    public class CompanyVM
    {
        public Company Company { get; set; }
        //// Company properties
        //[Key]
        //[StringLength(4, ErrorMessage = "Company Code must be 4 characters or less")]
        //[Required(ErrorMessage = "Company Code is required")]
        //public string COMCODE { get; set; }

        //[StringLength(80, ErrorMessage = "Company Name must be 80 characters or less")]
        //[Required(ErrorMessage = "Company Name is required")]
        //public string COMTEXT { get; set; }

        //[StringLength(80)]
        //public string ADDRESS1 { get; set; }

        //[StringLength(80)]
        //public string ADDRESS2 { get; set; }

        //// Dropdown for Countries
        //[ValidateNever]
        //public IEnumerable<SelectListItem> CountryList { get; set; }

        //// Selected Country
        //[StringLength(3)]
        //public string SelectedCountryCode { get; set; }

        //// Dropdown for Cities
        //[ValidateNever]
        //public IEnumerable<SelectListItem> CityList { get; set; }

        //// Selected City
        //[StringLength(3)]
        //public string SelectedCityCode { get; set; }

        //// Constructor to initialize lists
        //public CompanyVM()
        //{
        //    CountryList = new List<SelectListItem>();
        //    CityList = new List<SelectListItem>();
        //}

        //// Method to populate Country dropdown
        //public void PopulateCountryList(IEnumerable<Country> countries)
        //{
        //    CountryList = countries.Select(c => new SelectListItem
        //    {
        //        Value = c.COUNTRYCODE,
        //        Text = c.COUNTRYTEXT
        //    }).ToList();
        //}

        //// Method to populate City dropdown
        //public void PopulateCityList(IEnumerable<City> cities)
        //{
        //    CityList = cities.Select(c => new SelectListItem
        //    {
        //        Value = c.CITYCODE,
        //        Text = c.CITYTEXT
        //    }).ToList();
        //}

        //// Mapping method from Company to CompanyVM
        //public static CompanyVM FromCompany(Company company)
        //{
        //    return new CompanyVM
        //    {
        //        COMCODE = company.COMCODE,
        //        COMTEXT = company.COMTEXT,
        //        ADDRESS1 = company.ADDRESS1,
        //        ADDRESS2 = company.ADDRESS2
        //    };
        //}

        //// Mapping method from CompanyVM to Company
        //public Company ToCompany()
        //{
        //    return new Company
        //    {
        //        COMCODE = this.COMCODE,
        //        COMTEXT = this.COMTEXT,
        //        ADDRESS1 = this.ADDRESS1,
        //        ADDRESS2 = this.ADDRESS2
        //    };
        //}
    }
}