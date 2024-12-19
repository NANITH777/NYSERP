using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NYS_ERP.Models
{
    public class BOM
    {
        
        [Key]
        [Required]
        [MaxLength(4)]
        public string BOMDOCTYPE { get; set; }

        [MaxLength(80)]
        public string BOMDOCNUM { get; set; }

        public int? ISPASSIVE { get; set; }

        public string COMCODE { get; set; }

        [ForeignKey("COMCODE")]
        [ValidateNever]
        public Company Company { get; set; }
        [Timestamp]
        [Required]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    }
}
