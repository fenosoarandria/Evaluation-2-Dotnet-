using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class CategorieCoureurRepository
{
    private readonly ApplicationDbContext _context;

    public CategorieCoureurRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<CategorieCoureur> FindAll()
    {
        return _context._categorie_coureur?.ToList()?? new List<CategorieCoureur>();
    }

    public void Add(CategorieCoureur categorie_coureur)
    {
        if (categorie_coureur == null)
        {
            throw new ArgumentNullException(nameof(categorie_coureur));
        }

        _context._categorie_coureur.Add(categorie_coureur);
        _context.SaveChanges();
    }

    public void Update(CategorieCoureur  categorie_coureur)
    {
        if (categorie_coureur == null)
        {
            throw new ArgumentNullException(nameof(categorie_coureur));
        }

        _context._categorie_coureur.Update(categorie_coureur);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var categorie_coureurToDelete = _context._categorie_coureur.Find(id);
        if (categorie_coureurToDelete != null)
        {
            _context._categorie_coureur.Remove(categorie_coureurToDelete);
            _context.SaveChanges();
        }
    }
}