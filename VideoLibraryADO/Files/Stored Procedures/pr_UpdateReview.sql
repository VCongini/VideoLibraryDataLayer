/* Procedure designed to add a review by Vince Congini */

--Drop the old procedure.
--DROP PROCEDURE dbo.pr_UpdateReview

--Create the procedure
CREATE PROCEDURE dbo.pr_UpdateReview
	@reviewId int,
	@reviewText VARCHAR(MAX)

AS
	
	IF NOT EXISTS (SELECT ReviewId FROM dbo.Reviews WHERE ReviewId = @reviewId)
	BEGIN
		RAISERROR ('The Review ID does not exist.', -- Message text.
               16, -- Severity.
               1 -- State.
               );
		RETURN;
	END
	IF @reviewText IS NULL OR @reviewText = ''
	BEGIN
		RAISERROR ('The Review Information cannot be NULL.', -- Message text.
               16, -- Severity.
               1 -- State.
               );
		RETURN;
	END
	
BEGIN TRY
	UPDATE dbo.Reviews
	SET Review = @reviewText
	WHERE ReviewId = @reviewId
END TRY
BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
END CATCH