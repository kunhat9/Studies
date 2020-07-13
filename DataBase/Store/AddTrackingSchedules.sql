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
	,@type varchar(50)
) AS
BEGIN
BEGIN TRAN
BEGIN TRY
	CREATE TABLE #ListUserId(
		Id int identity
		,TrackingDate	datetime
		,TrackingNote	nvarchar(500)
		,TrackingUserId	int
		,TrackingScheduleId	int
	)
	CREATE TABLE #ListTemp(
		Id int identity
		,TrackingDate	datetime
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

	INSERT INTO #ListTemp ([TrackingDate]
			  ,[TrackingNote]
			  ,[TrackingUserId]
			  ,[TrackingScheduleId])
	SELECT DISTINCT [TrackingDate]
			  ,[TrackingNote]
			  ,[TrackingUserId]
			  ,[TrackingScheduleId] FROM #ListUserId

	DELETE TB_TRACKINGS WHERE TrackingScheduleId = @schedulesId AND CONVERT(DATE,TrackingDate) = CONVERT(DATE,@dateTracking)
	IF @type ='INSERT'
		BEGIN
			DECLARE @index int = 1
			DECLARE @count int  = (SELECT COUNT(1) FROM #ListTemp)
			WHILE @index <= @count
				BEGIN
					DECLARE @check VARCHAR(50) = (SELECT TrackingId FROM TB_TRACKINGS t JOIN #ListTemp u ON t.TrackingUserId = u.TrackingUserId AND t.TrackingScheduleId = u.TrackingScheduleId AND CONVERT(DATE,t.TrackingDate) = CONVERT(DATE,u.TrackingDate) AND u.Id = @index)
					IF @check IS NULL OR @check =''
						BEGIN
							INSERT INTO [dbo].[TB_TRACKINGS](
								[TrackingDate]
							  ,[TrackingNote]
							  ,[TrackingUserId]
							  ,[TrackingScheduleId]
							)
							SELECT DISTINCT [TrackingDate]
							  ,[TrackingNote]
							  ,[TrackingUserId]
							  ,[TrackingScheduleId]
							FROM #ListTemp  WHERE Id = @index
						END
				SET @index = @index +1 
				END
			SET @ecode = '00'
			SELECT @ecode ECODE , 'SUSCESS' edesc
		END
	ELSE IF @type ='DELETE'
		BEGIN
			DELETE TB_TRACKINGS WHERE TrackingUserId IN (SELECT TrackingUserId FROM #ListUserId ) AND TrackingScheduleId = @schedulesId AND CONVERT(DATE,TrackingDate) = CONVERT(DATE,@dateTracking)
			SET @ecode = '00'
			SELECT @ecode ECODE , 'SUSCESS' edesc
		END
	

COMMIT
END TRY
BEGIN CATCH
   ROLLBACK
   SET @ecode = '999'
   SELECT '999' ecode, ERROR_MESSAGE() edesc
END CATCH
END