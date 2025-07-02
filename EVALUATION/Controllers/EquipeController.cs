using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EVALUATION.Models;
using X.PagedList;

namespace EVALUATION.Controllers;

[ServiceFilter(typeof(SessionVerificationFilter))]
public class EquipeController : Controller
{
    private readonly ILogger<EquipeController> _logger;
    private readonly EtapeRepository _etape;
    private readonly EquipeRepository _equipe;
    private readonly CoureurRepository _coureur;
    private readonly CategorieRepository _categorie;
    private readonly EtapeCoureurRepository _etape_coureur;
    private readonly ClassementGeneralViewRepository _classement;
    private readonly ClassementGeneralCategorieViewRepository _classement_categorie;

    public EquipeController(ILogger<EquipeController> logger,ClassementGeneralCategorieViewRepository cl,CategorieRepository categorie,EquipeRepository equipe,EtapeRepository etape,CoureurRepository coureur,EtapeCoureurRepository e_coureur,ClassementGeneralViewRepository classement)
    {
        _logger = logger;
        _classement_categorie = cl;
        _categorie = categorie;
        _equipe = equipe;
        _etape = etape;
        _etape_coureur = e_coureur;
        _coureur = coureur;
        _classement = classement;
    }

    public IActionResult Index(int? page)
    {
        int pageSize = 2; // Nombre d'éléments par page
        int pageNumber = (page ?? 1);
       
#pragma warning disable CS8604 // Possible null reference argument.
        ViewBag.ListeClassement = _classement.FindAllByIdEquipe(HttpContext.Session.GetString("Equipe")).ToPagedList(pageNumber, pageSize);;
        ViewBag.Equipe = _equipe.FindById(HttpContext.Session.GetString("Equipe"));
        ViewBag.Etape = _etape.FindAll();
#pragma warning restore CS8604 // Possible null reference argument.
        return View();
    }

    public IActionResult ListeDesEtapes(int? page)
    {
        int pageSize = 5; // Nombre d'éléments par page
        int pageNumber = (page ?? 1);
       
        ViewBag.ListeEtape = _etape.FindAll().ToPagedList(pageNumber, pageSize);
        ViewBag.NombreCoureur = _etape_coureur.GetTotalCoureurByEtape();

        return View();
    }
    public IActionResult EtapeCoureur(string id_etape)
    {
        ViewBag.IdEtape = id_etape;
        ViewBag.Coureur = _coureur.FindAllByIdEquipe(HttpContext.Session.GetString("Equipe"));
        ViewBag.Etape = _etape.FindByIdEtape(id_etape);
        //Maka etape by id
        ViewBag.EtapeCoureur = _etape_coureur.FindAllByIdEtape(id_etape);
        //Maka nombre coureur by id
        ViewBag.NombreCoureur = _etape_coureur.CountEtapeCoureur(id_etape);
        return View();
    }
    [HttpPost]
    public IActionResult InsererEtapeCoureur(string id_etape,string[] id_coureur)
    {
        var nombre_coureur = _etape_coureur.CountEtapeCoureur(id_etape);
        var etape= _etape.GetEtapeById(id_etape);
        var errors = new Dictionary<string, string>();
        try
        {
            if(id_coureur.Count() == 0)
            {
            Console.WriteLine(id_coureur.Count());
                errors["message"] = "Veuiller selectionner au moin un coureur";
            }else{
                if (etape.NombreCoureurEquipe < (nombre_coureur+id_coureur.Count()))
                {
                    errors["message"] = " Le nombre de coureur dépasse le nombre de coureur par étape";
                }
            }


            if (errors.Any())
            {
                return Json(new { success = false, errors });
            }
            foreach (string item in id_coureur)
            {
                var etape_coureur = new EtapeCoureur{
                    IdEtape = id_etape,
                    IdCoureur = item
                };
                _etape_coureur.Add(etape_coureur);
            }

        }
        catch (Exception ex)
        {
            errors["message"] = ex.Message;
            return Json(new { success = false, errors });
        }
            return Json(new { success = true, redirectUrl = Url.Action("ListeDesEtapes", "Equipe") });
        
    }
    public IActionResult ClassementGeneral(int? page)
    {
        int pageSize = 7; // Nombre d'éléments par page
        int pageNumber = (page ?? 1);
       
        ViewBag.ClassementGeneral = _classement.FindClassementGeneral().ToPagedList(pageNumber, pageSize);
        
        return View();
    }
    public IActionResult PointChaqueEtape()
    {  
        ViewBag.PointChaqueEtape = _classement.FindPointChaqueEtape();
        return View();
    }

    
    public IActionResult ClassementGeneralEquipe()
    {  
        ViewBag.ClassementGeneralParEquipe = _classement.FindPointsByEquipe();
        return View();
    }

    public IActionResult EquipeCoureur(string id_etape)
    {
        ViewBag.IdEtape = id_etape;
        ViewBag.Coureur = _coureur.FindAllByIdEquipe(HttpContext.Session.GetString("Equipe"));
        ViewBag.Etape = _etape.FindByIdEtape(id_etape);
        //Maka etape by id
        ViewBag.EtapeCoureur = _etape_coureur.FindAllByIdEtape(id_etape);
        //Maka nombre coureur by id
        ViewBag.NombreCoureur = _etape_coureur.CountEtapeCoureur(id_etape);
        return View();
    }

    public IActionResult InsererEquipeCoureur(string id_etape,string[] id_coureur)
    {
        var nombre_coureur = _etape_coureur.CountEtapeCoureur(id_etape);
        var etape= _etape.GetEtapeById(id_etape);
        var errors = new Dictionary<string, string>();
        try
        {
            if (etape.NombreCoureurEquipe < (nombre_coureur+id_coureur.Count()))
            {
                errors["message"] = "Liste de coureur tres elever";
            }

            if (errors.Any())
            {
                return Json(new { success = false, errors });
            }
            foreach (string item in id_coureur)
            {
                var etape_coureur = new EtapeCoureur{
                    IdEtape = id_etape,
                    IdCoureur = item
                };
                _etape_coureur.Add(etape_coureur);
            }

        }
        catch (Exception ex)
        {
            errors["message"] = ex.Message;
            return Json(new { success = false, errors });
        }
            return Json(new { success = true, redirectUrl = Url.Action("ListeDesEtapes", "Equipe") });
        
    }
    public IActionResult ClassementGeneralCategorie(int? page)
    {
        int pageSize = 10; // Nombre d'éléments par page
        int pageNumber = (page ?? 1);
        ViewBag.ClassementGeneral = _classement.FindPointsByEquipe().ToPagedList(pageNumber, pageSize);;       
       
        ViewBag.Categorie = _categorie.FindAll();
        return View();
    }
    public IActionResult TrierCategorie(string id_categorie)
    {  
        ViewBag.Trie = _classement_categorie.FindClassementGeneralCategorie(id_categorie);
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
