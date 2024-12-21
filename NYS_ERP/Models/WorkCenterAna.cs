using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NYS_ERP.Models
{
    public class WorkCenterAna
    {
        [Key]
        [Required()]
        public string COMCODE { get; set; }
        [Key]
        [Required]
        public string WCMDOCTYPE { get; set; }
        [Key]
        [Required]
        [StringLength(25)]
        public string WCMDOCNUM { get; set; }
        [Key]
        [Required]
        public DateTime WCMDOCFROM { get; set; }
        [Key]
        [Required]
        public DateTime WCMDOCUNTIL { get; set; }

        [StringLength(4)]
        public string MAINWCMDOCTYPE { get; set; }

        [StringLength(25)]
        public string MAINWCMDOCNUM { get; set; }

        [StringLength(4)]
        public string CCMDOCTYPE { get; set; }

        [StringLength(25)]
        public string CCMDOCNUM { get; set; }

        [Range(0, 24)]
        public decimal WORKTIME { get; set; }

        [Range(0, 1)]
        public int ISDELETED { get; set; }

        [Range(0, 1)]
        public int ISPASSIVE { get; set; }

        [Key]
        [Required]
        public string LANCODE { get; set; }

        [StringLength(50)]
        public string WCMSTEXT { get; set; }

        [StringLength(250)]
        public string WCMLTEXT { get; set; }

        [Key]
        public string OPRDOCTYPE { get; set; }
        [Timestamp]
        [Required]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        [ForeignKey("COMCODE")]
        [ValidateNever]
        public Company Company { get; set; }

        [ForeignKey("WCMDOCTYPE")]
        [ValidateNever]
        public WorkCenter WorkCenter { get; set; }

        [ForeignKey("CCMDOCTYPE")]
        [ValidateNever]
        public CostCenter CostCenter { get; set; }

        [ForeignKey("OPRDOCTYPE")]
        [ValidateNever]
        public Operation Operation { get; set; }

        [ForeignKey("LANCODE")]
        [ValidateNever]
        public Language Language { get; set; }
    }
}
