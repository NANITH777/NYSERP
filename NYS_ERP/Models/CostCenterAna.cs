using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NYS_ERP.Models
{
    public class CostCenterAna
    {
        // Composite Primary Key fields
        [Key]
        public string COMCODE { get; set; }

        [Key]
        public string CCMDOCTYPE { get; set; }

        [Key]
        [StringLength(80)]
        public string CCMDOCNUM { get; set; }

        [Key]
        [Column(TypeName = "DATE")]
        public DateTime CCMDOCFROM { get; set; }

        [Key]
        [Column(TypeName = "DATE")]
        public DateTime CCMDOCUNTIL { get; set; }

        [Key]
        public string LANCODE { get; set; }

        // Navigation properties and Foreign Keys
        [ForeignKey("COMCODE")]
        [ValidateNever]
        public Company Company { get; set; }

        [ForeignKey("CCMDOCTYPE")]  
        [ValidateNever]
        public CostCenter CostCenter { get; set; }

        [ForeignKey("LANCODE")]
        [ValidateNever]
        public Language Language { get; set; }

        // Regular fields
        [Column(TypeName = "VARCHAR")]
        [StringLength(4)]
        public string MAINCCMDOCTYPE { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(25)]
        public string MAINCCMDOCNUM { get; set; }

        public bool ISDELETED { get; set; }

        public bool ISPASSIVE { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CCMSTEXT { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string CCMLTEXT { get; set; }

        [Timestamp]
        [Required]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }

}
