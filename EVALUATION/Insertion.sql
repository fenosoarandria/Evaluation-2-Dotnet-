-- Insertion de données de test
-- Administrateur
-- INSERT INTO administrateur(nom, email, mot_de_passe) VALUES ('ADMIN', 'admin@gmail.com', '12345');

-- Equipe
INSERT INTO equipe(nom, login, mot_de_passe) VALUES ('FENOSOA', 'fenosoa@gmail.com', '12345');
INSERT INTO equipe(nom, login, mot_de_passe) VALUES ('fitahiana', 'fitahiana@gmail.com', '12345');

-- Genre
INSERT INTO genre(nom) VALUES ('Homme');
INSERT INTO genre(nom) VALUES ('Femme');

-- Coureur
INSERT INTO coureur(nom, numero_dossard, date_de_naissance, id_genre, id_equipe) VALUES 
('Coureur 1', 101, '1990-01-01', 'GEN001', 'EQP001'),
('Coureur 2', 102, '1992-02-02', 'GEN001', 'EQP001'),
('Coureur 3', 103, '1994-03-03', 'GEN002', 'EQP001'),
('Coureur 4', 104, '1994-03-04', 'GEN002', 'EQP001'),
('Coureur 5', 105, '1994-03-05', 'GEN002', 'EQP001'),
('Coureur 6', 106, '1994-03-06', 'GEN001', 'EQP002'),
('Coureur 7', 107, '1994-03-07', 'GEN001', 'EQP002'),
('Coureur 8', 108, '1994-03-08', 'GEN001', 'EQP002');

-- Catégorie
-- INSERT INTO categorie(nom) VALUES ('Junior');
-- INSERT INTO categorie(nom) VALUES ('Senior');
-- INSERT INTO categorie(nom) VALUES ('Homme');
-- INSERT INTO categorie(nom) VALUES ('Femme');


-- Catégorie Coureur
-- INSERT INTO categorie_coureur(id_coureur, id_categorie) VALUES 
-- ('COU001', 'CAT001'),
-- ('COU002', 'CAT002'),
-- ('COU003', 'CAT002');

-- Etape
INSERT INTO etape(nom, longueur, nombre_coureur_equipe, rang_etape) VALUES 
('Etape 1', 10.5, 3, 3),
('Etape 2', 15.0, 3, 2);

-- Etape Coureur
INSERT INTO coureur_depart(id_etape, heure_depart) VALUES 
('ETP001', '2024-06-01 08:00:00'),
('ETP002', '2024-06-02 05:00:00');
-- ('ETP001', 'COU003'),
-- ('ETP002', 'COU001'),
-- ('ETP002', 'COU002'),
-- ('ETP002', 'COU003');

-- Temps Coureur
-- INSERT INTO temps_coureur(id_etape, id_coureur, heure_depart, heure_arrive) VALUES 
-- ('ETP001', 'COU001', '2023-06-01 08:00:00', '2023-06-01 08:30:00'),
-- ('ETP001', 'COU002', '2023-06-01 08:05:00', '2023-06-01 08:35:00'),
-- ('ETP001', 'COU003', '2023-06-01 08:10:00', '2023-06-01 08:40:00'),
-- ('ETP002', 'COU001', '2023-06-02 08:00:00', '2023-06-02 08:50:00'),
-- ('ETP002', 'COU002', '2023-06-02 08:05:00', '2023-06-02 08:55:00'),
-- ('ETP002', 'COU003', '2023-06-02 08:10:00', '2023-06-02 09:00:00');

-- Points
INSERT INTO points(rang, point) VALUES 
(1, 10),
(2, 6),
(3, 4),
(4, 3),
(5, 1);

-- -- Points Coureur
-- INSERT INTO points_coureur(id_etape, id_coureur, id_points) VALUES 
-- ('ETP001', 'COU001', 'PTS001'),
-- ('ETP001', 'COU002', 'PTS002'),
-- ('ETP001', 'COU003', 'PTS003'),
-- ('ETP002', 'COU001', 'PTS001'),
-- ('ETP002', 'COU002', 'PTS002'),
-- ('ETP002', 'COU003', 'PTS003');

-- -- Pénalité
-- INSERT INTO penalite(nom, temps_penalite) VALUES 
-- ('Pénalité 1', '00:05:00'),
-- ('Pénalité 2', '00:10:00');




SELECT id_equipe,id_coureur,sum(point) from classement_general_view where id_equipe = 'EQP001' group by id_equipe ,id_coureur;