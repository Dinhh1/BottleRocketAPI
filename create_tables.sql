

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

 -- Pickup Receipts

CREATE TABLE PickupReceipts
(
	Id int IDENTITY(1,1) NOT NULL,
	UserId nvarchar(128) NOT NULL,
	ScheduledPickupId int NOT NULL,
	AluminumTotal Decimal NULL DEFAULT 0,
	GlassTotal Decimal NULL DEFAULT 0,
	Plastic1Total Decimal NULL DEFAULT 0,
	Plastic2Total Decimal NULL DEFAULT 0,
	OverallTotal Decimal NULL Default 0
)

ALTER TABLE PickupReceipts ADD 
	CONSTRAINT PK_Receipt PRIMARY KEY CLUSTERED (Id ASC)
GO

ALTER TABLE PickupReceipts ADD
CONSTRAINT FK_Receipts_UserId
	FOREIGN KEY (UserId)
	REFERENCES AspNetUsers (Id)
	ON DELETE CASCADE
GO

ALTER TABLE PickupReceipts ADD
CONSTRAINT FK_Receipts_ScheduleId
	FOREIGN KEY (ScheduledPickupId)
	REFERENCES ScheduledPickups(Id)
GO

-- UserMetric

CREATE TABLE UserMetrics
(
	Id int IDENTITY(1,1) NOT NULL,
	UserId nvarchar(128) NOT NULL,
	ReceiptId int NOT NULL,
	AluminumWeight Decimal NULL DEFAULT 0,
	GlassWeight Decimal NULL DEFAULT 0,
	Plastic1Weight Decimal NULL DEFAULT 0,
	Plastic2Weight Decimal NULL DEFAULT 0
)

ALTER TABLE UserMetrics ADD 
	CONSTRAINT PK_UserMetrics PRIMARY KEY CLUSTERED (Id ASC)
GO

ALTER TABLE UserMetrics ADD
CONSTRAINT FK_Metrics_Receipt
	FOREIGN KEY (ReceiptId)
	REFERENCES PickupReceipts(Id)
	ON DELETE CASCADE
GO

ALTER TABLE UserMetrics ADD
CONSTRAINT FK_Metrics_UserId
	FOREIGN KEY (UserId)
	REFERENCES AspNetUsers (Id)
GO

