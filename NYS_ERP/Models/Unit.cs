using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NYS_ERP.Models
{
    public class Unit
    {       
        [Key]
        [StringLength(3)]
        public string UNITCODE { get; set; }

        [StringLength(80)]
        public string UNITTEXT { get; set; }
        public int ISMAINUNIT { get; set; }

        [StringLength(3)]
        public string MAINUNITCODE { get; set; }

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
