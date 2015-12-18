/* Procedure designed to add or update a rating by Vince Congini */

--Drop the old procedure.
--DROP PROCEDURE dbo.pr_AddUpdateRating

--Create the procedure
CREATE PROCEDURE dbo.pr_AddUpdateRating
	@videoId int,
	@userId uniqueidentifier,
	@rating int

AS
	
	IF @rating < 1 OR @rating > 5  
	BEGIN
		RAISERROR ('The rating needs to be greater than 0 or less than 5.', -- Message text.
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
	UPDATE dbo.Ratings
	SET Rating = @rating
	WHERE VideoId = @videoId

	INSERT INTO dbo.Ratings (VideoId, UserId, Rating)
	VALUES (@videoId, cast(@userId AS nvarchar(36)), @rating)
END TRY
BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
END CATCH