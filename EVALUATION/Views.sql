
-------------------------------------------

CREATE OR REPLACE VIEW classement AS
WITH penalites_par_etape_equipe AS (
    SELECT
        id_etape,
        id_equipe,
        SUM(temps_penalite::interval) AS somme_temps_penalites -- Somme des temps de pénalité convertis en INTERVAL
    FROM
        etape_equipe_penalite
    GROUP BY
        id_etape,
        id_equipe
),
classement AS (
    SELECT
        t.id_etape,
        t.id_coureur,
        c.id_equipe,
        t.heure_depart,
        t.heure_arrive,
        t.heure_arrive - t.heure_depart AS duree,
        COALESCE(pe.somme_temps_penalites, INTERVAL '0 seconds') AS temps_penalite, -- Utiliser la somme des pénalités
        DENSE_RANK() OVER(PARTITION BY t.id_etape ORDER BY (t.heure_arrive + COALESCE(pe.somme_temps_penalites, INTERVAL '0 seconds')) - t.heure_depart) AS rang
    FROM
        temps_coureur t
    JOIN
        coureur c ON t.id_coureur = c.id
    LEFT JOIN
        penalites_par_etape_equipe pe ON t.id_etape = pe.id_etape AND c.id_equipe = pe.id_equipe
)
SELECT
    classement.id_etape,
    classement.id_coureur,
    classement.id_equipe,
    classement.heure_depart,
    classement.heure_arrive,
    classement.duree + classement.temps_penalite AS duree_totale, -- Ajoute la pénalité à la durée totale
    classement.rang,
    classement.temps_penalite,
    COALESCE(points.point, 0) AS point
FROM
    classement
LEFT JOIN
    points ON classement.rang = points.rang
ORDER BY
    classement.id_etape,
    duree_totale;


--Classement general
CREATE OR REPLACE VIEW classement_general_view AS
    SELECT 
    c.*, 
    et.nom as etape,
    et.longueur ,
    et.nombre_coureur_equipe,
    et.rang_etape,
    co.nom as coureur,
    co.numero_dossard,
    co.date_de_naissance,
    co.id_genre,
    g.nom as genre,
    eq.nom as equipe
FROM 
    classement c
LEFT JOIN 
    coureur co ON c.id_coureur = co.id
LEFT JOIN 
    genre g ON co.id_genre = g.id
LEFT JOIN 
    equipe eq ON c.id_equipe = eq.id
LEFT JOIN 
    etape et ON c.id_etape = et.id;




-- CLASSEMENT CATEGORIE
CREATE OR REPLACE VIEW classement_categorie AS
WITH penalites_par_etape_equipe AS (
    SELECT
        id_etape,
        id_equipe,
        SUM(temps_penalite::interval) AS somme_temps_penalites -- Somme des temps de pénalité convertis en INTERVAL
    FROM
        etape_equipe_penalite
    GROUP BY
        id_etape,
        id_equipe
),
 classement AS (
    SELECT
        t.id_etape,
        t.id_coureur,
        c.id_equipe,
        cc.id_categorie,
        t.heure_depart,
        t.heure_arrive,
        t.heure_arrive - t.heure_depart AS duree,
        COALESCE(pe.somme_temps_penalites, INTERVAL '0 seconds') AS temps_penalite, -- Utiliser la somme des pénalités
        DENSE_RANK() OVER(PARTITION BY t.id_etape,cc.id_categorie ORDER BY (t.heure_arrive + COALESCE(pe.somme_temps_penalites, INTERVAL '0 seconds')) - t.heure_depart) AS rang
      FROM
        temps_coureur t
    JOIN
        coureur c ON t.id_coureur = c.id
    JOIN
        categorie_coureur cc ON t.id_coureur = cc.id_coureur
    LEFT JOIN
        penalites_par_etape_equipe pe ON t.id_etape = pe.id_etape AND c.id_equipe = pe.id_equipe
)
SELECT
    classement.id_etape,
    classement.id_coureur,
    classement.id_equipe,
    classement.id_categorie,
    classement.heure_depart,
    classement.heure_arrive,
    classement.duree + classement.temps_penalite AS duree_totale, -- Ajoute la pénalité à la durée totale
    classement.rang,
    classement.temps_penalite,
    COALESCE(points.point, 0) AS point
FROM
    classement
LEFT JOIN
    points ON classement.rang = points.rang;


CREATE OR REPLACE VIEW classement_general_categorie_view AS
    SELECT 
    c.*, 
    et.nom as etape,
    et.longueur ,
    et.nombre_coureur_equipe,
    et.rang_etape,
    co.nom as coureur,
    co.numero_dossard,
    co.date_de_naissance,
    co.id_genre,
    g.nom as genre,
    eq.nom as equipe,
    cat.nom as nom_categorie
FROM 
    classement_categorie c
LEFT JOIN 
    coureur co ON c.id_coureur = co.id
LEFT JOIN 
    genre g ON co.id_genre = g.id
LEFT JOIN 
    equipe eq ON c.id_equipe = eq.id
LEFT JOIN 
    etape et ON c.id_etape = et.id
LEFT JOIN 
    categorie cat ON c.id_categorie = cat.id;




