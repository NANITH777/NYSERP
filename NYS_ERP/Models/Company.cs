using System.ComponentModel.DataAnnotations;

namespace NYS_ERP.Models
{
    public class Company
    {
        [Key]
        [StringLength(4)]
        public string COMCODE { get; set; }

        [StringLength(80)]
        public string COMTEXT { get; set; }

        [StringLength(80)]
        public string ADDRESS1 { get; set; }

        [StringLength(80)]
        public string ADDRESS2 { get; set; }


    }
}
