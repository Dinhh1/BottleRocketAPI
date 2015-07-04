

-- SQL Statements

-- UserAddresses table

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

-- ScheduledPickups

CREATE TABLE ScheduledPickups
(
	Id int IDENTITY(1,1) NOT NULL,
	UserId nvarchar(128) NOT NULL,
	[DateCreated] datetime NOT NULL DEFAULT GETUTCDATE(),
	[DatePickedUp] datetime NULL,
	IsPickedUp bit NOT NULL,
	AddressId int NOT NULL
)

ALTER TABLE ScheduledPickups ADD
CONSTRAINT PK_Pickup PRIMARY KEY CLUSTERED (Id ASC)
GO

ALTER TABLE ScheduledPickups ADD
CONSTRAINT FK_PickUp_UserId
	FOREIGN KEY (UserId)
	REFERENCES AspNetUsers (Id)
	ON DELETE CASCADE
GO

ALTER TABLE ScheduledPickups ADD
CONSTRAINT FK_PickUp_Addy
	FOREIGN KEY (AddressId)
	REFERENCES UserAddresses(Id)
GO

-- Globals

CREATE TABLE Globals
(
	Id int IDENTITY(1,1) NOT NULL,
	[Key] nvarchar(max) NOT NULL,
	[Value] nvarchar(max) NOT NULL,
	DateCreated Datetime NOT NULL DEFAULT GETUTCDATE(),
	LastUpdated DateTime NOT NULL DEFAULT GETUTCDATE(),
	[Description] nvarchar(max) NULL
)

ALTER TABLE Globals ADD
CONSTRAINT PK_Globals PRIMARY KEY CLUSTERED (Id ASC)
GO

-- Communities

CREATE TABLE Communities
(
	Id int IDENTITY(1,1) NOT NULL,
	[Name] nvarchar(128) NOT NULL,
	DateCreated DateTime NOT NULL DEFAULT GETUTCDATE(),
	LastUpdated DateTime NOT NULL DEFAULT GETUTCDATE()
)

ALTER TABLE Communities ADD
CONSTRAINT PK_CommunityId PRIMARY KEY CLUSTERED (Id ASC)
GO


 -- Pickup Receipts
 -- DEPRECATED: Data Model was changed

--CREATE TABLE PickupReceipts
--(
--	Id int IDENTITY(1,1) NOT NULL,
--	UserId nvarchar(128) NOT NULL,
--	ScheduledPickupId int NOT NULL,
--	AluminumTotal Decimal NULL DEFAULT 0,
--	GlassTotal Decimal NULL DEFAULT 0,
--	Plastic1Total Decimal NULL DEFAULT 0,
--	Plastic2Total Decimal NULL DEFAULT 0,
--	OverallTotal Decimal NULL Default 0,
--	[DateCreated] datetime NOT NULL DEFAULT GETUTCDATE(),
--	[LastUpdated] datetime NOT NULL DEFAULT GETUTCDATE()
--)

--ALTER TABLE PickupReceipts ADD 
--	CONSTRAINT PK_Receipt PRIMARY KEY CLUSTERED (Id ASC)
--GO

--ALTER TABLE PickupReceipts ADD
--CONSTRAINT FK_Receipts_UserId
--	FOREIGN KEY (UserId)
--	REFERENCES AspNetUsers (Id)
--	ON DELETE CASCADE
--GO

--ALTER TABLE PickupReceipts ADD
--CONSTRAINT FK_Receipts_ScheduleId
--	FOREIGN KEY (ScheduledPickupId)
--	REFERENCES ScheduledPickups(Id)
--GO

---- UserMetric

--CREATE TABLE UserMetrics
--(
--	Id int IDENTITY(1,1) NOT NULL,
--	UserId nvarchar(128) NOT NULL,
--	ReceiptId int NOT NULL,
--	AluminumWeight Decimal NULL DEFAULT 0,
--	GlassWeight Decimal NULL DEFAULT 0,
--	Plastic1Weight Decimal NULL DEFAULT 0,
--	Plastic2Weight Decimal NULL DEFAULT 0,
--	[DateCreated] datetime NOT NULL DEFAULT GETUTCDATE(),
--	[LastUpdated] datetime NOT NULL DEFAULT GETUTCDATE()
--)

--ALTER TABLE UserMetrics ADD 
--	CONSTRAINT PK_UserMetrics PRIMARY KEY CLUSTERED (Id ASC)
--GO

--ALTER TABLE UserMetrics ADD
--CONSTRAINT FK_Metrics_Receipt
--	FOREIGN KEY (ReceiptId)
--	REFERENCES PickupReceipts(Id)
--	ON DELETE CASCADE
--GO

--ALTER TABLE UserMetrics ADD
--CONSTRAINT FK_Metrics_UserId
--	FOREIGN KEY (UserId)
--	REFERENCES AspNetUsers (Id)
--GO

