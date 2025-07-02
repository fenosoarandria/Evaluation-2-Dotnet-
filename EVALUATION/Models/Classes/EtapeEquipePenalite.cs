using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("etape_equipe_penalite")]
public class EtapeEquipePenalite
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("id_etape")]
    public string? IdEtape { get; set; }
    [Column("id_equipe")]
    public string? IdEquipe { get; set; }
    [Column("temps_penalite")]
    public TimeSpan TempsPenalite { get; set; }
   
    [ForeignKey("IdEtape")]
    public virtual Etape? Etapes {get;set;}
    [ForeignKey("IdEquipe")]
    public virtual Equipe? Equipes {get;set;}

}
