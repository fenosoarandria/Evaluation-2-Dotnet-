using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class PointsRepository
{
    private readonly ApplicationDbContext _context;

    public PointsRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Points> FindAll()
    {
        return _context._points?.ToList()?? new List<Points>();
    }

    public void Add(Points points)
    {
        if (points == null)
        {
            throw new ArgumentNullException(nameof(points));
        }

        _context._points.Add(points);
        _context.SaveChanges();
    }

    public void Update(Points  points)
    {
        if (points == null)
        {
            throw new ArgumentNullException(nameof(points));
        }

        _context._points.Update(points);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var pointsToDelete = _context._points.Find(id);
        if (pointsToDelete != null)
        {
            _context._points.Remove(pointsToDelete);
            _context.SaveChanges();
        }
    }
}