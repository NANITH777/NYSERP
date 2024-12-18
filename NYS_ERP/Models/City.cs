using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NYS_ERP.Models
{
    public class City
    {
        [Key]
        [StringLength(3)]
        public string CITYCODE { get; set; }

        [StringLength(80)]
        public string CITYTEXT { get; set; }

        [StringLength(4)]
        public string COMCODE { get; set; }

        [ForeignKey("COMCODE")]
        [ValidateNever]
        public Company Company { get; set; }

        [StringLength(3)]
        public string COUNTRYCODE { get; set; }

        [ForeignKey("COUNTRYCODE")]
        [ValidateNever]
        public Country Country { get; set; }
        [Timestamp]
        [Required]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
