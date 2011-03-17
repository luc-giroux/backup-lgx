USE [InetDb];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
--LGX : Trigger pour l'ajout de données par id works
CREATE TRIGGER [dbo].[IdWorks_Insert2] ON [dbo].[IdWorksView2]
INSTEAD OF INSERT
AS


DECLARE @AccessGroup					varchar(50)
DECLARE @AccueilSecurite				varchar(50)
DECLARE @AdressePermanente				varchar(50)
DECLARE @Amiante						varchar(50)
DECLARE @APBStatus						tinyint
DECLARE @CodeMetierROM					char(10) -- A VOIR SI VARCHAR
DECLARE @DateExpirationPermisTravail	varchar(50)
DECLARE @DateValiditePermis				varchar(50)
DECLARE @DateVisiteMedicale				varchar(50)
DECLARE @EHS1							varchar(50)
DECLARE @EHS2							varchar(50)
DECLARE @ImagePath						varchar(255)
DECLARE @IndividualId					smallint
DECLARE @LanguesParlees					varchar(100)
DECLARE @ListePermisDeConduire			varchar(50)
DECLARE @MentionParticuliere1			varchar(50)
DECLARE @MentionParticuliere2			varchar(50)
DECLARE @MentionParticuliere3			varchar(50)
DECLARE @MiFareCardNumber				varchar(16)
DECLARE @Mine							varchar(50)
DECLARE @Nationalite					varchar(50)
DECLARE @NiveauSecurite					varchar(50)
DECLARE @Nom							varchar(50)
DECLARE @NomPersonneContactUrgence		varchar(50)
DECLARE @NumCafatRuamTrav				varchar(50)
DECLARE @NumContrat						varchar(50)
DECLARE @NumeroPermisConduire			varchar(30)
DECLARE @NumeroPermisTravail			varchar(50)
DECLARE @NumeroUrgence					varchar(20)
DECLARE @PaysPermisConduire				varchar(20)
DECLARE @PeriodeDeTravail				varchar(50)
DECLARE @PermisDeConduire				bit
DECLARE @Port							varchar(50)
DECLARE @Prenom							varchar(50)
DECLARE @Qualification					varchar(50)
DECLARE @QuatreQuatre					varchar(50)
DECLARE @RecType						varchar(10)
DECLARE @Service						varchar(25)
DECLARE @Societe						varchar(50)
DECLARE @TempStartDate					datetime
DECLARE @TempStopDate					datetime
DECLARE @UserImage						Binary(5000)
DECLARE @Status							tinyint

-- insert data
SELECT

	@AccessGroup				= AccessGroup,
	@AccueilSecurite			= AccueilSecurite,
	@AdressePermanente			= AdressePermanente,
	@Amiante					= Amiante,
	@APBStatus					= APBStatus,
	@CodeMetierROM				= CodeMetierROM,
	@DateExpirationPermisTravail = DateExpirationPermisTravail,
	@DateValiditePermis			= DateValiditePermis,
	@DateVisiteMedicale			= DateVisiteMedicale,
	@EHS1						= EHS1,
	@EHS2						= EHS2,
	@ImagePath					= ImagePath,
	@IndividualId				= IndividualId,
	@LanguesParlees				= LanguesParlees,
	@ListePermisDeConduire		= ListePermisDeConduire,
	@MentionParticuliere1		= MentionParticuliere1,
	@MentionParticuliere2		= MentionParticuliere2,
	@MentionParticuliere3		= MentionParticuliere3,
	@MiFareCardNumber			= MiFareCardNumber,
	@Mine						= Mine,
	@Nationalite				= Nationalite,
	@NiveauSecurite				= NiveauSecurite,
	@Nom						= Nom,
	@NomPersonneContactUrgence	= NomPersonneContactUrgence,
	@NumCafatRuamTrav			= NumCafatRuamTrav,
	@NumContrat					= NumContrat,
	@NumeroPermisConduire		= NumeroPermisConduire,
	@NumeroPermisTravail		= NumeroPermisTravail,
	@NumeroUrgence				= NumeroUrgence,
	@PaysPermisConduire			= PaysPermisConduire,
	@PeriodeDeTravail			= PeriodeDeTravail,
	@PermisDeConduire			= PermisDeConduire,
	@Port						= Port,
	@Prenom						= Prenom,
	@Qualification				= Qualification,
	@QuatreQuatre				= QuatreQuatre,
	@RecType					= RecType,
	@Service					= Service,
	@Societe					= Societe,
	@TempStartDate				= TempStartDate,
	@TempStopDate				= TempStopDate,
	@UserImage					= UserImage

FROM inserted

-- Convert record type to tinyint
SET @Status = CASE @RecType WHEN 'TEMPORAIRE' THEN 1 WHEN 'DELETED' THEN 2 ELSE 0 END


/*
-- Temporary / permanent card processing
IF (@Status = 1) -- Temporary Card
BEGIN
	IF (@TempStart IS NULL)
	BEGIN
		SET @TempStart = CAST((@TempStartDate + ' ' + @TempStartTime) AS datetime)
		IF (@@ERROR <> 0)
		BEGIN
			RAISERROR ('TempStartDate or TempStartTime incorrect string format.', 16, 1) WITH SETERROR
			RETURN
		END
	END
	IF (@TempStop IS NULL)
	BEGIN

		SET @TempStop = CAST((@TempStopDate + ' ' + @TempStopTime) AS datetime)
		IF (@@ERROR <> 0)
		BEGIN
			RAISERROR ('TempStopDate or TempStopTime incorrect string format.', 16, 1) WITH SETERROR
			RETURN
		END
	END
END

ELSE -- Permanent Card
BEGIN -- Don't allow NULL to be inserted into non-nullable field
	IF (@TempStart IS NULL)
		SET @TempStart = 0
	IF (@TempStop IS NULL)
		SET @TempStop = 0
END

*/
-- Supply defaults for these fields
IF @APBStatus IS NULL
	SET @APBStatus = 1


-- Insertion des données dans la table INDIVIDUALS
INSERT INTO dbo.Individuals
		(IndivId,  TenantNdx,  FirstName,  LastName,  TempStart,  TempStop,  Status,  IssueLevel,  ImagePath,  AccessVerNdx, APBtype,  IamGraced,  IamDisabled)
VALUES	(@IndividualId, 1, @Prenom, @Nom, @TempStartDate, @TempStopDate, @Status, 0, @ImagePath, 1, @APBStatus, 0, 0)

-- Insertion des données dans la table INDIVCUSTOMDATA
IF (@Societe IS NOT NULL OR @Qualification IS NOT NULL OR @AccueilSecurite IS NOT NULL OR @NumCafatRuamTrav IS NOT NULL OR
	@ListePermisDeConduire IS NOT NULL OR @DateValiditePermis IS NOT NULL OR
	@DateVisiteMedicale IS NOT NULL OR @QuatreQuatre IS NOT NULL OR @EHS1 IS NOT NULL OR @EHS2 IS NOT NULL OR
	@Port IS NOT NULL OR @Mine IS NOT NULL OR @AccessGroup IS NOT NULL OR @NiveauSecurite IS NOT NULL)
INSERT INTO dbo.IndivCustomData
		(IndivNdx, TenantNdx,  Custom01,  Custom02,  Custom03,  Custom04, Custom07,
		 Custom08, Custom09,   Custom10,  Custom11,  Custom12,  Custom13,  Custom14,  Custom15,  Custom16)
VALUES	(@IndividualId,1, @Societe, @Qualification, @AccueilSecurite, @NumCafatRuamTrav, @ListePermisDeConduire,
		@DateValiditePermis, @DateVisiteMedicale,  @QuatreQuatre, @EHS1, @EHS2, @Port, @Mine, @AccessGroup, @NiveauSecurite)

		
-- Insertion des données dans la table INDIVCUSTOMDATA2
IF (@Nationalite IS NOT NULL OR @AdressePermanente IS NOT NULL OR @NomPersonneContactUrgence IS NOT NULL OR @NumeroUrgence IS NOT NULL OR
	@PaysPermisConduire IS NOT NULL OR @NumeroPermisConduire IS NOT NULL OR
	@NumeroPermisTravail IS NOT NULL OR @DateExpirationPermisTravail IS NOT NULL OR @CodeMetierROM IS NOT NULL OR @Service IS NOT NULL OR
	@NumContrat IS NOT NULL OR @LanguesParlees IS NOT NULL OR @PermisDeConduire IS NOT NULL OR @MentionParticuliere1 IS NOT NULL OR
	@MentionParticuliere2 IS NOT NULL OR @MentionParticuliere3 IS NOT NULL OR @PeriodeDeTravail IS NOT NULL)
INSERT INTO dbo.IndivCustomData2
		(IndivNum, TenantNdx,  Nationalite,  AdressePermanente,  NomPersonneContactUrgence,  NumeroUrgence, PaysPermisConduire,
		 NumeroPermisConduire, NumeroPermisTravail,   DateExpirationPermisTravail,  CodeMetierROM,  Service,  NumContrat,  LanguesParlees, 
		 PermisDeConduire, MentionParticuliere1, MentionParticuliere2, MentionParticuliere3, PeriodeDeTravail)
VALUES	(@IndividualId,1, @Nationalite, @AdressePermanente, @NomPersonneContactUrgence, @NumeroUrgence, @PaysPermisConduire,
		 @NumeroPermisConduire, @NumeroPermisTravail,   @DateExpirationPermisTravail,  @CodeMetierROM,  @Service,  @NumContrat,  @LanguesParlees, 
		 @PermisDeConduire, @MentionParticuliere1, @MentionParticuliere2, @MentionParticuliere3, @PeriodeDeTravail)
		
		
-- Insertion des données dans la table CARD
IF (@MiFareCardNumber IS NOT NULL)
	INSERT Cards (TenantNdx, IndivNdx, Resident, CardNumber, Disabled)
	VALUES (1, @IndividualId, 0, dbo.HexToBinary(@MiFareCardNumber), 0)

	
-- 	Insertion des données dans la table XRefIndivGroupDoor
-- Group assignment
-- JC 2009-05-03: Removed as Group management removed from IdWorks application
-- replaced with standard group 536870929 - Vavouto Main Entrance
--IF (@GroupNdx IS NOT NULL)
INSERT INTO dbo.XRefIndivGroupDoor
			(IndivNdx, TenantNdx,  DoorGroupNdx, PriorityOrder, DoorSchedule)
	VALUES	(@IndividualId, 1, 536870929,	 1, 0)

-- Picture saving
INSERT INTO [InetDb].[dbo].[IndivImages]
           ([TenantNdx], [IndivNdx], [UserImage])
     VALUES (1, @IndividualId, @UserImage)

-- Write change in DPURestore table and notify INET through INetMessenger
EXEC IdWorks_Notify 'IdWorks Insert', 1, @IndividualId
GO
