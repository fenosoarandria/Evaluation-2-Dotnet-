using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class CoureurDepartRepository
{
    private readonly ApplicationDbContext _context;

    public CoureurDepartRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<CoureurDepart> FindAll()
    {
        return _context._coureur_depart?
                    .ToList()?? new List<CoureurDepart>();
    }
public DateTime? FindDateDepartByEtape(string id_etape)
{
    var coureurDepart = _context._coureur_depart
                                .FirstOrDefault(cd => cd.IdEtape == id_etape);
    return coureurDepart != null ? coureurDepart.HeureDepart : null;
}
}