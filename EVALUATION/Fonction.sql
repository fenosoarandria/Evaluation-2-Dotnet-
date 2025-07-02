
-- select * from classement_general_view where id_etape = 'ETP001' and id_equipe = 'EQP005';

-- insert into etape_equipe_penalite(id_etape,id_equipe,temps_penalite) values('ETP001','EQP001','05:00:00');
-- truncate table etape_equipe_penalite;
CREATE OR REPLACE FUNCTION f_coureur_categorie()
RETURNS VOID AS $$
DECLARE
    id_categorie_g VARCHAR;
    id_categorie_a VARCHAR;
    c_age INTEGER;
    coureur_row RECORD;
BEGIN

    FOR coureur_row IN 
        SELECT coureur.*, genre.nom as g_nom 
        FROM coureur 
        JOIN genre ON coureur.id_genre = genre.id 
        WHERE coureur.id NOT IN (SELECT id_coureur FROM categorie_coureur)
    LOOP
        IF coureur_row.g_nom = 'M' THEN
            id_categorie_g := (SELECT id FROM categorie WHERE nom = 'Homme');
        ELSE
            id_categorie_g := (SELECT id FROM categorie WHERE nom = 'Femme');
        END IF;

        SELECT EXTRACT(YEAR FROM AGE(coureur_row.date_de_naissance)) INTO c_age;

        IF c_age < 18 THEN
            id_categorie_a := (SELECT id FROM categorie WHERE nom = 'Junior');
        ELSE
            id_categorie_a := (SELECT id FROM categorie WHERE nom = 'Senior');
        END IF;

        INSERT INTO categorie_coureur (id_coureur, id_categorie) VALUES (coureur_row.id, id_categorie_g);
        INSERT INTO categorie_coureur (id_coureur, id_categorie) VALUES (coureur_row.id, id_categorie_a);
    END LOOP;
    
    RETURN;
END;
$$ LANGUAGE plpgsql;
    








