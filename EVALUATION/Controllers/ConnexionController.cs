using Microsoft.AspNetCore.Mvc;

namespace EVALUATION.Controllers;
public class ConnexionController : Controller
{
    private readonly ILogger<ConnexionController> _logger;
    private readonly Login _login;
    public ConnexionController(ILogger<ConnexionController> logger,Login login)
    {
        _logger = logger;
        _login = login;
    }
    public IActionResult LoginEquipe()
    {
        return View();
    }
    [HttpPost]
    public IActionResult ConnecteEquipe(string login,string mot_de_passe)
    {
        var errors = new Dictionary<string, string>();
        try
        {
            string? id_equipe = _login.LoginEquipe(login,mot_de_passe);
            if (id_equipe == null)
            {
                errors["message"] = "Cette identifiant equipe n'existe pas";
            }

            if (errors.Any())
            {
                return Json(new { success = false, errors });
            }
#pragma warning disable CS8604 // Possible null reference argument.
            HttpContext.Session.SetString("Equipe", id_equipe);
#pragma warning restore CS8604 // Possible null reference argument.


        }
        catch (Exception ex)
        {
            errors["message"] = ex.Message;
            return Json(new { success = false, errors });
        }
            return Json(new { success = true, redirectUrl = Url.Action("Index", "Equipe") });
    }
    
    public IActionResult LoginAdmin()
    {
        return View();
    }
    [HttpPost]
    public IActionResult ConnecteAdmin(string login,string mot_de_passe)
    {
        var errors = new Dictionary<string, string>();
        try
        {
            string? id_admin = _login.LoginAdministrateur(login,mot_de_passe);
            if (id_admin == null)
            {
                errors["message"] = "Cette identifiant administrateur n'existe pas";
            }

            if (errors.Any())
            {
                return Json(new { success = false, errors });
            }
#pragma warning disable CS8604 // Possible null reference argument.
            HttpContext.Session.SetString("Admin", id_admin);
#pragma warning restore CS8604 // Possible null reference argument.
            
        }
        catch (Exception ex)
        {
            errors["message"] = ex.Message;
            return Json(new { success = false, errors });
        }
            return Json(new { success = true, redirectUrl = Url.Action("Index", "Administrateur") });
    }
    
    public IActionResult Deconnexion()
    {
         HttpContext.Session.Clear();
        // Redirigez l'utilisateur vers la page d'accueil ou de connexion après la déconnexion
        return RedirectToAction("LoginEquipe", "Connexion");
    }
    
    // public IActionResult Inscription()
    // {
    //     return View();
    // }
    // public IActionResult InscriptionConfirm(string nom,string prenom,string email,string mot_de_passe,string confirm_mot_de_passe)
    // {   
    //     var utilisateur = new Utilisateur{
    //         Nom = nom,
    //         Prenom = prenom,
    //         Email = email,
    //         MotDePasse = mot_de_passe
    //     };

    //     if(mot_de_passe.Equals(confirm_mot_de_passe))
    //     {
    //         servicesUtilisateur.AddUtilisateur(utilisateur);
    //         return Redirect("Login");
    //     }
    //         return Redirect("Inscription");
    // }
}
