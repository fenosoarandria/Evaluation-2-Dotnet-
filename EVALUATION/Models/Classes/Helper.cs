using Microsoft.EntityFrameworkCore;

public static class Helpers
{
    // Méthode pour obtenir le profil utilisateur basé sur l'ID utilisateur à partir de la session
    // public static string? GetClientProfile(HttpContext context, ClientRepository client)
    // {
    //     // Obtenez l'ID utilisateur de la session
    //     string? userId = context.Session.GetString("Client");

    //     // Si l'ID utilisateur est présent, récupérez le profil utilisateur
    //     if (!string.IsNullOrEmpty(userId))
    //     {
    //         return client.FindById(userId);
    //     }

    //     // Si l'ID utilisateur est absent, renvoyez null
    //     return null;
    // }
    public static int CalculateAge(this DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }
    public static void ResetDataBase(ApplicationDbContext context)
    {
        string sql = @"
            DO $$
            DECLARE
                table_record RECORD;
            BEGIN
                -- Tronquer toutes les tables
                FOR table_record IN
                    SELECT table_name
                    FROM information_schema.tables
                    WHERE table_schema = 'public' AND table_type = 'BASE TABLE'
                LOOP
                    EXECUTE format('TRUNCATE TABLE %I RESTART IDENTITY CASCADE', table_record.table_name);
                END LOOP;
            END $$;
        ";
        context.Database.ExecuteSqlRaw(sql);
        context.Database.ExecuteSqlRaw(@"
            DO
            $$
            DECLARE
                seq_name text;
            BEGIN
                FOR seq_name IN SELECT sequence_name FROM information_schema.sequences WHERE sequence_schema = 'public' LOOP
                    EXECUTE 'ALTER SEQUENCE ' || seq_name || ' RESTART WITH 1';
                END LOOP;
            END;
            $$;
        ");
        // Ajout d'un utilisateur dans la table btp
        string request = "INSERT INTO administrateur(nom, email, mot_de_passe) VALUES ('ADMIN', 'admin@gmail.com', '12345')";
        string request1 = "INSERT INTO categorie(nom) VALUES ('Homme')";
        string request2 = "INSERT INTO categorie(nom) VALUES ('Femme')";
        string request3 = "INSERT INTO categorie(nom) VALUES ('Junior')";
        string request4 = "INSERT INTO categorie(nom) VALUES ('Senior')";
        context.Database.ExecuteSqlRaw(request);
        context.Database.ExecuteSqlRaw(request1);
        context.Database.ExecuteSqlRaw(request2);
        context.Database.ExecuteSqlRaw(request3);
        context.Database.ExecuteSqlRaw(request4);
        context.SaveChanges();
    }
    public static TimeSpan CalculDifferenceHeures(DateTime heureDebut, DateTime heureFin)
    {
        // Vérifier si l'heure de fin est avant l'heure de début (par exemple, si le jour suivant)
        if (heureFin < heureDebut)
        {
            // Ajouter une journée à l'heure de fin pour obtenir une heure valide
            heureFin = heureFin.AddDays(1);
        }

        // Calculer la différence entre les deux heures
        TimeSpan difference = heureFin - heureDebut;

        return difference;
    }
   
}
