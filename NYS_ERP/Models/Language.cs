using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NYS_ERP.Models
{
    public class Language
    {
        [Key]
        [StringLength(3)]
        public string LANCODE { get; set; }

        [StringLength(80)]
        public string LANTEXT { get; set; }

        [StringLength(4)]
        public string COMCODE { get; set; } 

        [ForeignKey("COMCODE")]
        [ValidateNever] 
        public Company Company { get; set; }
        [Timestamp]
        [Required]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    }
}
