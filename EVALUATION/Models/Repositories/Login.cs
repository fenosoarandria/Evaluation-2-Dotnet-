    
public class Login
{    
    private readonly AdministrateurRepository _context_admin;
    private readonly EquipeRepository _context_equipe;

    public Login(AdministrateurRepository admin,EquipeRepository equipe)
    {
        _context_admin = admin;
        _context_equipe = equipe;
    }
    
    public string? LoginAdministrateur(string email,string mot_de_passe)
    {
        string? id_admin = _context_admin?.CheckLoginAdmin(email,mot_de_passe)?? null;
        if(id_admin != null){
            return id_admin;
        }
        return null;
    }
    public string? LoginEquipe(string email,string mot_de_passe)
    {
        string? id_equipe = _context_equipe.CheckLoginEquipe(email,mot_de_passe);
        if(id_equipe != null){
            return id_equipe;
        }
        return null;
    }

}
