/* Procedure designed to checkin a video by Vince Congini */

--Drop the old procedure.
--DROP PROCEDURE dbo.pr_CheckinVideo

--Create the procedure
CREATE PROCEDURE dbo.pr_CheckinVideo
	@videoId int,
	@userId uniqueidentifier

AS
	
	IF NOT EXISTS (SELECT VideoId FROM dbo.checkouts WHERE VideoId = @videoId) OR (SELECT ReturnDate FROM dbo.checkouts WHERE VideoId = @videoId) IS NOT NULL
	BEGIN
		RAISERROR ('The Video has not been checked out yet.', -- Message text.
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
	UPDATE dbo.Checkouts
	SET ReturnDate = CURRENT_TIMESTAMP
	WHERE VideoId = @videoId

	UPDATE dbo.Videos
	SET TotalCopies = (Totalcopies + 1)
	WHERE VideoId = @videoId
END TRY
BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
END CATCH