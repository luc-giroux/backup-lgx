------------------- SCRIPT  IMPORT STRUCTURE CONTRAT ------------------------
--- récupèree les valeurs de la table IMPORTSTRUCTURE et insère les valeurs
-- dans les bonnes tables WS WP CWP ContractStructure
DECLARE @WS varchar(50)
DECLARE @WSDescription varchar(max)
DECLARE @WP varchar(50)
DECLARE @WPDescription varchar(255)
DECLARE @CWP varchar(50)
DECLARE @CWPDescription varchar(255)
DECLARE @AllocatedHours int
DECLARE @Contract varchar(50)

BEGIN TRANSACTION


-- INSERT DES WS DANS LA BASE
DECLARE WORKTABLEWS CURSOR FOR                                       
SELECT distinct  
         [WS] 
      ,[WSDescription]
      ,[Contract]
  FROM [allianceTimesheets].[dbo].[IMPORTSTRUCTURE]

OPEN  WORKTABLEWS                                                    
FETCH NEXT FROM WORKTABLEWS INTO @WS,@WSDescription,@Contract
WHILE @@fetch_Status = 0                                               
            BEGIN  
            
            INSERT INTO [allianceTimesheets].[dbo].[WS]
           ([WSNumber]
           ,[ContractNumber]
           ,[Description]
           ,[Completed])
        VALUES
           (@WS
           ,@Contract
           ,@WSDescription
           ,0)
           
        IF @@ERROR <> 0
        BEGIN
                  PRINT N'WS : Contract ' + @Contract + ', WS : ' +@WS
                  GOTO ROLLBACK_ON_INSERT_WS
            END
   
        FETCH NEXT FROM WORKTABLEWS INTO @WS,@WSDescription,@Contract
    END 
CLOSE  WORKTABLEWS 
DEALLOCATE  WORKTABLEWS  


-- INSERT DES WP DANS LA BASE
DECLARE WORKTABLEWP CURSOR FOR                                       
SELECT distinct  
         [WP]
      ,[WPDescription]
      ,[Contract]
  FROM [allianceTimesheets].[dbo].[IMPORTSTRUCTURE]



OPEN  WORKTABLEWP                                                    
FETCH NEXT FROM WORKTABLEWP INTO @WP,@WPDescription,@Contract
WHILE @@fetch_Status = 0                                               
            BEGIN  
            
            INSERT INTO [allianceTimesheets].[dbo].[WP]
           ([WPNumber]
           ,[ContractNumber]
           ,[Description]
           ,[Completed])
     VALUES
           (@WP
           ,@Contract
           ,@WPDescription
           ,0)
           
        IF @@ERROR <> 0
        BEGIN
                  PRINT N'WP : Contract ' + @Contract + ', WP : '+@WP
                  GOTO ROLLBACK_ON_INSERT_WP
            END
   
        FETCH NEXT FROM WORKTABLEWP INTO @WP,@WPDescription,@Contract
    END 
CLOSE  WORKTABLEWP 
DEALLOCATE  WORKTABLEWP  



-- INSERT DES CWP DANS LA BASE
DECLARE WORKTABLECWP CURSOR FOR                                       
SELECT distinct  
         [CWP]
      ,[CWPDescription]
      ,[Contract]
  FROM [allianceTimesheets].[dbo].[IMPORTSTRUCTURE]


OPEN  WORKTABLECWP                                                    
FETCH NEXT FROM WORKTABLECWP INTO @CWP,@CWPDescription,@Contract
WHILE @@fetch_Status = 0                                               
            BEGIN  
            
            INSERT INTO [allianceTimesheets].[dbo].[CWP]
           ([CWPNumber]
           ,[ContractNumber]
           ,[Description]
           ,[Completed])
     VALUES
           (@CWP
           ,@Contract
           ,@CWPDescription
           ,0)
           
        IF @@ERROR <> 0
        BEGIN
                  PRINT N'CWP : Contract ' + @Contract + ', CWP : ' +@CWP
                  GOTO ROLLBACK_ON_INSERT_CWP
            END
   
        FETCH NEXT FROM WORKTABLECWP INTO @CWP,@CWPDescription,@Contract
    END 
CLOSE  WORKTABLECWP 
DEALLOCATE  WORKTABLECWP  



-- INSERT DES ASSOCIATIONS
DECLARE WORKTABLE CURSOR FOR                                       
SELECT [WS]
      ,[WP]
      ,[CWP]
      ,[AllocatedHours]
      ,[Contract]
  FROM [allianceTimesheets].[dbo].[IMPORTSTRUCTURE]


OPEN  WORKTABLE                                                    
FETCH NEXT FROM WORKTABLE INTO @WS,@WP,@CWP,@AllocatedHours,@Contract
WHILE @@fetch_Status = 0                                               
            BEGIN  
            
            INSERT INTO [allianceTimesheets].[dbo].[ContractStructure]
           ([ContractNumber]
           ,[WSNumber]
           ,[WPNumber]
           ,[CWPNumber]
           ,[AllocatedHours])
     VALUES
           (@Contract
           ,@WS
           ,@WP
           ,@CWP
           ,@AllocatedHours)
           
        IF @@ERROR <> 0
        BEGIN
                  PRINT N'ASSOC : Contract ' + @Contract + ', WS : ' +@WS+ ', WP : '+@WP+', CWP : ' +@CWP
                  GOTO ROLLBACK_ON_INSERT_ASSOC
          END
   
        FETCH NEXT FROM WORKTABLE INTO @WS,@WP,@CWP,@AllocatedHours,@Contract
    END 
CLOSE  WORKTABLE 
DEALLOCATE  WORKTABLE  


COMMIT TRANSACTION
GOTO FINISH

ROLLBACK_ON_INSERT_WS:
   PRINT N'WS : Erreur lors de l''insert d''un WS'
   ROLLBACK TRANSACTION
   GOTO FINISH
   

ROLLBACK_ON_INSERT_WP:
   PRINT N'WP : Erreur lors de l''insert d''un WP'
   ROLLBACK TRANSACTION
   GOTO FINISH
   
   
ROLLBACK_ON_INSERT_CWP:
   PRINT N'CWP : Erreur lors de l''insert d''un CWP'
   ROLLBACK TRANSACTION   
   GOTO FINISH
   
   
ROLLBACK_ON_INSERT_ASSOC:
   PRINT N'ASSOC : Erreur lors de l''insert d''une ASSOCIATION'
   ROLLBACK TRANSACTION   
   GOTO FINISH
   
   
FINISH:
PRINT N'END'
GO
        

