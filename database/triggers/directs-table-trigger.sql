USE [Messenger]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [dbo].[CHECK_UNIQUE_TUPLES]
ON [dbo].[Directs]
after insert
as

begin

	if exists( select * from Directs as directs, inserted as i
			   where (directs.FirstUserID=i.SecondUserID) and (directs.SecondUserID=i.FirstUserID))

	begin
		rollback transaction
		return
	end

end
GO

ALTER TABLE [dbo].[Directs] ENABLE TRIGGER [CHECK_UNIQUE_TUPLES]
GO


