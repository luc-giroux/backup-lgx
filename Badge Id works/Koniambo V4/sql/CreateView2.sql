/****** Object: View [dbo].[IdWorksView2]   Script Date: 18/05/2010 17:29:16 ******/
USE [InetDb];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
/*
Script de création de la vue IdWorksView2.
Cette vue est mappée avec l'application pour la création des badges.
Author: Luc Giroux
*/
CREATE VIEW dbo.IdWorksView2
AS
SELECT
	ind.IndivId AS IndividualId,
	icd.Custom15 AS AccessGroup,
	ind.FirstName AS Prenom,
	ind.LastName AS Nom,
	dbo.CardTextView.CardText AS MiFareCardNumber,
	ind.TempStart + 0 AS TempStartDate,
	ind.TempStop + 0 AS TempStopDate,
	(CASE ind.Status WHEN 0 THEN 'PERMANENT' WHEN 1 THEN 'TEMPORAIRE' WHEN 2 THEN 'DELETED' ELSE '' END) AS RecType,
	ind.APBtype + 0 AS APBStatus,
	dbo.IDWorksViewPath(ind.ImagePath) AS ImagePath,
	icd.Custom01 AS Societe,
	icd.Custom02 AS Qualification,
	icd.Custom03 AS AccueilSecurite,
	icd.Custom04 AS NumCafatRuamTrav,
	icd.Custom07 AS ListePermisDeConduire,
	icd.Custom08 AS DateValiditePermis,
	icd.Custom09 AS DateVisiteMedicale,
	icd.Custom10 AS QuatreQuatre,
	icd.Custom11 AS EHS1,
	icd.Custom12 AS EHS2,
	icd.Custom13 AS Port,
	icd.Custom14 AS Mine,
	icd.Custom16 AS NiveauSecurite,
	icd.Custom17 AS Amiante,
	dbo.IndivImages.UserImage,
	icd2.Nationalite,
	icd2.AdressePermanente,
	icd2.NomPersonneContactUrgence,
	icd2.NumeroUrgence,
	icd2.PaysPermisConduire,
	icd2.NumeroPermisConduire,
	icd2.NumeroPermisTravail,
	icd2.CodeMetierROM,
	icd2.Service,
	icd2.NumContrat,
	icd2.LanguesParlees,
	icd2.PermisDeConduire,
	icd2.MentionParticuliere1,
	icd2.MentionParticuliere2,
	icd2.MentionParticuliere3,
	icd2.PeriodeDeTravail,
	icd2.DateExpirationPermisTravail


FROM
	dbo.Individuals AS ind LEFT OUTER JOIN
	dbo.IndivImages ON ind.IndivId = dbo.IndivImages.IndivNdx LEFT OUTER JOIN
	dbo.CardTextView ON dbo.CardTextView.IndivNdx = ind.IndivId LEFT OUTER JOIN
	dbo.IndivCustomData AS icd ON ind.IndivId = icd.IndivNdx LEFT OUTER JOIN
	dbo.IndivCustomData2 AS icd2 ON ind.IndivId = icd2.IndivNum 

GO
