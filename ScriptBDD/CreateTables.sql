
CREATE TABLE lieu (
                lieu_id VARCHAR NOT NULL,
                label VARCHAR NOT NULL,
                adresse VARCHAR,
                capacite_max INT,
                CONSTRAINT lieu_pk PRIMARY KEY (lieu_id)
)

CREATE TABLE utilisateur (
                utilisateur_id BIGINT IDENTITY NOT NULL,
                login VARCHAR NOT NULL,
                password VARCHAR NOT NULL,
                prenom VARCHAR NOT NULL,
                nom VARCHAR NOT NULL,
                type_utilisateur VARCHAR NOT NULL,
                adresse VARCHAR,
                telephone VARCHAR,
                CONSTRAINT utilisateur_pk PRIMARY KEY (utilisateur_id)
)

CREATE TABLE discipline (
                discipline_id BIGINT IDENTITY NOT NULL,
                responsable_discipline_id BIGINT NOT NULL,
                label VARCHAR NOT NULL,
                description Text,
                CONSTRAINT discipline_pk PRIMARY KEY (discipline_id)
)

CREATE TABLE section (
                section_id VARCHAR NOT NULL,
                description TEXT,
                discipline_id BIGINT NOT NULL,
                label VARCHAR NOT NULL,
                prix VARCHAR NOT NULL,
                CONSTRAINT section_pk PRIMARY KEY (section_id)
)

CREATE TABLE responsable (
                utilisateur_id BIGINT NOT NULL,
                section_id VARCHAR NOT NULL,
                CONSTRAINT responsable_pk PRIMARY KEY (utilisateur_id, section_id)
)

CREATE TABLE seance (
                seance_id BIGINT IDENTITY NOT NULL,
                encadrant_id BIGINT NOT NULL,
                lieu_id VARCHAR NOT NULL,
                section_id VARCHAR NOT NULL,
                repetition VARCHAR,
                places_max INT NOT NULL,
                CONSTRAINT seance_pk PRIMARY KEY (seance_id)
)

CREATE TABLE creneau (
                creneau_id BIGINT NOT NULL,
                seance_id BIGINT NOT NULL,
                heure_debut DATETIME NOT NULL,
                heure_fin DATETIME NOT NULL,
                jour_de_la_semaine VARCHAR NOT NULL,
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
                type_document VARCHAR NOT NULL,
                path_document VARCHAR NOT NULL,
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

ALTER TABLE discipline ADD CONSTRAINT utilisateur_discipline_fk
FOREIGN KEY (responsable_discipline_id)
REFERENCES utilisateur (utilisateur_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE participe ADD CONSTRAINT utilisateur_participe_fk
FOREIGN KEY (utilisateur_id)
REFERENCES utilisateur (utilisateur_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE responsable ADD CONSTRAINT utilisateur_responsable_fk
FOREIGN KEY (utilisateur_id)
REFERENCES utilisateur (utilisateur_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE seance ADD CONSTRAINT utilisateur_seance_fk
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