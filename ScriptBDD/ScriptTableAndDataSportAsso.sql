USE [master]
GO
/****** Object:  Database [SportAsso]    Script Date: 13/01/2017 19:27:33 ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'SportAsso')
BEGIN
CREATE DATABASE [SportAsso]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SportAsso', FILENAME = N'D:\Programmes\SqlServer\MSSQL12.MSSQLSERVER\MSSQL\DATA\SportAsso.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SportAsso_log', FILENAME = N'D:\Programmes\SqlServer\MSSQL12.MSSQLSERVER\MSSQL\DATA\SportAsso_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END

GO
ALTER DATABASE [SportAsso] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SportAsso].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SportAsso] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SportAsso] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SportAsso] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SportAsso] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SportAsso] SET ARITHABORT OFF 
GO
ALTER DATABASE [SportAsso] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SportAsso] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SportAsso] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SportAsso] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SportAsso] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SportAsso] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SportAsso] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SportAsso] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SportAsso] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SportAsso] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SportAsso] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SportAsso] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SportAsso] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SportAsso] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SportAsso] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SportAsso] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SportAsso] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SportAsso] SET RECOVERY FULL 
GO
ALTER DATABASE [SportAsso] SET  MULTI_USER 
GO
ALTER DATABASE [SportAsso] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SportAsso] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SportAsso] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SportAsso] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SportAsso] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SportAsso', N'ON'
GO
USE [SportAsso]
GO
/****** Object:  Table [dbo].[discipline]    Script Date: 13/01/2017 19:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[discipline]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[discipline](
	[discipline_id] [bigint] IDENTITY(1,1) NOT NULL,
	[responsable_discipline_id] [bigint] NOT NULL,
	[label] [varchar](265) NOT NULL,
	[description] [text] NULL,
 CONSTRAINT [discipline_pk] PRIMARY KEY CLUSTERED 
(
	[discipline_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[document]    Script Date: 13/01/2017 19:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[document]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[document](
	[document_id] [bigint] IDENTITY(1,1) NOT NULL,
	[utilisateur_id] [bigint] NOT NULL,
	[type_document] [varchar](256) NOT NULL,
	[path_document] [varchar](256) NOT NULL,
	[valide] [int] NOT NULL,
 CONSTRAINT [document_pk] PRIMARY KEY CLUSTERED 
(
	[document_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[lieu]    Script Date: 13/01/2017 19:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lieu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[lieu](
	[lieu_id] [bigint] IDENTITY(1,1) NOT NULL,
	[label] [varchar](256) NOT NULL,
	[adresse] [varchar](256) NULL,
	[capacite_max] [int] NULL,
 CONSTRAINT [lieu_pk] PRIMARY KEY CLUSTERED 
(
	[lieu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[participe]    Script Date: 13/01/2017 19:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[participe]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[participe](
	[utilisateur_id] [bigint] NOT NULL,
	[seance_id] [bigint] NOT NULL,
	[a_payer] [bit] NOT NULL,
 CONSTRAINT [participe_pk] PRIMARY KEY CLUSTERED 
(
	[utilisateur_id] ASC,
	[seance_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[seance]    Script Date: 13/01/2017 19:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[seance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[seance](
	[seance_id] [bigint] IDENTITY(1,1) NOT NULL,
	[encadrant_id] [bigint] NOT NULL,
	[lieu_id] [bigint] NOT NULL,
	[section_id] [bigint] NOT NULL,
	[heure_debut] [time](7) NOT NULL,
	[heure_fin] [time](7) NOT NULL,
	[jour_de_la_semaine] [varchar](10) NOT NULL,
	[places_max] [int] NOT NULL,
 CONSTRAINT [seance_pk] PRIMARY KEY CLUSTERED 
(
	[seance_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[section]    Script Date: 13/01/2017 19:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[section]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[section](
	[section_id] [bigint] IDENTITY(1,1) NOT NULL,
	[description] [text] NULL,
	[discipline_id] [bigint] NOT NULL,
	[label] [varchar](256) NOT NULL,
	[prix] [varchar](256) NOT NULL,
	[responsable_id] [bigint] NULL,
 CONSTRAINT [section_pk] PRIMARY KEY CLUSTERED 
(
	[section_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[utilisateur]    Script Date: 13/01/2017 19:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[utilisateur]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[utilisateur](
	[utilisateur_id] [bigint] IDENTITY(1,1) NOT NULL,
	[login] [varchar](256) NOT NULL,
	[password] [varchar](256) NOT NULL,
	[prenom] [varchar](256) NOT NULL,
	[nom] [varchar](256) NOT NULL,
	[type_user] [varchar](20) NOT NULL,
	[adresse] [varchar](256) NULL,
	[telephone] [varchar](12) NULL,
 CONSTRAINT [utilisateur_pk] PRIMARY KEY CLUSTERED 
(
	[utilisateur_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[discipline] ON 

INSERT [dbo].[discipline] ([discipline_id], [responsable_discipline_id], [label], [description]) VALUES (1, 3, N'Aïkido', N'L''aïkido est un art martial japonais (budo), fondé par Morihei Ueshiba O sensei entre 1925 et 1969. L''aïkido a été officiellement reconnu par le gouvernement japonais en 1940 sous le nom d’aikibudo1 et sous le nom Aikido en 1942 donné par la « Dai Nippon Butoku Kai », organisme gouvernemental visant à regrouper tous les arts martiaux japonais pendant la guerre. Il a été créé à partir de l''expérience que son fondateur avait de l''enseignement des koryu (écoles d''arts martiaux anciennes), essentiellement l''aikijutsu de l''école daito ryu et le kenjutsu2 (art du sabre japonais). L''aïkido est né de la rencontre entre ces techniques de combat et une réflexion métaphysique de Morihei Ueshiba sur le sens de la pratique martiale à l''ère moderne.')
INSERT [dbo].[discipline] ([discipline_id], [responsable_discipline_id], [label], [description]) VALUES (2, 4, N'Athletisme', N'L’athlétisme est un sport qui comporte un ensemble de disciplines regroupées en courses, sauts, lancers, épreuves combinées et marche. L''origine du mot athlétisme vient du grec "Athlos", signifiant combat. Il s’agit de l’art de dépasser la performance des adversaires en vitesse ou en endurance, en distance ou en hauteur. Le nombre d''épreuves, individuelles ou par équipes, a varié avec le temps et les mentalités. L''athlétisme est l''un des rares sports universellement pratiqués, que ce soit dans le monde amateur ou au cours de nombreuses compétitions de tous niveaux. La simplicité et le peu de moyens nécessaires à sa pratique expliquent en partie ce succès. Les premières traces de concours athlétiques remontent aux civilisations antiques. La discipline s''est développée au cours des siècles, des premières épreuves à sa codification.')
INSERT [dbo].[discipline] ([discipline_id], [responsable_discipline_id], [label], [description]) VALUES (3, 3, N'Aviron', N'L''aviron fait partie de la famille des sports nautiques. C''est un sport olympique depuis la création des Jeux olympiques modernes en 1896 sous l''impulsion du Baron Pierre de Coubertin. Ce sport consiste à propulser un bateau à l''aide de rames, également appelées avirons, ou communément dans le milieu des pratiquants francophones « pelles ». On distingue deux catégories : l''aviron de rivière et l''aviron de mer.')
INSERT [dbo].[discipline] ([discipline_id], [responsable_discipline_id], [label], [description]) VALUES (4, 3, N'Badminton', N'Le badminton est un sport de raquette qui oppose soit deux joueurs ou joueuses (simples), soit deux paires (doubles), placés dans deux demi-terrains séparés par un filet. Les joueurs et joueuses, appelés badistes, marquent des points en frappant un volant à l''aide d''une raquette pour le faire tomber dans le terrain adverse. L''échange se termine dès que le volant touche le sol, ou s''il y a faute.')
INSERT [dbo].[discipline] ([discipline_id], [responsable_discipline_id], [label], [description]) VALUES (5, 3, N'Escrime', N'L’escrime est un sport de combat. Il s’agit de l’art de toucher un adversaire avec la pointe ou le tranchant (estoc et taille) d’une arme blanche sur les parties valables sans être touché.

On utilise trois types d''armes : l’épée (discipline olympique depuis 1900 pour les hommes et 1956 pour les femmes), le sabre (discipline olympique depuis 1896 pour les hommes et 2004 pour les femmes) et le fleuret (discipline olympique depuis 1896 pour les hommes et 1924 pour les femmes). Ces trois armes sont sexuées : épée féminine et masculine, fleuret féminin et masculin et sabre féminin et masculin. Les épreuves sont individuelles ou par équipes. Elles sont donc au nombre de douze.')
SET IDENTITY_INSERT [dbo].[discipline] OFF
SET IDENTITY_INSERT [dbo].[document] ON 

INSERT [dbo].[document] ([document_id], [utilisateur_id], [type_document], [path_document], [valide]) VALUES (1, 1, N'test', N'test', 0)
SET IDENTITY_INSERT [dbo].[document] OFF
SET IDENTITY_INSERT [dbo].[lieu] ON 

INSERT [dbo].[lieu] ([lieu_id], [label], [adresse], [capacite_max]) VALUES (1, N'Gymenase Louis Ferrier', N'22 Rue Napoléon', 500)
INSERT [dbo].[lieu] ([lieu_id], [label], [adresse], [capacite_max]) VALUES (2, N'Stade Jule Césear', N'2 Avenue  Blondie', 1000)
INSERT [dbo].[lieu] ([lieu_id], [label], [adresse], [capacite_max]) VALUES (3, N'Salle Polyvalente Guérande', N'34 Boulevard Malherbes', 20)
SET IDENTITY_INSERT [dbo].[lieu] OFF
INSERT [dbo].[participe] ([utilisateur_id], [seance_id], [a_payer]) VALUES (5, 2, 1)
INSERT [dbo].[participe] ([utilisateur_id], [seance_id], [a_payer]) VALUES (5, 10003, 0)
INSERT [dbo].[participe] ([utilisateur_id], [seance_id], [a_payer]) VALUES (8, 1, 0)
INSERT [dbo].[participe] ([utilisateur_id], [seance_id], [a_payer]) VALUES (8, 2, 0)
SET IDENTITY_INSERT [dbo].[seance] ON 

INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (1, 3, 1, 1, CAST(N'14:00:00' AS Time), CAST(N'20:00:00' AS Time), N'Mardi', 20)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (2, 3, 3, 1, CAST(N'17:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Samedi', 50)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10002, 3, 1, 3, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Lundi', 20)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10003, 4, 2, 1, CAST(N'14:00:00' AS Time), CAST(N'20:00:00' AS Time), N'Jeudi', 50)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10005, 11, 3, 2, CAST(N'12:20:00' AS Time), CAST(N'14:45:00' AS Time), N'Mercredi', 50)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10006, 3, 1, 5, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Samedi', 55)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10007, 3, 1, 15, CAST(N'14:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Dimanche', 10)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10008, 3, 1, 4, CAST(N'07:08:00' AS Time), CAST(N'09:00:00' AS Time), N'Jeudi', 10)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10009, 3, 1, 6, CAST(N'08:30:00' AS Time), CAST(N'20:00:00' AS Time), N'Lundi', 300)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10010, 3, 1, 7, CAST(N'05:50:00' AS Time), CAST(N'10:00:00' AS Time), N'Mercredi', 50)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10011, 3, 1, 13, CAST(N'10:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Vendredi', 60)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10012, 3, 1, 10, CAST(N'16:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Lundi', 88)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10013, 3, 1, 11, CAST(N'14:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Mardi', 65)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10014, 3, 1, 14, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Mercredi', 50)
INSERT [dbo].[seance] ([seance_id], [encadrant_id], [lieu_id], [section_id], [heure_debut], [heure_fin], [jour_de_la_semaine], [places_max]) VALUES (10015, 3, 1, 12, CAST(N'10:00:00' AS Time), CAST(N'14:45:00' AS Time), N'Vendredi', 20)
SET IDENTITY_INSERT [dbo].[seance] OFF
SET IDENTITY_INSERT [dbo].[section] ON 

INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (1, N'Cours Enfants', 1, N'Enfants', N'22€', 3)
INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (2, N'Cours Adulte', 1, N'Adultes', N'55€', 3)
INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (3, N'Préparations aux marathons', 2, N'Marathons', N'37€', 4)
INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (4, N'Cours Enfants', 2, N'Enfants', N'10€', 4)
INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (5, N'Préparations au compétition', 3, N'Compétition', N'120€', 3)
INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (6, N'Les petits baigneurs', 3, N'Cours Initiation', N'20€', 4)
INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (7, N'Cours Loisir', 4, N'Loisir', N'20€', 4)
INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (10, N'Cours d''aviron à l''attention des enfants', 3, N'Cours Enfant', N'12€', 11)
INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (11, N'Cours d''aviron en binôme pour adulte', 3, N'Cours Adulte', N'50€', 11)
INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (12, N'Cours loisirs', 4, N'Cours loisirs', N'10€', 12)
INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (13, N'Cours d''initation au Fleuret', 5, N'Fleuret', N'50€', 4)
INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (14, N'Cours d''épé', 1, N'Epée', N'50€', 11)
INSERT [dbo].[section] ([section_id], [description], [discipline_id], [label], [prix], [responsable_id]) VALUES (15, N'Cours de Sabre', 5, N'Sabre', N'50€', 3)
SET IDENTITY_INSERT [dbo].[section] OFF
SET IDENTITY_INSERT [dbo].[utilisateur] ON 

INSERT [dbo].[utilisateur] ([utilisateur_id], [login], [password], [prenom], [nom], [type_user], [adresse], [telephone]) VALUES (1, N'flo@gmail.com', N'flo', N'toto', N'pizzala', N'admin', N'116 chemin des bartavelles', N'0621682208')
INSERT [dbo].[utilisateur] ([utilisateur_id], [login], [password], [prenom], [nom], [type_user], [adresse], [telephone]) VALUES (2, N'admin@gmail.com', N'admin', N'Jean', N'Bono', N'admin', NULL, NULL)
INSERT [dbo].[utilisateur] ([utilisateur_id], [login], [password], [prenom], [nom], [type_user], [adresse], [telephone]) VALUES (3, N'lola@gmail.com', N'lola', N'Lola', N'Maines', N'encadrant', NULL, NULL)
INSERT [dbo].[utilisateur] ([utilisateur_id], [login], [password], [prenom], [nom], [type_user], [adresse], [telephone]) VALUES (4, N'Steph@gmail.com', N'Steph', N'Stéphaine', N'De Monaco', N'encadrant', NULL, NULL)
INSERT [dbo].[utilisateur] ([utilisateur_id], [login], [password], [prenom], [nom], [type_user], [adresse], [telephone]) VALUES (5, N'test@test.com', N'Test01@', N'Pierre', N'Palmade', N'adherent', N'Test', N'0621682208')
INSERT [dbo].[utilisateur] ([utilisateur_id], [login], [password], [prenom], [nom], [type_user], [adresse], [telephone]) VALUES (6, N'tata@tata.com', N'Tata01@', N'Tata', N'tata', N'adherent', N'tata', N'0621682208')
INSERT [dbo].[utilisateur] ([utilisateur_id], [login], [password], [prenom], [nom], [type_user], [adresse], [telephone]) VALUES (8, N'test2@test.com', N'Test02@', N'Jennifer', N'Manfredo', N'adherent', NULL, NULL)
INSERT [dbo].[utilisateur] ([utilisateur_id], [login], [password], [prenom], [nom], [type_user], [adresse], [telephone]) VALUES (9, N'test4@test.com', N'Test04@', N'Test4', N'Test4', N'adherent', NULL, NULL)
INSERT [dbo].[utilisateur] ([utilisateur_id], [login], [password], [prenom], [nom], [type_user], [adresse], [telephone]) VALUES (10, N'guillaume.broder@mail.com', N'guillaume', N'Guillaume', N'Border', N'adherent', NULL, NULL)
INSERT [dbo].[utilisateur] ([utilisateur_id], [login], [password], [prenom], [nom], [type_user], [adresse], [telephone]) VALUES (11, N'jaques@gmail.com', N'jacques', N'Jacques', N'Richard', N'encadrant', NULL, NULL)
INSERT [dbo].[utilisateur] ([utilisateur_id], [login], [password], [prenom], [nom], [type_user], [adresse], [telephone]) VALUES (12, N'mireille@gmail.com', N'mireille', N'Mirrielle', N'LeBlanc', N'encadrant', NULL, NULL)
SET IDENTITY_INSERT [dbo].[utilisateur] OFF
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_discipline_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[discipline]'))
ALTER TABLE [dbo].[discipline]  WITH CHECK ADD  CONSTRAINT [user_discipline_fk] FOREIGN KEY([responsable_discipline_id])
REFERENCES [dbo].[utilisateur] ([utilisateur_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_discipline_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[discipline]'))
ALTER TABLE [dbo].[discipline] CHECK CONSTRAINT [user_discipline_fk]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[adherent_document_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[document]'))
ALTER TABLE [dbo].[document]  WITH CHECK ADD  CONSTRAINT [adherent_document_fk] FOREIGN KEY([utilisateur_id])
REFERENCES [dbo].[utilisateur] ([utilisateur_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[adherent_document_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[document]'))
ALTER TABLE [dbo].[document] CHECK CONSTRAINT [adherent_document_fk]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[creneau_participe_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[participe]'))
ALTER TABLE [dbo].[participe]  WITH CHECK ADD  CONSTRAINT [creneau_participe_fk] FOREIGN KEY([seance_id])
REFERENCES [dbo].[seance] ([seance_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[creneau_participe_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[participe]'))
ALTER TABLE [dbo].[participe] CHECK CONSTRAINT [creneau_participe_fk]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_participe_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[participe]'))
ALTER TABLE [dbo].[participe]  WITH CHECK ADD  CONSTRAINT [user_participe_fk] FOREIGN KEY([utilisateur_id])
REFERENCES [dbo].[utilisateur] ([utilisateur_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_participe_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[participe]'))
ALTER TABLE [dbo].[participe] CHECK CONSTRAINT [user_participe_fk]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[lieu_creneau_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[seance]'))
ALTER TABLE [dbo].[seance]  WITH CHECK ADD  CONSTRAINT [lieu_creneau_fk] FOREIGN KEY([lieu_id])
REFERENCES [dbo].[lieu] ([lieu_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[lieu_creneau_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[seance]'))
ALTER TABLE [dbo].[seance] CHECK CONSTRAINT [lieu_creneau_fk]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[section_creneau_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[seance]'))
ALTER TABLE [dbo].[seance]  WITH CHECK ADD  CONSTRAINT [section_creneau_fk] FOREIGN KEY([section_id])
REFERENCES [dbo].[section] ([section_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[section_creneau_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[seance]'))
ALTER TABLE [dbo].[seance] CHECK CONSTRAINT [section_creneau_fk]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_seance_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[seance]'))
ALTER TABLE [dbo].[seance]  WITH CHECK ADD  CONSTRAINT [user_seance_fk] FOREIGN KEY([encadrant_id])
REFERENCES [dbo].[utilisateur] ([utilisateur_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_seance_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[seance]'))
ALTER TABLE [dbo].[seance] CHECK CONSTRAINT [user_seance_fk]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[discipline_section_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[section]'))
ALTER TABLE [dbo].[section]  WITH CHECK ADD  CONSTRAINT [discipline_section_fk] FOREIGN KEY([discipline_id])
REFERENCES [dbo].[discipline] ([discipline_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[discipline_section_fk]') AND parent_object_id = OBJECT_ID(N'[dbo].[section]'))
ALTER TABLE [dbo].[section] CHECK CONSTRAINT [discipline_section_fk]
GO
USE [master]
GO
ALTER DATABASE [SportAsso] SET  READ_WRITE 
GO
