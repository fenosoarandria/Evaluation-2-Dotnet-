using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("classement_general_view")]
public class ClassementGeneralView
{
    [ForeignKey("Coureur")]
    [Column("id_etape")]
    public string? IdEtape { get; set; }

    [ForeignKey("Coureur")]
    [Column("id_coureur")]
    public string? IdCoureur { get; set; }

    [ForeignKey("Equipe")]
    [Column("id_equipe")]
    public string? IdEquipe { get; set; }
    
    [Column("equipe")]
    public string? Equipe { get; set; }

    [Column("heure_depart")]
    public DateTime HeureDepart { get; set; }

    [Column("heure_arrive")]
    public DateTime HeureArrive { get; set; }
    [Column("temps_penalite")]
    public TimeSpan TempsPenalite { get; set; }
    
    [Column("duree_totale")]
    public TimeSpan Duree { get; set; }
    [Column("rang")]
    public int Rang { get; set; }
    [Column("point")]
    public int Point { get; set; }

    [Column("coureur")]
    public string? NomCoureur { get; set; }

    [Column("numero_dossard")]
    public int NumeroDossard { get; set; }

    [Column("date_de_naissance")]
    public DateTime DateDeNaissance { get; set; }

    [ForeignKey("Genre")]
    [Column("id_genre")]
    public string? IdGenre { get; set; }

    [Column("genre")]
    public string? Genre { get; set; }

    [Column("etape")]
    public string? Etape { get; set; }
    
    [Column("longueur")]
    public double? Longueur { get; set; }
    
    [Column("nombre_coureur_equipe")]
    public int? NombreCoureurEquipe { get; set; }
    
    [Column("rang_etape")]
    public int? RangEtape { get; set; }
    
    [ForeignKey("IdCoureur")] // Indique que la clé étrangère est IdCoureur
    public virtual Coureur? Coureurs { get; set; } // Propriété de navigation vers Coureur

    [ForeignKey("IdGenre")] // Indique que la clé étrangère est IdCoureur
    public virtual Genre? Genres { get; set; } // Propriété de navigation vers Coureur
    [ForeignKey("IdEtape")] // Indique que la clé étrangère est IdCoureur
    public virtual Etape? Etapes { get; set; } // Propriété de navigation vers Coureur
    [ForeignKey("IdEquipe")] // Indique que la clé étrangère est IdCoureur
    public virtual Equipe? Equipes { get; set; } // Propriété de navigation vers Coureur
}
