using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

[Table("equipe")]
public class Equipe
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("nom")]
    public string? Nom { get; set; }

    [Column("login")]
    public string? Login { get; set; }

    [Column("mot_de_passe")]
    public string? MotDePasse { get; set; }

}
