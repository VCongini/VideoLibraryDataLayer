/* Procedure designed to delete a video by Vince Congini */

--Drop the old procedure.
--DROP PROCEDURE dbo.pr_DeleteVideo

--Create the procedure
CREATE PROCEDURE dbo.pr_DeleteVideo
	@videoId int,
	@userId uniqueidentifier

AS
--Check for exception cases
	IF NOT EXISTS (SELECT UserId FROM dbo.UsersInRoles WHERE UserId = cast(@userId AS nvarchar(36)) AND RoleId IS NOT NULL)
	BEGIN
		RAISERROR ('The User is not a member of the Administrator Role.', -- Message text.
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
	IF NOT EXISTS (SELECT VideoId FROM dbo.Videos WHERE VideoId = @videoId)
	BEGIN
		RAISERROR ('The Video does not exist.', -- Message text.
               16, -- Severity.
               1 -- State.
               );
		RETURN;
	END
	IF @videoID = (SELECT VideoId FROM dbo.Checkouts WHERE Videoid = @VideoId AND ReturnDate IS NULL)
	BEGIN
		RAISERROR ('This Video has pending checkouts.', -- Message text.
               16, -- Severity.
               1 -- State.
               );
		RETURN;
	END
--Set the video to deleted.
BEGIN TRY
	UPDATE dbo.Videos
			SET IsDeleted = 1
			WHERE VideoId = @videoId
END TRY
BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
END CATCH