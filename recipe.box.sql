CREATE DATABASE [recipebox]
GO 
USE [recipebox]
GO
/****** Object:  Table [dbo].[ingredients]    Script Date: 3/1/2017 4:34:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ingredients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[recipes]    Script Date: 3/1/2017 4:34:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[ingredients] [varchar](500) NULL,
	[instructions] [varchar](1000) NULL,
	[rate] [int] NULL,
	[time] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[recipes_tags]    Script Date: 3/1/2017 4:34:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipes_tags](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[recipe_id] [int] NULL,
	[tag_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tags]    Script Date: 3/1/2017 4:34:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tags](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
