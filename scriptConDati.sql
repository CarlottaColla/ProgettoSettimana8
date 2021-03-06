USE [master]
GO
/****** Object:  Database [MostriVsEroi]    Script Date: 19/03/2021 15:28:52 ******/
CREATE DATABASE [MostriVsEroi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MostriVsEroi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MostriVsEroi.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MostriVsEroi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MostriVsEroi_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MostriVsEroi] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MostriVsEroi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MostriVsEroi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MostriVsEroi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MostriVsEroi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MostriVsEroi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MostriVsEroi] SET ARITHABORT OFF 
GO
ALTER DATABASE [MostriVsEroi] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MostriVsEroi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MostriVsEroi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MostriVsEroi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MostriVsEroi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MostriVsEroi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MostriVsEroi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MostriVsEroi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MostriVsEroi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MostriVsEroi] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MostriVsEroi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MostriVsEroi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MostriVsEroi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MostriVsEroi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MostriVsEroi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MostriVsEroi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MostriVsEroi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MostriVsEroi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MostriVsEroi] SET  MULTI_USER 
GO
ALTER DATABASE [MostriVsEroi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MostriVsEroi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MostriVsEroi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MostriVsEroi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MostriVsEroi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MostriVsEroi] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MostriVsEroi] SET QUERY_STORE = OFF
GO
USE [MostriVsEroi]
GO
/****** Object:  Table [dbo].[Armi]    Script Date: 19/03/2021 15:28:52 ******/
--La tabella Armi ha 
--ID: è autoincrementale e primary key, 
--Nome: nome dell'arma,
--PuntiDanno: sono i danni che infligge quell'arma, 
--Classe_ID: è la classe a cui appartiene l'arma
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Armi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](200) NOT NULL,
	[PuntiDanno] [int] NOT NULL,
	[Classe_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classi]    Script Date: 19/03/2021 15:28:52 ******/
--La tabella classi ha:
--ID: è primary key e autoincrementale,
--Nome: es. mago, orco..
--IsEroe: può assumere solo 1 o 0: 1 è una classe di eroe, 0 è una classe di mostro
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](200) NOT NULL,
	[IsEroe] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Eroi]    Script Date: 19/03/2021 15:28:52 ******/
--La tabella Eroi ha:
--ID: autoincrementale come primary key,
--Nome: un varchar che salva il nome,
--Classe_ID: che è una chiave esterna che si riferisce all'ID della tabella Classi (sarà una classe con IsEroe =1)
--Arma_ID: che sarà una di quelle presenti nella tabell armi e relativa alla classe,
--Livello_ID: tra quelli presenti nella tabella livelli,
--PuntiVita: che sono inizialmente uguali a quelli del livello ma cambiano combattendo,
--Giocatore_ID: è l'id del giocatore a cui appartiene l'eroe,
--TempoTotale: è il tempo totale di gioco di un eroe,
--Punti: inizialmente sono 0 ma aumentano quando si sconfigge un mostro e diminuiscono quando si scappa
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Eroi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](200) NOT NULL,
	[Classe_ID] [int] NOT NULL,
	[Arma_ID] [int] NOT NULL,
	[Livello_ID] [int] NOT NULL,
	[PuntiVita] [int] NOT NULL,
	[Giocatore_ID] [int] NOT NULL,
	[TempoTotale] [int] NOT NULL,
	[Punti] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Giocatori]    Script Date: 19/03/2021 15:28:52 ******/
--La tabella giocatori ha 
--ID: come primary key autoincrementale, 
--Nome: deve essere univoco,
--Ruolo_ID: è una chiave esterna che si riferisce all'ID della tabella Ruolo, che può essere Admin o User
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Giocatori](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](200) NOT NULL,
	[Ruolo_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Livelli]    Script Date: 19/03/2021 15:28:52 ******/
--La tabella Livelli ha:
--ID: primary key autoincrementale,
--PuntiVita: vengono assegnati quando un eroe sale a quel livello,
--PuntiPassaggio: sono i punti per passare a quel livello
--Numero: rappresenta il numero del livello (Attualmente è uguale all'id ma potrebbe non esserlo)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Livelli](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PuntiVita] [int] NOT NULL,
	[PuntiPassaggio] [int] NOT NULL,
	[Numero] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mostri]    Script Date: 19/03/2021 15:28:52 ******/
--La tabella mostri ha:
--ID: come chiave primaria e autoincrementale
--Nome: è il nome del mostro,
--Classe_ID: si riferisce a quale classe appartiene (avrà IsEroe = 0),
--Arma_ID: si riferisce all'id di un'arma appartenente alla stessa classe
--Livello_ID: è l'id del livello a cui appartiene
--PuntiVita: sono i punti vita relativi al livello che poi cambieranno durante il combattimento
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mostri](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](200) NOT NULL,
	[Classe_ID] [int] NOT NULL,
	[Arma_ID] [int] NOT NULL,
	[Livello_ID] [int] NOT NULL,
	[PuntiVita] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ruolo]    Script Date: 19/03/2021 15:28:52 ******/
--La tabella ruolo ha:
--ID: come chiave primaria e autoincrementale,
--TIPO: che può essere "Admin" o "User" (Gestito nell'applicazione)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ruolo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Armi] ON 

INSERT [dbo].[Armi] ([ID], [Nome], [PuntiDanno], [Classe_ID]) VALUES (1, N'Spadone di ferro', 10, 2)
INSERT [dbo].[Armi] ([ID], [Nome], [PuntiDanno], [Classe_ID]) VALUES (2, N'Arco', 8, 2)
INSERT [dbo].[Armi] ([ID], [Nome], [PuntiDanno], [Classe_ID]) VALUES (3, N'Bastone', 11, 3)
INSERT [dbo].[Armi] ([ID], [Nome], [PuntiDanno], [Classe_ID]) VALUES (4, N'Bacchetta', 9, 3)
INSERT [dbo].[Armi] ([ID], [Nome], [PuntiDanno], [Classe_ID]) VALUES (5, N'Ascia', 8, 5)
INSERT [dbo].[Armi] ([ID], [Nome], [PuntiDanno], [Classe_ID]) VALUES (6, N'Spada', 7, 4)
INSERT [dbo].[Armi] ([ID], [Nome], [PuntiDanno], [Classe_ID]) VALUES (7, N'Mazza', 13, 6)
INSERT [dbo].[Armi] ([ID], [Nome], [PuntiDanno], [Classe_ID]) VALUES (8, N'Ramo', 2, 5)
SET IDENTITY_INSERT [dbo].[Armi] OFF
GO
SET IDENTITY_INSERT [dbo].[Classi] ON 

INSERT [dbo].[Classi] ([ID], [Nome], [IsEroe]) VALUES (2, N'Guerriero', 1)
INSERT [dbo].[Classi] ([ID], [Nome], [IsEroe]) VALUES (3, N'Mago', 1)
INSERT [dbo].[Classi] ([ID], [Nome], [IsEroe]) VALUES (4, N'Cultista', 0)
INSERT [dbo].[Classi] ([ID], [Nome], [IsEroe]) VALUES (5, N'Orco', 0)
INSERT [dbo].[Classi] ([ID], [Nome], [IsEroe]) VALUES (6, N'SignoreDelMale', 0)
SET IDENTITY_INSERT [dbo].[Classi] OFF
GO
SET IDENTITY_INSERT [dbo].[Eroi] ON 

INSERT [dbo].[Eroi] ([ID], [Nome], [Classe_ID], [Arma_ID], [Livello_ID], [PuntiVita], [Giocatore_ID], [TempoTotale], [Punti]) VALUES (8, N'Frodo', 2, 1, 1, 18, 1, 34827, 10)
INSERT [dbo].[Eroi] ([ID], [Nome], [Classe_ID], [Arma_ID], [Livello_ID], [PuntiVita], [Giocatore_ID], [TempoTotale], [Punti]) VALUES (9, N'Gandalf', 3, 3, 1, 20, 1, 18791, -10)
INSERT [dbo].[Eroi] ([ID], [Nome], [Classe_ID], [Arma_ID], [Livello_ID], [PuntiVita], [Giocatore_ID], [TempoTotale], [Punti]) VALUES (11, N'Nanuk', 2, 1, 2, 40, 1, 47691, 30)
INSERT [dbo].[Eroi] ([ID], [Nome], [Classe_ID], [Arma_ID], [Livello_ID], [PuntiVita], [Giocatore_ID], [TempoTotale], [Punti]) VALUES (12, N'Principe Azzurro', 2, 1, 2, 38, 1, 236143, 50)
INSERT [dbo].[Eroi] ([ID], [Nome], [Classe_ID], [Arma_ID], [Livello_ID], [PuntiVita], [Giocatore_ID], [TempoTotale], [Punti]) VALUES (14, N'Principe', 2, 1, 1, 20, 6, 0, 0)
INSERT [dbo].[Eroi] ([ID], [Nome], [Classe_ID], [Arma_ID], [Livello_ID], [PuntiVita], [Giocatore_ID], [TempoTotale], [Punti]) VALUES (15, N'Oliver II', 2, 1, 1, 20, 6, 0, 0)
INSERT [dbo].[Eroi] ([ID], [Nome], [Classe_ID], [Arma_ID], [Livello_ID], [PuntiVita], [Giocatore_ID], [TempoTotale], [Punti]) VALUES (19, N'Mago rosso', 3, 3, 1, 20, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[Eroi] OFF
GO
SET IDENTITY_INSERT [dbo].[Giocatori] ON 

INSERT [dbo].[Giocatori] ([ID], [Nome], [Ruolo_ID]) VALUES (1, N'Lady', 1)
INSERT [dbo].[Giocatori] ([ID], [Nome], [Ruolo_ID]) VALUES (3, N'Giocatore1', 1)
INSERT [dbo].[Giocatori] ([ID], [Nome], [Ruolo_ID]) VALUES (6, N'Giocatore3', 2)
SET IDENTITY_INSERT [dbo].[Giocatori] OFF
GO
SET IDENTITY_INSERT [dbo].[Livelli] ON 

INSERT [dbo].[Livelli] ([ID], [PuntiVita], [PuntiPassaggio], [Numero]) VALUES (1, 20, 0, 1)
INSERT [dbo].[Livelli] ([ID], [PuntiVita], [PuntiPassaggio], [Numero]) VALUES (2, 40, 30, 2)
INSERT [dbo].[Livelli] ([ID], [PuntiVita], [PuntiPassaggio], [Numero]) VALUES (3, 60, 60, 3)
INSERT [dbo].[Livelli] ([ID], [PuntiVita], [PuntiPassaggio], [Numero]) VALUES (4, 80, 90, 4)
INSERT [dbo].[Livelli] ([ID], [PuntiVita], [PuntiPassaggio], [Numero]) VALUES (5, 100, 120, 5)
SET IDENTITY_INSERT [dbo].[Livelli] OFF
GO
SET IDENTITY_INSERT [dbo].[Mostri] ON 

INSERT [dbo].[Mostri] ([ID], [Nome], [Classe_ID], [Arma_ID], [Livello_ID], [PuntiVita]) VALUES (2, N'Orco facile', 5, 8, 1, 20)
INSERT [dbo].[Mostri] ([ID], [Nome], [Classe_ID], [Arma_ID], [Livello_ID], [PuntiVita]) VALUES (3, N'Orco medio', 5, 8, 2, 20)
INSERT [dbo].[Mostri] ([ID], [Nome], [Classe_ID], [Arma_ID], [Livello_ID], [PuntiVita]) VALUES (6, N'Signore Oscuro', 6, 7, 4, 80)
INSERT [dbo].[Mostri] ([ID], [Nome], [Classe_ID], [Arma_ID], [Livello_ID], [PuntiVita]) VALUES (8, N'Troll', 4, 6, 3, 60)
SET IDENTITY_INSERT [dbo].[Mostri] OFF
GO
SET IDENTITY_INSERT [dbo].[Ruolo] ON 

INSERT [dbo].[Ruolo] ([ID], [Tipo]) VALUES (2, N'Admin')
INSERT [dbo].[Ruolo] ([ID], [Tipo]) VALUES (1, N'Utente')
SET IDENTITY_INSERT [dbo].[Ruolo] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Armi__7D8FE3B2759AB15C]    Script Date: 19/03/2021 15:28:52 ******/
ALTER TABLE [dbo].[Armi] ADD UNIQUE NONCLUSTERED 
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Classi__7D8FE3B2E6FC094C]    Script Date: 19/03/2021 15:28:52 ******/
ALTER TABLE [dbo].[Classi] ADD UNIQUE NONCLUSTERED 
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Eroi__7D8FE3B243B14DB1]    Script Date: 19/03/2021 15:28:52 ******/
ALTER TABLE [dbo].[Eroi] ADD UNIQUE NONCLUSTERED 
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Giocator__7D8FE3B25F8B3299]    Script Date: 19/03/2021 15:28:52 ******/
ALTER TABLE [dbo].[Giocatori] ADD UNIQUE NONCLUSTERED 
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Mostri__7D8FE3B2CDDAD14F]    Script Date: 19/03/2021 15:28:52 ******/
ALTER TABLE [dbo].[Mostri] ADD UNIQUE NONCLUSTERED 
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Ruolo__8E762CB4C3B5F0AB]    Script Date: 19/03/2021 15:28:52 ******/
ALTER TABLE [dbo].[Ruolo] ADD UNIQUE NONCLUSTERED 
(
	[Tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Armi]  WITH CHECK ADD FOREIGN KEY([Classe_ID])
REFERENCES [dbo].[Classi] ([ID])
GO
ALTER TABLE [dbo].[Eroi]  WITH CHECK ADD FOREIGN KEY([Arma_ID])
REFERENCES [dbo].[Armi] ([ID])
GO
ALTER TABLE [dbo].[Eroi]  WITH CHECK ADD FOREIGN KEY([Classe_ID])
REFERENCES [dbo].[Classi] ([ID])
GO
ALTER TABLE [dbo].[Eroi]  WITH CHECK ADD FOREIGN KEY([Giocatore_ID])
REFERENCES [dbo].[Giocatori] ([ID])
GO
ALTER TABLE [dbo].[Eroi]  WITH CHECK ADD FOREIGN KEY([Livello_ID])
REFERENCES [dbo].[Livelli] ([ID])
GO
ALTER TABLE [dbo].[Giocatori]  WITH CHECK ADD FOREIGN KEY([Ruolo_ID])
REFERENCES [dbo].[Ruolo] ([ID])
GO
ALTER TABLE [dbo].[Mostri]  WITH CHECK ADD FOREIGN KEY([Arma_ID])
REFERENCES [dbo].[Armi] ([ID])
GO
ALTER TABLE [dbo].[Mostri]  WITH CHECK ADD FOREIGN KEY([Classe_ID])
REFERENCES [dbo].[Classi] ([ID])
GO
ALTER TABLE [dbo].[Mostri]  WITH CHECK ADD FOREIGN KEY([Livello_ID])
REFERENCES [dbo].[Livelli] ([ID])
GO
USE [master]
GO
ALTER DATABASE [MostriVsEroi] SET  READ_WRITE 
GO
