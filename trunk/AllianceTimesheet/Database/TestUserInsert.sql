USE [allianceTimesheets]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 12/21/2010 11:24:14 ******/

INSERT [dbo].[AppUser] ([userLogin], [LastName], [FirstName], [Email]) VALUES (N'KONINET\contractorca', N'contractorca', N'contractorca', N'contractorca@koninet.com')
INSERT [dbo].[AppUser] ([userLogin], [LastName], [FirstName], [Email]) VALUES (N'KONINET\contractorsupervisor', N'contractorsupervisor', N'contractorsupervisor', N'contractorsupervisor@koninet.net')
INSERT [dbo].[AppUser] ([userLogin], [LastName], [FirstName], [Email]) VALUES (N'KONINET\ownerca', N'ownerca', N'ownerca', N'ownerca@koninet.net')
INSERT [dbo].[AppUser] ([userLogin], [LastName], [FirstName], [Email]) VALUES (N'KONINET\ownersupervisor', N'ownersupervisor', N'ownersupervisor', N'ownersupervisor@koninet.net')
/****** Object:  Table [dbo].[UserRights]    Script Date: 12/21/2010 11:24:14 ******/
INSERT [dbo].[UserRights] ([userLogin], [contract], [role]) VALUES (N'KONINET\contractorca', N'C004', N'Contractor CA')
INSERT [dbo].[UserRights] ([userLogin], [contract], [role]) VALUES (N'KONINET\contractorsupervisor', N'C004', N'ContractorSupervisor')
INSERT [dbo].[UserRights] ([userLogin], [contract], [role]) VALUES (N'KONINET\ownerca', N'C004', N'Owner CA')
INSERT [dbo].[UserRights] ([userLogin], [contract], [role]) VALUES (N'KONINET\ownersupervisor', N'C004', N'OwnerSupervisor')
