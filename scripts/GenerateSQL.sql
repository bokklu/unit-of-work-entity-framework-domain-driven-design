USE [MarketingSuite]
GO
/****** Object:  Schema [Campaigns]    Script Date: 11/27/2022 12:27:44 AM ******/
CREATE SCHEMA [Campaigns]
GO
/****** Object:  Schema [Clients]    Script Date: 11/27/2022 12:27:44 AM ******/
CREATE SCHEMA [Clients]
GO
/****** Object:  Schema [Keywords]    Script Date: 11/27/2022 12:27:44 AM ******/
CREATE SCHEMA [Keywords]
GO
/****** Object:  Table [Campaigns].[Campaign]    Script Date: 11/27/2022 12:27:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Campaigns].[Campaign](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Active] [bit] NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Campaign] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Campaigns].[CampaignsKeywords]    Script Date: 11/27/2022 12:27:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Campaigns].[CampaignsKeywords](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CampaignID] [int] NOT NULL,
	[KeywordsPrimaryID] [bigint] NOT NULL,
 CONSTRAINT [PK_CampaignsKeywords] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Clients].[Client]    Script Date: 11/27/2022 12:27:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Clients].[Client](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Url] [nvarchar](100) NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Clients].[ClientsCampaigns]    Script Date: 11/27/2022 12:27:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Clients].[ClientsCampaigns](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CampaignID] [int] NOT NULL,
	[ClientID] [int] NOT NULL,
 CONSTRAINT [PK_ClientsCampaigns] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Keywords].[KeywordsPrimary]    Script Date: 11/27/2022 12:27:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Keywords].[KeywordsPrimary](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_KeywordsPrimary] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Keywords].[KeywordsSourcePrimary]    Script Date: 11/27/2022 12:27:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Keywords].[KeywordsSourcePrimary](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[KeywordsPrimaryID] [bigint] NOT NULL,
	[PrimarySourceID] [smallint] NOT NULL,
 CONSTRAINT [PK_KeywordsSourcePrimary] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Keywords].[SourcePrimary]    Script Date: 11/27/2022 12:27:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Keywords].[SourcePrimary](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Url] [nvarchar](200) NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_SourcePrimary] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [Campaigns].[Campaign] ADD  CONSTRAINT [DF_Campaign_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [Campaigns].[Campaign] ADD  CONSTRAINT [DF_Campaign_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Clients].[Client] ADD  CONSTRAINT [DF_Client_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Clients].[Client] ADD  CONSTRAINT [DF_Client_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [Keywords].[KeywordsPrimary] ADD  CONSTRAINT [DF_KeywordsPrimary_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Keywords].[KeywordsPrimary] ADD  CONSTRAINT [DF_KeywordsPrimary_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [Keywords].[SourcePrimary] ADD  CONSTRAINT [DF_SourcePrimary_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Campaigns].[CampaignsKeywords]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsKeywords_Campaign] FOREIGN KEY([CampaignID])
REFERENCES [Campaigns].[Campaign] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Campaigns].[CampaignsKeywords] CHECK CONSTRAINT [FK_CampaignsKeywords_Campaign]
GO
ALTER TABLE [Campaigns].[CampaignsKeywords]  WITH CHECK ADD  CONSTRAINT [FK_CampaignsKeywords_KeywordsPrimary] FOREIGN KEY([KeywordsPrimaryID])
REFERENCES [Keywords].[KeywordsPrimary] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Campaigns].[CampaignsKeywords] CHECK CONSTRAINT [FK_CampaignsKeywords_KeywordsPrimary]
GO
ALTER TABLE [Clients].[ClientsCampaigns]  WITH CHECK ADD  CONSTRAINT [FK_ClientsCampaigns_Campaign] FOREIGN KEY([CampaignID])
REFERENCES [Campaigns].[Campaign] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Clients].[ClientsCampaigns] CHECK CONSTRAINT [FK_ClientsCampaigns_Campaign]
GO
ALTER TABLE [Clients].[ClientsCampaigns]  WITH CHECK ADD  CONSTRAINT [FK_ClientsCampaigns_Client] FOREIGN KEY([ClientID])
REFERENCES [Clients].[Client] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Clients].[ClientsCampaigns] CHECK CONSTRAINT [FK_ClientsCampaigns_Client]
GO
ALTER TABLE [Keywords].[KeywordsSourcePrimary]  WITH CHECK ADD  CONSTRAINT [FK_KeywordsSourcePrimary_KeywordsPrimary] FOREIGN KEY([KeywordsPrimaryID])
REFERENCES [Keywords].[KeywordsPrimary] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Keywords].[KeywordsSourcePrimary] CHECK CONSTRAINT [FK_KeywordsSourcePrimary_KeywordsPrimary]
GO
ALTER TABLE [Keywords].[KeywordsSourcePrimary]  WITH CHECK ADD  CONSTRAINT [FK_KeywordsSourcePrimary_SourcePrimary] FOREIGN KEY([PrimarySourceID])
REFERENCES [Keywords].[SourcePrimary] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Keywords].[KeywordsSourcePrimary] CHECK CONSTRAINT [FK_KeywordsSourcePrimary_SourcePrimary]
GO
