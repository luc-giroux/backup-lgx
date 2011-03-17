USE [master]
GO
/****** Object:  Database [HR]    Script Date: 03/17/2011 17:02:46 ******/
CREATE DATABASE [HR] ON  PRIMARY 
( NAME = N'HR', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\HR.mdf' , SIZE = 55552KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HR_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\HR_log.LDF' , SIZE = 108608KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [HR] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HR].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HR] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [HR] SET ANSI_NULLS OFF
GO
ALTER DATABASE [HR] SET ANSI_PADDING OFF
GO
ALTER DATABASE [HR] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [HR] SET ARITHABORT OFF
GO
ALTER DATABASE [HR] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [HR] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [HR] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [HR] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [HR] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [HR] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [HR] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [HR] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [HR] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [HR] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [HR] SET  ENABLE_BROKER
GO
ALTER DATABASE [HR] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [HR] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [HR] SET TRUSTWORTHY ON
GO
ALTER DATABASE [HR] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [HR] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [HR] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [HR] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [HR] SET  READ_WRITE
GO
ALTER DATABASE [HR] SET RECOVERY FULL
GO
ALTER DATABASE [HR] SET  MULTI_USER
GO
ALTER DATABASE [HR] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [HR] SET DB_CHAINING OFF
GO
USE [HR]
GO
/****** Object:  User [PROJETKONIAMBO\wcurra]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\wcurra] FOR LOGIN [PROJETKONIAMBO\WCurra] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\testuserpaf]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\testuserpaf] FOR LOGIN [PROJETKONIAMBO\testuserpaf] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\testuserapproval]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\testuserapproval] FOR LOGIN [PROJETKONIAMBO\testuserapproval] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\SSomasundaram]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\SSomasundaram] FOR LOGIN [PROJETKONIAMBO\SSomasundaram] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\RKolkman]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\RKolkman] FOR LOGIN [PROJETKONIAMBO\RKolkman] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\RCarrier]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\RCarrier] FOR LOGIN [PROJETKONIAMBO\RCarrier] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [projetkoniambo\nducrocq]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [projetkoniambo\nducrocq] FOR LOGIN [PROJETKONIAMBO\NDucrocq] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [projetkoniambo\ndemeillier]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [projetkoniambo\ndemeillier] FOR LOGIN [PROJETKONIAMBO\ndemeillier] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\MTHOBY]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\MTHOBY] FOR LOGIN [PROJETKONIAMBO\MThoby] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\MJego]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\MJego] FOR LOGIN [PROJETKONIAMBO\MJego] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\MCharlier]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\MCharlier] FOR LOGIN [PROJETKONIAMBO\MCharlier] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\mbarbier]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\mbarbier] FOR LOGIN [PROJETKONIAMBO\mbarbier] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\malquier]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\malquier] FOR LOGIN [PROJETKONIAMBO\malquier] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\lgiroux]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\lgiroux] FOR LOGIN [PROJETKONIAMBO\LGiroux] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\LDilly]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\LDilly] FOR LOGIN [PROJETKONIAMBO\LDilly] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\kvos]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\kvos] FOR LOGIN [PROJETKONIAMBO\KVos] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\KHKoo]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\KHKoo] FOR LOGIN [PROJETKONIAMBO\KHKoo] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [projetkoniambo\jterrade]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [projetkoniambo\jterrade] FOR LOGIN [PROJETKONIAMBO\JTerrade] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\JPSpeltz]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\JPSpeltz] FOR LOGIN [PROJETKONIAMBO\JPSpeltz] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\JKabar]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\JKabar] FOR LOGIN [PROJETKONIAMBO\JKabar] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [projetkoniambo\jeverard]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [projetkoniambo\jeverard] FOR LOGIN [PROJETKONIAMBO\jeverard] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\JCastex]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\JCastex] FOR LOGIN [PROJETKONIAMBO\JCastex] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\IMoraru]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\IMoraru] FOR LOGIN [PROJETKONIAMBO\IMoraru] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\ifoulon]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\ifoulon] FOR LOGIN [PROJETKONIAMBO\ifoulon] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\gdenis]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\gdenis] FOR LOGIN [PROJETKONIAMBO\gdenis] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\GBatterham]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\GBatterham] FOR LOGIN [PROJETKONIAMBO\GBatterham] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\FToledo]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\FToledo] FOR LOGIN [PROJETKONIAMBO\FToledo] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\FRobin]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\FRobin] FOR LOGIN [PROJETKONIAMBO\FRobin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [projetkoniambo\dmutter]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [projetkoniambo\dmutter] FOR LOGIN [PROJETKONIAMBO\DMutter] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\dgriffiths]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\dgriffiths] FOR LOGIN [PROJETKONIAMBO\dgriffiths] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [projetkoniambo\dduncan]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [projetkoniambo\dduncan] FOR LOGIN [PROJETKONIAMBO\DDuncan] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\cbeaulieu]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\cbeaulieu] FOR LOGIN [PROJETKONIAMBO\cbeaulieu] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [projetkoniambo\bnagle]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [projetkoniambo\bnagle] FOR LOGIN [PROJETKONIAMBO\BNagle] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\bleroy]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\bleroy] FOR LOGIN [PROJETKONIAMBO\bleroy] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\BGibbon]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\BGibbon] FOR LOGIN [PROJETKONIAMBO\BGibbon] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [projetkoniambo\awells]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [projetkoniambo\awells] FOR LOGIN [PROJETKONIAMBO\awells] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\AKaramehmedovic]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\AKaramehmedovic] FOR LOGIN [PROJETKONIAMBO\AKaramehmedovic] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [PROJETKONIAMBO\ahunt]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [PROJETKONIAMBO\ahunt] FOR LOGIN [PROJETKONIAMBO\ahunt] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [KONINET\TP006706]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [KONINET\TP006706] FOR LOGIN [KONINET\TP006706] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [KONINET\smagrou]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [KONINET\smagrou] FOR LOGIN [KONINET\smagrou] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [KONINET\SCN30268]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [KONINET\SCN30268] FOR LOGIN [KONINET\scn30268] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [KONINET\rberryman]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [KONINET\rberryman] FOR LOGIN [KONINET\rberryman] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [KONINET\MAS01404]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [KONINET\MAS01404] FOR LOGIN [KONINET\MAS01404] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [KONINET\jbeaudoin]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [KONINET\jbeaudoin] FOR LOGIN [KONINET\jbeaudoin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [KONINET\gashley]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [KONINET\gashley] FOR LOGIN [KONINET\gashley] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [KONINET\bauger]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [KONINET\bauger] FOR LOGIN [KONINET\bauger] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [KONINET\approvaladmin]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [KONINET\approvaladmin] FOR LOGIN [KONINET\approvaladmin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [HTAPPUSR]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [HTAPPUSR] FOR LOGIN [HTAPPUSR] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [hatchglobal\raym2304]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [hatchglobal\raym2304] FOR LOGIN [HATCHGLOBAL\raym2304] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [HATCHGLOBAL\pica1193]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [HATCHGLOBAL\pica1193] FOR LOGIN [HATCHGLOBAL\pica1193] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [HATCHGLOBAL\mcca101015]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [HATCHGLOBAL\mcca101015] FOR LOGIN [HATCHGLOBAL\mcca101015] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [HATCHGLOBAL\kinc62856]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [HATCHGLOBAL\kinc62856] FOR LOGIN [HATCHGLOBAL\kinc62856] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [HATCHGLOBAL\gill104158]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [HATCHGLOBAL\gill104158] FOR LOGIN [HATCHGLOBAL\gill104158] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [HATCHGLOBAL\Gerr51919]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [HATCHGLOBAL\Gerr51919] FOR LOGIN [HATCHGLOBAL\Gerr51919] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [HATCHGLOBAL\fraz101003]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [HATCHGLOBAL\fraz101003] FOR LOGIN [HATCHGLOBAL\fraz101003] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [HATCHGLOBAL\daws103947]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [HATCHGLOBAL\daws103947] FOR LOGIN [HATCHGLOBAL\daws103947] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [HATCHGLOBAL\chan62858]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [HATCHGLOBAL\chan62858] FOR LOGIN [HATCHGLOBAL\chan62858] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [HATCHGLOBAL\caro2009]    Script Date: 03/17/2011 17:02:46 ******/
CREATE USER [HATCHGLOBAL\caro2009] FOR LOGIN [HATCHGLOBAL\caro2009] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Candidate]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Candidate](
	[CandidateID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NULL,
	[LastName] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED 
(
	[CandidateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PhaseCandidate]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhaseCandidate](
	[PhaseID] [int] NOT NULL,
	[CandidateID] [int] NOT NULL,
	[Prefered] [bit] NOT NULL,
	[Selected] [bit] NOT NULL,
	[VisaNotRequired] [bit] NOT NULL,
 CONSTRAINT [PK_PhaseCandidate] PRIMARY KEY CLUSTERED 
(
	[PhaseID] ASC,
	[CandidateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Country](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](50) NULL,
	[OriginCode] [varchar](10) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PointOfOrigin]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PointOfOrigin](
	[PointOfOriginID] [int] IDENTITY(1,1) NOT NULL,
	[TownName] [varchar](50) NULL,
	[CountryID] [int] NULL,
	[nbHoursMaxTravel] [int] NULL,
 CONSTRAINT [PK_PointOfOrigin] PRIMARY KEY CLUSTERED 
(
	[PointOfOriginID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Logistics]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logistics](
	[PhaseID] [int] NOT NULL,
	[LastRotation] [datetime] NULL,
	[PointOfOriginID] [int] NULL,
 CONSTRAINT [PK_Logistics] PRIMARY KEY CLUSTERED 
(
	[PhaseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [paf_data]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 03/17/2011 17:02:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyID] [char](3) NOT NULL,
	[CompanyName] [varchar](100) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
USE [HR]
GO
/****** Object:  UserDefinedFunction [dbo].[ForeignKeyCompany]    Script Date: 03/17/2011 17:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[ForeignKeyCompany](@Company char(3)) 
returns int WITH EXECUTE AS OWNER

as

begin

declare @result int

IF @Company is null 
	SET @result =  1
ELSE	
	select @result = count(*) from PAF_DATA.dbo.Company where CompanyID = @Company

return @result
end
GO
/****** Object:  Table [dbo].[Rotation]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rotation](
	[RotationType] [varchar](10) NOT NULL,
	[InDays] [int] NULL,
 CONSTRAINT [PK_Rotation] PRIMARY KEY CLUSTERED 
(
	[RotationType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PAFRevised]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PAFRevised](
	[PhaseID] [int] NOT NULL,
	[PAFNumber] [varchar](10) NULL,
	[PAFStatus] [varchar](10) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[IssuedDate] [datetime] NULL,
	[ReceivedDate] [datetime] NULL,
	[ApprovedBy] [varchar](40) NULL,
	[ApprovedDate] [datetime] NULL,
	[RejectedBy] [varchar](40) NULL,
	[RejectedDate] [datetime] NULL,
	[MaritalStatus] [varchar](2) NULL,
	[CampStatus] [char](3) NULL,
	[VisaNotRequired] [bit] NULL,
	[Condition] [varchar](3) NULL,
	[Company] [char](3) NULL,
	[RotationType] [varchar](10) NULL,
	[OutOfPolicy] [bit] NULL,
	[Revision] [int] NOT NULL,
	[RevisedBy] [varchar](30) NOT NULL,
	[RevisionDate] [datetime] NOT NULL,
	[Housing] [int] NULL,
	[Travel] [int] NULL,
	[RRCost] [int] NULL,
	[SubsistenceCost] [int] NULL,
	[MobCost] [int] NULL,
	[DemobCost] [int] NULL,
	[PointOfHire] [varchar](20) NULL,
	[NbChildren] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WorkGroup]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WorkGroup](
	[WorkGroupCode] [varchar](10) NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_WorkGroup] PRIMARY KEY CLUSTERED 
(
	[WorkGroupCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Discipline]    Script Date: 03/17/2011 17:02:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Discipline](
	[DisciplineCode] [char](4) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[WorkGroupCode] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Discipline] PRIMARY KEY CLUSTERED 
(
	[DisciplineCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NbHours]    Script Date: 03/17/2011 17:02:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NbHours](
	[Condition] [varchar](3) NOT NULL,
	[LocationID] [char](3) NOT NULL,
	[Construction] [char](1) NOT NULL,
	[Marital] [varchar](2) NOT NULL,
	[KNS] [char](1) NOT NULL,
	[WorkGroup] [varchar](5) NOT NULL,
	[HoursPerWeek] [int] NOT NULL,
	[WeeksPerYear] [int] NOT NULL,
	[LeaveRR] [int] NOT NULL,
	[PublicHolidays] [int] NOT NULL,
 CONSTRAINT [PK_NbHours] PRIMARY KEY CLUSTERED 
(
	[Condition] ASC,
	[LocationID] ASC,
	[Construction] ASC,
	[Marital] ASC,
	[KNS] ASC,
	[WorkGroup] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[getNbHoursPerWeek]    Script Date: 03/17/2011 17:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I.Foulon
-- Create date: 20 Dec 2010
-- Description:	get the number of hours per week
-- =============================================
CREATE FUNCTION [dbo].[getNbHoursPerWeek]
(	
	@Condition varchar(3),
	@Location char(3),
	@Discipline varchar(10),
	@Marital varchar(2),
	@Company varchar (3),
	@WorkGroup varchar(10)
)
RETURNS  int
AS
BEGIN
	DECLARE @NbHoursPerWeek as int
	DECLARE @ConditionCur varchar(3)		
	DECLARE @Construction char(1)
	DECLARE @ConstructionCur char(1)
	DECLARE @MaritalCur varchar(2)
	DECLARE @KNS varchar (1)
	DECLARE @KNSCur varchar (1)
	DECLARE @WorkGroupCur varchar(10)
	DECLARE @HoursPerWeekCur as int
	
	DECLARE HoursPerWeek CURSOR FOR 
	SELECT [Condition]		 
		  ,[Construction]
		  ,[Marital]
		  ,[KNS]
		  ,[WorkGroup]
		  ,[HoursPerWeek]
	FROM [NbHours]
	WHERE [LocationID] = @Location
	
	SET @Construction = '0'
	SELECT @Construction = '1'
	FROM Discipline where DisciplineCode in ('061S','062S','063S','064S','065S','066S','373S')
	AND DisciplineCode = @Discipline
	
	SET @KNS = '0'
	SELECT @KNS = '1'
	FROM paf_data.dbo.Company 
	where CompanyID = @Company
	and CompanyID = 'KNS'
	
	OPEN  HoursPerWeek                                                    
	FETCH NEXT FROM HoursPerWeek INTO @ConditionCur, @ConstructionCur, @MaritalCur, @KNSCur, @WorkGroupCur, @HoursPerWeekCur
	WHILE @@fetch_Status = 0                                               
	BEGIN
		
	   IF @Condition like @ConditionCur AND @Construction like @ConstructionCur
			AND @Marital like @MaritalCur AND @KNS like @KNSCur AND @WorkGroup like @WorkGroupCur
			SET @NbHoursPerWeek = @HoursPerWeekCur 			
	   
	   FETCH NEXT FROM HoursPerWeek INTO @ConditionCur, @ConstructionCur, @MaritalCur, @KNSCur, @WorkGroupCur, @HoursPerWeekCur 
     END 
	CLOSE  HoursPerWeek 
	DEALLOCATE  HoursPerWeek
	 
	RETURN @NbHoursPerWeek

END
GO
/****** Object:  Table [dbo].[AccountableManager]    Script Date: 03/17/2011 17:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccountableManager](
	[AccountableManagerID] [char](3) NOT NULL,
	[LastName] [varchar](30) NOT NULL,
	[FirstName] [varchar](30) NOT NULL,
 CONSTRAINT [PK_AccountableManager] PRIMARY KEY CLUSTERED 
(
	[AccountableManagerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'key of AccountableManager' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AccountableManager', @level2type=N'COLUMN',@level2name=N'AccountableManagerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LastName of the accountable manager' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AccountableManager', @level2type=N'COLUMN',@level2name=N'LastName'
GO
/****** Object:  Table [dbo].[Department]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentCode] [int] NOT NULL,
	[Description] [varchar](100) NULL,
	[AccountableManagerID] [char](3) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Job]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Job](
	[JobID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Speciality]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Speciality](
	[SpecialityID] [int] IDENTITY(1,1) NOT NULL,
	[JobID] [int] NOT NULL,
	[Description] [varchar](100) NULL,
 CONSTRAINT [PK_Speciality] PRIMARY KEY CLUSTERED 
(
	[SpecialityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Area]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Area](
	[AreaCode] [varchar](10) NOT NULL,
	[Description] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[AreaCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Function]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Function](
	[FunctionCode] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED 
(
	[FunctionCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sector]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sector](
	[SectorCode] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NULL,
 CONSTRAINT [PK_Sector] PRIMARY KEY CLUSTERED 
(
	[SectorCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Position]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Position](
	[PositionNumber] [varchar](8) NOT NULL,
	[DisciplineCode] [char](4) NOT NULL,
	[DepartmentCode] [int] NOT NULL,
	[WorkGroupCode] [varchar](10) NOT NULL,
	[AreaCode] [varchar](10) NULL,
	[FunctionCode] [int] NOT NULL,
	[JobID] [int] NOT NULL,
	[SpecialityID] [int] NULL,
	[RomeCode] [varchar](10) NULL,
	[RomeTitleFR] [varchar](100) NULL,
	[PositionTitleFR] [varchar](100) NULL,
	[SectorCode] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](40) NOT NULL,
	[Temporary] [bit] NULL,
	[ResponsibleManagerID] [char](3) NOT NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[PositionNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
USE [paf_data]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 03/17/2011 17:02:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [char](3) NOT NULL,
	[CompanyEmployeeNo] [varchar](20) NULL,
	[LastName] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[TotalHoursImported] [decimal](18, 2) NULL,
	[Email] [varchar](100) NULL,
	[Comments] [varchar](1024) NULL,
	[PAFNo] [varchar](50) NULL,
	[StartDate] [datetime] NULL,
	[RatePayLocation] [char](3) NULL,
	[RatePayCurrencyID] [char](4) NULL,
	[EHAHourlyRate] [decimal](18, 2) NULL,
	[RateAssignLocation] [char](3) NULL,
	[RateAssignCurrencyID] [char](4) NULL,
	[ActualDemobilisationDate] [datetime] NULL,
	[Creator] [varchar](50) NULL,
	[CreationDate] [datetime] NULL,
	[EffectiveDate] [datetime] NULL,
	[PAFRevisionNo] [int] NULL,
	[EmployeeLogin] [varchar](40) NULL,
	[EmployeeActive] [bit] NULL,
	[DisciplineCode] [char](4) NOT NULL,
	[CompanyManagerID] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
USE [HR]
GO
/****** Object:  UserDefinedFunction [dbo].[ForeignKeyEmployee]    Script Date: 03/17/2011 17:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[ForeignKeyEmployee](@EmployeeID int) 
returns int WITH EXECUTE AS OWNER

as

begin
Declare  @result int

if @EmployeeID is null
	set @result =  1
else
	select @result=  count(*) from paf_data.dbo.Employee where EmployeeID = @EmployeeID

return @result
end
GO
USE [paf_data]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 03/17/2011 17:02:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Location](
	[LocationID] [char](3) NOT NULL,
	[LocationName] [varchar](100) NOT NULL,
	[CompanyID] [char](3) NULL,
	[CurrencyID] [char](4) NULL,
	[StandardWorkingHours] [int] NULL,
	[Multiplier] [float] NULL,
	[OtherOfficeSpace] [decimal](18, 2) NULL,
	[OtherOfficeExpenses] [decimal](18, 2) NULL,
	[OtherIT] [decimal](18, 2) NULL,
	[OtherITCore] [decimal](18, 2) NULL,
	[OtherTotal] [decimal](18, 2) NULL,
	[Revision] [int] NULL,
	[RevisionEffectiveDate] [datetime] NULL,
	[Comments] [varchar](100) NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
USE [HR]
GO
/****** Object:  UserDefinedFunction [dbo].[ForeignKeyLocation]    Script Date: 03/17/2011 17:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[ForeignKeyLocation](@LocationID char(3)) 
returns int WITH EXECUTE AS OWNER

as

begin

return(select count(*) from paf_data.dbo.Location where LocationID = @LocationID)

end
GO
/****** Object:  Table [dbo].[ResponsibleManager]    Script Date: 03/17/2011 17:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ResponsibleManager](
	[ResponsibleManagerID] [char](3) NOT NULL,
	[LastName] [varchar](30) NOT NULL,
	[FirstName] [varchar](30) NOT NULL,
 CONSTRAINT [PK_ResponsibleManager] PRIMARY KEY CLUSTERED 
(
	[ResponsibleManagerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[ListManagers]    Script Date: 03/17/2011 17:02:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ListManagers]
AS
SELECT DISTINCT ID, Name, Lastname, Firstname FROM(
SELECT  AccountableManagerID As ID, FirstName + ', ' + LastName As Name,LastName, FirstName 
FROM   dbo.AccountableManager 
UNION ALL 
SELECT  ResponsibleManagerID As ID, FirstName + ', ' + LastName As Name,LastName, FirstName
FROM     dbo.ResponsibleManager) As Managers
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AccountableManager"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 106
               Right = 231
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ResponsibleManager"
            Begin Extent = 
               Top = 6
               Left = 269
               Bottom = 106
               Right = 460
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ListManagers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ListManagers'
GO
/****** Object:  Table [dbo].[Phase]    Script Date: 03/17/2011 17:02:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Phase](
	[PhaseID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [char](3) NULL,
	[EmployeeID] [int] NULL,
	[PositionNumber] [varchar](8) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[PhaseStatus] [varchar](10) NOT NULL,
	[TypeOfMobilisation] [char](4) NULL,
	[HoursPerWeek] [int] NULL,
	[CampStatus] [char](3) NULL,
	[MaritalStatus] [varchar](2) NULL,
	[PointOfOrigin] [char](4) NULL,
	[NbChildren] [int] NULL,
	[Condition] [varchar](3) NULL,
	[WorkLocation] [char](3) NULL,
	[Utilisation] [int] NULL,
	[Extension] [bit] NULL,
	[CreatedBy] [varchar](40) NULL,
	[CreatedDate] [datetime] NULL,
	[PAFNumber] [varchar](10) NULL,
	[PAFStatus] [varchar](50) NULL,
	[StartDatePAF] [datetime] NULL,
	[EndDatePAF] [datetime] NULL,
	[IssuedDate] [datetime] NULL,
	[ReceivedDate] [datetime] NULL,
	[ApprovedBy] [varchar](100) NULL,
	[RotationType] [varchar](10) NULL,
	[CompanyEmployeeNo] [varchar](100) NULL,
	[ActualMobDate] [datetime] NULL,
	[ActualDemobDate] [datetime] NULL,
	[VCCNumber] [varchar](20) NULL,
	[VisaStatus] [varchar](10) NULL,
	[ETA] [datetime] NULL,
	[LastRotation] [datetime] NULL,
	[PointOfOriginTown] [varchar](30) NULL,
	[PRFDATE] [varchar](100) NULL,
	[REFNumber] [varchar](100) NULL,
	[PMTCode] [varchar](100) NULL,
	[DatePosted] [varchar](100) NULL,
	[Candidate] [varchar](100) NULL,
 CONSTRAINT [PK_Phase] PRIMARY KEY CLUSTERED 
(
	[PhaseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Mobilisation]    Script Date: 03/17/2011 17:02:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Mobilisation](
	[PhaseID] [int] NOT NULL,
	[ActualMobDate] [datetime] NULL,
	[ActualDemobDate] [datetime] NULL,
	[VCCNumber] [varchar](20) NULL,
	[VisaStatus] [varchar](10) NULL,
	[ETA] [datetime] NULL,
 CONSTRAINT [PK_Mobilisation] PRIMARY KEY CLUSTERED 
(
	[PhaseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PAF]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PAF](
	[PhaseID] [int] NOT NULL,
	[PAFNumber] [varchar](10) NULL,
	[PAFStatus] [varchar](10) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[IssuedDate] [datetime] NULL,
	[ReceivedDate] [datetime] NULL,
	[ApprovedBy] [char](3) NULL,
	[ApprovedDate] [datetime] NULL,
	[RejectedBy] [char](3) NULL,
	[RejectedDate] [datetime] NULL,
	[MaritalStatus] [varchar](2) NULL,
	[CampStatus] [char](3) NULL,
	[VisaNotRequired] [bit] NULL,
	[Condition] [varchar](3) NULL,
	[Company] [char](3) NULL,
	[RotationType] [varchar](10) NULL,
	[OutOfPolicy] [bit] NULL,
	[Revision] [int] NOT NULL,
	[CompanyEmployeeNo] [varchar](20) NULL,
	[Housing] [int] NULL,
	[Travel] [int] NULL,
	[RRCost] [int] NULL,
	[SubsistenceCost] [int] NULL,
	[MobCost] [int] NULL,
	[DemobCost] [int] NULL,
	[PointOfHire] [varchar](20) NULL,
	[NbChildren] [int] NULL,
 CONSTRAINT [PK_PAF] PRIMARY KEY CLUSTERED 
(
	[PhaseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Recruitment]    Script Date: 03/17/2011 17:02:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Recruitment](
	[PhaseID] [int] NOT NULL,
	[CERefNumber] [varchar](10) NULL,
	[PMTCode] [varchar](10) NULL,
	[PostedToCEDate] [datetime] NULL,
	[CreatedBy] [varchar](30) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ClosedDate] [datetime] NULL,
	[Status] [varchar](10) NOT NULL,
	[Suivi] [varchar](100) NULL,
 CONSTRAINT [PK_Recruitment] PRIMARY KEY CLUSTERED 
(
	[PhaseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [UpdateStatusRecruitment]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I. Foulon
-- Create date: 14 Dec 2010
-- Description:	Update status
-- =============================================
CREATE TRIGGER [dbo].[UpdateStatusRecruitment]
   ON  [dbo].[PhaseCandidate]
   AFTER UPDATE
AS 
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @PhaseID int
	DECLARE @Selected bit
	DECLARE @PAFExists int
	
	SET @PAFExists = 0
	
	SELECT @PhaseID = [PhaseID],     
		   @Selected = [Selected] 
    FROM inserted
	WHERE NOT EXISTS (SELECT 1 FROM deleted WHERE deleted.Selected = inserted.Selected)
	
	IF @Selected = 1
		UPDATE [Recruitment]
		SET [Status] = 'Finished'
		WHERE [PhaseID] = @PhaseID
		
	IF @Selected = 0
		UPDATE [Recruitment]
		SET [Status] = 'InProgress'
		WHERE [PhaseID] = @PhaseID	
	
	SELECT @PAFExists = 1
    FROM inserted
    WHERE EXISTS (SELECT 1 FROM paf WHERE paf.Phaseid = inserted.Phaseid)
    
    IF @Selected = 1 AND @PAFExists = 1
		UPDATE [HR].[dbo].[PAF]
		   SET [PAFNumber] = null
			  ,[PAFStatus] = 'ToApprove'
			  ,[StartDate] = null
			  ,[EndDate] = null
			  ,[IssuedDate] = null
			  ,[ReceivedDate] = null
			  ,[ApprovedBy] = null
			  ,[ApprovedDate] = null
			  ,[RejectedBy] = null
			  ,[RejectedDate] = null
			  ,[MaritalStatus] = null
			  ,[CampStatus] = null
			  ,[VisaNotRequired] = null
			  ,[Condition] = null
			  ,[Company] = null
			  ,[RotationType] = null
			  ,[OutOfPolicy] = 0     
			  ,[CompanyEmployeeNo] = null
			  ,[Housing] = null
			  ,[Travel] = null
			  ,[RRCost] = null
			  ,[SubsistenceCost] = null
			  ,[MobCost] = null
			  ,[DemobCost] = null
			  ,[PointOfHire] = null
			  ,[NbChildren] = 0
		 WHERE [PhaseID] = @PhaseID
	
END
GO
/****** Object:  Table [dbo].[PRF]    Script Date: 03/17/2011 17:02:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRF](
	[PhaseID] [int] NOT NULL,
	[ReceptionDate] [datetime] NULL,
	[HoursPerWeek] [int] NULL,
 CONSTRAINT [PK_PRF_1] PRIMARY KEY CLUSTERED 
(
	[PhaseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhaseHistory]    Script Date: 03/17/2011 17:02:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PhaseHistory](
	[PhaseID] [int] NOT NULL,
	[CompanyID] [char](3) NULL,
	[EmployeeID] [int] NULL,
	[PositionNumber] [varchar](8) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[PhaseStatus] [varchar](10) NOT NULL,
	[TypeOfMobilisation] [char](4) NULL,
	[HoursPerWeek] [int] NULL,
	[CampStatus] [char](3) NULL,
	[MaritalStatus] [char](2) NULL,
	[PointOfOrigin] [char](4) NULL,
	[NbChildren] [int] NULL,
	[Condition] [varchar](3) NULL,
	[RotationType] [varchar](10) NULL,
	[WorkLocation] [char](3) NULL,
	[Utilisation] [int] NULL,
	[Extension] [bit] NULL,
	[HistoryOrigin] [varchar](100) NULL,
	[HistoryBy] [varchar](30) NOT NULL,
	[HistoryDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[GetPhaseRecords]    Script Date: 03/17/2011 17:02:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I. Foulon
-- Create date: 2 mars 2011
-- Description:	return the record of the phase validated at the end date
-- =============================================
CREATE FUNCTION [dbo].[GetPhaseRecords] 
							(@EndDate date)
							
RETURNS @retTable TABLE 
(		
	[PhaseID] [int] NOT NULL,
	[CompanyID] [char](3) NULL,
	[EmployeeID] [int] NULL,
	[PositionNumber] [varchar](8) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[PhaseStatus] [varchar](10) NOT NULL,
	[TypeOfMobilisation] [char](4) NULL,
	[HoursPerWeek] [int] NULL,
	[CampStatus] [char](3) NULL,
	[MaritalStatus] [char](2) NULL,
	[PointOfOrigin] [char](4) NULL,
	[NbChildren] [int] NULL,
	[Condition] [varchar](3) NULL,
	[RotationType] [varchar](10) NULL,
	[WorkLocation] [char](3) NULL,
	[Utilisation] [int] NULL,	
	[Extension] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[HistoryOrigin] [varchar](100) NULL,
	[HistoryBy] [varchar](30) NULL,
	[HistoryDate] [datetime] NOT NULL	
)
AS
BEGIN
	
	DECLARE @PhaseID int
	DECLARE @CreatedDate date
	
	DECLARE PHASES CURSOR STATIC LOCAL FOR                                       
    SELECT PhaseID, CreatedDate FROM Phase

		OPEN  PHASES                                                    
		FETCH NEXT FROM PHASES INTO @PhaseID, @CreatedDate 
		WHILE @@fetch_Status = 0                                               
				BEGIN  
				
				INSERT INTO @retTable
				SELECT  TOP 1 T.* FROM
				(SELECT	   [PhaseID]
						  ,[CompanyID]
						  ,[EmployeeID]
						  ,[PositionNumber]
						  ,[StartDate]
						  ,[EndDate]
						  ,[PhaseStatus]
						  ,[TypeOfMobilisation]
						  ,[HoursPerWeek]
						  ,[CampStatus]
						  ,[MaritalStatus]
						  ,[PointOfOrigin]
						  ,[NbChildren]
						  ,[Condition]
						  ,[RotationType]
						  ,[WorkLocation]
						  ,[Utilisation]
						  ,[Extension]		
						  ,[CreatedDate]				 
					      ,'current' as [HistoryOrigin]
					      ,null as [HistoryBy]
					      ,GETDATE()	as [HistoryDate]						
				   FROM Phase
				  WHERE (Convert(date,CreatedDate) <= @EndDate)
				 UNION
			 	 SELECT	   [PhaseID]
						  ,[CompanyID]
						  ,[EmployeeID]
						  ,[PositionNumber]
						  ,[StartDate]
						  ,[EndDate]
						  ,[PhaseStatus]
						  ,[TypeOfMobilisation]
						  ,[HoursPerWeek]
						  ,[CampStatus]
						  ,[MaritalStatus]
						  ,[PointOfOrigin]
						  ,[NbChildren]
						  ,[Condition]
						  ,[RotationType]
						  ,[WorkLocation]
						  ,[Utilisation]
						  ,[Extension]	
						  , @CreatedDate as [CreatedDate]						 			 
					      ,[HistoryOrigin]
					      ,[HistoryBy]
					      ,[HistoryDate]							
				   FROM PhaseHistory
				  WHERE (Convert(date,HistoryDate) >= @EndDate)
				  AND @CreatedDate <= @EndDate) AS T
				  WHERE T.PhaseID = @PhaseID					 			  
				ORDER BY HistoryDate ASC	
				
			    FETCH NEXT FROM PHASES INTO @PhaseID, @CreatedDate 
			END 
		CLOSE  PHASES 
		DEALLOCATE  PHASES  
		
	RETURN					     
END
GO
/****** Object:  StoredProcedure [dbo].[HeaderVariation]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I.Foulon
-- Create date: 11 March 2011
-- Description:	select all variations
-- =============================================
CREATE PROCEDURE [dbo].[HeaderVariation]
	@FirstDate datetime,@SecondDate datetime
AS
BEGIN
	
	SET NOCOUNT ON;
	DECLARE @Val1 int
	DECLARE @Val2 int
	
	DECLARE @deltaNbPhasesACTIVEorOPEN int
	DECLARE @deltaNbPhasesCLOSED int
	DECLARE @deltaNbPhasesCANCELLED int
	DECLARE @nbNewPhases int
	
    select @Val1 = COUNT(*) from dbo.GetPhaseRecords(@SecondDate) where PhaseStatus in ('OPEN','ACTIVE')
	-- MOINS
	select @Val2 = COUNT(*) from dbo.GetPhaseRecords(@FirstDate) where PhaseStatus in ('OPEN','ACTIVE')
	
	SET @deltaNbPhasesACTIVEorOPEN = @Val1 - @Val2
	
	select @Val1 = COUNT(*) from dbo.GetPhaseRecords(@SecondDate) where PhaseStatus in ('CLOSED')
	-- MOINS
	select @Val2 = COUNT(*) from dbo.GetPhaseRecords(@FirstDate) where PhaseStatus in ('CLOSED')
	
	SET @deltaNbPhasesCLOSED = @Val1 - @Val2
	
	select @Val1 = COUNT(*) from dbo.GetPhaseRecords(@SecondDate) where PhaseStatus in ('CANCELLED')
	-- MOINS
	select @Val2 = COUNT(*) from dbo.GetPhaseRecords(@FirstDate) where PhaseStatus in ('CANCELLED')
	
	SET @deltaNbPhasesCANCELLED = @Val1 - @Val2
	
	select @nbNewPhases = COUNT(*) from Phase where CreatedDate between @FirstDate and dateadd(dd,1,@SecondDate)
	
	SELECT @deltaNbPhasesACTIVEorOPEN AS deltaNbPhasesACTIVEorOPEN,
	   @deltaNbPhasesCLOSED AS deltaNbPhasesCLOSED,
	   @deltaNbPhasesCANCELLED AS deltaNbPhasesCANCELLED,
	   @nbNewPhases AS nbNewPhases	
END
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[PhaseID] [int] NOT NULL,
	[Description] [varchar](1000) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Userlogin] [varchar](40) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [PAFRevisedHistory]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I.Foulon
-- Create date: 14 Dec 2010
-- Description:	Keep revision of th PAF
-- =============================================
CREATE TRIGGER [dbo].[PAFRevisedHistory] 
   ON  [dbo].[PAF]
   AFTER UPDATE
AS 
BEGIN

	SET NOCOUNT ON;
 
        -- The PAF hase been revised
		INSERT INTO [HR].[dbo].[PAFRevised]
           ([PhaseID]
           ,[PAFNumber]
           ,[PAFStatus]
           ,[StartDate]
           ,[EndDate]
           ,[IssuedDate]
           ,[ReceivedDate]
           ,[ApprovedBy]
           ,[ApprovedDate]
           ,[RejectedBy]
           ,[RejectedDate]
           ,[MaritalStatus]
           ,[CampStatus]
           ,[VisaNotRequired]
           ,[Condition]
           ,[Company]
           ,[RotationType]
           ,[OutOfPolicy]
           ,[Revision]
           ,[RevisedBy]
           ,[RevisionDate])
		SELECT [PhaseID]
			  ,[PAFNumber]
			  ,[PAFStatus]
			  ,[StartDate]
			  ,[EndDate]
			  ,[IssuedDate]
			  ,[ReceivedDate]
			  ,[ApprovedBy]
			  ,[ApprovedDate]
			  ,[RejectedBy]
			  ,[RejectedDate]
			  ,[MaritalStatus]
			  ,[CampStatus]
			  ,[VisaNotRequired]
			  ,[Condition]
			  ,[Company]
			  ,[RotationType]
			  ,[OutOfPolicy]
			  ,[Revision]
			  ,SYSTEM_USER
			  ,GETDATE()
		FROM deleted WHERE NOT EXISTS (SELECT 1 FROM inserted WHERE inserted.[Revision] = deleted.[Revision])
END
GO
/****** Object:  Table [dbo].[NbHoursHistory]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NbHoursHistory](
	[Condition] [varchar](3) NOT NULL,
	[LocationID] [char](3) NOT NULL,
	[Construction] [char](1) NOT NULL,
	[Marital] [varchar](2) NOT NULL,
	[KNS] [char](1) NOT NULL,
	[WorkGroup] [varchar](5) NOT NULL,
	[HoursPerWeek] [int] NOT NULL,
	[WeeksPerYear] [int] NOT NULL,
	[LeaveRR] [int] NOT NULL,
	[PublicHolidays] [int] NOT NULL,
	[HistoryDate] [datetime] NOT NULL,
 CONSTRAINT [PK_NbHoursHistory] PRIMARY KEY CLUSTERED 
(
	[Condition] ASC,
	[LocationID] ASC,
	[Construction] ASC,
	[Marital] ASC,
	[KNS] ASC,
	[WorkGroup] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Nationality]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Nationality](
	[NationalityID] [int] IDENTITY(1,1) NOT NULL,
	[NationalityName] [varchar](50) NULL,
 CONSTRAINT [PK_Nationality] PRIMARY KEY CLUSTERED 
(
	[NationalityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PositionHistory]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PositionHistory](
	[PositionNumber] [varchar](8) NOT NULL,
	[DisciplineCode] [char](4) NULL,
	[DepartmentCode] [char](2) NULL,
	[WorkGroupCode] [varchar](10) NULL,
	[AreaCode] [varchar](10) NULL,
	[FunctionCode] [int] NULL,
	[JobID] [int] NULL,
	[SpecialityID] [int] NULL,
	[RomeCode] [varchar](10) NULL,
	[RomeTitleFR] [varchar](100) NULL,
	[PositionTitleFR] [varchar](100) NULL,
	[SectorCode] [int] NULL,
	[HistoryDate] [datetime] NULL,
	[HistoryUser] [varchar](40) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RotationPerYear]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RotationPerYear](
	[MaritalStatus] [char](2) NOT NULL,
	[CampStatus] [char](3) NOT NULL,
	[PointOfOrigin] [char](4) NOT NULL,
	[NbRotationPerYear] [int] NOT NULL,
 CONSTRAINT [PK_RotationPerYear] PRIMARY KEY CLUSTERED 
(
	[MaritalStatus] ASC,
	[CampStatus] ASC,
	[PointOfOrigin] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RomeCode]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RomeCode](
	[RomeCode] [varchar](9) NOT NULL,
	[DescriptionFR] [varchar](100) NULL,
	[DescriptionEN] [varchar](100) NULL,
 CONSTRAINT [PK_RomeCode] PRIMARY KEY CLUSTERED 
(
	[RomeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MaritalCondition]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MaritalCondition](
	[MaritalStatus] [varchar](3) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_MaritalCondition] PRIMARY KEY CLUSTERED 
(
	[MaritalStatus] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[GenerateStaffing]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I. Foulon
-- Create date: 30 Nov 2010
-- Description:	used to generate report Staffing plan
-- =============================================
CREATE PROCEDURE [dbo].[GenerateStaffing] (@startdate datetime, @enddate datetime)

	
AS
BEGIN

	-- LGX : to avoid this error in Visual Studio TableAdapter configuration wizard fails if you are using 
	-- stored procedures that uses temp tables
	-- https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=106244&wa=wsignin1.0
	IF 1=0 BEGIN
	SET FMTONLY OFF
	END
	

	SET NOCOUNT ON;
	
	--DECLARE @startdate datetime
	--DECLARE @enddate datetime
	
	--SET @startdate = '2011-01-01'
	--SET @enddate = '2011-03-01'
	
	DECLARE @end datetime	
	DECLARE @monthYear varchar(30)
	DECLARE @SRC as  varchar(MAX)
	DECLARE @MANNING as varchar(MAX)	
	DECLARE @REQ as  nvarchar(MAX)	
	
	DECLARE @Calendar as  varchar(MAX)
	
    SET @end = '2013-06-30'  
    SET @Calendar = ''

	DECLARE @no_of_month int
	DECLARE @compteur int
	
	SET @no_of_month = datediff(mm,'2011-01-01',@end) + 1
	SET @compteur = @no_of_month
	

	CREATE TABLE #StaffingPlan (
	[dy]					[int] IDENTITY(0,1) NOT NULL,
	[year]					[int]  NULL,
	[month]					[varchar](20)  NULL	
	)
	
	WHILE @compteur > 0
	BEGIN
		INSERT INTO #StaffingPlan ([month],[year]) 
		VALUES (DATENAME(mm, DATEADD(mm,@compteur - 1,'2011-01-01')), DATEPART(yy, DATEADD(mm,@compteur - 1,'2011-01-01')))
		SET @compteur = @compteur - 1
	END		
		
	SET ROWCOUNT 0
	
	DECLARE CALENDAR CURSOR FOR                                       
    SELECT [month] + convert(varchar,[Year]) from #StaffingPlan
    
	OPEN  CALENDAR                                                    
	FETCH NEXT FROM CALENDAR INTO @monthYear
	WHILE @@fetch_Status = 0                                               
	BEGIN  
		
		  SET @Calendar =  ',[' + @monthYear + ']' + @Calendar
	
		  FETCH NEXT FROM CALENDAR INTO @monthYear
    END 
	CLOSE  CALENDAR 
	DEALLOCATE  CALENDAR  
	
	SET @Calendar = SUBSTRING(@Calendar,2,LEN(@Calendar)-1)
	
	SET @MANNING = 'CASE							
						WHEN MONTH(DATEADD(mm,dy,'''+ Convert(varchar(10),@startdate,101) + ''')) = MONTH([GetPhaseRecords].[StartDate]) 
							AND YEAR(DATEADD(mm,dy,'''+ Convert(varchar(10),@startdate,101) + ''')) = YEAR([GetPhaseRecords].[StartDate])
							AND DAY([GetPhaseRecords].[StartDate]) BETWEEN 1 AND 15  THEN convert(decimal,[GetPhaseRecords].[Utilisation])/100	
	
						WHEN MONTH(DATEADD(mm,dy,'''+ Convert(varchar(10),@startdate,101) + ''')) = MONTH([GetPhaseRecords].[EndDate]) 
						    AND YEAR(DATEADD(mm,dy,'''+ Convert(varchar(10),@startdate,101) + ''')) = YEAR([GetPhaseRecords].[EndDate])
							AND DAY([GetPhaseRecords].[EndDate]) BETWEEN 16 AND 31  THEN convert(decimal,[GetPhaseRecords].[Utilisation])/100	
							
						WHEN MONTH(DATEADD(mm,dy,'''+ Convert(varchar(10),@startdate,101) + ''')) = MONTH([GetPhaseRecords].[StartDate]) 
						    AND YEAR(DATEADD(mm,dy,'''+ Convert(varchar(10),@startdate,101) + ''')) = YEAR([GetPhaseRecords].[StartDate])								
							AND DAY([GetPhaseRecords].[StartDate]) BETWEEN 16 AND 31  THEN 0.5 * convert(decimal,[GetPhaseRecords].[Utilisation])/100
																	
						WHEN MONTH(DATEADD(mm,dy,'''+ Convert(varchar(10),@startdate,101) + ''')) = MONTH([GetPhaseRecords].[EndDate]) 
							AND YEAR(DATEADD(mm,dy,'''+ Convert(varchar(10),@startdate,101) + ''')) = YEAR([GetPhaseRecords].[EndDate])
							AND DAY([GetPhaseRecords].[EndDate]) BETWEEN 1 AND 15  THEN 0.5 * convert(decimal,[GetPhaseRecords].[Utilisation])/100
					
						WHEN DATEADD(mm,dy,'''+ Convert(varchar(10),@startdate,101) + ''') > [GetPhaseRecords].[StartDate] AND DATEADD(mm,dy,'''+ Convert(varchar(10),@startdate,101) + ''') < [GetPhaseRecords].[EndDate] THEN
							convert(decimal,[GetPhaseRecords].[Utilisation])/100
						ELSE 0
					   END'
	
	SET @SRC = 'SELECT [GetPhaseRecords].[PhaseID] As [KEY],
						   [Position].[RomeCode] AS [ROME_CODE],
						   [Position].[RomeTitleFR] AS [TITRE_ROME_FRANCAIS],
						   [Position].[PositionTitleFR] AS [POSITION_TITLE_FRENCH__applies_to_SITE_ONLY],
						   [Sector].[Description] AS [SECTEUR],
						   [Recruitment].[PostedToCEDate] AS [DATE_POSTED_TO_CAP_EMPLOI],
						   [Recruitment].[CERefNumber] AS [CAP_EMPLOI_reference_number],
						   [Recruitment].[PMTCode] AS [PMT_Cleared_with_CE_positions_code],
						   [AccountableManager].[AccountableManagerID] As [Accountable_Manager],
						   [Position].[ResponsibleManagerID] As [Responsible_Manager],
						   [Position].[PositionNumber] AS [NEW_POSITION_NUMBER],
						   [Job].[Description] as [POSITION],
						   [Position].[WorkGroupCode] AS [GROUP],
						   [Department].[Description] as [DEPARTMENT],
						   [Area].[Description] as [AREA],
						   [Function].[Description] as [FUNCTION_exdiscipline],
						   [Discipline].[Description] as [DISCIPLINE_TIMESHEET],
						   [L1].[LocationName] as [WORK_LOCATION],
						   [GetPhaseRecords].[Condition] AS [STATUS_L__E__ACE__LOE],
						   [PAF].[Condition] AS [CompareConditionPAF],
						   [GetPhaseRecords].[StartDate] as [PLANNED_START_DATE_as__Sep10],
						   [GetPhaseRecords].[EndDate] as [PLANNED_DEMOBILISATION_CURRENT],
						   [GetPhaseRecords].[Utilisation] As [UTILISATION],
						   [Employee].[Lastname] AS [INCUMBENTS_FAMILY_NAME],
						   [Employee].[Firstname] AS [INCUMBENTS_FIRST_NAME],
						   [GetPhaseRecords].[HoursPerWeek] AS [HRS__WEEK],
						   [PRF].[ReceptionDate] AS [PRF_reception_date_HRrecruitment],   
						   case 
							when [Employee].[Lastname] is null then       
								[Candidate].[LastName] + '' '' +[Candidate].[FirstName] 
							else null
						   end AS [Selected_Candidates_name],
						   [Nationality].[NationalityName] AS [Nationality],
						   [EmployeeDetail].Sex AS [Sexe_M__F],
						   [GetPhaseRecords].[NbChildren] AS [Nb_Children],
						   [PAF].[NbChildren] AS [CompareNbChildrenPAF],
						   [PAF].[ReceivedDate] AS [PAF_RECEIVED_DATE_HRrecruitment],
						   [PAF].[StartDate] AS [PAF_START_DATE_PAF_Form],		   
						   [L2].[LocationName] AS [Point_of_HIRE],
						   [EmployeeDetail].[ContractStartDate] AS [CONTRACT_START_DATE],
						   [Mobilisation].[ActualMobDate] AS [ACTUAL_MOBILISATION_DATE_SITE],
						   [PAF].[EndDate] AS [PAF_END_DATE_PAF_Form],
						   [EmployeeDetail].[ContractEndDate] AS [CONTRACT_END_DATE],
						   [Mobilisation].[ActualDemobDate] AS [ACTUAL_DMOB_DATE],						 	   
						   [PAF].[OutOfPolicy] AS [OUT_OF_POLICY],
						   [GetPhaseRecords].[MaritalStatus] AS [SINGLE__PARTNER__FAMILY__FU],
						   [PAF].[MaritalStatus] AS [CompareMaritalPAF],
						   [GetPhaseRecords].[CampStatus] AS [INSIDE__OUTSIDE_CAMP],
						   [PAF].[CampStatus] AS [CompareCampPAF],
						   [GetPhaseRecords].[TypeOfMobilisation] AS [Type_of_Mobilisation_for_E_LWE_ACE],
						   [GetPhaseRecords].[RotationType] AS [TYPE_OF_ROTATION],
						   [PAF].[RotationType] AS [CompareRotationPAF],
						   [PAF].[IssuedDate] AS [PAF_ISSUE_DATE_PAFTrack],
						   [PAF].[PAFStatus] AS [PAF_STATUS_PAF_Track],
						   [PAF].[ApprovedBy] AS [APPROVED_BY_PAF_Track],
						   [PAF].[PAFNumber] AS [PAF_NUMBER],
						   [Employee].[CompanyEmployeeNo] AS [EMPLOYEE_NUMBER],
						   [GetPhaseRecords].[CompanyID] AS [COMPANY],
						   [PAF].[Company] AS [CompareCompagnyPAF],
						   [Employee].[StartDate] AS [PAF_START_DATE_PAF_database],
						   [Employee].[ActualDemobilisationDate] AS [PAF_End_Date],
							DATEDIFF(MONTH, [GetPhaseRecords].[EndDate], [Employee].[ActualDemobilisationDate]) AS [DELTA], 
						   [Mobilisation].[VCCNumber] AS [VCC_reference_number],
						   [Mobilisation].[VisaStatus] AS [MOB__DMOB_STATUS],
						   [Mobilisation].[ETA] AS [ETA],
						   [GetPhaseRecords].[PointOfOrigin] AS [OrigineCode],
						   [PointOfOrigin].[TownName] AS [PointofOrigin],		  		  			  			
						   [Logistics].[LastRotation] AS [LastRotation],
						   DATEADD(dd,[Rotation].[InDays],[Logistics].[LastRotation]) AS [NextRotation],							   						   						  
						   DATENAME(mm, DATEADD(mm,dy,'''+ Convert(varchar(10),@startdate,101) + ''')) + Convert(varchar(30),DATEPART(yy, DATEADD(mm,dy,'''+ Convert(varchar(10),@startdate,101) + '''))) AS [MonthYear],
						   ' +@MANNING + ' AS [Manning],
						   [dbo].[getAverageHrsPerMth]([GetPhaseRecords].[Condition],[GetPhaseRecords].[WorkLocation],[Position].[DisciplineCode],[GetPhaseRecords].[MaritalStatus], [GetPhaseRecords].[CompanyID],[Position].[WorkGroupCode]) AS [Average]					
					 FROM [dbo].[GetPhaseRecords](''' + Convert(varchar(10),@enddate,101) + ''')
					INNER JOIN [Position] ON [Position].[PositionNumber] = [GetPhaseRecords].[PositionNumber]
					LEFT OUTER JOIN [Sector] ON [Position].[SectorCode] = [Sector].[SectorCode]
					LEFT OUTER JOIN [Job] ON [Position].[JobID] = [Job].[JobID]	
					LEFT JOIN [Recruitment] ON [Recruitment].[PhaseID] = [GetPhaseRecords].[PhaseID]
					LEFT OUTER JOIN [Department] ON [Department].[DepartmentCode] = [Position].[DepartmentCode]
					LEFT OUTER JOIN [AccountableManager] ON [AccountableManager].[AccountableManagerID] = [Department].[AccountableManagerID]
					LEFT OUTER JOIN [Function] ON [Function].[FunctionCode] = [Position].[FunctionCode]		
					LEFT OUTER JOIN [Area] ON [Area].[AreaCode] = [Position].[AreaCode]
					LEFT OUTER JOIN [Discipline] ON [Discipline].[DisciplineCode] = [Position].[DisciplineCode]				
					LEFT OUTER JOIN [PAF_DATA].[dbo].[Location] as L1 ON [L1].[LocationID] = [GetPhaseRecords].[WorkLocation]
					LEFT OUTER JOIN [PAF_DATA].[dbo].[Employee] ON [Employee].[EmployeeID] = [GetPhaseRecords].[EmployeeID]
					LEFT OUTER JOIN [EmployeeDetail] ON [EmployeeDetail].[EmployeeID] = [Employee].[EmployeeID]
					LEFT OUTER JOIN [PRF] ON [PRF].[PhaseID] = [GetPhaseRecords].[PhaseID]
					LEFT OUTER JOIN [PhaseCandidate] ON [PhaseCandidate].[PhaseID] = [GetPhaseRecords].[PhaseID] AND [PhaseCandidate].[Selected]=1
					LEFT JOIN [Candidate] ON [PhaseCandidate].[CandidateID] = [Candidate].[CandidateID]
					LEFT OUTER JOIN [Nationality] ON [Nationality].[NationalityID] = [EmployeeDetail].[Nationality]
					LEFT OUTER JOIN [PAF] ON [PAF].[PhaseID] = [GetPhaseRecords].[PhaseID]
					LEFT OUTER JOIN [Rotation] ON [PAF].[RotationType] = [Rotation].[RotationType]
					LEFT OUTER JOIN [Mobilisation] ON [Mobilisation].[PhaseID] = [GetPhaseRecords].[PhaseID]
					LEFT OUTER JOIN [PAF_DATA].[dbo].[Location] as L2 ON [L2].[LocationID] = [PAF].[PointOfHire]
					LEFT OUTER JOIN [Logistics] ON [Logistics].[PhaseID] = [GetPhaseRecords].[PhaseID]
					LEFT OUTER JOIN [PointOfOrigin] ON [PointOfOrigin].[PointOfOriginID] = [Logistics].[PointOfOriginID]	
					,#StaffingPlan
					WHERE [GetPhaseRecords].[PhaseStatus] not in (''CLOSED'',''CANCELLED'')'
	
	SET @REQ = 'SELECT  [KEY],
			   [ROME_CODE],
			   [TITRE_ROME_FRANCAIS] ,
			   [POSITION_TITLE_FRENCH__applies_to_SITE_ONLY] ,
			   [SECTEUR] ,
			   [DATE_POSTED_TO_CAP_EMPLOI],
			   [CAP_EMPLOI_reference_number] ,
			   [PMT_Cleared_with_CE_positions_code] ,
			   [Accountable_Manager] ,
			   [Responsible_Manager] ,
			   [NEW_POSITION_NUMBER] ,
			   [POSITION] ,
			   [GROUP] ,
			   [DEPARTMENT] ,
			   [AREA] ,
			   [FUNCTION_exdiscipline] ,
			   [DISCIPLINE_TIMESHEET] ,
			   [WORK_LOCATION] ,
			   [STATUS_L__E__ACE__LOE] ,
			   [CompareConditionPAF],
			   [PLANNED_START_DATE_as__Sep10],
			   [PLANNED_DEMOBILISATION_CURRENT],
			   [UTILISATION],
			   [INCUMBENTS_FAMILY_NAME] ,
			   [INCUMBENTS_FIRST_NAME] ,
			   [HRS__WEEK],
			   [PRF_reception_date_HRrecruitment],
			   [Selected_Candidates_name] ,
			   [Nationality] ,
			   [Sexe_M__F] ,
			   [Nb_Children],
			   [CompareNbChildrenPAF],			   
			   [PAF_RECEIVED_DATE_HRrecruitment],
			   [PAF_START_DATE_PAF_Form],
			   [Point_of_HIRE] ,
			   [CONTRACT_START_DATE],
			   [ACTUAL_MOBILISATION_DATE_SITE],
			   [PAF_END_DATE_PAF_Form],
			   [CONTRACT_END_DATE],
			   [ACTUAL_DMOB_DATE],			  
			   [OUT_OF_POLICY],
			   [SINGLE__PARTNER__FAMILY__FU] ,
			   [CompareMaritalPAF],
			   [INSIDE__OUTSIDE_CAMP] ,
			   [CompareCampPAF],
			   [Type_of_Mobilisation_for_E_LWE_ACE],
			   [TYPE_OF_ROTATION] ,
			   [CompareRotationPAF],
			   [PAF_ISSUE_DATE_PAFTrack],
			   [PAF_STATUS_PAF_Track] ,
			   [APPROVED_BY_PAF_Track] ,
			   [PAF_NUMBER] ,
			   [EMPLOYEE_NUMBER] ,
			   [COMPANY] ,
			   [CompareCompagnyPAF],
			   [PAF_START_DATE_PAF_database],
			   [PAF_End_Date],
			   [DELTA],
			   [VCC_reference_number] ,
			   [MOB__DMOB_STATUS] ,
			   [ETA],
			   [OrigineCode],
			   [PointofOrigin],		  		  			  			
			   [LastRotation],
			   [NextRotation],
			   [Average],'
			    + @Calendar +
			   'FROM ('+ @SRC +') as source  
					PIVOT
					  (
					  MAX([Manning]) FOR [MonthYear] in (' + @Calendar + ' )
					  ) As p
					GROUP BY   [KEY],
        	   [ROME_CODE],
			   [TITRE_ROME_FRANCAIS] ,
			   [POSITION_TITLE_FRENCH__applies_to_SITE_ONLY] ,
			   [SECTEUR] ,
			   [DATE_POSTED_TO_CAP_EMPLOI],
			   [CAP_EMPLOI_reference_number] ,
			   [PMT_Cleared_with_CE_positions_code] ,
			   [Accountable_Manager] ,
			   [Responsible_Manager] ,
			   [NEW_POSITION_NUMBER] ,
			   [POSITION] ,
			   [GROUP] ,
			   [DEPARTMENT] ,
			   [AREA] ,
			   [FUNCTION_exdiscipline] ,
			   [DISCIPLINE_TIMESHEET] ,
			   [WORK_LOCATION] ,
			   [STATUS_L__E__ACE__LOE] ,
			   [CompareConditionPAF],		
			   [PLANNED_START_DATE_as__Sep10],
			   [PLANNED_DEMOBILISATION_CURRENT],
			   [UTILISATION],
			   [INCUMBENTS_FAMILY_NAME] ,
			   [INCUMBENTS_FIRST_NAME] ,
			   [HRS__WEEK],
			   [PRF_reception_date_HRrecruitment],
			   [Selected_Candidates_name] ,
			   [Nationality] ,
			   [Nb_Children],
			   [CompareNbChildrenPAF],
			   [Sexe_M__F] ,
			   [PAF_RECEIVED_DATE_HRrecruitment],
			   [PAF_START_DATE_PAF_Form],
			   [Point_of_HIRE] ,
			   [CONTRACT_START_DATE],
			   [ACTUAL_MOBILISATION_DATE_SITE],
			   [PAF_END_DATE_PAF_Form],
			   [CONTRACT_END_DATE],
			   [ACTUAL_DMOB_DATE],
			   [STATUS_L__E__ACE__LOE] ,		   
			   [OUT_OF_POLICY],
			   [SINGLE__PARTNER__FAMILY__FU] ,
			   [CompareMaritalPAF],
			   [INSIDE__OUTSIDE_CAMP] ,
			   [CompareCampPAF],
			   [Type_of_Mobilisation_for_E_LWE_ACE],
			   [TYPE_OF_ROTATION] ,
			   [CompareRotationPAF],
			   [PAF_ISSUE_DATE_PAFTrack],
			   [PAF_STATUS_PAF_Track] ,
			   [APPROVED_BY_PAF_Track] ,
			   [PAF_NUMBER] ,
			   [EMPLOYEE_NUMBER] ,
			   [COMPANY] ,
			   [CompareCompagnyPAF],
			   [PAF_START_DATE_PAF_database],
			   [PAF_End_Date],
			   [DELTA],
			   [VCC_reference_number] ,
			   [MOB__DMOB_STATUS] ,
			   [ETA],
			   [OrigineCode],
			   [PointofOrigin],		  		  			  			
			   [LastRotation],
			   [NextRotation],			
			   [Average],' + @Calendar
	

	exec sp_executesql @REQ
	DROP TABLE  #StaffingPlan
END
GO
/****** Object:  Synonym [dbo].[Company]    Script Date: 03/17/2011 17:02:55 ******/
CREATE SYNONYM [dbo].[Company] FOR [PAF_DATA].[dbo].[Company]
GO
/****** Object:  Synonym [dbo].[Location]    Script Date: 03/17/2011 17:02:55 ******/
CREATE SYNONYM [dbo].[Location] FOR [PAF_DATA].[dbo].[Location]
GO
/****** Object:  Synonym [dbo].[EmployeeInfo]    Script Date: 03/17/2011 17:02:55 ******/
CREATE SYNONYM [dbo].[EmployeeInfo] FOR [paf_data].[dbo].[EmployeeInfo]
GO
/****** Object:  Table [dbo].[AllowanceHistory]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AllowanceHistory](
	[MaritalStatus] [char](2) NOT NULL,
	[CampStatus] [char](3) NOT NULL,
	[SubsistenceCost] [int] NOT NULL,
	[MobCost] [int] NOT NULL,
	[DemobCost] [int] NOT NULL,
	[RRCost] [int] NOT NULL,
	[HousingCost] [int] NOT NULL,
	[HistoryDate] [datetime] NOT NULL,
	[HistoryByUser] [varchar](40) NULL,
 CONSTRAINT [PK_AllowanceHistory] PRIMARY KEY CLUSTERED 
(
	[MaritalStatus] ASC,
	[CampStatus] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Allowance]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Allowance](
	[MaritalStatus] [char](2) NOT NULL,
	[CampStatus] [char](3) NOT NULL,
	[SubsistenceCost] [int] NOT NULL,
	[MobCost] [int] NOT NULL,
	[DemobCost] [int] NOT NULL,
	[RRCost] [int] NOT NULL,
	[HousingCost] [int] NOT NULL,
 CONSTRAINT [PK_Allowance] PRIMARY KEY CLUSTERED 
(
	[MaritalStatus] ASC,
	[CampStatus] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[PAFForm]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PAFForm]
AS
/*SELECT    
 dbo.Candidate.CandidateID, dbo.Candidate.LastName, dbo.Candidate.FirstName, dbo.Candidate.Prefered, dbo.Candidate.Selected, 
                      dbo.PhaseCandidate.PhaseID, dbo.PhaseCandidate.CandidateID AS Expr1, dbo.PAF.PhaseID AS Expr2, dbo.PAF.PAFNumber, dbo.PAF.PAFStatus, 
                      dbo.PAF.StartDate, dbo.PAF.EndDate, dbo.PAF.IssuedDate, dbo.PAF.ReceivedDate, dbo.PAF.ApprovedBy, dbo.PAF.RejectedBy, dbo.PAF.MaritalStatus, 
                      dbo.PAF.CampStatus, dbo.PAF.VisaNotRequired, dbo.PAF.ConditionStatus, dbo.PAF.OutOfPolicy, dbo.PAF.revision
                      
FROM         dbo.Candidate INNER JOIN
                      dbo.PhaseCandidate ON dbo.Candidate.CandidateID = dbo.PhaseCandidate.CandidateID INNER JOIN
                      dbo.PAF ON dbo.PhaseCandidate.PhaseID = dbo.PAF.PhaseID*/
                      select PAF.PhaseID,
                             Candidate.CandidateID, 
						     Candidate.LastName, 
						     Candidate.FirstName,
						     PAF.Revision 
					  FROM Candidate
                      INNER JOIN dbo.PhaseCandidate ON dbo.Candidate.CandidateID = dbo.PhaseCandidate.CandidateID
                      INNER JOIN dbo.PAF ON PAF.PhaseID = PhaseCandidate.PhaseID
                      WHERE PhaseCandidate.Selected = 1
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Candidate"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 121
               Right = 190
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PhaseCandidate"
            Begin Extent = 
               Top = 6
               Left = 228
               Bottom = 91
               Right = 380
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PAF"
            Begin Extent = 
               Top = 6
               Left = 418
               Bottom = 121
               Right = 578
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PAFForm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PAFForm'
GO
/****** Object:  View [dbo].[MobilisationForm]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MobilisationForm]
AS
/*SELECT    
 dbo.Candidate.CandidateID, dbo.Candidate.LastName, dbo.Candidate.FirstName, dbo.Candidate.Prefered, dbo.Candidate.Selected, 
                      dbo.PhaseCandidate.PhaseID, dbo.PhaseCandidate.CandidateID AS Expr1, dbo.PAF.PhaseID AS Expr2, dbo.PAF.PAFNumber, dbo.PAF.PAFStatus, 
                      dbo.PAF.StartDate, dbo.PAF.EndDate, dbo.PAF.IssuedDate, dbo.PAF.ReceivedDate, dbo.PAF.ApprovedBy, dbo.PAF.RejectedBy, dbo.PAF.MaritalStatus, 
                      dbo.PAF.CampStatus, dbo.PAF.VisaNotRequired, dbo.PAF.ConditionStatus, dbo.PAF.OutOfPolicy, dbo.PAF.revision
                      
FROM         dbo.Candidate INNER JOIN
                      dbo.PhaseCandidate ON dbo.Candidate.CandidateID = dbo.PhaseCandidate.CandidateID INNER JOIN
                      dbo.PAF ON dbo.PhaseCandidate.PhaseID = dbo.PAF.PhaseID*/
                      select PhaseID,
							 Candidate.CandidateID, 
						     Candidate.LastName, 
						     Candidate.FirstName
					  FROM Candidate
                      INNER JOIN dbo.PhaseCandidate ON dbo.Candidate.CandidateID = dbo.PhaseCandidate.CandidateID                 
                      WHERE PhaseCandidate.Selected = 1
GO
/****** Object:  Table [dbo].[Calendar]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calendar](
	[dy] [int] IDENTITY(0,1) NOT NULL,
	[temp] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketHistory]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TicketHistory](
	[MaritalStatus] [char](2) NOT NULL,
	[NbChildren] [int] NOT NULL,
	[PointOfOrigin] [char](4) NOT NULL,
	[Price] [int] NULL,
	[HistoryDate] [datetime] NOT NULL,
	[HistoryByUser] [varchar](40) NULL,
 CONSTRAINT [PK_TicketHistory] PRIMARY KEY CLUSTERED 
(
	[MaritalStatus] ASC,
	[NbChildren] ASC,
	[PointOfOrigin] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ticket](
	[MaritalStatus] [char](2) NOT NULL,
	[NbChildren] [int] NOT NULL,
	[PointOfOrigin] [char](4) NOT NULL,
	[Price] [int] NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[MaritalStatus] ASC,
	[NbChildren] ASC,
	[PointOfOrigin] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TEMP8]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP8](
	[STFNO] [nvarchar](255) NULL,
	[LNAME] [nvarchar](255) NULL,
	[FNAME] [nvarchar](255) NULL,
	[VCCNO] [nvarchar](255) NULL,
	[ETA] [nvarchar](255) NULL,
	[ACTLBEG] [datetime] NULL,
	[ACTLEND] [datetime] NULL,
	[STATUS] [nvarchar](255) NULL,
	[VCCNAT] [nvarchar](255) NULL,
	[COMPANY] [nvarchar](255) NULL,
	[F11] [nvarchar](255) NULL,
	[F12] [nvarchar](255) NULL,
	[F13] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TEMP7]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP7](
	[KEY] [float] NULL,
	[New Position Number] [nvarchar](255) NULL,
	[PLANNED STATUS] [nvarchar](255) NULL,
	[CAMP] [nvarchar](255) NULL,
	[Marital Status] [nvarchar](255) NULL,
	[Nb of Children] [float] NULL,
	[Rotation] [nvarchar](255) NULL,
	[Point of Origin] [nvarchar](255) NULL,
	[Company] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TEMP6]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP6](
	[Nationality] [nvarchar](255) NULL,
	[Sexe (M / F)] [nvarchar](255) NULL,
	[CONTRACT START DATE] [datetime] NULL,
	[CONTRACT END DATE] [datetime] NULL,
	[EMPLOYEE NUMBER] [nvarchar](255) NULL,
	[F6] [nvarchar](255) NULL,
	[F7] [nvarchar](255) NULL,
	[F8] [nvarchar](255) NULL,
	[F9] [nvarchar](255) NULL,
	[F10] [nvarchar](255) NULL,
	[F11] [nvarchar](255) NULL,
	[F12] [nvarchar](255) NULL,
	[F13] [nvarchar](255) NULL,
	[F14] [nvarchar](255) NULL,
	[F15] [nvarchar](255) NULL,
	[F16] [nvarchar](255) NULL,
	[F17] [nvarchar](255) NULL,
	[F18] [nvarchar](255) NULL,
	[F19] [nvarchar](255) NULL,
	[F20] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TEMP5]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP5](
	[Responsible Manager] [nvarchar](255) NULL,
	[Discipline (SQL)] [nvarchar](255) NULL,
	[NEW POSITION NUMBER] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TEMP4]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP4](
	[New Position Number] [nvarchar](255) NULL,
	[PLANNED START DATE as Sep10] [nvarchar](255) NULL,
	[PLANNED DEMOBILISATION (CURRENT)] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TEMP3]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP3](
	[New Position Number] [nvarchar](255) NULL,
	[PLANNED START DATE as Sep10] [nvarchar](255) NULL,
	[adjust phase start date] [nvarchar](255) NULL,
	[PLANNED DEMOBILISATION (CURRENT)] [nvarchar](255) NULL,
	[phase extension / reduction] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TEMP2]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP2](
	[ROME CODE] [nvarchar](255) NULL,
	[TITRE ROME FRANCAIS] [nvarchar](255) NULL,
	[POSITION TITLE (FRENCH) - applies to SITE ONLY] [nvarchar](255) NULL,
	[SECTEUR] [nvarchar](255) NULL,
	[DATE POSTED TO CAP EMPLOI] [datetime] NULL,
	[CAP EMPLOI reference number] [float] NULL,
	[OUVERTURE A L INTERNATIONALE] [nvarchar](255) NULL,
	[PMT "Cleared with CE" positions code] [nvarchar](255) NULL,
	[NEW POSITION NUMBER] [nvarchar](255) NULL,
	[FUNCTION (ex-discipline)] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TEMP1]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP1](
	[Nom] [nvarchar](255) NULL,
	[Zone] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[HistoPhase]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[HistoPhase]
AS
  
SELECT [PhaseID]
      ,[CompanyID]
      ,[EmployeeID]
      ,[PositionNumber]
      ,[StartDate]
      ,[EndDate]
      ,[PhaseStatus]
      ,[TypeOfMobilisation]
      ,[HoursPerWeek]
      ,[CampStatus]
      ,[MaritalStatus]
      ,[PointOfOrigin]
      ,[NbChildren]
      ,[Condition]
      ,[WorkLocation]
      ,[Utilisation]
      ,[Extension]        
      ,'current record' as HistoryOrigin
      ,'' as HistoryBy
      ,getDate() as HistoryDate
FROM         dbo.Phase 
UNION ALL
SELECT [PhaseID]
      ,[CompanyID]
      ,[EmployeeID]
      ,[PositionNumber]
      ,[StartDate]
      ,[EndDate]
      ,[PhaseStatus]
      ,[TypeOfMobilisation]
      ,[HoursPerWeek]
      ,[CampStatus]
      ,[MaritalStatus]
      ,[PointOfOrigin]
      ,[NbChildren]
      ,[Condition]
      ,[WorkLocation]
      ,[Utilisation]
      ,[Extension]
      ,[HistoryOrigin]
      ,[HistoryBy]
      ,[HistoryDate] 
      FROM  dbo.PhaseHistory
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[2] 2[39] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Phase"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 121
               Right = 223
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'HistoPhase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'HistoPhase'
GO
/****** Object:  Table [dbo].[WORKTABLE]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WORKTABLE](
	[Key Position (Contract)] [money] NULL,
	[Discipline (SQL)] [nvarchar](255) NULL,
	[incremental number] [nvarchar](255) NULL,
	[formatted number] [nvarchar](255) NULL,
	[Housing level] [nvarchar](255) NULL,
	[Point of Origin            (for E& LWE only)] [varchar](255) NULL,
	[Type of Mobilisation (for E,LWE, ACE)] [nvarchar](255) NULL,
	[Position's status] [nvarchar](255) NULL,
	[Revision Nber] [nvarchar](255) NULL,
	[New / Closed / Re-activated] [nvarchar](255) NULL,
	[PRIOR POSITION NUMBER] [nvarchar](255) NULL,
	[ROME CODE] [nvarchar](255) NULL,
	[TITRE ROME FRANCAIS] [nvarchar](255) NULL,
	[POSITION TITLE (FRENCH) - applies to SITE ONLY] [nvarchar](255) NULL,
	[SECTEUR] [nvarchar](255) NULL,
	[DATE POSTED TO CAP EMPLOI] [nvarchar](255) NULL,
	[CAP EMPLOI reference number] [nvarchar](255) NULL,
	[OUVERTURE A L INTERNATIONALE] [nvarchar](255) NULL,
	[PMT "Cleared with CE" positions code] [nvarchar](255) NULL,
	[Accountable Manager] [nvarchar](255) NULL,
	[Responsible Manager] [nvarchar](255) NULL,
	[NEW POSITION NUMBER] [nvarchar](255) NULL,
	[POSITION] [nvarchar](255) NULL,
	[GROUP] [nvarchar](255) NULL,
	[DEPARTMENT] [nvarchar](255) NULL,
	[AREA] [nvarchar](255) NULL,
	[FUNCTION (ex-discipline)] [nvarchar](255) NULL,
	[DISCIPLINE (TIMESHEET)] [nvarchar](255) NULL,
	[WORK LOCATION] [nvarchar](255) NULL,
	[PLANNED STATUS] [nvarchar](255) NULL,
	[PLANNED START DATE (as @ Sep-10)] [nvarchar](255) NULL,
	[FORECAST START DATE] [nvarchar](255) NULL,
	[PLANNED DEMOBILISATION (CURRENT)] [nvarchar](255) NULL,
	[UTILISATION] [nvarchar](255) NULL,
	[ACTUAL / FUTURE EMPLOYEE] [nvarchar](255) NULL,
	[INCUMBENT'S FAMILY NAME] [nvarchar](255) NULL,
	[INCUMBENT'S FIRST NAME] [nvarchar](255) NULL,
	[HRS / WEEK] [nvarchar](255) NULL,
	[PRF reception date (HR-recruitment)] [nvarchar](255) NULL,
	[Selected Candidate's name] [nvarchar](255) NULL,
	[Nationality] [nvarchar](255) NULL,
	[Sexe (M / F)] [nvarchar](255) NULL,
	[PAF RECEIVED DATE (HR-recruitment)] [nvarchar](255) NULL,
	[PAF START DATE (PAF Form)] [nvarchar](255) NULL,
	[Point of HIRE] [nvarchar](255) NULL,
	[CONTRACT START DATE] [nvarchar](255) NULL,
	[ACTUAL MOBILISATION DATE (SITE)] [nvarchar](255) NULL,
	[PAF END DATE (PAF Form)] [nvarchar](255) NULL,
	[NOTICE PERIOD] [nvarchar](255) NULL,
	[CONTRACT END DATE] [nvarchar](255) NULL,
	[ACTUAL DMOB DATE] [nvarchar](255) NULL,
	[ACTUAL STATUS (L / E / ACE / LOE)] [nvarchar](255) NULL,
	[OUT OF POLICY] [nvarchar](255) NULL,
	[SINGLE / PARTNER / FAMILY / FU] [nvarchar](255) NULL,
	[CONTRACT HOUSING PLANNED ] [nvarchar](255) NULL,
	[ACTUAL INSIDE / OUTSIDE CAMP] [nvarchar](255) NULL,
	[TYPE OF ROTATION] [nvarchar](255) NULL,
	[PAF ISSUE DATE (PAFTrack)] [nvarchar](255) NULL,
	[PAF STATUS (PAF Track)] [nvarchar](255) NULL,
	[APPROVED BY (PAF Track)] [nvarchar](255) NULL,
	[PAF NUMBER] [nvarchar](255) NULL,
	[EMPLOYEE NUMBER] [nvarchar](255) NULL,
	[COMPANY] [nvarchar](255) NULL,
	[PAF START DATE (PAF database)] [nvarchar](255) NULL,
	[FIRST DAY (Timesheet) including TT] [nvarchar](255) NULL,
	[PAF End Date] [nvarchar](255) NULL,
	[DELTA in MONTH (if <0, PAF will need extension)] [nvarchar](255) NULL,
	[VCC reference number] [nvarchar](255) NULL,
	[MOB / DMOB STATUS] [nvarchar](255) NULL,
	[ETA] [nvarchar](255) NULL,
	[Comments] [nvarchar](255) NULL,
	[Comment] [nvarchar](255) NULL,
	[Dec-10] [float] NULL,
	[Jan-11] [float] NULL,
	[Feb-11] [float] NULL,
	[Mar-11] [float] NULL,
	[Apr-11] [float] NULL,
	[May-11] [float] NULL,
	[Jun-11] [float] NULL,
	[Jul-11] [float] NULL,
	[Aug-11] [float] NULL,
	[Sep-11] [float] NULL,
	[Oct-11] [float] NULL,
	[Nov-11] [float] NULL,
	[Dec-11] [float] NULL,
	[Jan-12] [float] NULL,
	[Feb-12] [float] NULL,
	[Mar-12] [float] NULL,
	[Apr-12] [float] NULL,
	[May-12] [float] NULL,
	[Jun-12] [float] NULL,
	[Jul-12] [nvarchar](255) NULL,
	[Aug-12] [nvarchar](255) NULL,
	[Sep-12] [nvarchar](255) NULL,
	[Oct-12] [nvarchar](255) NULL,
	[Nov-12] [nvarchar](255) NULL,
	[Dec-12] [nvarchar](255) NULL,
	[Months to Go] [nvarchar](255) NULL,
	[Average Hours per Month] [nvarchar](255) NULL,
	[Hrs to Go] [nvarchar](255) NULL,
	[Point of Origin            (for E& LWE only)1] [nvarchar](255) NULL,
	[LAST R&R or 1ST MOB DATE] [nvarchar](255) NULL,
	[NEXT R&R DUE DATE] [nvarchar](255) NULL,
	[1] [money] NULL,
	[2] [money] NULL,
	[3] [money] NULL,
	[4] [money] NULL,
	[5] [money] NULL,
	[6] [money] NULL,
	[7] [money] NULL,
	[8] [money] NULL,
	[9] [money] NULL,
	[10] [money] NULL,
	[11] [money] NULL,
	[12] [money] NULL,
	[13] [money] NULL,
	[14] [money] NULL,
	[15] [money] NULL,
	[16] [money] NULL,
	[17] [money] NULL,
	[18] [money] NULL,
	[19] [money] NULL,
	[20] [money] NULL,
	[21] [money] NULL,
	[22] [money] NULL,
	[23] [money] NULL,
	[24] [money] NULL,
	[25] [money] NULL,
	[26] [money] NULL,
	[27] [money] NULL,
	[28] [money] NULL,
	[29] [money] NULL,
	[30] [money] NULL,
	[31] [money] NULL,
	[111] [money] NULL,
	[211] [money] NULL,
	[311] [money] NULL,
	[41] [money] NULL,
	[51] [money] NULL,
	[61] [money] NULL,
	[71] [money] NULL,
	[81] [money] NULL,
	[91] [money] NULL,
	[101] [money] NULL,
	[112] [money] NULL,
	[121] [money] NULL,
	[131] [money] NULL,
	[141] [money] NULL,
	[151] [money] NULL,
	[161] [money] NULL,
	[171] [money] NULL,
	[181] [money] NULL,
	[191] [money] NULL,
	[201] [money] NULL,
	[212] [money] NULL,
	[221] [money] NULL,
	[231] [money] NULL,
	[241] [money] NULL,
	[251] [money] NULL,
	[261] [money] NULL,
	[271] [money] NULL,
	[281] [money] NULL,
	[291] [money] NULL,
	[301] [money] NULL,
	[122] [money] NULL,
	[222] [money] NULL,
	[32] [money] NULL,
	[42] [money] NULL,
	[52] [money] NULL,
	[62] [money] NULL,
	[72] [money] NULL,
	[82] [money] NULL,
	[92] [money] NULL,
	[102] [money] NULL,
	[113] [money] NULL,
	[123] [money] NULL,
	[132] [money] NULL,
	[142] [money] NULL,
	[152] [money] NULL,
	[162] [money] NULL,
	[172] [money] NULL,
	[182] [money] NULL,
	[192] [money] NULL,
	[202] [money] NULL,
	[213] [money] NULL,
	[223] [money] NULL,
	[232] [money] NULL,
	[242] [money] NULL,
	[252] [money] NULL,
	[262] [money] NULL,
	[272] [money] NULL,
	[282] [money] NULL,
	[292] [money] NULL,
	[302] [money] NULL,
	[312] [money] NULL,
	[133] [money] NULL,
	[233] [money] NULL,
	[33] [money] NULL,
	[43] [money] NULL,
	[53] [money] NULL,
	[63] [money] NULL,
	[73] [money] NULL,
	[83] [money] NULL,
	[93] [money] NULL,
	[103] [money] NULL,
	[114] [money] NULL,
	[124] [money] NULL,
	[134] [money] NULL,
	[143] [money] NULL,
	[153] [money] NULL,
	[163] [money] NULL,
	[173] [money] NULL,
	[183] [money] NULL,
	[193] [money] NULL,
	[203] [money] NULL,
	[214] [money] NULL,
	[224] [money] NULL,
	[234] [money] NULL,
	[243] [money] NULL,
	[253] [money] NULL,
	[263] [money] NULL,
	[273] [money] NULL,
	[283] [money] NULL,
	[293] [money] NULL,
	[303] [money] NULL,
	[313] [money] NULL,
	[144] [money] NULL,
	[244] [money] NULL,
	[34] [money] NULL,
	[44] [money] NULL,
	[54] [money] NULL,
	[64] [money] NULL,
	[74] [money] NULL,
	[84] [money] NULL,
	[94] [money] NULL,
	[104] [money] NULL,
	[115] [money] NULL,
	[125] [money] NULL,
	[135] [money] NULL,
	[145] [money] NULL,
	[154] [money] NULL,
	[164] [money] NULL,
	[174] [money] NULL,
	[184] [money] NULL,
	[194] [money] NULL,
	[204] [money] NULL,
	[215] [money] NULL,
	[225] [money] NULL,
	[235] [money] NULL,
	[245] [money] NULL,
	[254] [money] NULL,
	[264] [money] NULL,
	[274] [money] NULL,
	[284] [money] NULL,
	[155] [money] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [UpdateDataForCosts]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I.Foulon
-- Create date: 14 Dec 2010
-- Description:	Update Data used to calcul costs in table phase
-- =============================================
CREATE TRIGGER [dbo].[UpdateDataForCosts] 
   ON  [dbo].[PAF]
   AFTER UPDATE
AS 
BEGIN

	SET NOCOUNT ON;

    DECLARE @PhaseID int
    DECLARE @Company char(3)
    DECLARE @Condition varchar(3)
    DECLARE @CampStatus char(3)
    DECLARE @MaritalStatus varchar(2)
    
    -- the company has been modified in the PAF
    SELECT @PhaseID = [PhaseID],
		   @Company = [Company]
    FROM deleted WHERE NOT EXISTS (SELECT 1 FROM inserted WHERE inserted.[Company] = deleted.[Company])
    
    IF @Company IS NOT NULL
		EXECUTE [dbo].[UpdateCompanyForCalculOfCosts] @PhaseID, @Company
		
	-- the condition has been modified in the PAF
	SELECT @PhaseID = [PhaseID],
		   @Condition = [Condition]
    FROM deleted WHERE NOT EXISTS (SELECT 1 FROM inserted WHERE inserted.[Condition] = deleted.[Condition])
    
    IF @Condition IS NOT NULL
		EXECUTE [dbo].[UpdateConditionForCalculOfCosts] @PhaseID, @Condition

	-- the CampStatus has been modified in the PAF
	SELECT @PhaseID = [PhaseID],
		   @CampStatus = [CampStatus]
    FROM deleted WHERE NOT EXISTS (SELECT 1 FROM inserted WHERE inserted.[CampStatus] = deleted.[CampStatus])
    
    IF @CampStatus IS NOT NULL
		EXECUTE [dbo].[UpdateCampStatusForCalculOfCosts] @PhaseID, @CampStatus

	-- the Marital Status has been modified in the PAF 
	SELECT @PhaseID = [PhaseID],
		   @MaritalStatus = [MaritalStatus]
    FROM deleted WHERE NOT EXISTS (SELECT 1 FROM inserted WHERE inserted.[MaritalStatus] = deleted.[MaritalStatus])
    
    IF @MaritalStatus IS NOT NULL
		EXECUTE [dbo].[UpdateMaritalStatusForCalculOfCosts] @PhaseID, @MaritalStatus						

END
GO
/****** Object:  View [dbo].[WORKTABLE2]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[WORKTABLE2]
AS
select COUNT(*) as nb
      	   ,EmployeeID
           ,[CONTRACT START DATE]
           ,[CONTRACT END DATE]
           ,[Sexe (M / F)] 
		   ,NationalityID 
		 FROM paf_data.dbo.Employee e1
         LEFT JOIN WORKTABLE w1 
		 ON  w1.[INCUMBENT'S FAMILY NAME] = e1.Lastname
		 AND w1.[INCUMBENT'S FIRST NAME] = e1.FirstName
         left outer join Nationality on Nationality.NationalityName = w1.[Nationality]      		
      		group by EmployeeID
				   ,[CONTRACT START DATE]
				   ,[CONTRACT END DATE]
				   ,[Sexe (M / F)] 
				   ,NationalityID
      		--having COUNT (*) = 1
GO
/****** Object:  Trigger [UpdateStatus]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I.Foulon
-- Create date: 14 Dec 2010
-- Description:	Update Status
-- =============================================
CREATE TRIGGER [dbo].[UpdateStatus] 
   ON  [dbo].[PAF]
   AFTER UPDATE, INSERT
AS 
BEGIN

	SET NOCOUNT ON;

    DECLARE @PhaseID int
    DECLARE @ApprovedBy varchar(40)
    DECLARE @OLDRejectedBy varchar(40)
    DECLARE @NEWRejectedBy varchar(40)
    DECLARE @CompanyID varchar(5)

    SELECT @PhaseID = [PhaseID] FROM inserted
    SELECT @NEWRejectedBy = [RejectedBy] FROM inserted
    SELECT @OLDRejectedBy = [RejectedBy] FROM deleted
    SELECT @CompanyID = [CompanyID] FROM Phase WHERE [PhaseID] = @PhaseID 
    
    -- The PAF has been approved
    SELECT @ApprovedBy = [ApprovedBy]
    FROM inserted 
    --WHERE NOT EXISTS (SELECT 1 FROM deleted WHERE inserted.[ApprovedBy] = deleted.[ApprovedBy])
    
    IF @ApprovedBy IS NOT NULL  AND @NEWRejectedBy IS NULL
    BEGIN
		UPDATE [PAF]
		SET [PAFStatus] = 'Approved',
			[ApprovedDate] = GETDATE()
		WHERE [PhaseID] = @PhaseID
		
		SELECT 1 FROM [Mobilisation] WHERE [PhaseID] = @PhaseID
		IF @@ROWCOUNT < 1
		BEGIN
			INSERT INTO [Mobilisation] ([PhaseID]) VALUES (@PhaseID)			
		    INSERT INTO [Logistics] ([PhaseID]) VALUES (@PhaseID)
		END
	END
		
	
    -- The PAF has been rejected    
    
    IF @NEWRejectedBy IS NOT NULL AND @OLDRejectedBy IS NULL 
    BEGIN
		
		UPDATE [PAF]
		SET [PAFStatus] = 'Rejected',
			[RejectedDate] = GETDATE()
		WHERE [PhaseID] = @PhaseID									
	END
	
	SELECT 1 FROM inserted WHERE [ApprovedBy] IS NULL AND [RejectedBy] IS NULL
	IF @@ROWCOUNT = 1
		UPDATE [PAF]
		SET [PAFStatus] = 'ToApprove',
		    [ApprovedDate] = null,
			[RejectedDate] = null
		WHERE [PhaseID] = @PhaseID
		
	-- When the planned Company is KNS, we approve directly the PAF
	IF @CompanyID = 'KNS'
    BEGIN
		UPDATE [PAF]
		SET [PAFStatus] = 'Approved',
		    [ApprovedBy]  ='GAS',
			[ApprovedDate] = GETDATE()
		WHERE [PhaseID] = @PhaseID
		
		SELECT 1 FROM [Mobilisation] WHERE [PhaseID] = @PhaseID
		IF @@ROWCOUNT < 1
		BEGIN
			INSERT INTO [Mobilisation] ([PhaseID]) VALUES (@PhaseID)			
		    INSERT INTO [Logistics] ([PhaseID]) VALUES (@PhaseID)
		END
	END
END
GO
/****** Object:  Trigger [TriggerReprise@]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I.Foulon	
-- Create date: 20 Dec 2010	
-- Description:	Trigger temporaire de reprise
-- =============================================
CREATE TRIGGER [dbo].[TriggerReprise@] 
   ON  [dbo].[Phase]
   AFTER UPDATE
AS 
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @status varchar(10)
    DECLARE @PhaseID int
    DECLARE @EmployeeID int
    DECLARE @WorkLocation char(3)
    DECLARE @Company char(3)
    DECLARE @KNS char(1)
    DECLARE @Workgroup varchar(10)
    DECLARE @PositionNumber char(8)
    DECLARE @HoursPerWeek int
    DECLARE @PRFDATE datetime
    DECLARE @Utilisation int
    DECLARE @Candidate varchar(100)
    DECLARE @CERefNumber varchar(10)
    DECLARE @PMTCode varchar(10)
    DECLARE @FirstName varchar(50)
    DECLARE @LastName varchar(50)
    DECLARE @PostedToCEDate datetime
    DECLARE @CandidateKEY int
    DECLARE @PAFNumber varchar(10)
    DECLARE @PAFStatus varchar(10)
    DECLARE @StartDate datetime
    DECLARE @EndDate datetime
    DECLARE @IssuedDate datetime
    DECLARE @ReceivedDate datetime
    DECLARE @ApprovedBy varchar(50)     
    DECLARE @MaritalStatus varchar(2)
    DECLARE @CampStatus char(3)        
    DECLARE @Condition varchar(3)
    DECLARE @RotationType varchar(10)       
    DECLARE @CompanyEmployeeNo char(10)
    DECLARE @LastnameManager varchar(100)
    DECLARE @ManagerID char(3)
    DECLARE @ActualMobDate datetime
    DECLARE @ActualDemobDate datetime
    DECLARE @VCCNumber varchar(20)
    DECLARE @VisaStatus varchar(10)
    DECLARE @ETA datetime
    DECLARE @LastRotation datetime
    DECLARE @PointOfOrigin varchar(50)
    DECLARE @PointOfOriginID int
    DECLARE @Department int
    DECLARE @flag int
    
    SET @flag = 0
    select @status = [PhaseStatus], 
		   @PhaseID = [PhaseID],
		   @WorkLocation = [WorkLocation],
		   @Company = [CompanyID],
		   @PositionNumber = PositionNumber,
		   @PRFDATE = PRFDATE,
		   @Utilisation = Utilisation,
		   @EmployeeID =[EmployeeID],
		   @PAFNumber = pafnumber,
		   @StartDate = startdatepaf,
		   @EndDate = enddatepaf,
		   @IssuedDate = issueddate,
		   @ReceivedDate = receiveddate,
		   @ApprovedBy = ApprovedBy,
		   @RotationType = RotationType,
		   @CompanyEmployeeNo = CompanyEmployeeNo,
		   @Condition = Condition,
		   @WorkLocation = WorkLocation,
		   @MaritalStatus = MaritalStatus,
		   @Candidate = Candidate,
		   @PAFStatus = PAFStatus,
		   @PointOfOriginID = PointOfOriginTown,
		   @CampStatus = CampStatus
		   ,@ActualmobDate = ActualMobDate
           ,@ActualDemobDate = ActualDemobDate
           ,@VCCNumber = VCCNumber
           ,@VisaStatus = VisaStatus
           ,@ETA = ETA
           ,@LastRotation = LastRotation
    from inserted
    
    select   @Workgroup = WorkGroupCode,
			 @Department = DepartmentCode
    from Position
    where PositionNumber = @PositionNumber
    
    
    -- Calcul de nbHous per week
    --SELECT @HoursPerWeek = dbo.getNbHoursPerWeek(@Condition,@WorkLocation,@Department,@MaritalStatus,@Company,@Workgroup)
  
    --UPDATE Phase
    --SET HoursPerWeek = @HoursPerWeek
    --WHERE PhaseID = @PhaseID
    
    -- Init le PRF all time!!!
    --INSERT INTO PRF (PhaseID,ReceptionDate,HoursPerWeek)
    --VALUES (@PhaseID,@PRFDATE,@HoursPerWeek*@Utilisation/100)
    
    -- Init le recrutement pour toutes les phases actives
    -- + quand un candidat est sélectionné
    if @status = 'ACTIVE' 
    BEGIN
	/*	INSERT INTO [HR].[dbo].[Recruitment]
           ([PhaseID]
           ,[CERefNumber]
           ,[PMTCode]
           ,[PostedToCEDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[Status])
     VALUES
           (@PhaseID
           ,@CERefNumber
           ,@PMTCode
           ,@PostedToCEDate
           ,'system_admin'
           ,GETDATE()         
           ,'Finished')*/
           update Recruitment
			set Status = 'Finished'
			where PhaseID = @PhaseID
				if (@Candidate is not null AND @Candidate != '')	
				BEGIN
				
					SELECT  @LastName =
							CASE 
								WHEN CHARINDEX(' ', @Candidate) > 0 THEN SUBSTRING(@Candidate, 1, 
									 CHARINDEX(' ', @Candidate) - 1)				
							END,
							@FirstName = 
							CASE 
								WHEN CHARINDEX(' ', @Candidate) > 0 THEN SUBSTRING(@Candidate, 
									 CHARINDEX(' ', @Candidate) + 1, LEN(@Candidate) - CHARINDEX(@Candidate, ' '))
							END
							
							
					INSERT INTO Candidate (EmployeeID,LastName,FirstName) VALUES (@EmployeeID,@LastName,@FirstName)
					SELECT @CandidateKEY = MAX(CandidateID) FROM Candidate
					INSERT INTO PhaseCandidate (PhaseID,CandidateID,Prefered,Selected,VisaNotRequired) VALUES (@PhaseID,@CandidateKEY,0,1,0)
				END
				ELSE
				BEGIN
					INSERT INTO Candidate (EmployeeID,LastName,FirstName)
					SELECT EmployeeID,
						   Lastname,
						   FirstName
					FROM paf_data.dbo.Employee
					WHERE EmployeeID = @EmployeeID
					SELECT @CandidateKEY = MAX(CandidateID) FROM Candidate
					INSERT INTO PhaseCandidate (PhaseID,CandidateID,Prefered,Selected,VisaNotRequired) 			
					VALUES (@PhaseID,@CandidateKEY,0,1,0)	
				END -- fin de traitement candidat pour PHASE ACTIVE
		
			-- INIT DU PAF
			SELECT @LastnameManager =
					CASE 
						WHEN CHARINDEX(' ', @ApprovedBy) > 0 THEN SUBSTRING(@ApprovedBy, 
							 CHARINDEX(' ', @ApprovedBy) + 1, LEN(@ApprovedBy) - CHARINDEX(@ApprovedBy, ' '))
					END
			SELECT @ManagerID = ID FROM ListManagers where Lastname = @LastnameManager
			
		SELECT @flag =1 from [HR].[dbo].[PAF] WHERE PhaseID = @PhaseID	
		IF @flag = 1
			UPDATE [HR].[dbo].[PAF]
			SET [PAFNumber] = ISNULL(@PAFNumber,[PAFNumber])
			   ,[PAFStatus] = 'Approved'
			   ,[StartDate] = ISNULL(@StartDate,[StartDate])
			   ,[EndDate] = ISNULL(@EndDate,[EndDate])
			   ,[IssuedDate] = ISNULL(@IssuedDate,[IssuedDate])
			   ,[ReceivedDate] = ISNULL(@ReceivedDate,[ReceivedDate])
			   ,[ApprovedBy] = ISNULL(@ManagerID,[ApprovedBy])
			   ,[MaritalStatus] = ISNULL(@MaritalStatus,[MaritalStatus])
			   ,[CampStatus] = ISNULL(@CampStatus,[CampStatus])
			   ,[Condition] = ISNULL(@Condition,[Condition])
			   ,[Company] = ISNULL(@Company,[Company])
			   ,[RotationType] = ISNULL(@RotationType,[RotationType])	
			   ,[CompanyEmployeeNo] = ISNULL(@CompanyEmployeeNo,[CompanyEmployeeNo])
			 WHERE PhaseID = @PhaseID
		ELSE								
			INSERT INTO [HR].[dbo].[PAF]
			   ([PhaseID]
			   ,[PAFNumber]
			   ,[PAFStatus]
			   ,[StartDate]
			   ,[EndDate]
			   ,[IssuedDate]
			   ,[ReceivedDate]
			   ,[ApprovedBy]
			   ,[MaritalStatus]
			   ,[CampStatus]
			   ,[Condition]
			   ,[Company]
			   ,[RotationType]
			   ,[Revision]
			   ,[CompanyEmployeeNo])
		 VALUES
			   (@PhaseID
			   ,@PAFNumber
			   ,'Approved'
			   ,@StartDate
			   ,@EndDate
			   ,@IssuedDate
			   ,@ReceivedDate
			   ,@ManagerID
			   ,@MaritalStatus
			   ,@CampStatus
			   ,@Condition
			   ,@Company
			   ,@RotationType
			   ,0
			   ,@CompanyEmployeeNo)  
    END -- fin traitement PHASE ACTIVE
    
    if @PAFStatus = 'Approved' or @ApprovedBy is not null
    BEGIN
		-- INIT MOBILISATION
		SET @flag = 0
		SELECT @flag =1 from [HR].[dbo].[Mobilisation] WHERE PhaseID = @PhaseID
		IF @flag = 1
			UPDATE [HR].[dbo].[Mobilisation]
			SET [ActualMobDate] = ISNULL(@ActualmobDate,[ActualMobDate])
			   ,[ActualDemobDate] = ISNULL(@ActualDemobDate,[ActualDemobDate])
			   ,[VCCNumber] = ISNULL(@VCCNumber,[VCCNumber])
			   ,[VisaStatus] = ISNULL(@VisaStatus,[VisaStatus])			   
			   ,[ETA] = ISNULL(@ETA,[ETA])
			 WHERE PhaseID = @PhaseID
		ELSE
			INSERT INTO [HR].[dbo].[Mobilisation]
			   ([PhaseID]
			   ,[ActualMobDate]
			   ,[ActualDemobDate]
			   ,[VCCNumber]
			   ,[VisaStatus]
			   ,[ETA])
			   VALUES
			   (@PhaseID
			   ,@ActualmobDate
			   ,@ActualDemobDate
			   ,@VCCNumber
			   ,@VisaStatus
			   ,@ETA)

		-- INIT LOGISTICS	
		SET @flag = 0
		SELECT @flag =1 from [HR].[dbo].[Mobilisation] WHERE PhaseID = @PhaseID
		IF @flag = 1
			UPDATE [HR].[dbo].[Logistics]
			SET [LastRotation] = ISNULL(@LastRotation,[LastRotation]),
				[PointOfOriginID] = ISNULL(@PointOfOriginID,[PointOfOriginID])
			WHERE PhaseID = @PhaseID		    
		ELSE			
		INSERT INTO [HR].[dbo].[Logistics]
           ([PhaseID]
           ,[LastRotation]
           ,[PointOfOriginID])
         VALUES
           (@PhaseID
           ,@LastRotation
           ,@PointOfOriginID)
    END
END
GO
/****** Object:  Trigger [TriggerReprise]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I.Foulon	
-- Create date: 20 Dec 2010	
-- Description:	Trigger temporaire de reprise
-- =============================================
CREATE TRIGGER [dbo].[TriggerReprise] 
   ON  [dbo].[Phase]
   AFTER UPDATE
AS 
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @status varchar(10)
    DECLARE @PhaseID int
    DECLARE @EmployeeID int
    DECLARE @WorkLocation char(3)
    DECLARE @Company char(3)
    DECLARE @KNS char(1)
    DECLARE @Workgroup varchar(10)
    DECLARE @PositionNumber char(8)
    DECLARE @HoursPerWeek int
    DECLARE @PRFDATE datetime
    DECLARE @Utilisation int
    DECLARE @Candidate varchar(100)
    DECLARE @CERefNumber varchar(10)
    DECLARE @PMTCode varchar(10)
    DECLARE @FirstName varchar(50)
    DECLARE @LastName varchar(50)
    DECLARE @PostedToCEDate datetime
    DECLARE @CandidateKEY int
    DECLARE @PAFNumber varchar(10)
    DECLARE @PAFStatus varchar(10)
    DECLARE @StartDate datetime
    DECLARE @EndDate datetime
    DECLARE @IssuedDate datetime
    DECLARE @ReceivedDate datetime
    DECLARE @ApprovedBy varchar(50)     
    DECLARE @MaritalStatus varchar(2)
    DECLARE @CampStatus char(3)        
    DECLARE @Condition varchar(3)
    DECLARE @RotationType varchar(10)       
    DECLARE @CompanyEmployeeNo char(10)
    DECLARE @LastnameManager varchar(100)
    DECLARE @ManagerID char(3)
    DECLARE @ActualMobDate datetime
    DECLARE @ActualDemobDate datetime
    DECLARE @VCCNumber varchar(20)
    DECLARE @VisaStatus varchar(10)
    DECLARE @ETA datetime
    DECLARE @LastRotation datetime
    DECLARE @PointOfOrigin varchar(50)
    DECLARE @PointOfOriginID int
    DECLARE @Department int
    
    
    select @status = [PhaseStatus], 
		   @PhaseID = [PhaseID],
		   @WorkLocation = [WorkLocation],
		   @Company = [CompanyID],
		   @PositionNumber = PositionNumber,
		   @PRFDATE = PRFDATE,
		   @Utilisation = Utilisation,
		   @EmployeeID =[EmployeeID],
		   @PAFNumber = pafnumber,
		   @StartDate = startdatepaf,
		   @EndDate = enddatepaf,
		   @IssuedDate = issueddate,
		   @ReceivedDate = receiveddate,
		   @ApprovedBy = ApprovedBy,
		   @RotationType = RotationType,
		   @CompanyEmployeeNo = CompanyEmployeeNo,
		   @Condition = Condition,
		   @WorkLocation = WorkLocation,
		   @MaritalStatus = MaritalStatus,
		   @Candidate = Candidate,
		   @PAFStatus = PAFStatus,
		   @PointOfOriginID = PointOfOriginTown
    from inserted
    
    select   @Workgroup = WorkGroupCode,
			 @Department = DepartmentCode
    from Position
    where PositionNumber = @PositionNumber
    
    
    -- Calcul de nbHous per week
    SELECT @HoursPerWeek = dbo.getNbHoursPerWeek(@Condition,@WorkLocation,@Department,@MaritalStatus,@Company,@Workgroup)
  
    UPDATE Phase
    SET HoursPerWeek = @HoursPerWeek
    WHERE PhaseID = @PhaseID
    
    -- Init le PRF all time!!!
    INSERT INTO PRF (PhaseID,ReceptionDate,HoursPerWeek)
    VALUES (@PhaseID,@PRFDATE,@HoursPerWeek*@Utilisation/100)
    
    -- Init le recrutement pour toutes les phases actives
    -- + quand un candidat est sélectionné
    if @status = 'ACTIVE' 
    BEGIN
		INSERT INTO [HR].[dbo].[Recruitment]
           ([PhaseID]
           ,[CERefNumber]
           ,[PMTCode]
           ,[PostedToCEDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[Status])
     VALUES
           (@PhaseID
           ,@CERefNumber
           ,@PMTCode
           ,@PostedToCEDate
           ,'system_admin'
           ,GETDATE()         
           ,'Finished')
				if (@Candidate is not null AND @Candidate != '')	
				BEGIN
				
					SELECT  @LastName =
							CASE 
								WHEN CHARINDEX(' ', @Candidate) > 0 THEN SUBSTRING(@Candidate, 1, 
									 CHARINDEX(' ', @Candidate) - 1)				
							END,
							@FirstName = 
							CASE 
								WHEN CHARINDEX(' ', @Candidate) > 0 THEN SUBSTRING(@Candidate, 
									 CHARINDEX(' ', @Candidate) + 1, LEN(@Candidate) - CHARINDEX(@Candidate, ' '))
							END
							
							
					INSERT INTO Candidate (EmployeeID,LastName,FirstName) VALUES (@EmployeeID,@LastName,@FirstName)
					SELECT @CandidateKEY = MAX(CandidateID) FROM Candidate
					INSERT INTO PhaseCandidate (PhaseID,CandidateID,Prefered,Selected,VisaNotRequired) VALUES (@PhaseID,@CandidateKEY,0,1,0)
				END
				ELSE
				BEGIN
					INSERT INTO Candidate (EmployeeID,LastName,FirstName)
					SELECT EmployeeID,
						   Lastname,
						   FirstName
					FROM paf_data.dbo.Employee
					WHERE EmployeeID = @EmployeeID
					SELECT @CandidateKEY = MAX(CandidateID) FROM Candidate
					INSERT INTO PhaseCandidate (PhaseID,CandidateID,Prefered,Selected,VisaNotRequired) 			
					VALUES (@PhaseID,@CandidateKEY,0,1,0)	
				END -- fin de traitement candidat pour PHASE ACTIVE
		
			-- INIT DU PAF
			SELECT @LastnameManager =
					CASE 
						WHEN CHARINDEX(' ', @ApprovedBy) > 0 THEN SUBSTRING(@ApprovedBy, 
							 CHARINDEX(' ', @ApprovedBy) + 1, LEN(@ApprovedBy) - CHARINDEX(@ApprovedBy, ' '))
					END
			SELECT @ManagerID = ID FROM ListManagers where Lastname = @LastnameManager
										
			INSERT INTO [HR].[dbo].[PAF]
			   ([PhaseID]
			   ,[PAFNumber]
			   ,[PAFStatus]
			   ,[StartDate]
			   ,[EndDate]
			   ,[IssuedDate]
			   ,[ReceivedDate]
			   ,[ApprovedBy]
			   ,[MaritalStatus]
			   ,[CampStatus]
			   ,[Condition]
			   ,[Company]
			   ,[RotationType]
			   ,[Revision]
			   ,[CompanyEmployeeNo])
		 VALUES
			   (@PhaseID
			   ,@PAFNumber
			   ,'Approved'
			   ,@StartDate
			   ,@EndDate
			   ,@IssuedDate
			   ,@ReceivedDate
			   ,@ManagerID
			   ,@MaritalStatus
			   ,@CampStatus
			   ,@Condition
			   ,@Company
			   ,@RotationType
			   ,0
			   ,@CompanyEmployeeNo)  
    END -- fin traitement PHASE ACTIVE
    
    if @status = 'OPEN' 
    BEGIN
		-- INIT RECRUITMENT when phase is open and candidate not null
        if (@Candidate is not null AND @Candidate != '')	
        BEGIN
        INSERT INTO [HR].[dbo].[Recruitment]
           ([PhaseID]
           ,[CERefNumber]
           ,[PMTCode]
           ,[PostedToCEDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[Status])
           VALUES
           (@PhaseID
           ,@CERefNumber
           ,@PMTCode
           ,@PostedToCEDate
           ,'system_admin'
           ,GETDATE()         
           ,'Finished')
		
		SELECT  @LastName =
				CASE 
					WHEN CHARINDEX(' ', @Candidate) > 0 THEN SUBSTRING(@Candidate, 1, 
						 CHARINDEX(' ', @Candidate) - 1)				
				END,
				@FirstName = 
				CASE 
					WHEN CHARINDEX(' ', @Candidate) > 0 THEN SUBSTRING(@Candidate, 
						 CHARINDEX(' ', @Candidate) + 1, LEN(@Candidate) - CHARINDEX(@Candidate, ' '))
				END
			    
				
		INSERT INTO Candidate (LastName,FirstName) VALUES (@LastName,@FirstName)
		SELECT @CandidateKEY = MAX(CandidateID) FROM Candidate
		INSERT INTO PhaseCandidate (PhaseID,CandidateID,Prefered,Selected,VisaNotRequired) VALUES (@PhaseID,@CandidateKEY,0,1,0)
		
		-- INIT PAF when the phase is OPEN
			SELECT @LastnameManager =
			    CASE 
					WHEN CHARINDEX(' ', @ApprovedBy) > 0 THEN SUBSTRING(@ApprovedBy, 
						 CHARINDEX(' ', @ApprovedBy) + 1, LEN(@ApprovedBy) - CHARINDEX(@ApprovedBy, ' '))
				END			
		
		SELECT @ManagerID = ID FROM ListManagers where Lastname = @LastnameManager
		
		SELECT @PAFStatus =
				case @PAFStatus	 
					when 'approved' THEN 'Approved'				
					else 'ToApprove'
				end
		
		INSERT INTO [HR].[dbo].[PAF]
           ([PhaseID]
           ,[PAFNumber]
           ,[PAFStatus]
           ,[StartDate]
           ,[EndDate]
           ,[IssuedDate]
           ,[ReceivedDate]
           ,[ApprovedBy]
           ,[MaritalStatus]
           ,[CampStatus]
           ,[Condition]
           ,[Company]
           ,[RotationType]
           ,[Revision]
           ,[CompanyEmployeeNo])
     VALUES
           (@PhaseID
        ,@PAFNumber
           ,@PAFStatus
           ,@StartDate
           ,@EndDate
           ,@IssuedDate
           ,@ReceivedDate
           ,@ManagerID
           ,@MaritalStatus
           ,@CampStatus
           ,@Condition
           ,@Company
           ,@RotationType
           ,0
           ,@CompanyEmployeeNo)  
		END
		ELSE -- il n'ya pas de candidat selectionné
		BEGIN
			INSERT INTO [HR].[dbo].[Recruitment]
			   ([PhaseID]
			   ,[CERefNumber]
			   ,[PMTCode]
			   ,[PostedToCEDate]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[Status])
			   VALUES
			   (@PhaseID
			   ,@CERefNumber
			   ,@PMTCode
			   ,@PostedToCEDate
			   ,'system_admin'
			   ,GETDATE()         
			   ,'InProgress')
		END
    END
    
    if @PAFStatus = 'Approved' or @ApprovedBy is not null
    BEGIN
		-- INIT MOBILISATION
		INSERT INTO [HR].[dbo].[Mobilisation]
           ([PhaseID]
           ,[ActualMobDate]
           ,[ActualDemobDate]
           ,[VCCNumber]
           ,[VisaStatus]
           ,[ETA])
     VALUES
           (@PhaseID
           ,@ActualmobDate
           ,@ActualDemobDate
           ,@VCCNumber
           ,@VisaStatus
           ,@ETA)

		-- INIT LOGISTICS			
		INSERT INTO [HR].[dbo].[Logistics]
           ([PhaseID]
           ,[LastRotation]
           ,[PointOfOriginID])
         VALUES
           (@PhaseID
           ,@LastRotation
           ,@PointOfOriginID)
    END
END
GO
/****** Object:  UserDefinedFunction [dbo].[getAverageHrsPerMth]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I.Foulon
-- Create date: 20 Dec 2010
-- Description:	get the average hours per month
-- =============================================
CREATE FUNCTION [dbo].[getAverageHrsPerMth]
(	
	@Condition varchar(3),
	@Location char(3),
	@Discipline varchar(10),
	@Marital varchar(2),
	@Company varchar (3),
	@WorkGroup varchar(10)
)
RETURNS  float
AS
BEGIN
	
	DECLARE @Construction char(1)
	DECLARE @KNS varchar (1)	
	DECLARE @Average float
	DECLARE @ConditionCur varchar(3)		
	DECLARE @ConstructionCur char(1)
	DECLARE @MaritalCur varchar(2)
	DECLARE @KNSCur varchar (1)
	DECLARE @WorkGroupCur varchar(10)	
	DECLARE @HoursPerWeekCur int
	DECLARE @WeeksPerYearCur int
	DECLARE @LeaveRRCur int
	DECLARE @PublicHolidaysCur int
	
	SET @Construction = '0'
	SELECT @Construction = '1'
	FROM Discipline where DisciplineCode in ('061S','062S','063S','064S','065S','066S','373S')
	AND DisciplineCode = @Discipline
	
	SET @KNS = '0'
	SELECT @KNS = '1'
	FROM paf_data.dbo.Company 
	where CompanyID = @Company
	and CompanyID = 'KNS'
	
	DECLARE HoursPerWeek CURSOR FOR 
	SELECT [Condition]		 
		  ,[Construction]
		  ,[Marital]
		  ,[KNS]
		  ,[WorkGroup]
		  ,[HoursPerWeek]
		  ,[WeeksPerYear]
		  ,[LeaveRR]
		  ,[PublicHolidays]
	FROM [NbHours]
	WHERE [LocationID] = @Location
	OR [KNS] = '1' AND [KNS] = @KNS 
	
	OPEN  HoursPerWeek                                                    
	FETCH NEXT FROM HoursPerWeek INTO @ConditionCur, @ConstructionCur, @MaritalCur, @KNSCur, @WorkGroupCur, 
									  @HoursPerWeekCur,@WeeksPerYearCur, @LeaveRRCur, @PublicHolidaysCur
	WHILE @@fetch_Status = 0                                               
	BEGIN
	                                           
			IF @Condition like @ConditionCur AND @Construction like @ConstructionCur
			AND @Marital like @MaritalCur AND @KNS like @KNSCur AND @WorkGroup like @WorkGroupCur
			
			SET @Average = (@HoursPerWeekCur * @WeeksPerYearCur 
							- @LeaveRRCur * @HoursPerWeekCur
							- @PublicHolidaysCur * @HoursPerWeekCur/5)/12	 	
	
	   FETCH NEXT FROM HoursPerWeek INTO @ConditionCur, @ConstructionCur, @MaritalCur, @KNSCur, @WorkGroupCur, 
									  @HoursPerWeekCur,@WeeksPerYearCur, @LeaveRRCur, @PublicHolidaysCur 
     END 
	CLOSE  HoursPerWeek 
	DEALLOCATE  HoursPerWeek
	 
	RETURN @Average

END
GO
/****** Object:  Table [dbo].[EmployeeDetail]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmployeeDetail](
	[EmployeeID] [int] NOT NULL,
	[ContractEndDate] [datetime] NULL,
	[ContractStartDate] [datetime] NULL,
	[Sex] [char](1) NULL,
	[BirthDate] [datetime] NULL,
	[Nationality] [int] NULL,
 CONSTRAINT [PK_EmployeeDetail] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[ListPositions]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ListPositions]
AS
SELECT     dbo.Position.PositionNumber, dbo.Discipline.Description AS Discipline, dbo.Department.Description AS Department, 
                      dbo.WorkGroup.Description AS WorkGroup, dbo.Area.Description AS Area,dbo.Job.Description As Job,
                       dbo.Job.JobID, dbo.Position.DepartmentCode, 
                      dbo.Position.DisciplineCode, dbo.Position.WorkGroupCode, dbo.Position.AreaCode
FROM         dbo.Area INNER JOIN
                      dbo.Position ON dbo.Area.AreaCode = dbo.Position.AreaCode INNER JOIN
                      dbo.Department ON dbo.Position.DepartmentCode = dbo.Department.DepartmentCode INNER JOIN
                      dbo.Discipline ON dbo.Position.DisciplineCode = dbo.Discipline.DisciplineCode 										
                      INNER JOIN
                      dbo.WorkGroup ON dbo.Position.WorkGroupCode = dbo.WorkGroup.WorkGroupCode  INNER JOIN
                      dbo.Job ON dbo.Position.JobID = Job.JobID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Area"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 91
               Right = 190
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Department"
            Begin Extent = 
               Top = 6
               Left = 228
               Bottom = 106
               Right = 421
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Discipline"
            Begin Extent = 
               Top = 6
               Left = 459
               Bottom = 91
               Right = 611
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "WorkGroup"
            Begin Extent = 
               Top = 6
               Left = 649
               Bottom = 91
               Right = 809
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Position"
            Begin Extent = 
               Top = 96
               Left = 38
               Bottom = 211
               Right = 201
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ListPositions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ListPositions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ListPositions'
GO
/****** Object:  Table [dbo].[DisciplineResponsible]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DisciplineResponsible](
	[key] [int] IDENTITY(1,1) NOT NULL,
	[DisciplineCode] [char](4) NOT NULL,
	[ResponsibleManagerID] [char](3) NOT NULL,
	[Principal] [bit] NOT NULL,
 CONSTRAINT [PK_DisciplineResponsible] PRIMARY KEY CLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [BlockUpdateIfExistsInPosition]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I. Foulon
-- Create date: 15 March 2011
-- Description:	Block update couple disciplinecode/responsible
-- =============================================
CREATE TRIGGER [dbo].[BlockUpdateIfExistsInPosition] ON [dbo].[DisciplineResponsible]
   INSTEAD OF UPDATE
AS 
BEGIN 

	SET NOCOUNT ON;
	DECLARE @DisciplineCodedel char(4)
	DECLARE @ResponsibleManagerIDdel char(3)
	
	DECLARE @DisciplineCodeins char(4)
	DECLARE @ResponsibleManagerIDins char(3)
	DECLARE @Principalins bit
	DECLARE @KEY int
	
	DECLARE @Control1 int
	DECLARE @Control2 int
	DECLARE @Control3 int
	
	SET @Control1 = 0
	SET @Control2 = 0
	SET @Control3 = 0

    select @DisciplineCodedel = DisciplineCode,
		   @ResponsibleManagerIDdel = ResponsibleManagerID
	from deleted
	
	select @DisciplineCodeins = DisciplineCode,
		   @ResponsibleManagerIDins = ResponsibleManagerID,
		   @Principalins  = Principal,
		   @KEY = [Key]
	from inserted
	
	-- on ne peut pas modifier un couple disciplineCode/Responsible, si il existe dans une position
	select @Control1 = 1 from Position where DisciplineCode = @DisciplineCodedel
	and ResponsibleManagerID = @ResponsibleManagerIDdel

	-- on ne peut pas avoir 2 responsables principaux pour une discipline donnée
	select @Control2 = 1 from DisciplineResponsible 
	where Principal = 1
	and DisciplineCode = @DisciplineCodeins
			
	-- chaque couple disciplineCode/responsible est unique
	select @Control3 = 1 from DisciplineResponsible 
	where DisciplineCode = @DisciplineCodeins 
	and ResponsibleManagerID = @ResponsibleManagerIDins	
	and [KEY] <> @KEY  
	
	IF (@Control1 <> 1 OR (@DisciplineCodedel = @DisciplineCodeins 
					   AND @ResponsibleManagerIDdel = @ResponsibleManagerIDins)) 
	AND (@Control2 <> 1 AND @Principalins = 1 OR @Principalins = 0) 
	AND (@Control3 <> 1)
	
		 UPDATE  DisciplineResponsible
		 SET DisciplineCode = @DisciplineCodeins,
			 responsibleManagerID  = @ResponsibleManagerIDins,
			 principal = @Principalins
		 WHERE [Key] = @KEY


END
GO
/****** Object:  Trigger [BlockInsertIfExistsInPosition]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I. Foulon
-- Create date: 15 March 2011
-- Description:	Block insert couple disciplinecode/responsible
-- =============================================
CREATE TRIGGER [dbo].[BlockInsertIfExistsInPosition] ON [dbo].[DisciplineResponsible]
   INSTEAD OF INSERT
AS 
BEGIN 

	SET NOCOUNT ON;
	DECLARE @DisciplineCodedel char(4)
	DECLARE @ResponsibleManagerIDdel char(3)
	
	DECLARE @DisciplineCodeins char(4)
	DECLARE @ResponsibleManagerIDins char(3)
	DECLARE @Principalins bit
	DECLARE @KEY bit
	
	DECLARE @Control1 int
	DECLARE @Control2 int
	DECLARE @Control3 int
	
	SET @Control1 = 0
	SET @Control2 = 0
	SET @Control3 = 0

    select @DisciplineCodedel = DisciplineCode,
		   @ResponsibleManagerIDdel = ResponsibleManagerID
	from deleted
	
	select @DisciplineCodeins = DisciplineCode,
		   @ResponsibleManagerIDins = ResponsibleManagerID,
		   @Principalins  = Principal,
		   @KEY = [Key]
	from inserted
		

	-- on ne peut pas avoir 2 responsables principaux pour une discipline donnée
	select @Control2 = 1 from DisciplineResponsible 
	where Principal = 1
	and DisciplineCode = @DisciplineCodeins
			
	-- chaque couple disciplineCode/responsible est unique
	select @Control3 = 1 from DisciplineResponsible 
	where DisciplineCode = @DisciplineCodeins 
	and ResponsibleManagerID = @ResponsibleManagerIDins	
	
	IF @Control1 <> 1 AND (@Control2 <> 1 AND @Principalins = 1) AND @Control3 <> 1
		 INSERT INTO DisciplineResponsible
		 SELECT @DisciplineCodeins,
				@ResponsibleManagerIDins,
				@Principalins
END
GO
/****** Object:  Trigger [BlockDelIfExistsInPosition]    Script Date: 03/17/2011 17:02:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		I. Foulon
-- Create date: 15 March 2011
-- Description:	Block update couple disciplinecode/responsible
-- =============================================
CREATE TRIGGER [dbo].[BlockDelIfExistsInPosition] ON [dbo].[DisciplineResponsible]
   INSTEAD OF DELETE
AS 
BEGIN 

	SET NOCOUNT ON;
	DECLARE @DisciplineCodedel char(4)
	DECLARE @ResponsibleManagerIDdel char(3)
	
	DECLARE @KEY bit
	
	DECLARE @Control1 int
	
	SET @Control1 = 0


    select @DisciplineCodedel = DisciplineCode,
		   @ResponsibleManagerIDdel = ResponsibleManagerID,
		   @KEY = [Key]
	from deleted

	
	-- on ne peut pas supprimer un couple disciplineCode/Responsible, si il existe dans une position
	select @Control1 = 1 from Position where DisciplineCode = @DisciplineCodedel
	and ResponsibleManagerID = @ResponsibleManagerIDdel

	
	IF @Control1 <> 1 
		 DELETE FROM  DisciplineResponsible
		 WHERE [Key] = @KEY


END
GO
/****** Object:  Default [DF__Disciplin__Princ__44CA3770]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[DisciplineResponsible] ADD  DEFAULT ((0)) FOR [Principal]
GO
USE [paf_data]
GO
/****** Object:  Default [DF_Employee_Creator]    Script Date: 03/17/2011 17:02:57 ******/
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_Creator]  DEFAULT (suser_sname()) FOR [Creator]
GO
/****** Object:  Default [DF_Employee_CreationDate]    Script Date: 03/17/2011 17:02:57 ******/
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
/****** Object:  Default [DF_Employee_EmployeeActive]    Script Date: 03/17/2011 17:02:57 ******/
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_EmployeeActive]  DEFAULT ('1') FOR [EmployeeActive]
GO
USE [HR]
GO
/****** Object:  Check [FK_Phase_Company]    Script Date: 03/17/2011 17:02:50 ******/
ALTER TABLE [dbo].[Phase]  WITH CHECK ADD  CONSTRAINT [FK_Phase_Company] CHECK  (([dbo].[ForeignKeyCompany]([CompanyID])>(0)))
GO
ALTER TABLE [dbo].[Phase] CHECK CONSTRAINT [FK_Phase_Company]
GO
/****** Object:  Check [FK_Phase_Employee]    Script Date: 03/17/2011 17:02:50 ******/
ALTER TABLE [dbo].[Phase]  WITH CHECK ADD  CONSTRAINT [FK_Phase_Employee] CHECK  (([dbo].[ForeignKeyEmployee]([EmployeeID])>(0)))
GO
ALTER TABLE [dbo].[Phase] CHECK CONSTRAINT [FK_Phase_Employee]
GO
/****** Object:  Check [FK_Phase_Location]    Script Date: 03/17/2011 17:02:50 ******/
ALTER TABLE [dbo].[Phase]  WITH CHECK ADD  CONSTRAINT [FK_Phase_Location] CHECK  (([dbo].[ForeignKeyLocation]([WorkLocation])>(0)))
GO
ALTER TABLE [dbo].[Phase] CHECK CONSTRAINT [FK_Phase_Location]
GO
/****** Object:  Check [FK_PAF_Company]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[PAF]  WITH CHECK ADD  CONSTRAINT [FK_PAF_Company] CHECK  (([dbo].[ForeignKeyCompany]([Company])>(0)))
GO
ALTER TABLE [dbo].[PAF] CHECK CONSTRAINT [FK_PAF_Company]
GO
/****** Object:  Check [FK_EmployeeDetail_Employee]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[EmployeeDetail]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetail_Employee] CHECK  (([dbo].[ForeignKeyEmployee]([EmployeeID])>(0)))
GO
ALTER TABLE [dbo].[EmployeeDetail] CHECK CONSTRAINT [FK_EmployeeDetail_Employee]
GO
USE [paf_data]
GO
/****** Object:  Check [FK_Employee_Discipline]    Script Date: 03/17/2011 17:02:57 ******/
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Discipline] CHECK  (([dbo].[ForeignKeyDiscipline]([DisciplineCode])>(0)))
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Discipline]
GO
USE [HR]
GO
/****** Object:  ForeignKey [FK_Recruitment_Phase]    Script Date: 03/17/2011 17:02:48 ******/
ALTER TABLE [dbo].[Recruitment]  WITH CHECK ADD  CONSTRAINT [FK_Recruitment_Phase] FOREIGN KEY([PhaseID])
REFERENCES [dbo].[Phase] ([PhaseID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Recruitment] CHECK CONSTRAINT [FK_Recruitment_Phase]
GO
/****** Object:  ForeignKey [FK_PRF_Phase]    Script Date: 03/17/2011 17:02:48 ******/
ALTER TABLE [dbo].[PRF]  WITH CHECK ADD  CONSTRAINT [FK_PRF_Phase] FOREIGN KEY([PhaseID])
REFERENCES [dbo].[Phase] ([PhaseID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PRF] CHECK CONSTRAINT [FK_PRF_Phase]
GO
/****** Object:  ForeignKey [FK_Mobilisation_Phase]    Script Date: 03/17/2011 17:02:48 ******/
ALTER TABLE [dbo].[Mobilisation]  WITH CHECK ADD  CONSTRAINT [FK_Mobilisation_Phase] FOREIGN KEY([PhaseID])
REFERENCES [dbo].[Phase] ([PhaseID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Mobilisation] CHECK CONSTRAINT [FK_Mobilisation_Phase]
GO
/****** Object:  ForeignKey [FK_Discipline_WorkGroup]    Script Date: 03/17/2011 17:02:48 ******/
ALTER TABLE [dbo].[Discipline]  WITH CHECK ADD  CONSTRAINT [FK_Discipline_WorkGroup] FOREIGN KEY([WorkGroupCode])
REFERENCES [dbo].[WorkGroup] ([WorkGroupCode])
GO
ALTER TABLE [dbo].[Discipline] CHECK CONSTRAINT [FK_Discipline_WorkGroup]
GO
/****** Object:  ForeignKey [FK_Comments_Phase]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Phase] FOREIGN KEY([PhaseID])
REFERENCES [dbo].[Phase] ([PhaseID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Phase]
GO
/****** Object:  ForeignKey [FK_PhaseCandidate_CandidateID]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[PhaseCandidate]  WITH CHECK ADD  CONSTRAINT [FK_PhaseCandidate_CandidateID] FOREIGN KEY([CandidateID])
REFERENCES [dbo].[Candidate] ([CandidateID])
GO
ALTER TABLE [dbo].[PhaseCandidate] CHECK CONSTRAINT [FK_PhaseCandidate_CandidateID]
GO
/****** Object:  ForeignKey [FK_PhaseCandidate_Phase]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[PhaseCandidate]  WITH CHECK ADD  CONSTRAINT [FK_PhaseCandidate_Phase] FOREIGN KEY([PhaseID])
REFERENCES [dbo].[Phase] ([PhaseID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhaseCandidate] CHECK CONSTRAINT [FK_PhaseCandidate_Phase]
GO
/****** Object:  ForeignKey [FK_PAF_Phase]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[PAF]  WITH CHECK ADD  CONSTRAINT [FK_PAF_Phase] FOREIGN KEY([PhaseID])
REFERENCES [dbo].[Phase] ([PhaseID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PAF] CHECK CONSTRAINT [FK_PAF_Phase]
GO
/****** Object:  ForeignKey [FK_PAF_Rotation]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[PAF]  WITH CHECK ADD  CONSTRAINT [FK_PAF_Rotation] FOREIGN KEY([RotationType])
REFERENCES [dbo].[Rotation] ([RotationType])
GO
ALTER TABLE [dbo].[PAF] CHECK CONSTRAINT [FK_PAF_Rotation]
GO
/****** Object:  ForeignKey [FK_Speciality_Job]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[Speciality]  WITH CHECK ADD  CONSTRAINT [FK_Speciality_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([JobID])
GO
ALTER TABLE [dbo].[Speciality] CHECK CONSTRAINT [FK_Speciality_Job]
GO
/****** Object:  ForeignKey [FK_PointOfOrigin_Country]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[PointOfOrigin]  WITH CHECK ADD  CONSTRAINT [FK_PointOfOrigin_Country] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Country] ([CountryID])
GO
ALTER TABLE [dbo].[PointOfOrigin] CHECK CONSTRAINT [FK_PointOfOrigin_Country]
GO
/****** Object:  ForeignKey [FK_Department_AccountableManager]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_AccountableManager] FOREIGN KEY([AccountableManagerID])
REFERENCES [dbo].[AccountableManager] ([AccountableManagerID])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_AccountableManager]
GO
/****** Object:  ForeignKey [FK_Logistics_PointOfOrigin]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[Logistics]  WITH CHECK ADD  CONSTRAINT [FK_Logistics_PointOfOrigin] FOREIGN KEY([PointOfOriginID])
REFERENCES [dbo].[PointOfOrigin] ([PointOfOriginID])
GO
ALTER TABLE [dbo].[Logistics] CHECK CONSTRAINT [FK_Logistics_PointOfOrigin]
GO
/****** Object:  ForeignKey [FK_EmployeeDetail_Nationality]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[EmployeeDetail]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetail_Nationality] FOREIGN KEY([Nationality])
REFERENCES [dbo].[Nationality] ([NationalityID])
GO
ALTER TABLE [dbo].[EmployeeDetail] CHECK CONSTRAINT [FK_EmployeeDetail_Nationality]
GO
/****** Object:  ForeignKey [FK_Position_Area]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Area] FOREIGN KEY([AreaCode])
REFERENCES [dbo].[Area] ([AreaCode])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_Area]
GO
/****** Object:  ForeignKey [FK_Position_Department]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Department] FOREIGN KEY([DepartmentCode])
REFERENCES [dbo].[Department] ([DepartmentCode])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_Department]
GO
/****** Object:  ForeignKey [FK_Position_Function]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Function] FOREIGN KEY([FunctionCode])
REFERENCES [dbo].[Function] ([FunctionCode])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_Function]
GO
/****** Object:  ForeignKey [FK_Position_Job]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([JobID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_Job]
GO
/****** Object:  ForeignKey [FK_Position_Sector]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Sector] FOREIGN KEY([SectorCode])
REFERENCES [dbo].[Sector] ([SectorCode])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_Sector]
GO
/****** Object:  ForeignKey [FK_Position_Speciality]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Speciality] FOREIGN KEY([SpecialityID])
REFERENCES [dbo].[Speciality] ([SpecialityID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_Speciality]
GO
/****** Object:  ForeignKey [FK_Position_WorkGroup]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_WorkGroup] FOREIGN KEY([WorkGroupCode])
REFERENCES [dbo].[WorkGroup] ([WorkGroupCode])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_WorkGroup]
GO
/****** Object:  ForeignKey [FK_DisciplineResponsible_Discipline]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[DisciplineResponsible]  WITH CHECK ADD  CONSTRAINT [FK_DisciplineResponsible_Discipline] FOREIGN KEY([DisciplineCode])
REFERENCES [dbo].[Discipline] ([DisciplineCode])
GO
ALTER TABLE [dbo].[DisciplineResponsible] CHECK CONSTRAINT [FK_DisciplineResponsible_Discipline]
GO
/****** Object:  ForeignKey [FK_DisciplineResponsible_ResponsibleManager]    Script Date: 03/17/2011 17:02:55 ******/
ALTER TABLE [dbo].[DisciplineResponsible]  WITH CHECK ADD  CONSTRAINT [FK_DisciplineResponsible_ResponsibleManager] FOREIGN KEY([ResponsibleManagerID])
REFERENCES [dbo].[ResponsibleManager] ([ResponsibleManagerID])
GO
ALTER TABLE [dbo].[DisciplineResponsible] CHECK CONSTRAINT [FK_DisciplineResponsible_ResponsibleManager]
GO
USE [paf_data]
GO
/****** Object:  ForeignKey [FK_Employee_AssignedLocation]    Script Date: 03/17/2011 17:02:57 ******/
ALTER TABLE [dbo].[Employee]  WITH NOCHECK ADD  CONSTRAINT [FK_Employee_AssignedLocation] FOREIGN KEY([RateAssignLocation])
REFERENCES [dbo].[Location] ([LocationID])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_AssignedLocation]
GO
/****** Object:  ForeignKey [FK_Employee_Company]    Script Date: 03/17/2011 17:02:57 ******/
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Company] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([CompanyID])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Company]
GO
/****** Object:  ForeignKey [FK_Employee_CompanyManager]    Script Date: 03/17/2011 17:02:57 ******/
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_CompanyManager] FOREIGN KEY([CompanyManagerID])
REFERENCES [dbo].[CompanyManager] ([CompanyManagerID])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_CompanyManager]
GO
/****** Object:  ForeignKey [FK_Employee_RatePayLocation]    Script Date: 03/17/2011 17:02:57 ******/
ALTER TABLE [dbo].[Employee]  WITH NOCHECK ADD  CONSTRAINT [FK_Employee_RatePayLocation] FOREIGN KEY([RatePayLocation])
REFERENCES [dbo].[Location] ([LocationID])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_RatePayLocation]
GO
/****** Object:  ForeignKey [FK_Location_Company]    Script Date: 03/17/2011 17:02:57 ******/
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Company] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([CompanyID])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Company]
GO
/****** Object:  ForeignKey [FK_Location_CurrencyID]    Script Date: 03/17/2011 17:02:57 ******/
ALTER TABLE [dbo].[Location]  WITH NOCHECK ADD  CONSTRAINT [FK_Location_CurrencyID] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[Currency] ([CurrencyID])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_CurrencyID]
GO
