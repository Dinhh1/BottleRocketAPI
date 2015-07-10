USE [bottlerocket-api_db]
GO
/****** Object:  StoredProcedure [dbo].[SpCreateHouse]    Script Date: 7/7/2015 8:08:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SpCreatePickupReceipts]
	@userId nvarchar(128),
	@pickupCycleId int,
	@totalAmount Decimal,
	@bagCount decimal
AS
BEGIN

-- Check if the pickup cycle exists

	IF NOT EXISTS (SELECT pc.Id FROM PickupCycles as pc where pc.Id = @pickupCycleId)
	BEGIN 
		RAISERROR ('Invalid pickup cycle id', 18,1);
		RETURN 0;
	END

	IF NOT EXISTS (SELECT sp.Id FROM ScheduledPickups as sp where sp.UserId = @userId and sp.IsPickedUp = 0)
	BEGIN 
		RAISERROR ('User do not have a pending pickup', 18,1);
		RETURN 0;
	END

	BEGIN TRANSACTION;

-- declare local variables
	DECLARE @scheduledPickupId int;
	DECLARE @actualDatePickedUp DateTime;

-- get the scheduled pickup id
	SELECT @scheduledPickupId = sp.Id FROM ScheduledPickups as sp where sp.UserId = @userId and sp.IsPickedUp = 0

-- get the actual pickup date
	SELECT @actualDatePickedUp = pc.PickupDate FROM PickupCycles as pc where pc.Id = @pickupCycleId

	-- update our scheduled pickup to true and set the pickup date
	UPDATE [ScheduledPickups] SET [IsPickedUp] = 1, [DatePickedUp] = @actualDatePickedUp

	IF @@ERROR <> 0
	BEGIN
		ROLLBACK TRANSACTION;
		RAISERROR('Failed to update scheduled pickup',18,1);
		RETURN 0;
	END

	-- insert pickup receipts
	INSERT INTO [PickupReceipts]
	(
		UserId,
		ScheduledPickupId,
		TotalAmount,
		PickupCycleId
	)
	VALUES
	(
		@userId,
		@scheduledPickupId,
		@totalAmount,
		@pickupCycleId
	)

	IF @@ERROR <> 0
	BEGIN
		ROLLBACK TRANSACTION;
		RAISERROR('Failed to insert pickup receipt',18,1);
		RETURN 0;
	END

	DECLARE @insertedId int;
	SET @insertedId = @@IDENTITY;

	INSERT INTO [UserMetrics]
	(
		UserId,
		ReceiptId,
		BagCount
	)
	VALUES
	(
		@userId,
		@insertedId,
		@bagCount
	)
	
	iF @@ERROR <> 0
	BEGIN
		ROLLBACK TRANSACTION;
		RAISERROR('Failed to user metric',18,1);
		RETURN 0;
	END



	COMMIT TRANSACTION;

	select um.Id as MetricId, pr.Id as ReceiptId, um.UserId as UserId, um.BagCount as BagCount, pr.TotalAmount as TotalAmount, pr.PickupCycleId as PickupCycleId 
	from UserMetrics as um left join PickupReceipts as pr on um.ReceiptId = pr.Id

END


