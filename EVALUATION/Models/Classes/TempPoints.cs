using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper;

[Table("temp_points")]
public class TempPoints
{
    [Column("classement")]
    public int Classement { get; set; }

    [Column("points")]
    public int Points { get; set; }
    public static TempPoints MapTempPoint(CsvReader csv)
    {
        return new TempPoints
        {
            Classement = csv.GetField<int>("classement"),
            Points = csv.GetField<int>("points")
        };
    }

}
