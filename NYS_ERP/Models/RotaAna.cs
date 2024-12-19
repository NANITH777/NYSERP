using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NYS_ERP.Models
{
    public class RotaAna
    {
        //[StringLength(4)]
        //[Required]
        //public string COMCODE { get; set; }

        //[ForeignKey("COMCODE")]
        //[ValidateNever]
        //public Company Company { get; set; }

        //[ForeignKey("Rota")]
        //[ValidateNever]
        //public Rota Rota { get; set; } 

        //[StringLength(4)]
        //[Required]
        //public string ROTDOCTYPE { get; set; } // Ürün Ağacı/Maliyet Merkezi Tipi

        //[StringLength(25)]
        //[Required]
        //public string ROTDOCNUM { get; set; } // Ürün Ağacı/Maliyet Merkezi Kodu

        //[Required]
        //public DateTime ROTDOCFROM { get; set; } // Geçerlilik Başlangıç

        //[Required]
        //public DateTime ROTDOCUNTIL { get; set; } // Geçerlilik Bitiş

        //[ForeignKey("MaterialType")]
        //[ValidateNever]
        //public MaterialType MaterialType { get; set; } 

        //[StringLength(4)]
        //[Required]
        //public string MATDOCTYPE { get; set; } // Malzeme Tipi

        //[StringLength(25)]
        //[Required]
        //public string MATDOCNUM { get; set; } // Malzeme Kodu

        //[Range(0, 99999.99)]
        //public decimal? QUANTITY { get; set; } // Temel/Bileşen Miktar

        //public bool? ISDELETED { get; set; } // Silindi? (0: Hayır, 1: Evet)

        //public bool? ISPASSIVE { get; set; } // Pasif mi? (0: Hayır, 1: Evet)

        //[StringLength(25)]
        //public string DRAWNUM { get; set; } // Çizim Numarası

        //// Specific to Operation Content
        //[Required]
        //public int? OPRNUM { get; set; } // Operasyon Numarası

        //[ForeignKey("WorkCenter")]
        //[ValidateNever]
        //public WorkCenter WorkCenter { get; set; } // Relation avec la table WorkCenter

        //[StringLength(4)]
        //public string WCMDOCTYPE { get; set; } // İş Merkezi Tipi

        //[StringLength(25)]
        //public string WCMDOCNUM { get; set; } // İş Merkezi Kodu

        //[StringLength(4)]
        //public string OPRDOCTYPE { get; set; } // Operasyon Kodu

        //[Range(0, 99.99)]
        //public decimal? SETUPTIME { get; set; } // Operasyon Hazırlık Süresi(Saat)

        //[Range(0, 99.99)]
        //public decimal? MACHINETIME { get; set; } // Operasyon Makine Süresi(Saat)

        //[Range(0, 99.99)]
        //public decimal? LABOURTIME { get; set; } // Operasyon İşçilik Süresi(Saat)

        //// Specific to BOM Content
        //[ForeignKey("BOM")]
        //[ValidateNever]
        //public BOM BOM { get; set; } 

        //[StringLength(4)]
        //[Required]
        //public string BOMDOCTYPE { get; set; } // Ürün Ağacı Tipi

        //[StringLength(25)]
        //[Required]
        //public string BOMDOCNUM { get; set; } // Ürün Ağacı Kodu

        //[Required]
        //public int? CONTENTNUM { get; set; } // İçerik Numarası

        //[StringLength(25)]
        //public string COMPONENT { get; set; } // Bileşen Kodu

        //[Range(0, 99999.99)]
        //public decimal? COMPONENTQUANTITY { get; set; } // Bileşen Miktarı (Quantité de composant)

        //[Timestamp]
        //[Required]
        //public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    }
}
