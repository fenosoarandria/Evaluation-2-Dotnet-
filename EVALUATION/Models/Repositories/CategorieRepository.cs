using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class CategorieRepository
{
    private readonly ApplicationDbContext _context;

    public CategorieRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Categorie> FindAll()
    {
        return _context._categorie?.ToList()?? new List<Categorie>();
    }

    public void Add(Categorie categorie)
    {
        if (categorie == null)
        {
            throw new ArgumentNullException(nameof(categorie));
        }

        _context._categorie.Add(categorie);
        _context.SaveChanges();
    }

    public void Update(Categorie  categorie)
    {
        if (categorie == null)
        {
            throw new ArgumentNullException(nameof(categorie));
        }

        _context._categorie.Update(categorie);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var categorieToDelete = _context._categorie.Find(id);
        if (categorieToDelete != null)
        {
            _context._categorie.Remove(categorieToDelete);
            _context.SaveChanges();
        }
    }
}