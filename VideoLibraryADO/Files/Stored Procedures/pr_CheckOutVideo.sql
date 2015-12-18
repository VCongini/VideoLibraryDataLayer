/* Procedure designed to add a review by Vince Congini */

--Drop the old procedure.
--DROP PROCEDURE dbo.pr_CheckOutVideo

--Create the procedure
CREATE PROCEDURE dbo.pr_CheckOutVideo
	@videoId int,
	@userId uniqueidentifier
	
AS
	
	IF @videoID = (SELECT VideoId FROM dbo.Checkouts WHERE Videoid = @VideoId AND UserId = cast(@userId AS nvarchar(36)))
	BEGIN
		RAISERROR ('This user has already checked out this video.', -- Message text.
               16, -- Severity.
               1 -- State.
               );
		RETURN;
	END
	IF 0 <= (SELECT TotalCopies FROM dbo.Videos WHERE VideoId = @videoId)  
	BEGIN
		RAISERROR ('No available copies for checkout.', -- Message text.
               16, -- Severity.
               1 -- State.
               );
		RETURN;
	END
	IF NOT EXISTS (SELECT VideoId FROM dbo.Videos WHERE VideoId = @videoId)
	BEGIN
		RAISERROR ('The Video does not exist.', -- Message text.
               16, -- Severity.
               1 -- State.
               );
		RETURN;
	END
	IF NOT EXISTS (SELECT UserId FROM dbo.Users WHERE UserId = cast(@userId AS nvarchar(36)))
	BEGIN
		RAISERROR ('The User does not exist.', -- Message text.
               16, -- Severity.
               1 -- State.
               );
		RETURN;
	END
	
BEGIN TRY
	INSERT INTO dbo.Checkouts (VideoId, UserId, CheckoutDate)
	VALUES (@videoId, cast(@userId AS nvarchar(36)), CURRENT_TIMESTAMP)

	UPDATE dbo.Videos
	SET TotalCopies = (Totalcopies - 1)
	WHERE VideoId = @videoId
END TRY
BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
END CATCH