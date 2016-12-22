
CREATE TABLE lieu (
                lieu_id BIGINT IDENTITY NOT NULL,
                label VARCHAR(256) NOT NULL,
                adresse VARCHAR(256),
                capacite_max INT,
                CONSTRAINT lieu_pk PRIMARY KEY (lieu_id)
)

CREATE TABLE utilisateur (
                utilisateur_id BIGINT IDENTITY NOT NULL,
                login VARCHAR(256) NOT NULL,
                password VARCHAR(256) NOT NULL,
                prenom VARCHAR(256) NOT NULL,
                nom VARCHAR(256) NOT NULL,
                type_user VARCHAR(20) NOT NULL,
                adresse VARCHAR(256),
                telephone VARCHAR(12),
                CONSTRAINT utilisateur_pk PRIMARY KEY (utilisateur_id)
)

CREATE TABLE discipline (
                discipline_id BIGINT IDENTITY NOT NULL,
                responsable_discipline_id BIGINT NOT NULL,
                label VARCHAR(265) NOT NULL,
                description Text,
                CONSTRAINT discipline_pk PRIMARY KEY (discipline_id)
)

CREATE TABLE section (
                section_id BIGINT IDENTITY NOT NULL,
                description TEXT,
                discipline_id BIGINT NOT NULL,
                label VARCHAR(256) NOT NULL,
                prix VARCHAR(256) NOT NULL,
                CONSTRAINT section_pk PRIMARY KEY (section_id)
)

CREATE TABLE responsable (
                utilisateur_id BIGINT NOT NULL,
                section_id BIGINT NOT NULL,
                CONSTRAINT responsable_pk PRIMARY KEY (utilisateur_id, section_id)
)

CREATE TABLE seance (
                seance_id BIGINT IDENTITY NOT NULL,
                encadrant_id BIGINT NOT NULL,
                lieu_id BIGINT NOT NULL,
                section_id BIGINT NOT NULL,
                places_max INT NOT NULL,
                CONSTRAINT seance_pk PRIMARY KEY (seance_id)
)

CREATE TABLE creneau (
                creneau_id BIGINT NOT NULL,
                seance_id BIGINT NOT NULL,
                heure_debut DATETIME NOT NULL,
                heure_fin DATETIME NOT NULL,
                jour_de_la_semaine VARCHAR(10) NOT NULL,
                CONSTRAINT creneau_pk PRIMARY KEY (creneau_id)
)

CREATE TABLE participe (
                utilisateur_id BIGINT NOT NULL,
                seance_id BIGINT NOT NULL,
                a_payer BIT NOT NULL,
                CONSTRAINT participe_pk PRIMARY KEY (utilisateur_id, seance_id)
)

CREATE TABLE document (
                document_id BIGINT IDENTITY NOT NULL,
                utilisateur_id BIGINT NOT NULL,
                type_document VARCHAR(256) NOT NULL,
                path_document VARCHAR(256) NOT NULL,
                CONSTRAINT document_pk PRIMARY KEY (document_id)
)

ALTER TABLE seance ADD CONSTRAINT lieu_creneau_fk
FOREIGN KEY (lieu_id)
REFERENCES lieu (lieu_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE document ADD CONSTRAINT adherent_document_fk
FOREIGN KEY (utilisateur_id)
REFERENCES utilisateur (utilisateur_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE discipline ADD CONSTRAINT user_discipline_fk
FOREIGN KEY (responsable_discipline_id)
REFERENCES utilisateur (utilisateur_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE participe ADD CONSTRAINT user_participe_fk
FOREIGN KEY (utilisateur_id)
REFERENCES utilisateur (utilisateur_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE responsable ADD CONSTRAINT user_responsable_fk
FOREIGN KEY (utilisateur_id)
REFERENCES utilisateur (utilisateur_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE seance ADD CONSTRAINT user_seance_fk
FOREIGN KEY (encadrant_id)
REFERENCES utilisateur (utilisateur_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE section ADD CONSTRAINT discipline_section_fk
FOREIGN KEY (discipline_id)
REFERENCES discipline (discipline_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE seance ADD CONSTRAINT section_creneau_fk
FOREIGN KEY (section_id)
REFERENCES section (section_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE responsable ADD CONSTRAINT section_responsable_fk
FOREIGN KEY (section_id)
REFERENCES section (section_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE participe ADD CONSTRAINT creneau_participe_fk
FOREIGN KEY (seance_id)
REFERENCES seance (seance_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE creneau ADD CONSTRAINT seance_creneau_fk
FOREIGN KEY (seance_id)
REFERENCES seance (seance_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION