using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class ClassementGeneralCoefficientEtapeViewRepository
{
    private readonly ApplicationDbContext _context;

    public ClassementGeneralCoefficientEtapeViewRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<ClassementGeneralCoefficientEtapeView> FindAll()
    {
        return _context._classement_general_coefficient_etape_views?.ToList()?? new List<ClassementGeneralCoefficientEtapeView>();
    }
    // public List<ClassementGeneralView> FindAllByIdEtapeAndIdEquipe(string id_etape ,string id_equipe)
    // {
    //     return _context._classement_general_views?.Where(e => e.IdEtape = id_etape)?? new List<ClassementGeneralView>();
    // }
    public List<ClassementGeneralView> FindAllByIdEquipeEtape(string id_etape,string id_equipe)
    {
        return _context._classement_general_views?
                .Where(i => i.IdEtape == id_etape)
                .Where(i => i.IdEquipe == id_equipe)
                .ToList()?? new List<ClassementGeneralView>();
    }
    public List<ClassementGeneralCoefficientEtapeView> FindAllByIdEtape(string id_etape)
    {
        return _context._classement_general_coefficient_etape_views?
                .Include(c => c.Coureurs)
                .Where(i => i.IdEtape == id_etape)
                .OrderBy(i => i.Rang)
                .ToList()?? new List<ClassementGeneralCoefficientEtapeView>();
    }


public List<ClassementGeneralCoefficientEtapeView> FindPointChaqueEtape()
{
    // Initialiser une liste pour stocker les données de point pour chaque étape
    var classements = new List<ClassementGeneralCoefficientEtapeView>();

    // Vérifier si _context._classement_general_views est null ou non vide
    if (_context._classement_general_coefficient_etape_views != null && _context._classement_general_coefficient_etape_views.Any())
    {
        // Regrouper les points par étape et calculer la somme des points pour chaque étape
        var pointsParEtapeQuery = _context._classement_general_coefficient_etape_views
            .Include(classement => classement.Etapes) // Inclure les données de navigation de Coureurs
            .GroupBy(p => p.IdEtape)
            .Select(g => new ClassementGeneralCoefficientEtapeView
            {
                IdEtape = g.First().IdEtape,
                Equipes = g.First().Equipes,
                PointCoefficient = g.Sum(p => p.PointCoefficient),
                Coefficient = g.First().Coefficient,
                RangCoefficient = g.First().RangCoefficient,
                Etapes = g.First().Etapes
            })
            .OrderByDescending(cp => cp.PointCoefficient);


        // Ajouter les données de point pour chaque étape dans la liste
        foreach (var result in pointsParEtapeQuery)
        {
            classements.Add(result);
        }
    }
    return classements;
}


}