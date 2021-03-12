USE [Messenger]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create trigger [dbo].[SET_NAMES_AFTER_DELETE]
on [dbo].[Users]
after delete
as

begin

update DirectMessages
set ComposerName='Deleted Account'
where ComposerID=(select deleted.ID from deleted)

update GroupMessages
set ComposerName='Deleted Account'
where ComposerID=(select deleted.ID from deleted)

delete from Followers where UserID=(select deleted.ID from deleted) or FollowerID=(select deleted.ID from deleted)

end
GO

ALTER TABLE [dbo].[Users] ENABLE TRIGGER [SET_NAMES_AFTER_DELETE]
GO


