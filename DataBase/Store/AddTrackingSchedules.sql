USE DB_STUDIES
GO
IF OBJECT_ID('AddTrackingSchedules', 'P') IS NOT NULL
DROP PROC AddTrackingSchedules
GO
CREATE PROCEDURE AddTrackingSchedules
(
	@dateTracking nvarchar(50)
	,@note nvarchar(max)
	,@listId varchar(max)
	, @schedulesId varchar(50)
) AS
BEGIN
BEGIN TRAN
BEGIN TRY
	CREATE TABLE #ListUserId(
		TrackingDate	datetime
		,TrackingNote	nvarchar(500)
		,TrackingUserId	int
		,TrackingScheduleId	int
	
	)
	DECLARE @ecode VARCHAR(50), @edesc VARCHAR(50)
	INSERT INTO #ListUserId(TrackingUserId)
	SELECT * FROM FN_SplitStringToTable(@listId,',')
	UPDATE #ListUserId
		SET TrackingDate = GETDATE(),
			TrackingNote = @note,
			TrackingScheduleId = @schedulesId
	INSERT INTO [dbo].[TB_TRACKINGS](
		[TrackingDate]
      ,[TrackingNote]
      ,[TrackingUserId]
      ,[TrackingScheduleId]
	)
	SELECT * FROM #ListUserId
	SET @ecode = '00'
	SELECT @ecode ECODE , 'SUSCESS' edesc

COMMIT
END TRY
BEGIN CATCH
   ROLLBACK
   SET @ecode = '999'
   SELECT '999' ecode, ERROR_MESSAGE() edesc
END CATCH
END