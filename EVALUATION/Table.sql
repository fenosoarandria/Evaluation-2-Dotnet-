-- \c postgres
-- Suppression de la base de données existante
-- DROP DATABASE IF EXISTS course;
-- Création de la base de données
CREATE DATABASE course;
-- Connexion à la base de données
\c course;

-- Création des séquences et des tables
CREATE SEQUENCE administrateur_seq;
CREATE TABLE IF NOT EXISTS administrateur(
    id VARCHAR PRIMARY KEY DEFAULT CONCAT('ADM', LPAD(nextval('administrateur_seq')::TEXT, 3, '0')),
    nom VARCHAR NOT NULL,
    email VARCHAR UNIQUE NOT NULL,
    mot_de_passe VARCHAR NOT NULL
);

CREATE SEQUENCE equipe_seq;
CREATE TABLE IF NOT EXISTS equipe(
    id VARCHAR PRIMARY KEY DEFAULT CONCAT('EQP', LPAD(nextval('equipe_seq')::TEXT, 3, '0')),
    nom VARCHAR NOT NULL,
    login VARCHAR UNIQUE NOT NULL,
    mot_de_passe VARCHAR NOT NULL
);

CREATE SEQUENCE genre_seq;
CREATE TABLE IF NOT EXISTS genre(
    id VARCHAR PRIMARY KEY DEFAULT CONCAT('GEN', LPAD(nextval('genre_seq')::TEXT, 3, '0')),
    nom VARCHAR UNIQUE NOT NULL --(Homme , femme)
);

CREATE SEQUENCE coureur_seq;
CREATE TABLE IF NOT EXISTS coureur(
    id VARCHAR PRIMARY KEY DEFAULT CONCAT('COU', LPAD(nextval('coureur_seq')::TEXT, 3, '0')),
    nom VARCHAR NOT NULL,
    numero_dossard INTEGER UNIQUE NOT NULL,
    date_de_naissance DATE NOT NULL,
    id_genre VARCHAR NOT NULL REFERENCES genre(id),
    id_equipe VARCHAR NOT NULL REFERENCES equipe(id)
);

CREATE SEQUENCE categorie_seq;
CREATE TABLE IF NOT EXISTS categorie(
    id VARCHAR PRIMARY KEY DEFAULT CONCAT('CAT', LPAD(nextval('categorie_seq')::TEXT, 3, '0')),
    nom VARCHAR UNIQUE NOT NULL --(junior,senior...)
);


CREATE SEQUENCE categorie_coureur_seq;
CREATE TABLE IF NOT EXISTS categorie_coureur(
    id VARCHAR PRIMARY KEY DEFAULT CONCAT('CAC', LPAD(nextval('categorie_coureur_seq')::TEXT, 3, '0')),
    id_coureur VARCHAR NOT NULL REFERENCES coureur(id),
    id_categorie VARCHAR NOT NULL REFERENCES categorie(id)
);

CREATE SEQUENCE etape_seq;
CREATE TABLE IF NOT EXISTS etape(
    id VARCHAR PRIMARY KEY DEFAULT CONCAT('ETP', LPAD(nextval('etape_seq')::TEXT, 3, '0')),
    nom VARCHAR NOT NULL,
    longueur NUMERIC NOT NULL, --En km
    nombre_coureur_equipe INTEGER NOT NULL,
    rang_etape INTEGER NOT NULL
);

CREATE SEQUENCE etape_coureur_seq;
CREATE TABLE etape_coureur (
    id VARCHAR PRIMARY KEY DEFAULT concat('ETC', LPAD(nextval('etape_coureur_seq')::TEXT, 3, '0')) ,
    id_etape VARCHAR NOT NULL REFERENCES etape(id),
    id_coureur VARCHAR NOT NULL REFERENCES coureur(id)
);

CREATE SEQUENCE temps_coureur_seq;
CREATE TABLE temps_coureur (
    id VARCHAR PRIMARY KEY DEFAULT concat('TEC', LPAD(nextval('temps_coureur_seq')::TEXT, 3, '0')),
    id_etape VARCHAR NOT NULL REFERENCES etape(id),
    id_coureur VARCHAR NOT NULL REFERENCES coureur(id),
    heure_depart TIMESTAMP NOT NULL,
    heure_arrive TIMESTAMP NOT NULL
);

CREATE SEQUENCE points_seq;
CREATE TABLE IF NOT EXISTS points (
    id VARCHAR PRIMARY KEY DEFAULT concat('PTS', LPAD(nextval('points_seq')::TEXT, 3, '0')),
    rang INTEGER NOT NULL,
    point NUMERIC NOT NULL
);

--Tsy ilaina ito
-- CREATE SEQUENCE points_coureur_seq;
-- CREATE TABLE IF NOT EXISTS points_coureur (
--     id VARCHAR PRIMARY KEY DEFAULT concat('POC', LPAD(nextval('points_coureur_seq')::TEXT, 3, '0')) ,
--     id_etape VARCHAR NOT NULL REFERENCES etape(id),
--     id_coureur VARCHAR NOT NULL REFERENCES coureur(id),
--     id_points VARCHAR NOT NULL REFERENCES points(id)
-- );

-- CREATE SEQUENCE penalite_seq;
-- CREATE TABLE IF NOT EXISTS penalite(
--     id VARCHAR PRIMARY KEY DEFAULT CONCAT('PEN', LPAD(nextval('penalite_seq')::TEXT, 3, '0')), 
--     nom VARCHAR NOT NULL,
--     temps_penalite TIME NOT NULL
-- );

CREATE SEQUENCE etape_equipe_penalite_seq;
CREATE TABLE IF NOT EXISTS etape_equipe_penalite(
    id VARCHAR PRIMARY KEY DEFAULT CONCAT('EEP', LPAD(nextval('etape_equipe_penalite_seq')::TEXT, 3, '0')), 
    id_etape VARCHAR REFERENCES etape(id),
    id_equipe VARCHAR REFERENCES equipe(id),
    temps_penalite TIME NOT NULL
);

CREATE TABLE IF NOT EXISTS temp_etape(
    etape VARCHAR NOT NULL,
    longueur NUMERIC NOT NULL,
    nb_coureur INTEGER NOT NULL,
    rang INTEGER NOT NULL,
    date_depart DATE NOT NULL,
    heure_depart TIME NOT NULL
);
CREATE TABLE IF NOT EXISTS temp_points(
    classement INTEGER NOT NULL,
    points INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS temp_resultat(
    etape_rang INTEGER NOT NULL,
    numero_dossard INTEGER NOT NULL,
    nom VARCHAR NOT NULL,
    genre VARCHAR NOT NULL,
    date_de_naissance DATE NOT NULL,
    equipe VARCHAR NOT NULL,
    arrive TIMESTAMP
);

CREATE SEQUENCE coureur_depart_seq;
CREATE TABLE IF NOT EXISTS coureur_depart(
    id VARCHAR PRIMARY KEY DEFAULT CONCAT('COD', LPAD(nextval('coureur_depart_seq')::TEXT, 3, '0')), 
    id_etape VARCHAR NOT NULL REFERENCES etape(id),
    heure_depart TIMESTAMP NOT NULL
);
CREATE SEQUENCE coureur_arrive_seq;
CREATE TABLE IF NOT EXISTS coureur_arrive(
    id VARCHAR PRIMARY KEY DEFAULT CONCAT('COA', LPAD(nextval('coureur_arrive_seq')::TEXT, 3, '0')), 
    id_etape VARCHAR NOT NULL REFERENCES etape(id),
    id_coureur VARCHAR NOT NULL REFERENCES coureur(id),
    heure_arrive TIMESTAMP NOT NULL
);
------------------------------------Temp etape-----------------------------------------------
 INSERT INTO categorie(nom) VALUES ('Homme');
INSERT INTO categorie(nom) VALUES ('Femme');
INSERT INTO categorie(nom) VALUES ('Junior');
INSERT INTO categorie(nom) VALUES ('Senior');
INSERT INTO administrateur(nom, email, mot_de_passe) VALUES ('ADMIN', 'admin@gmail.com', '12345');

--------------------------------------------------------------------------------------------------
CREATE SEQUENCE coefficient_etape_seq;
CREATE TABLE coefficient_etape(
    id VARCHAR PRIMARY KEY DEFAULT CONCAT('COE', LPAD(nextval('coefficient_etape_seq')::TEXT, 3, '0')), 
    id_etape VARCHAR REFERENCES etape(id),
    coefficient NUMERIC
);

CREATE OR REPLACE VIEW classement_general_coefficient_etape AS
    SELECT 
    c.*,
    COALESCE(ce.coefficient, 1) AS coefficient
    FROM classement_general_view c
    LEFT JOIN coefficient_etape ce ON c.id_etape = ce.id_etape ;


CREATE OR REPLACE VIEW classement_general_coefficient_etape_rang AS
    SELECT 
    cc.*,
    coefficient * point as point_coefficient,
    DENSE_RANK() OVER(PARTITION BY cc.id_etape ORDER BY (point * coefficient)) AS rang_coefficient
    FROM classement_general_coefficient_etape as cc;
--------------------------------------------------------------------------------------
-- INSERT INTO coefficient_etape(id_etape,coefficient) VALUES ('ETP002',4);
