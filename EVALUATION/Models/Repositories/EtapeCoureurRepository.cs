
using Microsoft.EntityFrameworkCore;

public class EtapeCoureurRepository
{
    private readonly ApplicationDbContext _context;

    public EtapeCoureurRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public List<EtapeCoureur> FindAll()
    {
        return _context._etape_coureur
            .Include(d => d.Etape)
            .Include(d => d.Coureur)
            .ToList(); // Supposant que le DbSet s'appelle EtapeCoureur
    }
    public List<EtapeCoureur> FindAllByIdEtape(string id_etape)
    {
        return _context._etape_coureur
            .Include(d => d.Etape)
            .Include(d => d.Coureur)
            .Where(e => e.IdEtape == id_etape)
            .ToList(); // Supposant que le DbSet s'appelle EtapeCoureur
    }

    public void Add(EtapeCoureur etape_coureur)
    {
        if (etape_coureur == null)
        {
            throw new ArgumentNullException(nameof(etape_coureur));
        }

        _context._etape_coureur.Add(etape_coureur);
        _context.SaveChanges();
    }
    
    public Dictionary<string, int> GetTotalCoureurByEtape()
    {
#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
#pragma warning disable CS8621 // Nullability of reference types in return type doesn't match the target delegate (possibly because of nullability attributes).
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        var result = _context._etape_coureur
                            .GroupBy(ec => ec.IdEtape)
                            .Select(g => new { IdEtape = g.Key, TotalCoureur = g.Count() })
                            .ToDictionary(x => x.IdEtape, x => x.TotalCoureur)?? null;
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning restore CS8621 // Nullability of reference types in return type doesn't match the target delegate (possibly because of nullability attributes).
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CS8603 // Possible null reference return.
        return result;
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
    }
    public int CountEtapeCoureur(string etapeId)
    {
        return _context._etape_coureur.Count(ec => ec.IdEtape == etapeId);
    }
}
