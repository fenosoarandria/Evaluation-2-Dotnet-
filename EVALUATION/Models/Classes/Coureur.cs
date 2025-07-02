using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

[Table("coureur")]
public class Coureur
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("nom")]
    public string? Nom { get; set; }

    [Column("numero_dossard")]
    public int? NumeroDossard { get; set; }

    [Column("date_de_naissance")]
    public DateTime? DateDeNaissance { get; set; }

    [Column("id_genre")]
    public string? IdGenre { get; set; }

    [Column("id_equipe")]
    public string? IdEquipe { get; set; }
    
    [ForeignKey("IdGenre")]
    public virtual Genre? Genre{get;set;}
    public virtual ICollection<EtapeCoureur>? EtapeCoureurs { get; set; }
    [NotMapped] // Indique à Entity Framework de ne pas mapper cette propriété à la base de données
    public int NumberOfRunners { get; set; } 

}
