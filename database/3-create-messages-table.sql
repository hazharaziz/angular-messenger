CREATE TABLE Messenger.[dbo].[Messages]
(
	MessageID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	ComposerID INT FOREIGN KEY REFERENCES Messenger.dbo.Users(ID) NOT NULL ,
	ReplyToID INT,
	Text NVARCHAR(400) NOT NULL DEFAULT '',
	ComposerName NVARCHAR(70) NOT NULL,
	DateTime datetime NOT NULL,
);

SELECT * FROM Messenger.dbo.Messages;


