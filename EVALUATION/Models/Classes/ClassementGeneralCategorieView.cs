using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("classement_general_categorie_view")]
public class ClassementGeneralCategorieView
{

    [Column("id_etape")]
    public string? IdEtape { get; set; }

    [Column("id_coureur")]
    public string? IdCoureur { get; set; }

    [Column("id_equipe")]
    public string? IdEquipe { get; set; }

    [Column("id_categorie")]
    public string? IdCategorie { get; set; }

    [Column("nom_categorie")]
    public string? NomCategorie { get; set; }

    [Column("heure_depart")]
    public DateTime HeureDepart { get; set; }

    [Column("heure_arrive")]
    public DateTime HeureArrive { get; set; }

    [Column("duree")]
    public TimeSpan Duree { get; set; }

    [Column("rang")]
    public int Rang { get; set; }

    [Column("point")]
    public int Point { get; set; }

    [Column("etape")]
    public string? Etape { get; set; }

    [Column("longueur")]
    public double Longueur { get; set; }

    [Column("nombre_coureur_equipe")]
    public int NombreCoureurEquipe { get; set; }

    [Column("rang_etape")]
    public int RangEtape { get; set; }

    [Column("coureur")]
    public string? Coureur { get; set; }

    [Column("numero_dossard")]
    public int NumeroDossard { get; set; }

    [Column("date_de_naissance")]
    public DateTime DateDeNaissance { get; set; }

    [Column("id_genre")]
    public int IdGenre { get; set; }

    [Column("genre")]
    public string? Genre { get; set; }

    [Column("equipe")]
    public string? Equipe { get; set; }
    // [Column("temps_penalite")]
    // public TimeSpan TempsPenalite { get; set; }
    

    [ForeignKey("IdEquipe")] // Indique que la clé étrangère est IdCoureur
    public virtual Equipe? Equipes { get; set; } // Propriété de navigation vers Coureur
    [ForeignKey("IdCategorie")] // Indique que la clé étrangère est IdCoureur
    public virtual Categorie? Categories { get; set; } // Propriété de navigation vers Coureur
}

