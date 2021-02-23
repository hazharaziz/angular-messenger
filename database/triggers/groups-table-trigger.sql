USE [Messenger]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create trigger [dbo].[ADD_CREATOR_TO_GROUP]
on [dbo].[Groups]
after insert
as

begin
	insert into GroupMembers(GroupID, UserID)
	select i.GroupID, i.CreatorID from inserted i
end
GO

ALTER TABLE [dbo].[Groups] ENABLE TRIGGER [ADD_CREATOR_TO_GROUP]
GO


