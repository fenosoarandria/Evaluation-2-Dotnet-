using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class EtapeRepository
{
    private readonly ApplicationDbContext _context;

    public EtapeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Etape> FindAll()
    {
        return _context._etape?.ToList()?? new List<Etape>();
    }
    public Etape FindByIdEtape(string id_etape)
    {
        return _context._etape?.Find(id_etape)?? new Etape();
    }
    public Etape GetEtapeById(string etapeId)
    {
        return _context._etape.FirstOrDefault(e => e.Id == etapeId) ?? new Etape();
    }
}