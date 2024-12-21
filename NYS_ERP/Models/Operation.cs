using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NYS_ERP.Models
{
    public class Operation
    {
        [Key]
        [Required]
        [MaxLength(4)]
        public string OPRDOCTYPE { get; set; }

        [MaxLength(80)]
        public string OPRDOCNUM { get; set; }

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
