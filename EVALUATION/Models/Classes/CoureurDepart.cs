using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

[Table("coureur_depart")]
public class CoureurDepart
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }
    [Column("id_etape")]
    public string? IdEtape { get; set; }
    [Column("heure_depart")]
    public DateTime HeureDepart { get; set; }
    
}
