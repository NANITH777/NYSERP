using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NYS_ERP.Models
{
    public class MaterialType
    {
        [Key]
        [StringLength(4)]
        public string MATDOCTYPE { get; set; }

        [StringLength(80)]
        public string MATDOCNUM { get; set; }

        public int ISPASSIVE { get; set; }
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
