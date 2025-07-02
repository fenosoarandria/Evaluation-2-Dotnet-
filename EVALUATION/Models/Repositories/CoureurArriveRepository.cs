using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class CoureurArriveRepository
{
    private readonly ApplicationDbContext _context;

    public CoureurArriveRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<CoureurArrive> FindAll()
    {
        return _context._coureur_arrive?
                    .ToList()?? new List<CoureurArrive>();
    }
}