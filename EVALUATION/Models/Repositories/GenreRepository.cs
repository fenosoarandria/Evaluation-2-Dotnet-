using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class GenreRepository
{
    private readonly ApplicationDbContext _context;

    public GenreRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Genre> FindAll()
    {
        return _context._genre?.ToList()?? new List<Genre>();
    }

    public void Add(Genre genre)
    {
        if (genre == null)
        {
            throw new ArgumentNullException(nameof(genre));
        }

        _context._genre.Add(genre);
        _context.SaveChanges();
    }

    public void Update(Genre  genre)
    {
        if (genre == null)
        {
            throw new ArgumentNullException(nameof(genre));
        }

        _context._genre.Update(genre);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var genreToDelete = _context._genre.Find(id);
        if (genreToDelete != null)
        {
            _context._genre.Remove(genreToDelete);
            _context.SaveChanges();
        }
    }
}