using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EVALUATION.Models;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

namespace EVALUATION.Controllers;

[ServiceFilter(typeof(SessionVerificationFilter))]
public class AdministrateurController : Controller
{
    private readonly ILogger<AdministrateurController> _logger;
    private readonly EtapeRepository _etape;
    private readonly EquipeRepository _equipe;
    private readonly CoureurRepository _coureur;
    private readonly CoureurArriveRepository _coureur_arrive;
    private readonly CoureurDepartRepository _coureur_depart;
    private readonly EtapeCoureurRepository _etape_coureur;
    private readonly ApplicationDbContext _context;
    private readonly ClassementGeneralViewRepository _classement;
    private readonly ClassementGeneralCoefficientEtapeViewRepository _classement_coefficient;
    private readonly ClassementGeneralCategorieViewRepository _classement_categorie;
    private readonly TempsCoureurRepository _temps;
    private readonly Import _csv;
    private readonly CategorieRepository _categorie;
    private readonly ImportTemporaireModel _import;
    private readonly EtapeEquipePenaliteRepository _etape_equipe_penalite;


    public AdministrateurController(ILogger<AdministrateurController> logger,ClassementGeneralCoefficientEtapeViewRepository ccc,CoureurDepartRepository cour,CoureurArriveRepository c,EtapeEquipePenaliteRepository eqp,EquipeRepository equipe,CategorieRepository cat,ClassementGeneralCategorieViewRepository ct,ImportTemporaireModel import,EtapeRepository etape,CoureurRepository coureur,EtapeCoureurRepository e_coureur,ApplicationDbContext context,ClassementGeneralViewRepository classement,TempsCoureurRepository temps,Import csv)
    {
        _logger = logger;
        _classement_coefficient = ccc;
        _coureur_arrive = c;
        _coureur_depart = cour;
        _etape_equipe_penalite = eqp;
        _equipe = equipe;
        _categorie = cat;
        _classement_categorie = ct;
        _import = import;
        _etape = etape;
        _etape_coureur = e_coureur;
        _coureur = coureur;
        _context = context;
        _classement = classement;
        _temps = temps;
        _csv = csv;
    }

    public IActionResult Index()
    {
         ViewBag.Equipe = _equipe.NombreEquipe();
        ViewBag.Coureur = _coureur.NombreCoureur();
        return View();
    }
    public IActionResult ListeDesEtapes(int? page)
    {
        int pageSize = 10; // Nombre d'éléments par page
        int pageNumber = (page ?? 1);
       
        ViewBag.ListeEtape = _etape.FindAll().ToPagedList(pageNumber, pageSize);
        return View();
    }
    public IActionResult TempsEtapeCoureur(string IdEtape,int? page)
    {
         int pageSize = 5; // Nombre d'éléments par page
        int pageNumber = (page ?? 1);
       
        ViewBag.EtapeCoureur = _etape_coureur.FindAllByIdEtape(IdEtape).ToPagedList(pageNumber, pageSize);;
        
        // ViewBag.IdEtape = IdEtape;
        ViewBag.NombreCoureur = _etape.GetEtapeById(IdEtape);
        ViewBag.CoureurArrive = _coureur_arrive.FindAll();

        // //Coureur existee temps
        // ViewBag.TempsExiste = _temps.GetCoureurIdsByEtapeId(IdEtape);
        ViewBag.IdEtape = IdEtape;
        return View();
    }
    public IActionResult InsererTempsEtapeCoureur(string id_etape, string id_coureur, string dates_arrive, string heures_arrive)
    {
        var errors = new Dictionary<string, string>();
        try
        {
            var dateArrive = DateTime.ParseExact(dates_arrive + " " + heures_arrive, "yyyy-MM-dd HH:mm", null);
            if (DateTime.ParseExact(dates_arrive + " " + heures_arrive, "yyyy-MM-dd HH:mm", null) < _coureur_depart.FindDateDepartByEtape(id_etape))
            {
                errors["message"] = "La date heure arriver ne doit pas etre inferieur au date depart";
            }
            if (errors.Any())
            {
                return Json(new { success = false, errors });
            }

            // Construction de la requête d'insertion
            string sqlQuery = $@"
                INSERT INTO coureur_arrive (id_etape, id_coureur, heure_arrive) 
                VALUES ('{id_etape}', '{id_coureur}', '{dateArrive:yyyy-MM-dd HH:mm:ss}');";

            // Exécution de la requête SQL brute
            _context.Database.ExecuteSqlRaw(sqlQuery);
            string sqlQuery2 = $@"
                INSERT INTO temps_coureur (id_etape, id_coureur,heure_depart, heure_arrive) 
                VALUES ('{id_etape}', '{id_coureur}', '{_coureur_depart.FindDateDepartByEtape(id_etape)}','{dateArrive:yyyy-MM-dd HH:mm:ss}');";

            // Exécution de la requête SQL brute
            _context.Database.ExecuteSqlRaw(sqlQuery2);
        }
        catch (Exception ex)
        {
            errors["message"] = ex.Message;
            return Json(new { success = false, errors });
        }
            return Json(new { success = true, redirectUrl = Url.Action("ClassementGeneral", "Administrateur") });       
    }
    public IActionResult ClassementGeneral(int? page)
    {
        int pageSize = 10; // Nombre d'éléments par page
        int pageNumber = (page ?? 1);
       
        ViewBag.ClassementGeneral = _classement.FindClassementGeneral().ToPagedList(pageNumber, pageSize);
        return View();
    }
    public IActionResult CoureurEtape()
    {
        ViewBag.Etape = _etape.FindAll();
        return View();
    }
    public IActionResult PointChaqueEtape()
    {  
        ViewBag.PointChaqueEtape = _classement_coefficient.FindPointChaqueEtape();
        return View();
    }
    public IActionResult ClassementGeneralEquipe()
    {  
        ViewBag.ClassementGeneralParEquipe = _classement.FindPointsByEquipe();
        return View();
    }
    public IActionResult ImportDonnee()
    {
        return View();
    }
    public IActionResult ImportEtapeResultat(IFormFile file_etape,IFormFile file_resultat)
    {
        if (file_etape == null || file_resultat.Length == 0 || file_resultat == null || file_etape.Length == 0)
            {
                TempData["Error"] = "Le fichier est vide ou n'existe pas.";
                return Ok(TempData["Error"]);
            }
            try
            {
                _csv.ImportCsvToDatabase("temp_etape",file_etape, TempEtape.MapTempEtape);
                _import.InsertDataFromTempEtapes();
                _csv.ImportCsvToDatabase("temp_resultat",file_resultat, TempResultat.MapTempResultat);
                _import.InsertDataFromTempResult();
                return RedirectToAction("Index","Administrateur");
            }
         
        catch (Exception ex)
        {
            // Gérez les erreurs, par exemple en affichant un message d'erreur
            string message = ex.Message;
            return View("Exception",message);  
        }          
    }
    public IActionResult ImportPoints()
    {
        return View();
    }
    public IActionResult ImportPointsBase(IFormFile file)
    {
        if (file == null || file.Length == 0)
            {
                TempData["Error"] = "Le fichier est vide ou n'existe pas.";
                return Ok(TempData["Error"]);
            }
            try
            {
                _csv.ImportCsvToDatabase("temp_points",file, TempPoints.MapTempPoint);
                _import.InsertDataFromTempPoints();
                return RedirectToAction("Index","Administrateur");
            }
         
        catch (Exception ex)
        {
            // Gérez les erreurs, par exemple en affichant un message d'erreur
            string message = ex.Message;
            return View("Exception",message);  
        }          
    }
    public IActionResult Restore(){
        Helpers.ResetDataBase(_context);
        return RedirectToAction("Index","Administrateur");
    }
   
    public IActionResult GenereCategorie()
    {
        return View();
    }
    public IActionResult GenereCategorieFunction()
    {
        _context.Database.ExecuteSqlRaw("SELECT f_coureur_categorie()");
        return RedirectToAction("Index","Administrateur");
    }
public IActionResult ClassementGeneralCategorie(int? page)
    {
        int pageSize = 10; // Nombre d'éléments par page
        int pageNumber = (page ?? 1);
        ViewBag.ClassementGeneral = _classement_coefficient.FindPointChaqueEtape().ToPagedList(pageNumber, pageSize);;       
        ViewBag.Categorie = _categorie.FindAll();


        ViewBag.maxPoints = _classement.FindPointsByEquipe().Max(liste => liste.Point);

    // Passer les données à la vue avec le maximum des points
        return View();
    }
    public IActionResult TrierCategorie(string id_categorie)
    {  
        ViewBag.Trie = _classement_categorie.FindClassementGeneralCategorie(id_categorie);

        return View();
    }
        // ViewBag.maxPoints = _classement_categorie.FindClassementGeneralCategorie(id_categorie).Max(liste => liste.Point);
    public IActionResult Penalite()
    {  
        ViewBag.Penalite = _etape_equipe_penalite.FindAll();
        return View();
    }
    public IActionResult AjouterPenalite()
    {  
        ViewBag.Etape = _etape.FindAll();
        ViewBag.Equipe = _equipe.FindAll();
        return View();
    }
    public IActionResult AjouterHeurePenalite(string id_etape,string id_equipe,TimeSpan temps_penalite)
    {  
        if(_classement.FindAllByIdEquipeEtape(id_etape,id_equipe).Count == 0){
            return Ok("Il n'y a pas de etape et equipe");
        }
        else{
            var penalite = new EtapeEquipePenalite{
                IdEtape = id_etape,
                IdEquipe = id_equipe,
                TempsPenalite = temps_penalite
            };
            _etape_equipe_penalite.Add(penalite);
        }
        return RedirectToAction("Penalite" ,"Administrateur");
    }
    public IActionResult DeletePenalite(string id)
    {  
        _etape_equipe_penalite.Delete(id);
        return RedirectToAction("Penalite","Administrateur");
    }

    public IActionResult Pdf(int? page)
    {  
         int pageSize = 10; // Nombre d'éléments par page
        int pageNumber = (page ?? 1);
        ViewBag.ClassementGeneral = _classement.FindPointsByEquipe().ToPagedList(pageNumber, pageSize);;       
        ViewBag.Categorie = _categorie.FindAll();
        return View();
    }
    public IActionResult GeneratePdf()
    {
        byte[] pdfBytes =PdfGenerator.GenererChampions(_classement.FindPointsByEquipe());        
        // Renvoyer le PDF généré comme un fichier téléchargeable
        return File(pdfBytes, "application/pdf", $"{DateTime.Now}.pdf");
    }

    public IActionResult ClassementCoureurEtape(string id_etape,int? page)
    {  
        int pageSize = 10; // Nombre d'éléments par page
        int pageNumber = (page ?? 1);
        ViewBag.id_etape = id_etape;
       
        ViewBag.PointCoureurChaqueEtape = _classement_coefficient.FindAllByIdEtape(id_etape).ToPagedList(pageNumber, pageSize);;
        return View();
    }

    public IActionResult ClassementCoureurEquipe(string id_equipe)
    {  
       
        ViewBag.Total = _classement.FindTotalPointEquipe(id_equipe);
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
