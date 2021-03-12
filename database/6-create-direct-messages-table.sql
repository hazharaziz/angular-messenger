USE [Messenger]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DirectMessages](
	[DirectMessageID] [int] IDENTITY(1,1) NOT NULL,
	[DirectID] [int] NOT NULL,
	[ComposerID] [int] NOT NULL,
	[ReplyToID] [int] NOT NULL,
	[Text] [nvarchar](400) NOT NULL,
	[ComposerName] [nvarchar](40) NOT NULL,
	[DateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_DirectMessages] PRIMARY KEY CLUSTERED 
(
	[DirectMessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DirectMessages]  WITH CHECK ADD  CONSTRAINT [FK_DirectMessages_Directs] FOREIGN KEY([DirectID])
REFERENCES [dbo].[Directs] ([DirectID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DirectMessages] CHECK CONSTRAINT [FK_DirectMessages_Directs]
GO


