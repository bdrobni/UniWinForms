USE [master]
GO
/****** Object:  Database [radnja]    Script Date: 18/05/2024 19:15:08 ******/
CREATE DATABASE [radnja]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'radnja', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\radnja.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'radnja_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\radnja_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [radnja] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [radnja].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [radnja] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [radnja] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [radnja] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [radnja] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [radnja] SET ARITHABORT OFF 
GO
ALTER DATABASE [radnja] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [radnja] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [radnja] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [radnja] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [radnja] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [radnja] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [radnja] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [radnja] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [radnja] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [radnja] SET  ENABLE_BROKER 
GO
ALTER DATABASE [radnja] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [radnja] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [radnja] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [radnja] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [radnja] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [radnja] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [radnja] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [radnja] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [radnja] SET  MULTI_USER 
GO
ALTER DATABASE [radnja] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [radnja] SET DB_CHAINING OFF 
GO
ALTER DATABASE [radnja] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [radnja] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [radnja] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [radnja] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [radnja] SET QUERY_STORE = ON
GO
ALTER DATABASE [radnja] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [radnja]
GO
/****** Object:  Table [dbo].[Artikal]    Script Date: 18/05/2024 19:15:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artikal](
	[sifra] [int] NOT NULL,
	[naziv] [varchar](20) NOT NULL,
	[barkod] [int] NOT NULL,
	[cena] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sifra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kasir]    Script Date: 18/05/2024 19:15:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kasir](
	[sifra] [int] NOT NULL,
	[korisnickoime] [varchar](20) NULL,
	[lozinka] [varchar](12) NULL,
	[ime] [varchar](20) NULL,
	[prezime] [varchar](20) NULL,
	[jesteadmin] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[sifra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Popust]    Script Date: 18/05/2024 19:15:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Popust](
	[sifra] [int] NOT NULL,
	[opis] [varchar](50) NOT NULL,
	[kolicina] [float] NOT NULL,
	[sifartikla] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sifra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Racun]    Script Date: 18/05/2024 19:15:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Racun](
	[sifra] [int] NOT NULL,
	[vreme] [datetime] NOT NULL,
	[ukupno] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sifra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stavka]    Script Date: 18/05/2024 19:15:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stavka](
	[sifracuna] [int] NOT NULL,
	[sifartikla] [int] NOT NULL,
	[brkomada] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Popust]  WITH CHECK ADD FOREIGN KEY([sifartikla])
REFERENCES [dbo].[Artikal] ([sifra])
GO
ALTER TABLE [dbo].[Stavka]  WITH CHECK ADD FOREIGN KEY([sifartikla])
REFERENCES [dbo].[Artikal] ([sifra])
GO
ALTER TABLE [dbo].[Stavka]  WITH CHECK ADD FOREIGN KEY([sifracuna])
REFERENCES [dbo].[Racun] ([sifra])
GO
USE [master]
GO
ALTER DATABASE [radnja] SET  READ_WRITE 
GO
