using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

[Table("temps_coureur")]
public class TempsCoureur
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("id_etape")]
    public string? IdEtape { get; set; }

    [Column("id_coureur")]
    public string? IdCoureur { get; set; }

    [Column("heure_depart")]
    public DateTime? HeureDepart { get; set; }

    [Column("heure_arrive")]
    public DateTime? HeureArrive { get; set; }

}
