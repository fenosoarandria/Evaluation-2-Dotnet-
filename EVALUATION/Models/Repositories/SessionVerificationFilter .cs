using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class SessionVerificationFilter : IActionFilter
{
   public void OnActionExecuting(ActionExecutingContext context)
{
    var equipeSession = context.HttpContext.Session.GetString("Equipe");
    var adminSession = context.HttpContext.Session.GetString("Admin");
    
    // Suivi de l'état de chaque session
    bool equipeSessionExists = !string.IsNullOrEmpty(equipeSession);
    bool adminSessionExists = !string.IsNullOrEmpty(adminSession);

    // Vérification si les deux sessions sont vides
    if (!equipeSessionExists && !adminSessionExists)
    {
        // Rediriger vers une page d'accueil générale si les deux sessions sont vides
        context.Result = new RedirectToActionResult("LoginEquipe", "Connexion", null);
        return;
    }

    // Vérification si la session client est vide
    if (!equipeSessionExists)
    {
        // Rediriger vers la page d'accueil du client si la session client est vide
        if (context.HttpContext.User.IsInRole("Equipe"))
        {
            context.Result = new RedirectToActionResult("LoginEquipe", "Connexion", null);
            return;
        }
    }

    // Vérification si la session admin est vide
    if (!adminSessionExists)
    {
        // Rediriger vers la page d'accueil de l'admin si la session admin est vide
        if (context.HttpContext.User.IsInRole("Admin"))
        {
            context.Result = new RedirectToActionResult("LoginAdmin", "Connexion", null);
            return;
        }
    }

    // Les deux sessions sont valides, laisser l'action s'exécuter normalement
}




    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Ne rien faire après l'exécution de l'action
    }
}
