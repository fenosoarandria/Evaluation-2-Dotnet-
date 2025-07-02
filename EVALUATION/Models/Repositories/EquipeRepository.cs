using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class EquipeRepository
{
    private readonly ApplicationDbContext _context;

    public EquipeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Equipe> FindAll()
    {
        return _context._equipe?.ToList()?? new List<Equipe>();
    }
    public int NombreEquipe()
    {
        return _context._equipe.Count();
    }

   public Equipe FindById(string id_equipe)
    {
        return _context._equipe?.FirstOrDefault(a => a.Id == id_equipe)?? new Equipe();
    }
    public string? CheckLoginEquipe(string login, string mot_de_passe)
    {
        var equipe = _context._equipe?.FirstOrDefault(p => p.Nom == login && p.Nom == mot_de_passe);
        if (equipe != null)
        {
            return equipe.Id;
        }
        // Retourner une valeur par défaut si aucune personne correspondante n'est trouvée
        return null;
    }   
}