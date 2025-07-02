using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

[Table("etape_coureur")]
public class EtapeCoureur
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("id_etape")]
    public string? IdEtape { get; set; }

    [Column("id_coureur")]
    public string? IdCoureur { get; set; }
    // Propriété calculée pour obtenir le total des coureurs associés à cette étape
    [ForeignKey("IdEtape")]
    public virtual Etape? Etape{get;set;}
    
    [ForeignKey("IdCoureur")]
    public virtual Coureur? Coureur{get;set;}
}
