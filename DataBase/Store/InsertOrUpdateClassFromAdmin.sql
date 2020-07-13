USE DB_STUDIES
GO
IF OBJECT_ID('InsertOrUpdateClassFromAdmin', 'P') IS NOT NULL
DROP PROC InsertOrUpdateClassFromAdmin
GO
CREATE PROCEDURE InsertOrUpdateClassFromAdmin
(
	@scheduleId varchar(50)
	,@boxSubjectId nvarchar(50)
	,@price varchar(50)
	,@startDate varchar(50)
	,@endDate varchar(50)
	,@dayOfWeek varchar(50)
	,@timeIn varchar(50)
	,@timeEnd varchar(50)
	,@status varchar(50)
	,@userId varchar(50)
	,@note nvarchar(max)
	,@scheduleFileId varchar(50)
	,@roomId varchar(50)
	
) AS
BEGIN
	DECLARE @ecode VARCHAR(50), @edesc VARCHAR(50)
	SET XACT_ABORT ON
	BEGIN TRAN
	BEGIN TRY
		DECLARE @classCode VARCHAR(50)
		DECLARE @demo VARCHAR(50) = (SELECT CONVERT(varchar(50),DATEPART(DAY, GETDATE())))+(SELECT CONVERT(varchar(50),DATEPART(MONTH, GETDATE())))+(SELECT CONVERT(varchar(50),DATEPART(YEAR, GETDATE())))
		SET @classCode = (@demo+'CL'+RIGHT('0' + CAST(NEXT VALUE FOR TB_SCHEDULESEQ AS varchar), 8))
		-- kiem tra xem co lop day chua
		DECLARE @count INT = (SELECT COUNT(1) FROM TB_SCHEDULES WHERE ScheduleId = @scheduleId)
		DECLARE @tuition decimal(18,0) = (SELECT CONVERT(decimal(18,0),@price*UserNumberSalary) FROM TB_USERS where UserId = @userId)
		IF @count > 0
			BEGIN
				-- da co roi thy chi can update lai thoi 
				UPDATE TB_SCHEDULES
					SET		[ScheduleStatus] = @status
						  ,[ScheduleDateBegin] = CONVERT(DATE,@startDate)
						  ,[ScheduleDateEnd] = CONVERT(DATE,@endDate)
						  ,[SchedulePrice] = CONVERT(decimal(18,0),@price)
						  ,[ScheduleIdBoxSubjectId] = @boxSubjectId
						  ,[ScheduleUserId] = @userId
						  , ScheduleFileId = @scheduleFileId
				WHERE ScheduleId = @scheduleId

			END
		ELSE
			BEGIN
				-- chua co thy moi insert lai
				CREATE TABLE #TempId(
					ScheduleId INT
				)
				INSERT INTO TB_SCHEDULES([ScheduleCode]
										  ,[ScheduleDateCreate]
										  ,[ScheduleStatus]
										  ,[ScheduleDateBegin]
										  ,[ScheduleDateEnd]
										  ,[SchedulePrice]
										  ,[ScheduleIdBoxSubjectId]
										  ,[ScheduleUserId]
										  ,[ScheduleFileId])
				OUTPUT INSERTED.ScheduleId INTO #TempId
				VALUES(@classCode
						,GETDATE()
						,@status
						,CONVERT(DATE,@startDate)
						,CONVERT(DATE,@endDate)
						,CONVERT(decimal(18,0),@price)
						,@boxSubjectId
						,@userId
						,@scheduleFileId
				)
			SET @scheduleId = (SELECT TOP 1 ScheduleId FROM #TempId)
			DROP TABLE #TempId
			END
		DELETE FROM TB_SCHEDULE_DETAILS WHERE ScheduleDetailScheduleId = @scheduleId
		INSERT INTO TB_SCHEDULE_DETAILS (
			[ScheduleDetailDayOfWeek]
		  ,[ScheduleDetailTimeFrom]
		  ,[ScheduleDetailTimeTo]
		  ,[ScheduleDetailNote]
		  ,[ScheduleDetailScheduleId]
		  ,[ScheduleDetailRoomClass]
		)
		VALUES(
		@dayOfWeek
		,CONVERT(time,@timeIn)
		,CONVERT(time,@timeEnd)
		,@note
		,@scheduleId
		,@roomId
		)
		SET @ecode = '00'
		SET @edesc ='Suscess'
	COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
		SET @ecode = '00'
		SET @edesc =ERROR_MESSAGE()
	END CATCH
	SELECT @ecode ECODE , @edesc EDESC 
	
END