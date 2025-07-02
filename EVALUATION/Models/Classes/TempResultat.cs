using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper;

[Table("temp_resultat")]
public class TempResultat
{
    
    [Column("etape_rang")]
    public int EtapeRang { get; set; }

    
    [Column("numero_dossard")]
    public int NumeroDossard { get; set; }

    
    [Column("nom")]
    public string? Nom { get; set; }

    
    [Column("genre")]
    public string? Genre { get; set; }

    
    [Column("date_de_naissance")]
    public DateTime? DateDeNaissance { get; set; }

    
    [Column("equipe")]
    public string? Equipe { get; set; }

    [Column("arrive")]
    public DateTime? Arrive { get; set; }
    public static TempResultat MapTempResultat(CsvReader csv)
    {
        return new TempResultat
        {
            EtapeRang = csv.GetField<int>("etape_rang"),
            NumeroDossard = csv.GetField<int>("numero dossard"),
            Nom = csv.GetField<string>("nom"),
            Genre = csv.GetField<string>("genre"),
            DateDeNaissance =  Contrainte.ParseDate(csv.GetField<string>("date naissance")),
            Equipe = csv.GetField<string>("equipe"),
            Arrive = Contrainte.ParseDate(csv.GetField<string>("arrivée"))
        };
    }

   }


