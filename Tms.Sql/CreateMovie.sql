USE [Tms]
GO

/****** Object:  Table [dbo].[Movie]    Script Date: 07/12/2022 08:36:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Movie](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[MovieId] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Language] [nvarchar](50) NOT NULL,
	[OttPlatform] [nvarchar](50) NULL,
	[ReleaseDate] [datetime] NULL,
	[TrailerDate] [datetime] NULL,
	[TeaserDate] [datetime] NULL,
	[OttReleaseDate] [datetime] NULL,
	[Image] [nvarchar](250) NULL,
	[IsUploadedToTelegram] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 
 )
 GO


