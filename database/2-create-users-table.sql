CREATE TABLE Messenger.[dbo].[Users] 
(
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Username VARCHAR(30) NOT NULL,
	Password VARCHAR(30) NOT NULL,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	IsPublic INT DEFAULT 0 NOT NULL,
);

CREATE UNIQUE INDEX UNIQUE_USERNAME
ON Messenger.dbo.Users(Username);

SELECT * FROM Messenger.dbo.Users;