USE [InetDb]
GO

/****** Object:  Table [dbo].[Import]    Script Date: 06/18/2010 15:55:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Import](
	[ImportNumber] [int] IDENTITY(1,1) NOT NULL,
	[VCCNumber] [decimal](18, 0) NULL,
	[LastName] [nvarchar](255) NULL,
	[FirstName] [nvarchar](255) NULL,
	[Nationalite] [nvarchar](255) NULL,
	[Compagnie] [nvarchar](255) NULL,
	[Qualification] [nvarchar](255) NULL,
	[LanguesParlees] [nvarchar](255) NULL,
	[NomPersonneContactUrgence] [nvarchar](255) NULL,
	[NumeroUrgence] [nvarchar](255) NULL,
	[TempStartDate] [datetime] NULL,
	[CodeMetierROM] [nvarchar](255) NULL,
	[NumCafatRuamTrav] [nvarchar](255) NULL,
 CONSTRAINT [PK__Import__48EE7E0208162EEB] PRIMARY KEY CLUSTERED 
(
	[ImportNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


