using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class ClassementGeneralViewRepository
{
    private readonly ApplicationDbContext _context;

    public ClassementGeneralViewRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<ClassementGeneralView> FindAll()
    {
        return _context._classement_general_views?.ToList()?? new List<ClassementGeneralView>();
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
    public List<ClassementGeneralView> FindAllByIdEtape(string id_etape)
    {
        return _context._classement_general_views?
                .Where(i => i.IdEtape == id_etape)
                .OrderBy(i => i.Rang)
                .ToList()?? new List<ClassementGeneralView>();
    }
    public IEnumerable<IGrouping<string, ClassementGeneralView>> FindAllByIdEquipe(string idEquipe)
    {
        var query = _context._classement_general_views
            .Where(c => c.IdEquipe == idEquipe)
            .OrderBy(r => r.RangEtape)
            .ToList(); // Exécutez la requête et convertissez-la en liste

        if (query == null || !query.Any())
        {
            return new List<IGrouping<string, ClassementGeneralView>>(); // Retourne une liste vide de groupes si la requête est null ou vide
        }

    #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return query.GroupBy(c => c.IdEtape).ToList();
    #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
   
    public List<ClassementGeneralView> FindClassementGeneral()
    {
        // Vérifiez si _context.ClassementGeneralView n'est pas null ou vide
        if (_context._classement_general_views == null || !_context._classement_general_views.Any())
        {
            return new List<ClassementGeneralView>();
        }

            // Exécuter la requête en incluant les données de navigation Coureurs
    #pragma warning disable CS8602 // Dereference of a possibly null reference.
            var coureurPoints = _context._classement_general_views
            .Include(classement => classement.Equipes) // Inclure les données de navigation de Coureurs
            .Include(classement => classement.Coureurs) // Inclure les données de navigation de Coureurs
            .Include(classement => classement.Genres) // Inclure les données de navigation de Coureurs
            .GroupBy(p => p.IdCoureur)
            .Select(g => new ClassementGeneralView
            {
                IdCoureur = g.Key,
                Point = g.Sum(p => p.Point),
                NomCoureur = g.FirstOrDefault().NomCoureur, // Assurez-vous de récupérer le nom du coureur
                Coureurs = g.FirstOrDefault().Coureurs, // Assurez-vous d'inclure les données du coureur
                Genres = g.FirstOrDefault().Genres, // Assurez-vous d'inclure les données du coureur
            })
            .OrderByDescending(cp => cp.Point)
            .ToList();
    #pragma warning restore CS8602 // Dereference of a possibly null reference.

            return coureurPoints;
    }
    

//     public Dictionary<string, int> FindPointChaqueEtape()
//     {
//         // Créer un dictionnaire pour stocker les points de chaque étape
//         var pointsParEtape = new Dictionary<string, int>();

//         // Vérifier si _context._classement_general_views est null ou non vide
//         if (_context._classement_general_views != null && _context._classement_general_views.Any())
//         {
//             // Regrouper les points par étape et calculer la somme des points pour chaque étape
//             var pointsParEtapeQuery = _context._classement_general_views
//                 .GroupBy(p => p.IdEtape)
//                 .Select(g => new
//                 {
//                     IdEtape = g.First().Etape,
//                     TotalPoints = g.Sum(p => p.Point)
//                 });

//             // Ajouter les points de chaque étape dans le dictionnaire
//             foreach (var result in pointsParEtapeQuery)
//             {
// #pragma warning disable CS8604 // Possible null reference argument.
//                 pointsParEtape.Add(result.IdEtape, result.TotalPoints);
// #pragma warning restore CS8604 // Possible null reference argument.
//             }
//         }
//         return pointsParEtape;
//     }


public List<ClassementGeneralView> FindPointChaqueEtape()
{
    // Initialiser une liste pour stocker les données de point pour chaque étape
    var classements = new List<ClassementGeneralView>();

    // Vérifier si _context._classement_general_views est null ou non vide
    if (_context._classement_general_views != null && _context._classement_general_views.Any())
    {
        // Regrouper les points par étape et calculer la somme des points pour chaque étape
        var pointsParEtapeQuery = _context._classement_general_views
            .Include(classement => classement.Etapes) // Inclure les données de navigation de Coureurs
            .GroupBy(p => p.IdEtape)
            .Select(g => new ClassementGeneralView
            {
                IdEtape = g.First().IdEtape,
                Etape = g.First().Etape,
                Point = g.Sum(p => p.Point),
                Etapes = g.First().Etapes
            })
            .OrderByDescending(cp => cp.Point);


        // Ajouter les données de point pour chaque étape dans la liste
        foreach (var result in pointsParEtapeQuery)
        {
            classements.Add(result);
        }
    }
    return classements;
}

    public List<ClassementGeneralView> FindPointsByEquipe()
    {
        // Vérifier si _context._classement_general_views est null ou non vide
        if (_context._classement_general_views == null || !_context._classement_general_views.Any())
        {
            return new List<ClassementGeneralView>(); // Retourner une liste vide si aucune donnée n'est disponible
        }

        // Récupérer les points par équipe et les trier par ordre décroissant de points
        var pointsParEquipe = _context._classement_general_views
            .Include(classement => classement.Equipes) // Inclure les données de navigation de Coureurs
            .Include(classement => classement.Etapes) // Inclure les données de navigation de Coureurs
            .GroupBy(p => p.IdEquipe)
            .Select(g => new ClassementGeneralView
            {
                IdEquipe = g.First().Equipe,
                Point = g.Sum(p => p.Point),
                Equipes = g.First().Equipes,
                Etapes = g.First().Etapes,
            })
            .OrderByDescending(p => p.Point)
            .ToList(); // Exécuter la requête

        return pointsParEquipe;
    }

public List<ClassementGeneralView> FindTotalPointEquipe(string id_equipe)
{
    // Vérifier si _context.ClassementGeneralView n'est pas null ou vide
    if (_context._classement_general_views == null || !_context._classement_general_views.Any())
    {
        return new List<ClassementGeneralView>();
    }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var equipePoints = _context._classement_general_views
        .Include(classement => classement.Equipes) // Inclure les données de navigation de Coureurs
        .Include(classement => classement.Coureurs) // Inclure les données de navigation de Coureurs
        .Where(cat => cat.Equipes.Nom == id_equipe)
        .GroupBy(c => new { c.IdEquipe, c.IdCoureur })
        .Select(g => new ClassementGeneralView
        {
            IdEquipe = g.Key.IdEquipe,
            Equipe = g.FirstOrDefault().Equipe, // Assurez-vous de récupérer le nom du coureur
            Coureurs = g.FirstOrDefault().Coureurs, // Assurez-vous de récupérer le nom du coureur
            Point = g.Sum(c => c.Point)
        })
        .OrderByDescending(cp => cp.Point)
        .ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return equipePoints;
}
}