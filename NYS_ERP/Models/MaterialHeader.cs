using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NYS_ERP.Models
{
    //public class MaterialHeader
    //{
    //    // Clés composées
    //    [Key]
    //    [Column("COMCODE")]
    //    [StringLength(4)]
    //    public string COMCODE { get; set; }

    //    [Key]
    //    [Column("MATDOCTYPE")]
    //    [StringLength(4)]
    //    //[ForeignKey("MaterialType")] 
    //    public string DOCTYPE { get; set; }

    //    [Key]
    //    [Column("MATDOCNUM")]
    //    [StringLength(25)]
    //    //[ForeignKey("MaterialType")]
    //    public string DOCTYPETEXT { get; set; }

    //    [Key]
    //    [Column("MATDOCFROMDATE")]
    //    [DataType(DataType.Date)]
    //    public DateTime ValidityStart { get; set; }

    //    [Key]
    //    [Column("MATDOCUNTILDATE")]
    //    [DataType(DataType.Date)]
    //    public DateTime ValidityEnd { get; set; }

    //    // Relations
    //    [ForeignKey("COMCODE")]
    //    [ValidateNever]
    //    public Company Company { get; set; }


    //    [ForeignKey("DOCTYPE, DOCTYPETEXT")] 
    //    [ValidateNever]
    //    public MaterialType MaterialType { get; set; }

    //    public ICollection<MaterialText> Translations { get; set; }

    //    // Autres propriétés
    //    [Column("SUPPLYTYPE")]
    //    public int SupplyType { get; set; }

    //    [Column("STUNIT")]
    //    [StringLength(3)]
    //    public string StockUnit { get; set; }

    //    [Column("NETWEIGHT", TypeName = "decimal(12,3)")]
    //    public decimal NetWeight { get; set; }

    //    [Column("NWUNIT")]
    //    [StringLength(3)]
    //    public string NetWeightUnit { get; set; }

    //    [Column("BRUTWEIGHT", TypeName = "decimal(12,3)")]
    //    public decimal GrossWeight { get; set; }

    //    [Column("BWUNIT")]
    //    [StringLength(3)]
    //    public string GrossWeightUnit { get; set; }

    //    [Column("ISBOM")]
    //    public int HasBillOfMaterials { get; set; }

    //    [Column("BOMDOCTYPE")]
    //    [StringLength(4)]
    //    public string BOMType { get; set; }

    //    [Column("BOMDOCNUM")]
    //    [StringLength(25)]
    //    public string BOMCode { get; set; }

    //    [Column("ISROUTE")]
    //    public int HasRoute { get; set; }

    //    [Column("ROTDOCTYPE")]
    //    [StringLength(4)]
    //    public string RouteType { get; set; }

    //    [Column("ROTDOCNUM")]
    //    [StringLength(25)]
    //    public string RouteCode { get; set; }

    //    [Column("ISDELETED")]
    //    public int IsDeleted { get; set; }

    //    [Column("ISPASSIVE")]
    //    public int IsPassive { get; set; }

    //    [Timestamp]
    //    [Required]
    //    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    //}

    //public class MaterialText
    //{
    //    [Key]
    //    [Column("COMCODE")]
    //    [StringLength(4)]
    //    public string CompanyCode { get; set; }

    //    [Key]
    //    [Column("MATDOCTYPE")]
    //    [StringLength(4)]
    //    public string MaterialDocType { get; set; }

    //    [Key]
    //    [Column("MATDOCNUM")]
    //    [StringLength(25)]
    //    public string MaterialCode { get; set; }

    //    [Key]
    //    [Column("MATDOCFROMDATE")]
    //    [DataType(DataType.Date)]
    //    public DateTime ValidityStart { get; set; }

    //    [Key]
    //    [Column("MATDOCUNTILDATE")]
    //    [DataType(DataType.Date)]
    //    public DateTime ValidityEnd { get; set; }

    //    [Key]
    //    [Column("LANCODE")]
    //    [StringLength(2)]
    //    public string LanguageCode { get; set; }

    //    [Column("MATSTEXT")]
    //    [StringLength(50)]
    //    public string ShortDescription { get; set; }

    //    [Column("MATLTEXT")]
    //    [StringLength(250)]
    //    public string LongDescription { get; set; }

    //    // Navigation property vers MaterialHeader
    //    [ForeignKey("COMCODE,DOCTYPE,DOCTYPETEXT,ValidityStart,ValidityEnd")]
    //    [ValidateNever]
    //    public MaterialHeader MaterialHeader { get; set; }

    //    [Timestamp]
    //    [Required]
    //    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    //}
}
