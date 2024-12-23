using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NYS_ERP.Models
{
    public class Material
    {
        [Key]
        [Required]
        public string COMCODE { get; set; }
        [Key]
        [Required]
        public string MATDOCTYPE { get; set; }
        [Key]
        [Required]
        public string MATDOCNUM { get; set; }
        [Key]
        [Required]
        public DateTime MATDOCFROM { get; set; }
        [Key]
        [Required]
        public DateTime MATDOCUNTIL { get; set; }
        [Key]
        [Required]
        public string LANCODE { get; set; }

        [ForeignKey("COMCODE")]
        [ValidateNever]
        public Company Company { get; set; }

        [ForeignKey("MATDOCTYPE")]
        [ValidateNever]
        public MaterialType MaterialType { get; set; }

        [ForeignKey("LANCODE")]
        [ValidateNever]
        public Language Language { get; set; }

        [ForeignKey("BOMDOCTYPE")]
        [ValidateNever]
        public BOM BOM { get; set; }

        [ForeignKey("ROTDOCTYPE")]
        [ValidateNever]
        public Rota Rota { get; set; }

        [Range(0, 1)]
        public int SUPPLYTYPE { get; set; }
        [StringLength(3)]
        [Required]
        public string STUNIT { get; set; }
        [Column(TypeName = "decimal(12,3)")]
        public decimal? NETWEIGHT { get; set; }
        [StringLength(3)]
        public string? NWUNIT { get; set; }
        [Column(TypeName = "decimal(12,3)")]
        public decimal? BRUTWEIGHT { get; set; }
        [StringLength(3)]
        public string? BWUNIT { get; set; }
        public bool ISBOM { get; set; }  

        public string BOMDOCTYPE { get; set; }  
        public string BOMDOCNUM { get; set; }
        public bool ISROUTE { get; set; }  
        public string ROTDOCTYPE { get; set; }  
        public string ROTDOCNUM { get; set; }
        public bool ISDELETED { get; set; }
        public bool ISPASSIVE { get; set; }
        [Required]
        [StringLength(50)]
        public string MATSTEXT { get; set; }
        [Required]
        [StringLength(250)]
        public string MATLTEXT { get; set; }
        [Timestamp]
        [Required]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
