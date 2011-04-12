PRINT '***************************  DEBUT DE L''IMPORT'
DECLARE Workers CURSOR FOR
 SELECT  [Family name                                     ]
          ,[First Name]          
          ,[Company]
          ,[VCC number]
          ,[Nationality]
          ,[Subcontractor]
          ,[Contract]
          ,[Arrival Date]
          ,[Badge number]
          ,[Status]
          ,[TRADE]
          ,[Position Category]            
  FROM [allianceTimesheets].[dbo].[IMPORTWORKER]
  
DECLARE @FamilyName as varchar(200)
DECLARE @FirstName as varchar(200)
DECLARE @Company as varchar(200)
DECLARE @VCCNumber as int
DECLARE @Nationality as varchar(200)
DECLARE @SubContractor as varchar(200)
DECLARE @Contract as varchar(200)
DECLARE @ArrivalDate as datetime
DECLARE @BadgeNumber as int
DECLARE @Status  as char(1)
DECLARE @Trade as varchar(50)
DECLARE @PositionCategory as varchar(50)   
DECLARE @SubcontractorID as int
DECLARE @WorkerID as int
DECLARE @nbWorkersImport as int
DECLARE @nbWorkersImportInCha as varchar(50)

SET @nbWorkersImport = 0

OPEN  Workers                                                    
FETCH NEXT FROM Workers INTO @FamilyName ,@FirstName ,@Company ,@VCCNumber ,@Nationality ,@SubContractor ,
                             @Contract ,@ArrivalDate ,@BadgeNumber ,@Status ,@Trade ,@PositionCategory
WHILE @@fetch_Status = 0                                               
BEGIN
  BEGIN TRANSACTION
  PRINT N' Import du Worker ' + @FamilyName
  
  -- Clean de champ text
  SET  @FamilyName = ltrim(rtrim(@FamilyName))
  SET  @Status = ltrim(rtrim(@Status))
  SET  @FirstName = ltrim(rtrim(@FirstName))
  SET  @Company = ltrim(rtrim(@Company))
  SET  @Contract = ltrim(rtrim(@Contract))
  SET  @Nationality = ltrim(rtrim(@Nationality))
  SET  @SubContractor = ltrim(rtrim(@SubContractor))
  SET  @Trade = ltrim(rtrim(@Trade))
  SET  @PositionCategory = ltrim(rtrim(@PositionCategory))
  
  -- on regarde si le subcontractor existe
  SELECT 1 FROM  [allianceTimesheets].[dbo].Subcontractor WHERE ltrim(rtrim(SubcontractorName)) = ltrim(rtrim(@SubContractor))
                               AND ltrim(rtrim(ContractNumber)) = ltrim(rtrim(@Contract))
  -- si il n'existe pas on le crée
  IF @@ROWCOUNT < 1
  BEGIN
    INSERT INTO [allianceTimesheets].[dbo].[Subcontractor]
             ([SubcontractorName]
             ,[ContractNumber]
             ,[Active])
       VALUES
             (@SubContractor
             ,@Contract
             ,1)
     IF (@@ERROR <> 0)
    BEGIN     
      --RAISERROR('Erreur lors de l''insert dans Subcontractor, Impossible d '' importer le user : %s, parce que : %s',15,1,@FamilyName,@ErrorMessage)
      ROLLBACK TRANSACTION      
      RETURN
    END 
  END
  
  
  SELECT @SubcontractorID = SubcontractorID 
  FROM   [allianceTimesheets].[dbo].[Subcontractor]
  WHERE  ltrim(rtrim(SubcontractorName)) = ltrim(rtrim(@SubContractor))
  AND ltrim(rtrim(ContractNumber)) = ltrim(rtrim(@Contract))
  
  -- insertion du worker courant dans la table Worker
  INSERT INTO [allianceTimesheets].[dbo].[Worker]
           ([LastName]
           ,[FirstName]
           ,[CompanyName]
           ,[VCCNumber]
           ,[Nationality]
           ,[SubcontractorId]
           ,[InductionDone]
           ,[ArrivalDate]
           ,[BadgeNumber]
           ,[Status]
           ,[CreatedDate])
     VALUES (@FamilyName,@FirstName,@Company,@VCCNumber,@Nationality,@SubcontractorID,1,@ArrivalDate,@BadgeNumber,@Status, getDate())
  
    IF (@@ERROR <> 0)
    BEGIN      
      --RAISERROR('Erreur lors de l''insert dans Worker, Impossible d '' importer le user : %s, parce que : %s',15,1,@FamilyName,@ErrorMessage)
      ROLLBACK TRANSACTION   
      RETURN        
    END
    
  -- on récupère la clé du dernier worker insérer
  SELECT @WorkerID = MAX(WorkerID) FROM [allianceTimesheets].[dbo].Worker
  
  
  SELECT 1 FROM [allianceTimesheets].[dbo].Trade
  WHERE ltrim(rtrim(ContractNumber)) = ltrim(rtrim(@Contract))
  AND ltrim(rtrim(Trade)) = ltrim(rtrim(@Trade))
   IF @@ROWCOUNT < 1
  BEGIN
    -- insertion du trade  dans la table Trade
 INSERT INTO [allianceTimesheets].[dbo].[Trade]
           ([Trade]
           ,[ContractNumber])
     VALUES
           (@Trade
           ,@Contract)
    IF (@@ERROR <> 0)
    BEGIN     
     -- RAISERROR('Erreur lors de l''insert dans Trade, Impossible d '' importer le user : %s, parce que : %s',15,1,@FamilyName,@ErrorMessage)
      ROLLBACK TRANSACTION 
      RETURN     
    END 
  END         
  
  
  SELECT 1 FROM [allianceTimesheets].[dbo].PositionCategory
  WHERE ltrim(rtrim(ContractNumber)) = ltrim(rtrim(@Contract))
  AND ltrim(rtrim(PositionCategory)) = ltrim(rtrim(@PositionCategory))
   IF @@ROWCOUNT < 1
  BEGIN
    -- insertion de la position category dans la table PositionCategory
  INSERT INTO [allianceTimesheets].[dbo].[PositionCategory]
           ([PositionCategory]
           ,[ContractNumber])
     VALUES
           (@PositionCategory
           ,@Contract)
    IF (@@ERROR <> 0)
    BEGIN     
    --  RAISERROR('Erreur lors de l''insert dans PositionCategory, Impossible d '' importer le user : %s, parce que : %s',15,1,@FamilyName,@ErrorMessage)
      ROLLBACK TRANSACTION 
      RETURN     
    END 
  END         
  
  SELECT 1 FROM [allianceTimesheets].[dbo].WorkerContract
  WHERE WorkerID = @WorkerID 
  AND ltrim(rtrim(ContractNumber)) = ltrim(rtrim(@Contract))
  
  -- si il n'existe pas on le crée
  IF @@ROWCOUNT < 1 
  BEGIN
    -- insertion du worker courant dans la table WorkerContract
  INSERT INTO [allianceTimesheets].[dbo].[WorkerContract]
           ([WorkerId]
           ,[ContractNumber]
           ,[Trade]
           ,[PositionCategory]
           ,[Active]
           ,[CreatedDate])
     VALUES
           (@WorkerID
           ,@Contract
           ,@Trade
           ,@PositionCategory
           ,1
           ,getDate()) 
    IF (@@ERROR <> 0)
    BEGIN    
     -- RAISERROR('Erreur lors de l''insert dans WorkerContract, Impossible d '' importer le user : %s, parce que : %s',15,1,@FamilyName,@ErrorMessage)
      ROLLBACK TRANSACTION 
      RETURN     
    END 
  END            
  COMMIT TRANSACTION
  SET @nbWorkersImport = @nbWorkersImport + 1
  SET @nbWorkersImportInCha = CONVERT(varchar,@nbWorkersImport)
  PRINT N' Worker ' + @FamilyName +' importé avec success n° ' + @nbWorkersImportInCha + '.'
  
  FETCH NEXT FROM Workers INTO @FamilyName ,@FirstName ,@Company ,@VCCNumber ,@Nationality ,@SubContractor ,
                               @Contract ,@ArrivalDate ,@BadgeNumber ,@Status ,@Trade ,@PositionCategory
END 
CLOSE  Workers 
DEALLOCATE  Workers  
GO
PRINT '***************************  FIN DE L''IMPORT'
