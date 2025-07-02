using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

[Table("points")]
public class Points
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("rang")]
    public int? Rang { get; set; }

    [Column("points")]
    public double? Point { get; set; }

}
