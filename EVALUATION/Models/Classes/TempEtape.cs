using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using CsvHelper;

[Table("temp_etape")]
public class TempEtape
{
    
    [Column("etape")]
    public string? Etape { get; set; }

    
    [Column("longueur")]
    public double Longueur { get; set; }

    
    [Column("nb_coureur")]
    public int NbCoureur { get; set; }

    
    [Column("rang")]
    public int Rang { get; set; }

    
    [Column("date_depart")]
    public DateTime? DateDepart { get; set; }

    
    [Column("heure_depart")]
    public TimeSpan HeureDepart { get; set; }
    public static TempEtape MapTempEtape(CsvReader csv)
    {
        return new TempEtape
        {
            Etape = csv.GetField<string>("etape"),
            Longueur = Contrainte.ParseDistance(csv.GetField<string>("longueur").Replace(',', '.')),
            NbCoureur = csv.GetField<int>("nb coureur"),
            Rang = csv.GetField<int>("rang"),
            DateDepart =  Contrainte.ParseDate(csv.GetField<string>("date départ")),
            HeureDepart = csv.GetField<TimeSpan>("heure départ")
    
        };
    }
}
