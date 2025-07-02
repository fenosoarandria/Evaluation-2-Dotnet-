using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class ClassementGeneralCategorieViewRepository
{
    private readonly ApplicationDbContext _context;

    public ClassementGeneralCategorieViewRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<ClassementGeneralCategorieView> FindAll()
    {
        return _context._classement_general_categorie_views?.ToList()?? new List<ClassementGeneralCategorieView>();
    }
    public List<ClassementGeneralCategorieView> FindClassementGeneral()
    {
        // Vérifier si _context.ClassementGeneralCategorieView n'est pas null ou vide
        if (_context._classement_general_categorie_views == null || !_context._classement_general_categorie_views.Any())
        {
            return new List<ClassementGeneralCategorieView>();
        }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var equipePoints = _context._classement_general_categorie_views
            .Include(classement => classement.Equipes) // Inclure les données de navigation de Coureurs
            .GroupBy(c => c.IdEquipe)
            .Select(g => new ClassementGeneralCategorieView
            {
                IdEquipe = g.Key,
                Equipe = g.FirstOrDefault().Equipe, // Assurez-vous de récupérer le nom du coureur
                Point = g.Sum(c => c.Point)
            })
            .OrderByDescending(cp => cp.Point)
            .ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return equipePoints;
    }
 
    public List<ClassementGeneralCategorieView> FindClassementGeneralCategorie(string id_categorie)
    {
        // Vérifier si _context.ClassementGeneralCategorieView n'est pas null ou vide
        if (_context._classement_general_categorie_views == null || !_context._classement_general_categorie_views.Any())
        {
            return new List<ClassementGeneralCategorieView>();
        }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var equipePoints = _context._classement_general_categorie_views
            .Include(classement => classement.Equipes) // Inclure les données de navigation de Coureurs
            .Include(classement => classement.Categories) // Inclure les données de navigation de Coureurs
            .Where(cat => cat.IdCategorie == id_categorie)
            .GroupBy(c => c.IdEquipe)
            .Select(g => new ClassementGeneralCategorieView
            {
                IdEquipe = g.Key,
                Equipe = g.FirstOrDefault().Equipe, // Assurez-vous de récupérer le nom du coureur
                Categories = g.FirstOrDefault().Categories, // Assurez-vous de récupérer le nom du coureur
                Point = g.Sum(c => c.Point)
            })
            .OrderByDescending(cp => cp.Point)
            .ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return equipePoints;
    }
    
}