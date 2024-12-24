using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NYS_ERP.Models
{
    public class RotaAna
    {
        [Key]
        public string COMCODE { get; set; }

        [Key]
        public string ROTDOCTYPE { get; set; }

        [Key]
        public string ROTDOCNUM { get; set; } 

        [Key]
        [Column(TypeName = "DATE")]
        public DateTime ROTDOCFROM { get; set; }

        [Key]
        [Column(TypeName = "DATE")]
        public DateTime ROTDOCUNTIL { get; set; }

        [Key]
        public string MATDOCTYPE { get; set; } 

        [Key]
        public string MATDOCNUM { get; set; } 

        [Required]
        [Range(0, 99999.99)]
        public decimal QUANTITY { get; set; } 

        public bool ISDELETED { get; set; } 

        public bool ISPASSIVE { get; set; } 

        [StringLength(25)]
        public string? DRAWNUM { get; set; } 

        [Key]
        [Range(0, 9999)]
        public int OPRNUM { get; set; } 

        public string WCMDOCTYPE { get; set; } 

        public string WCMDOCNUM { get; set; } 

        public string OPRDOCTYPE { get; set; } 

        [Range(0, 999.99)]
        public decimal? SETUPTIME { get; set; } 

        [Range(0, 999.99)]
        public decimal? MACHINETIME { get; set; } 

        [Range(0, 999.99)]
        public decimal? LABOURTIME { get; set; } 

        [Key]
        public string BOMDOCTYPE { get; set; } 

        [Key]
        public string BOMDOCNUM { get; set; } 

        [Key]
        [Range(1, 9999)]
        public int CONTENTNUM { get; set; } 

        [StringLength(25)]
        public string COMPONENT { get; set; } 

        [Required]
        [Range(0, 99999.99)]
        public decimal? COMPONENT_QUANTITY { get; set; } 

        [Timestamp]
        [Required]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        [ForeignKey("COMCODE")]
        [ValidateNever]
        public Company Company { get; set; }

        [ForeignKey("ROTDOCTYPE")]
        [ValidateNever]
        public Rota Rota { get; set; }

        [ForeignKey("BOMDOCTYPE")]
        [ValidateNever]
        public BOM BOM { get; set; }

        [ForeignKey("MATDOCTYPE")]
        [ValidateNever]
        public MaterialType MaterialType { get; set; }
        [ForeignKey("WCMDOCTYPE")]
        [ValidateNever]
        public WorkCenter WorkCenter { get; set; }

        [ForeignKey("OPRDOCTYPE")]
        [ValidateNever]
        public Operation Operation { get; set; }

    }
}
