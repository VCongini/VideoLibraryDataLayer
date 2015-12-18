/* Procedure designed to add a review by Vince Congini */

--Drop the old procedure.
--DROP PROCEDURE dbo.pr_AddReview

--Create the procedure
CREATE PROCEDURE dbo.pr_AddReview
	@videoId int,
	@userId uniqueidentifier,
	@reviewText varchar(MAX)
AS
	IF @reviewText IS NULL OR @reviewText = '' 
	BEGIN
		RAISERROR ('The Review Information cannot be NULL.', -- Message text.
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
	INSERT INTO dbo.Reviews (VideoId, UserId, Review)
	VALUES (@videoId, cast(@userId AS nvarchar(36)), @reviewText)
END TRY
BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
END CATCH