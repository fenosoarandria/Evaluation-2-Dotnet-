using Microsoft.EntityFrameworkCore;

public class ImportTemporaireModel
{
    private readonly ApplicationDbContext _dbContext;
    public ImportTemporaireModel(ApplicationDbContext csv){
        _dbContext = csv;
    }
    public void InsertDataFromTempEtapes()
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                // Insérer les données dans la table Unite
                _dbContext.Database.ExecuteSqlRaw(@"INSERT INTO etape (nom,longueur,nombre_coureur_equipe,rang_etape) 
                                                        SELECT DISTINCT ON (e.etape, e.rang)
                                                            e.etape,e.longueur,e.nb_coureur,e.rang 
                                                        FROM temp_etape e
                                                    ");                

                // Insérer les données dans la table TypeMaison
                _dbContext.Database.ExecuteSqlRaw(@"INSERT INTO coureur_depart (id_etape, heure_depart)
                                                    SELECT DISTINCT ON (te.date_depart, te.heure_depart)
                                                        e.id as id_etape, (te.date_depart + te.heure_depart) as date_heure_depart
                                                        FROM temp_etape te
                                                        JOIN etape e ON e.nom = te.etape and e.rang_etape = te.rang");
                transaction.Commit(); // Commit la transaction
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de l'insertion des données : " + ex.Message);
                // La transaction sera rollback automatiquement car Commit() n'a pas été appelé
            }
        }
    }
    public void InsertDataFromTempResult()
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                // Insérer les données dans la table Unite
                _dbContext.Database.ExecuteSqlRaw(@"INSERT INTO genre (nom) 
                                                        SELECT DISTINCT ON (genre) genre 
                                                        FROM temp_resultat");

                // Insérer les données dans la table TypeMaison
                _dbContext.Database.ExecuteSqlRaw(@"INSERT INTO equipe (nom,login,mot_de_passe) 
                                                    SELECT DISTINCT ON (equipe) equipe, equipe || '@gmail.com' AS email, '1234' AS password
                                                    FROM temp_resultat");
                                                                        // Insérer les données dans la table TypeMaison
                _dbContext.Database.ExecuteSqlRaw(@"INSERT INTO coureur(nom, numero_dossard, date_de_naissance, id_genre, id_equipe)  
                                                        SELECT DISTINCT ON (nom, numero_dossard)
                                                        tr.nom,tr.numero_dossard,tr.date_de_naissance,g.id as id_genre,e.id as id_equipe 
                                                        FROM temp_resultat tr
                                                            JOIN equipe e ON tr.equipe = e.nom
                                                            JOIN genre g ON tr.genre = g.nom");
                                                                        // Insérer les données dans la table TypeMaison
                _dbContext.Database.ExecuteSqlRaw(@"INSERT INTO etape_coureur(id_etape, id_coureur)  
                                                        SELECT DISTINCT e.id as id_etape,c.id as id_coureur 
                                                            FROM  temp_resultat tr
                                                                JOIN etape e ON tr.etape_rang = e.rang_etape
                                                                JOIN coureur c ON tr.nom = c.nom");
                                                    // Insérer les données dans la table TypeMaison
                _dbContext.Database.ExecuteSqlRaw(@"INSERT INTO coureur_arrive (id_etape,id_coureur, heure_arrive)
                                                    SELECT DISTINCT e.id as id_etape, c.id as id_coureur , tr.arrive as date_heure_arrive
                                                        FROM temp_resultat tr
                                                            JOIN etape e ON tr.etape_rang = e.rang_etape
                                                            JOIN coureur c ON tr.nom = c.nom
                                                            JOIN etape_coureur ec ON c.id = ec.id_coureur AND e.id = ec.id_etape");
                                                                                                                // Insérer les données dans la table TypeMaison
                _dbContext.Database.ExecuteSqlRaw(@"INSERT INTO temps_coureur (id_etape, id_coureur, heure_depart, heure_arrive)
                                                    SELECT  cd.id_etape, ca.id_coureur, cd.heure_depart, ca.heure_arrive
                                                        FROM coureur_depart cd
                                                            JOIN coureur_arrive ca ON cd.id_etape = ca.id_etape ");

                transaction.Commit(); // Commit la transaction
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de l'insertion des données : " + ex.Message);
                // La transaction sera rollback automatiquement car Commit() n'a pas été appelé
            }
        }
        
    }
    public void InsertDataFromTempPoints()
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                // Insérer les données dans la table Unite
                _dbContext.Database.ExecuteSqlRaw(@"INSERT INTO points (rang,point)
                                                    SELECT classement,points 
                                                        FROM temp_points");                
                transaction.Commit(); // Commit la transaction
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de l'insertion des données : " + ex.Message);
                // La transaction sera rollback automatiquement car Commit() n'a pas été appelé
            }
        }
    }
}