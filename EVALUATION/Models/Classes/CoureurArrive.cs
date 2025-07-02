using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

[Table("coureur_arrive")]
public class CoureurArrive
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }
    [Column("id_etape")]
    public string? IdEtape { get; set; }
    [Column("id_coureur")]
    public string? IdCoureur { get; set; }
    [Column("heure_arrive")]
    public DateTime HeureArrive { get; set; }
    
}
