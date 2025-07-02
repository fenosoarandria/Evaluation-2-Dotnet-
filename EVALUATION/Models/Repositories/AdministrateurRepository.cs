using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class AdministrateurRepository
{
    private readonly ApplicationDbContext _context;

    public AdministrateurRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Administrateur> FindAll()
    {
        return _context._administrateur?.ToList()?? new List<Administrateur>();
    }

    public string? CheckLoginAdmin(string email, string mot_de_passe)
    {
        var admin = _context._administrateur?.FirstOrDefault(p => p.Email == email && p.MotDePasse == mot_de_passe);
        if (admin != null)
        {
            return admin.Id;
        }
        // Retourner une valeur par défaut si aucune personne correspondante n'est trouvée
        return null;
    }
}