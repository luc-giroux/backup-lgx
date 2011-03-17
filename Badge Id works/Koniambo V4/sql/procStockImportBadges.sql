USE [InetDb]
GO
/****** Object:  StoredProcedure [dbo].[ImportDonnesBadges]    Script Date: 06/18/2010 16:22:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Luc GIROUX
-- Create date: 17 Juin 2010
-- Description:	Procedure stock�e pour l'insertion auto des
--				donn�es des badges depuis la table import
--				vers la vue idworks view2
-- =============================================
ALTER PROCEDURE [dbo].[ImportDonnesBadges] 
	-- Add the parameters for the stored procedure here
AS

	SET NOCOUNT ON;
	--turns of the check for truncation
	SET ANSI_WARNINGS OFF;
	
	--D�calarations des variables
	DECLARE @VCCNumber						varchar(255)
	DECLARE @LastName						varchar(255)
	DECLARE @FirstName						varchar(255)
	DECLARE @DateNaissance					varchar(255)
	DECLARE @TelPerso						varchar(255)
	DECLARE @Email							varchar(255)
	DECLARE @Qualification					varchar(255)
	DECLARE @Compagnie						varchar(255)
	DECLARE @Nationalite					varchar(255)
	DECLARE @NomPersonneContactUrgence		varchar(255)
	DECLARE @NumeroUrgence					varchar(255)
	DECLARE @LanguesParlees					varchar(255)
	DECLARE @ImagePath						varchar(255)
	DECLARE @TempStartDate					datetime
	DECLARE @TempStopDate					datetime
	DECLARE @StartDate						varchar(255)
	DECLARE @StopDate						varchar(255)
	DECLARE @PeriodeDeTravail				varchar(255)
	DECLARE @CodeMetierROM					varchar(255)
	DECLARE @NumeroPermisTravail			varchar(255)
	DECLARE @NumCafatRuamTrav				varchar(255)
	DECLARE @IndividualId					smallint
	
	DECLARE @error							varchar(255)
	
	DECLARE @vcccount						int
	DECLARE @firstId						smallint
	DECLARE @lastId							smallint
	DECLARE @donnesImport					smallint
	
	DECLARE @importOK						int
	
	--Curseur qui r�cup�re toutes les donn�es de la table import.
	DECLARE cursImport CURSOR LOCAL FOR
			SELECT [VCCNumber]
				  ,[LastName]
				  ,[FirstName]
				  ,[Nationalite]
				  ,[Compagnie]
				  ,[Qualification]
				  ,[LanguesParlees]
				  ,[NomPersonneContactUrgence]
				  ,[NumeroUrgence]
				  ,[TempStartDate]
				  ,[CodeMetierROM]
				  ,[NumCafatRuamTrav]
			  FROM [InetDb].[dbo].[Import]


	SET @importOK = 0
	
	--On v�rifie qu'il y a des donn�es � importer
	SELECT @donnesImport = COUNT(*) FROM dbo.Import
	IF @donnesImport = 0
		BEGIN
			PRINT N'Aucune donn�e � importer.'
			PRINT N'Fin de la proc�dure.'
			Return
		END

	-- d�marrage transaction 
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
	BEGIN TRANSACTION IMPORT_BADGES	
	  
	--ouverture du curseur		  
	OPEN cursImport
	
	-- lecture du premier enregistrement
	FETCH cursImport 
	INTO @VCCNumber, @LastName, @FirstName, @Nationalite,
		 @Compagnie, @Qualification, @LanguesParlees, @NomPersonneContactUrgence,
		 @NumeroUrgence, @TempStartDate, @CodeMetierROM, @NumCafatRuamTrav

	
	--On stocke les ID des premiers et derniers individus qui seront enregistr� pour le rapport de fin
	SELECT @firstId = MAX([IndivId]+ 1)  FROM [dbo].[Individuals]
	IF @firstId IS NULL SET @firstId = 1
	SELECT @lastId = COUNT(*) FROM [dbo].Import
	SET @lastId = (@lastId + @firstId) - 1
	
	-- boucle de traitement
	WHILE @@fetch_Status = 0
		BEGIN
			-- On v�rifie que l'individu n'a pas d�j� �t� import�
			SELECT @vcccount = count(*) FROM IndivCustomData2 WHERE vccnumber = @VCCNumber
			IF @vcccount <> 0
				BEGIN
					SET @importOK = 1
					SET @error = '~~~~~~Le VCC number a d�j� �t� import�' + '[vccnumber = ' + @VCCNumber + '~~~~~~]'
					PRINT N'' + @error
					RAISERROR (@error, 16, 1)
					GOTO Branch_Rollback
				END
			-- On r�cup�re le champ individualId et on l'incr�mente
			SELECT @IndividualId = MAX([IndivId]+ 1)  FROM [dbo].[Individuals]; 
			IF @IndividualId IS NULL SET @IndividualId = 1;
			
			--Conversion des dates 
			SELECT @TempStopDate = DATEADD (year , 1 , @TempStartDate);
				IF @@ERROR != 0
					BEGIN
						SET @importOK = 1
						SET @error = '~~~~~~Erreur lors de la conversion des dates' + '[vccnumber = ' + @VCCNumber + '~~~~~~]'
						PRINT N'' + @error
						RAISERROR (@error, 16, 1)
						GOTO Branch_Rollback
					END
			SELECT @StartDate = CONVERT(VARCHAR, @TempStartDate, 101);
				IF @@ERROR != 0
					BEGIN
						SET @importOK = 1
						SET @error = '~~~~~~Erreur lors de la conversion des dates' + '[vccnumber = ' + @VCCNumber + '~~~~~~]'
						PRINT N'' + @error
						RAISERROR (@error, 16, 1)
						GOTO Branch_Rollback
					END
			SELECT @StopDate = CONVERT(VARCHAR, @TempStopDate, 101);
				IF @@ERROR != 0
					BEGIN
						SET @importOK = 1
						SET @error = '~~~~~~Erreur lors de la conversion des dates' + '[vccnumber = ' + @VCCNumber + '~~~~~~]'
						PRINT N'' + @error
						RAISERROR (@error, 16, 1)
						GOTO Branch_Rollback
					END
			
			
		--Libell� p�riode de travail
		SET @PeriodeDeTravail = 'Du ' + @StartDate + ' au ' + @StopDate;
		
		-- Conversion des nationalit� en fran�ais pour le mapping des drapeaux dans ID works
		SET @Nationalite = UPPER (@Nationalite);
		
		IF @Nationalite = 'CHINESE' SET @Nationalite = 'Chinois' 
		ELSE IF @Nationalite = 'KOREAN' SET @Nationalite = 'Korea South' 
		ELSE IF @Nationalite = 'THAI' SET @Nationalite = 'Tha�landais' 
		ELSE IF @Nationalite = 'INDONESIAN' SET @Nationalite = 'Indon�sien' 
		ELSE IF @Nationalite = 'PHILIPPINES' SET @Nationalite = 'Philippin'
		ELSE
			BEGIN
				SET @importOK = 1
				SET @error = 'Nationalit� erron�e ! Valeurs possibles: CHINESE, KOREAN, THAI, INDONESIAN, PHILIPPINES' + '[vccnumber = ' + @VCCNumber + ']'
				PRINT N'ERREUR IMPORT: ' + @error
				RAISERROR (@error, 16, 1)
				GOTO Branch_Rollback
			END	

		-- Images
		SET @ImagePath = 'Z:\\' + CONVERT(varchar, @VCCNumber) + '.jpg';

		-- CAFAT , permis travail
		SET @NumeroPermisTravail = @NumCafatRuamTrav;
		
		--INSERTION DES DONN�ES DANS LES TABLES INDIVCUSTOMDATA, INDIVCUSTOMDATA2, CARD, INDIVIDUAL VIA
		--LA VUE IDWORKSVIEW2
		
		INSERT INTO [InetDb].[dbo].[IdWorksView2]
			   ([IndividualId]
			   ,[AccessGroup]
			   ,[Prenom]
			   ,[Nom]
			   ,[TempStartDate]
			   ,[TempStopDate]
			   ,[RecType]
			   ,[APBStatus]
			   ,[ImagePath]
			   ,[Societe]
			   ,[Qualification]
			   ,[NumCafatRuamTrav]
			   ,[Nationalite]
			   ,[NomPersonneContactUrgence]
			   ,[NumeroUrgence]
			   ,[NumeroPermisTravail]
			   ,[CodeMetierROM]
			   ,[LanguesParlees]
			   ,[PeriodeDeTravail]
			   ,[vccnumber])
		 VALUES
			   (@IndividualId
			   ,'Entrepreneur'
			   ,@FirstName
			   ,@LastName
			   ,@TempStartDate
			   ,@TempStopDate
			   ,'TEMPORAIRE'
			   ,1
			   ,@ImagePath
			   ,@Compagnie
			   ,@Qualification
			   ,@NumCafatRuamTrav
			   ,@Nationalite
			   ,@NomPersonneContactUrgence
			   ,@NumeroUrgence
			   ,@NumeroPermisTravail
			   ,@CodeMetierROM
			   ,@LanguesParlees
			   ,@PeriodeDeTravail
			   ,@VCCNumber)
			
			PRINT N'Import OK pour individualId:' + CONVERT(VARCHAR, @IndividualId) + ' - VCCnumber:' + CONVERT(VARCHAR, @VCCNumber)
			-- lecture de l'enregistrement suivant
			FETCH cursImport 
			INTO @VCCNumber, @LastName, @FirstName, @Nationalite,
				@Compagnie, @Qualification, @LanguesParlees, @NomPersonneContactUrgence,
				@NumeroUrgence, @TempStartDate, @CodeMetierROM, @NumCafatRuamTrav
	END -- Fin de la boucle sur le fetch




IF @importOK = 0
	BEGIN
		COMMIT TRANSACTION IMPORT_BADGES
		--L'import s'est bien pass�: on supprime les donn�es de la table IMPORT
		PRINT N'Suppression des donn�es de la table IMPORT'
		DELETE FROM InetDb.dbo.Import

		-- Print des donn�es ins�r�es
		PRINT N'---------------------------------------------------------------------------------------------------'
		PRINT N'Import r�alis�.'
		PRINT N'Les badges peuvent �tre imprim�s pour les individus n� ' + CONVERT(VARCHAR, @firstId) +' � ' + CONVERT(VARCHAR, @lastId) + '.'
		PRINT N'---------------------------------------------------------------------------------------------------'
	END
ELSE
	BEGIN
		Branch_Rollback:
		PRINT N'----ROLLBACK----'
		ROLLBACK TRANSACTION IMPORT_BADGES
		PRINT N'---------------------------------------------------------------------------------------------------'
		PRINT N'IMPORT ANNUL�'
		PRINT N'---------------------------------------------------------------------------------------------------'
	END