using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class CoureurRepository
{
    private readonly ApplicationDbContext _context;

    public CoureurRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Coureur> FindAll()
    {
        return _context._coureur?
                    .Include(d => d.Genre)
                    .ToList()?? new List<Coureur>();
    }
     public int NombreCoureur()
    {
        return _context._coureur.Count();
    }

    public List<Coureur> FindAllByIdEquipe(string? id_equipe)
    {
        return _context._coureur?
                .Include(i => i.Genre)
                .Where(i => i.IdEquipe == id_equipe)
                .ToList()?? new List<Coureur>();
    }
    public List<Coureur> GetNumberOfRunnersPerTeam()
    {
        var numberOfRunnersPerTeam = _context._coureur
                                            .GroupBy(c => c.IdEquipe)
                                            .Select(g => new Coureur { IdEquipe = g.Key, NumberOfRunners = g.Count() })
                                            .ToList();
        return numberOfRunnersPerTeam;
    }
}