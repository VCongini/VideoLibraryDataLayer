/* Procedure designed to add or update a video by Vince Congini */

--Drop the old procedure.
--DROP PROCEDURE dbo.pr_AddUpdateVideo

--Create the procedure
CREATE PROCEDURE dbo.pr_AddUpdateVideo
	@videoId int,
	@title varchar(35),
	@year int,
	@director varchar(35),
	@totalCopies int,
	@formatCode varchar(7),
	@userId uniqueidentifier

AS
	
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
	IF @title IS NULL OR @title = '' OR @year IS NULL OR @year = '' OR @director IS NULL OR @director = ''
	BEGIN
		RAISERROR ('Please supply a value for Title, Year, and Director.', -- Message text.
               16, -- Severity.
               1 -- State.
               );
		RETURN;
	END
	IF @totalCopies < 0
	BEGIN
		RAISERROR ('The total copies cannot be negative.', -- Message text.
               16, -- Severity.
               1 -- State.
               );
		RETURN;
	END

BEGIN TRY
	If EXISTS (SELECT @videoId FROM dbo.Videos WHERE VideoId = @videoId)
	BEGIN
		UPDATE dbo.Videos
		SET Title = @title, Year = @year, Director = @Director, TotalCopies = @totalCopies, FormatCode = @formatCode
		WHERE VideoId = @videoId
	END
	ELSE
	BEGIN
		INSERT INTO dbo.Videos (Title, Year, Director, TotalCopies, FormatCode)
		VALUES (@title, @year, @director, @totalCopies, @formatCode)
	END
END TRY
BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
END CATCH