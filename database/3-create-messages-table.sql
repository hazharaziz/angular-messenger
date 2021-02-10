CREATE TABLE Messenger.[dbo].[Messages]
(
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	ComposerID INT FOREIGN KEY REFERENCES Messenger.dbo.Users(ID),
	ReplyToID INT FOREIGN KEY REFERENCES Messenger.dbo.Messages(ID),
	Text NVARCHAR(400) NOT NULL DEFAULT '',
	ComposerName NVARCHAR(70) NOT NULL,
	DateTime datetime NOT NULL,
);

SELECT * FROM Messenger.dbo.Messages;


