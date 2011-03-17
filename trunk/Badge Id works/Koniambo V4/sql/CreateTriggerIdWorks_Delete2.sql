USE [InetDb];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
--LGX : Trigger opur la suppression de données via id works
CREATE TRIGGER [dbo].[IdWorks_Delete2] ON [dbo].[IdWorksView2]
INSTEAD OF DELETE
AS

DECLARE @TenantName	varchar(16)
DECLARE @IndivId	smallint

DECLARE DelSearch CURSOR LOCAL FOR SELECT IndividualId FROM deleted
OPEN DelSearch

WHILE 0<>1
BEGIN

	FETCH NEXT FROM DelSearch INTO @IndivId
	IF @@FETCH_STATUS <> 0
		BREAK

	-- do the delete
	DELETE FROM dbo.IndivCustomData
	WHERE  IndivNdx = @IndivId

	DELETE FROM dbo.IndivCustomData2
	WHERE  IndivNum= @IndivId

	DELETE FROM dbo.Cards
	WHERE  IndivNdx = @IndivId

	DELETE FROM dbo.XRefIndivGroupDoor
	WHERE IndivNdx = @IndivId

	DELETE FROM dbo.Individuals
	WHERE IndivId = @IndivId

	-- Remove Picture from DB
	DELETE FROM [InetDb].[dbo].[IndivImages]
      WHERE IndivNdx = @IndivId 

	-- Write change in DPURestore table and notify INET through INetMessenger
	EXEC IdWorks_Notify 'IdWorks Delete', 1, @IndivId
END

CLOSE DelSearch
DEALLOCATE DelSearch
GO
