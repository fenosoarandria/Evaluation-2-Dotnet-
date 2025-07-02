using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class TempsCoureurRepository
{
    private readonly ApplicationDbContext _context;

    public TempsCoureurRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<TempsCoureur> FindAll()
    {
        return _context._temps_coureur?.ToList()?? new List<TempsCoureur>();
    }
    public string[] GetCoureurIdsByEtapeId(string etapeId)
    {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        return _context._temps_coureur
                       .Where(tc => tc.IdEtape == etapeId)
                       .Select(tc => tc.IdCoureur)
                       .ToArray();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
    }

    public void Add(TempsCoureur temps_coureur)
    {
        if (temps_coureur == null)
        {
            throw new ArgumentNullException(nameof(temps_coureur));
        }

        _context._temps_coureur.Add(temps_coureur);
        _context.SaveChanges();
    }

    public void Update(TempsCoureur  temps_coureur)
    {
        if (temps_coureur == null)
        {
            throw new ArgumentNullException(nameof(temps_coureur));
        }

        _context._temps_coureur.Update(temps_coureur);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var temps_coureurToDelete = _context._temps_coureur.Find(id);
        if (temps_coureurToDelete != null)
        {
            _context._temps_coureur.Remove(temps_coureurToDelete);
            _context.SaveChanges();
        }
    }
}