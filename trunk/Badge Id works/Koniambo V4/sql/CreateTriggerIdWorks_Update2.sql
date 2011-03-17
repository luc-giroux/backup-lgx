USE [InetDb];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
--BUILD 2149
CREATE TRIGGER [dbo].[IdWorks_Update2] ON [dbo].[IdWorksView2]
INSTEAD OF UPDATE
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
--
DECLARE @CustExists		bit
DECLARE @CustNeeded		bit
DECLARE @Cust2Exists		bit
DECLARE @Cust2Needed		bit
DECLARE @OldMiFareCardNumber	varchar(16)
DECLARE @OldImagePath	varchar(260)

DECLARE UpdSearch CURSOR LOCAL FOR SELECT
		AccessGroup,
		AccueilSecurite,
		AdressePermanente,
		Amiante,
		APBStatus,
		CodeMetierROM,
		DateExpirationPermisTravail,
		DateValiditePermis,
		DateVisiteMedicale,
		EHS1,
		EHS2,
		ImagePath,
		IndividualId,
		LanguesParlees,
		ListePermisDeConduire,
		MentionParticuliere1,
		MentionParticuliere2,
		MentionParticuliere3,
		MiFareCardNumber,
		Mine,
		Nationalite,
		NiveauSecurite,
		Nom,
		NomPersonneContactUrgence,
		NumCafatRuamTrav,
		NumContrat,
		NumeroPermisConduire,
		NumeroPermisTravail,
		NumeroUrgence,
		PaysPermisConduire,
		PeriodeDeTravail,
		PermisDeConduire,
		Port,
		Prenom,
		Qualification,
		QuatreQuatre,
		RecType,
		Service,
		Societe,
		TempStartDate,
		TempStopDate,
		UserImage
		FROM inserted
OPEN UpdSearch

DECLARE DelSearch CURSOR LOCAL FOR SELECT
		MiFareCardNumber,
		ImagePath
		FROM deleted
OPEN DelSearch

WHILE 0<>1
BEGIN

	-- Update data
	FETCH NEXT FROM UpdSearch INTO
			 @AccessGroup					,
			 @AccueilSecurite				,
			 @AdressePermanente				,
			 @Amiante						,
			 @APBStatus						,
			 @CodeMetierROM					,
			 @DateExpirationPermisTravail	,
			 @DateValiditePermis			,
			 @DateVisiteMedicale			,
			 @EHS1							,
			 @EHS2							,
			 @ImagePath						,
			 @IndividualId					,
			 @LanguesParlees				,
			 @ListePermisDeConduire			,
			 @MentionParticuliere1			,
			 @MentionParticuliere2			,
			 @MentionParticuliere3			,
			 @MiFareCardNumber				,
			 @Mine							,
			 @Nationalite					,
			 @NiveauSecurite				,
			 @Nom							,
			 @NomPersonneContactUrgence		,
			 @NumCafatRuamTrav				,
			 @NumContrat					,
			 @NumeroPermisConduire			,
			 @NumeroPermisTravail			,
			 @NumeroUrgence					,
			 @PaysPermisConduire			,
			 @PeriodeDeTravail				,
			 @PermisDeConduire				,
			 @Port							,
			 @Prenom						,
			 @Qualification					,
			 @QuatreQuatre					,
			 @RecType						,
			 @Service						,
			 @Societe						,
			 @TempStartDate					,
			 @TempStopDate					,
			 @UserImage						

	IF @@FETCH_STATUS <> 0
	BREAK

	FETCH NEXT FROM DelSearch INTO @OldMiFareCardNumber, @OldImagePath
	IF @@FETCH_STATUS <> 0
	BREAK

	-- Convert record type from varchar(1) to tinyint
	SET @Status = CASE @RecType WHEN 'TEMPORAIRE' THEN 1 WHEN 'DELETED' THEN 2 ELSE 0 END


/*
	-- Handle temporary / permanent cards
	IF (@Status = 1) -- Temporary Card
	BEGIN
		IF ((NOT(UPDATE(TempStart))) OR (@TempStartDate IS NULL))
		BEGIN
			SET @TempStartDate = CAST((@TempStartDate + ' ' + @TempStartTime) AS datetime)
			IF (@@ERROR <> 0)
			BEGIN
				RAISERROR ('TempStartDate or TempStartTime incorrect string format.', 16, 1) WITH SETERROR
				RETURN
			END
		END
		IF ((NOT(UPDATE(TempStop))) OR (@TempStopDate IS NULL))
		BEGIN
			SET @TempStopDate = CAST((@TempStopDate + ' ' + @TempStopTime) AS datetime)
			IF (@@ERROR <> 0)
			BEGIN
				RAISERROR ('TempStopDate or TempStopTime incorrect string format.', 16, 1) WITH SETERROR
				RETURN
			END
		END
	END

	ELSE -- Permanent Card
	BEGIN -- Don't allow NULL to be inserted into non-nullable field
		IF (@TempStartDate IS NULL)
			SET @TempStartDate = 0
		IF (@TempStopDate IS NULL)
			SET @TempStopDate = 0
	END
*/
	-- Supply defaults for these fields
	IF @APBStatus IS NULL
		SET @APBStatus = 1


	UPDATE dbo.Individuals
	SET
		FirstName			= @Prenom,
		LastName			= @Nom,
		TempStart			= @TempStartDate,
		TempStop			= @TempStopDate,
		Status				= @Status,
		IssueLevel			= 0,
		ImagePath			= @ImagePath,
		APBtype				= @APBStatus,
		IamGraced			= 0,
		IamDisabled			= 0
	WHERE
		IndivId = @IndividualId AND TenantNdx = 1
		
		--OK JUSQUE LA

	-- Update / insert in table INDIVCUSTOMDATA
	IF (@Societe IS NOT NULL OR @Qualification IS NOT NULL OR @AccueilSecurite IS NOT NULL OR @NumCafatRuamTrav IS NOT NULL OR
		@ListePermisDeConduire IS NOT NULL OR @DateValiditePermis IS NOT NULL OR
		@DateVisiteMedicale IS NOT NULL OR @QuatreQuatre IS NOT NULL OR @EHS1 IS NOT NULL OR @EHS2 IS NOT NULL OR
		@Port IS NOT NULL OR @Mine IS NOT NULL OR @AccessGroup IS NOT NULL OR @NiveauSecurite IS NOT NULL)
		SET @CustNeeded = 1
	ELSE
		SET @CustNeeded = 0

	IF (EXISTS (SELECT * FROM IndivCustomData WHERE IndivNdx = @IndividualId AND TenantNdx = 1))
		SET @CustExists = 1
	ELSE
		SET @CustExists = 0

	IF @CustNeeded=1
	BEGIN
		IF @CustExists=1
			UPDATE IndivCustomData
			SET
				Custom01 = @Societe,
				Custom02 = @Qualification,
				Custom03 = @AccueilSecurite,
				Custom04 = @NumCafatRuamTrav,
				Custom07 = @ListePermisDeConduire,
				Custom08 = @DateValiditePermis,
				Custom09 = @DateVisiteMedicale,
				Custom10 = @QuatreQuatre,
				Custom11 = @EHS1,
				Custom12 = @EHS2,
				Custom13 = @Port,
				Custom14 = @Mine,
				Custom15 = @AccessGroup,
				Custom16 = @NiveauSecurite
			WHERE
				IndivNdx = @IndividualId AND TenantNdx = 1
		ELSE
			INSERT INTO dbo.IndivCustomData
					(IndivNdx, TenantNdx,  Custom01,  Custom02,  Custom03,  Custom04, Custom07,
					 Custom08, Custom09,   Custom10,  Custom11,  Custom12,  Custom13,  Custom14,  Custom15,  Custom16)
			VALUES	(@IndividualId,1, @Societe, @Qualification, @AccueilSecurite, @NumCafatRuamTrav, @ListePermisDeConduire,
					@DateValiditePermis, @DateVisiteMedicale,  @QuatreQuatre, @EHS1, @EHS2, @Port, @Mine, @AccessGroup, @NiveauSecurite)
	END
	--ELSE
	--BEGIN
	--	IF @CustExists=1
	--		DELETE FROM IndivCustomData WHERE @TenantNdx = 1 AND @IndividualId = IndivNdx
	--END

	
	
	
	-- Update / insert in table INDIVCUSTOMDATA2
	IF (@Nationalite IS NOT NULL OR @AdressePermanente IS NOT NULL OR @NomPersonneContactUrgence IS NOT NULL OR @NumeroUrgence IS NOT NULL OR
		@PaysPermisConduire IS NOT NULL OR @NumeroPermisConduire IS NOT NULL OR
		@NumeroPermisTravail IS NOT NULL OR @DateExpirationPermisTravail IS NOT NULL OR @CodeMetierROM IS NOT NULL OR @Service IS NOT NULL OR
		@NumContrat IS NOT NULL OR @LanguesParlees IS NOT NULL OR @PermisDeConduire IS NOT NULL OR @MentionParticuliere1 IS NOT NULL OR
		@MentionParticuliere2 IS NOT NULL OR @MentionParticuliere3 IS NOT NULL OR @PeriodeDeTravail IS NOT NULL)
		SET @Cust2Needed = 1
	ELSE
		SET @Cust2Needed = 0

	IF (EXISTS (SELECT * FROM IndivCustomData2 WHERE IndivNum = @IndividualId AND TenantNdx = 1))
		SET @Cust2Exists = 1
	ELSE
		SET @Cust2Exists = 0

	IF @Cust2Needed=1
	BEGIN
		IF @Cust2Exists=1
			UPDATE IndivCustomData2
			SET
				Nationalite = @Nationalite,
				AdressePermanente = @AdressePermanente,
				NomPersonneContactUrgence = @NomPersonneContactUrgence,
				NumeroUrgence = @NumeroUrgence,
				PaysPermisConduire = @PaysPermisConduire,
				NumeroPermisConduire = @NumeroPermisConduire,
				NumeroPermisTravail = @NumeroPermisTravail,
				DateExpirationPermisTravail = @DateExpirationPermisTravail,
				CodeMetierROM = @CodeMetierROM,
				Service = @Service,
				NumContrat = @NumContrat,
				LanguesParlees = @LanguesParlees,
				PermisDeConduire = @PermisDeConduire,
				MentionParticuliere1 = @MentionParticuliere1,
				MentionParticuliere2 = @MentionParticuliere2,
				MentionParticuliere3 = @MentionParticuliere3,
				PeriodeDeTravail = @PeriodeDeTravail
			WHERE
				IndivNum = @IndividualId AND TenantNdx = 1
		ELSE
			INSERT INTO dbo.IndivCustomData2
					(IndivNum, TenantNdx,  Nationalite,  AdressePermanente,  NomPersonneContactUrgence,  NumeroUrgence, PaysPermisConduire,
					 NumeroPermisConduire, NumeroPermisTravail,   DateExpirationPermisTravail,  CodeMetierROM,  Service,  NumContrat,  LanguesParlees, 
					 PermisDeConduire, MentionParticuliere1, MentionParticuliere2, MentionParticuliere3, PeriodeDeTravail)
			VALUES	(@IndividualId,1, @Nationalite, @AdressePermanente, @NomPersonneContactUrgence, @NumeroUrgence, @PaysPermisConduire,
					 @NumeroPermisConduire, @NumeroPermisTravail,   @DateExpirationPermisTravail,  @CodeMetierROM,  @Service,  @NumContrat,  @LanguesParlees, 
					 @PermisDeConduire, @MentionParticuliere1, @MentionParticuliere2, @MentionParticuliere3, @PeriodeDeTravail)
	END
	--ELSE
	--BEGIN
	--	IF @Cust2Exists=1
	--		DELETE FROM IndivCustomData2 WHERE @TenantNdx = 1 AND @IndividualId = IndivNdx
	--END
	
	
	
	-- JC 2009-05-03 : update Image in DB
	UPDATE IndivImages
	SET UserImage = @UserImage
	WHERE TenantNdx = 1 AND IndivNdx = @IndividualId

	-- Update / insert / delete card
	IF (@MiFareCardNumber IS NOT NULL AND @MiFareCardNumber<>'')

		IF (@OldMiFareCardNumber IS NOT NULL)
			-- user already has a card, update it
			UPDATE Cards
			SET CardNumber = dbo.HexToBinary(@MiFareCardNumber)
			WHERE CardNumber = dbo.HexToBinary(@OldMiFareCardNumber)
		ELSE
			-- user did not have a card, add one
			INSERT INTO Cards (TenantNdx, IndivNdx, Resident, CardNumber, Disabled)
			VALUES (1, @IndividualId, 0, dbo.HexToBinary(@MiFareCardNumber), 0)
	ELSE
		IF (@OldMiFareCardNumber IS NOT NULL)
			-- user has a card to be deleted
			DELETE Cards
			WHERE CardNumber = dbo.HexToBinary(@OldMiFareCardNumber)


	-- Clear current image if the path has changed
	IF CONVERT(binary(260),@ImagePath)<>CONVERT(binary(260),@OldImagePath)
		DELETE IndivImages WHERE TenantNdx=1 AND IndivNdx=@IndividualId

	-- Write change in DPURestore table and notify INET through INetMessenger
	EXEC IdWorks_Notify 'IdWorks Update', 1, @IndividualId

END

CLOSE DelSearch
DEALLOCATE DelSearch
CLOSE UpdSearch
DEALLOCATE UpdSearch
GO
