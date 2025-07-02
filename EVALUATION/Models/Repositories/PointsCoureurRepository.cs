using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class PointsCoureurRepository
{
    private readonly ApplicationDbContext _context;

    public PointsCoureurRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<PointsCoureur> FindAll()
    {
        return _context._points_coureur?.ToList()?? new List<PointsCoureur>();
    }

    public void Add(PointsCoureur points_coureur)
    {
        if (points_coureur == null)
        {
            throw new ArgumentNullException(nameof(points_coureur));
        }

        _context._points_coureur.Add(points_coureur);
        _context.SaveChanges();
    }

    public void Update(PointsCoureur  points_coureur)
    {
        if (points_coureur == null)
        {
            throw new ArgumentNullException(nameof(points_coureur));
        }

        _context._points_coureur.Update(points_coureur);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var points_coureurToDelete = _context._points_coureur.Find(id);
        if (points_coureurToDelete != null)
        {
            _context._points_coureur.Remove(points_coureurToDelete);
            _context.SaveChanges();
        }
    }
}