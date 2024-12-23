using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NYS_ERP.Models
{
    public class BOMAna
    {
        [Key]
        public string COMCODE { get; set; } 

        [Key]
        public string BOMDOCTYPE { get; set; } 

        [Key]
        public string BOMDOCNUM { get; set; } 

        [Key]
        [Column(TypeName = "DATE")]
        public DateTime BOMDOCFROM { get; set; }

        [Key]
        [Column(TypeName = "DATE")]
        public DateTime BOMDOCUNTIL { get; set; } 

        [Key]
        public string MATDOCTYPE { get; set; } 
        [Key]
        [StringLength(25)]
        public string MATDOCNUM { get; set; }

        [Required]
        [Range(0, 99999.99)]
        public decimal QUANTITY { get; set; } 

        public bool ISDELETED { get; set; } 

        public bool ISPASSIVE { get; set; }

        [StringLength(25)]
        public string? DRAWNUM { get; set; }

        [Key]
        [Required]
        [Range(1, 9999)]
        public int CONTENTNUM { get; set; } 

        [StringLength(25)]
        public string COMPONENT { get; set; }
        [Required]
        [StringLength(4)]
        public string COMPBOMDOCTYPE { get; set; } 

        [Required]
        [StringLength(25)]
        public string COMPBOMDOCNUM { get; set; }

        [Required]
        [Range(0, 99999.99)]
        public decimal? COMPONENT_QUANTITY { get; set; }
        [Timestamp]
        [Required]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        [ForeignKey("COMCODE")]
        [ValidateNever]
        public Company Company { get; set; }

        [ForeignKey("BOMDOCTYPE")]
        [ValidateNever]
        public BOM BOM { get; set; }

        [ForeignKey("MATDOCTYPE")]
        [ValidateNever]
        public MaterialType MaterialType { get; set; }
    }
}
