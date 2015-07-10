

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

-- pickup cycles

CREATE TABLE PickupCycles
(
	Id int IDENTITY(1,1) NOT NULL,
	AluminumWeight DECIMAL(6,3) NULL DEFAULT 0,
	GlassWeight DECIMAL(6,3) NULL DEFAULT 0,
	StandardPlasticWeight DECIMAL(6,3) NULL DEFAULT 0,
	MiscPlasticWeight DECIMAL(6,3) NULL DEFAULT 0,
	TotalBags DECIMAL(6,3) NULL DEFAULT 0,
	CommunityId int NULL,
	DateCreated Datetime Not NULL DEFAULT GETUTCDATE(),
	LastUpdated DateTime NOT NULL DEFAULT GETUTCDATE(),
	Notes nvarchar(max) NULL,
	PickupDate DateTime NOT NULL DEFAULT GETUTCDATE()
)


ALTER TABLE PickupCycles ADD
CONSTRAINT PK_PickupCycleId PRIMARY KEY CLUSTERED (Id ASC)
GO

ALTER TABLE PickupCycles ADD
CONSTRAINT FK_Cycle_CommunityId
	FOREIGN KEY (CommunityId)
	REFERENCES Communities (Id)
GO


 -- Pickup Receipts

CREATE TABLE PickupReceipts
(
	Id int IDENTITY(1,1) NOT NULL,
	UserId nvarchar(128) NOT NULL,
	ScheduledPickupId int NOT NULL,
	TotalAmount DECIMAL(18,4) NULL Default 0,
	PickupCycleId int NOT NULL,
	[DateCreated] datetime NOT NULL DEFAULT GETUTCDATE(),
	[LastUpdated] datetime NOT NULL DEFAULT GETUTCDATE()
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

ALTER TABLE PickupReceipts ADD
CONSTRAINT FK_Receipts_PickupCycleId
	FOREIGN KEY (PickupCycleId)
	REFERENCES PickupCycles(Id)
GO

ALTER TABLE PickupReceipts ADD
CONSTRAINT
	PR_UniqueCycle UNIQUE (PickupCycleId)
GO

---- UserMetric

CREATE TABLE UserMetrics
(
	Id int IDENTITY(1,1) NOT NULL,
	UserId nvarchar(128) NOT NULL,
	ReceiptId int NOT NULL,
	BagCount DECIMAL(6,3) NULL DEFAULT 0,
	[DateCreated] datetime NOT NULL DEFAULT GETUTCDATE(),
	[LastUpdated] datetime NOT NULL DEFAULT GETUTCDATE()
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
