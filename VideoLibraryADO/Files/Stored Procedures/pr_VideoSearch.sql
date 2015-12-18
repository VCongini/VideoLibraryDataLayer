/* Procedure designed to search through videos by Vince Congini */

--Drop the old procedure.
--DROP PROCEDURE dbo.pr_VideoSearch

--Create the procedure
CREATE PROCEDURE dbo.pr_VideoSearch
	@title varchar(30) = NULL,
	@director varchar(30) = NULL,
	@year int = NULL

AS
BEGIN TRY
	SELECT VideoId, Title, Year, Director
	FROM dbo.Videos
	WHERE (@title IS NULL OR Title LIKE '%' + @title + '%')
		AND (@director IS NULL OR Director LIKE '%' + @director + '%')
		AND (@year IS NULL OR Year = @year)
		AND IsDeleted = 'False'
END TRY
BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
END CATCH
