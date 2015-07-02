

-- SQL Statements
-- Create UserAddresses table

CREATE TABLE UserAddresses 
(
	Id int IDENTITY(1,1) NOT NULL,
	UserId nvarchar(128) NOT NULL,
	[Address] nvarchar(max) NOT NULL,
	[City] nvarchar(200) NOT NULL,
	[State] nvarchar(2) NOT NULL,
	[ZipCode] nvarchar(10) NOT NULL,
	[DateCreated] datetime NOT NULL DEFAULT GETUTCDATE(),
	[LastUpdated] datetime NOT NULL DEFAULT GETUTCDATE()
)
GO

ALTER TABLE UserAddresses ADD
CONSTRAINT PK_Addresses PRIMARY KEY CLUSTERED (Id ASC)
GO

ALTER TABLE UserAddresses ADD
CONSTRAINT FK_Addy_User_Id
	FOREIGN KEY (UserId)
	REFERENCES AspNetUsers (Id)
	ON DELETE CASCADE

GO

CREATE TABLE ScheduledPickup
(
	Id int IDENTITY(1,1) NOT NULL,
	UserId nvarchar(128) NOT NULL,
	[DateCreated] datetime NOT NULL DEFAULT GETUTCDATE(),
	[DatePickedUp] datetime NULL,
	IsPickedUp bit NOT NULL,
	AddressId int NOT NULL
)

ALTER TABLE ScheduledPickup ADD
CONSTRAINT PK_Pickup PRIMARY KEY CLUSTERED (Id ASC)
GO

ALTER TABLE ScheduledPickup ADD
CONSTRAINT FK_PickUp_UserId
	FOREIGN KEY (UserId)
	REFERENCES AspNetUsers (Id)
	ON DELETE CASCADE
GO

ALTER TABLE ScheduledPickup ADD
CONSTRAINT FK_PickUp_Addy
	FOREIGN KEY (AddressId)
	REFERENCES UserAddresses(Id)
GO


