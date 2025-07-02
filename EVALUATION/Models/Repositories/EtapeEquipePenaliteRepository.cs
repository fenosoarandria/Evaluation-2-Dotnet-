using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class EtapeEquipePenaliteRepository
{
    private readonly ApplicationDbContext _context;

    public EtapeEquipePenaliteRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<EtapeEquipePenalite> FindAll()
    {
        return _context._etape_equipe_penalite?
        .Include(e => e.Etapes)
        .Include(e => e.Equipes)
        .ToList()?? new List<EtapeEquipePenalite>();
    }

    public void Add(EtapeEquipePenalite penalite)
    {
        if (penalite == null)
        {
            throw new ArgumentNullException(nameof(penalite));
        }

        _context._etape_equipe_penalite.Add(penalite);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var penaliteToDelete = _context._etape_equipe_penalite.Find(id);
        if (penaliteToDelete != null)
        {
            _context._etape_equipe_penalite.Remove(penaliteToDelete);
            _context.SaveChanges();
        }
    }
}