USE DB_STUDIES
GO
IF OBJECT_ID('ExportOrderTuition', 'P') IS NOT NULL
DROP PROC ExportOrderTuition
GO
CREATE PROCEDURE ExportOrderTuition
(
	@userId VARCHAR(50)
	, @startDate varchar(50)
	, @endDate varchar(50)
	, @type varchar(50)
) AS
BEGIN
BEGIN TRAN
BEGIN TRY
	DECLARE @ecode varchar(50), @edesc varchar(50)
	CREATE TABLE #TempCountStudies(
	[ROW] int IDENTITY(1,1) PRIMARY KEY
		,ScheduleId INT -- lop
		, TrackingUserId int -- ngay 
		, CountNumber INT -- so luong hoc sinh diem danh ngay theoo lop
	)
	CREATE TABLE #TempCountTeacher(
			[ROW] int IDENTITY(1,1) PRIMARY KEY,
			ScheduleId INT -- lop
			, TrackingDate DATETIME -- ngay 
			, CountStudies INT -- so luong hoc sinh diem danh ngay theoo lop
			)
	CREATE TABLE #TempResult(
			TransNumber decimal(18,0),
			TransUserId varchar(50),
			TransType varchar(50),
			TransNote nvarchar(max),
			TransDateCreated datetime,
			TransBeginTime datetime,
			TransEndTime datetime
	)
	IF @type = 'STUDIES'
		BEGIN
			
			
			INSERT INTO #TempCountStudies(ScheduleId,TrackingUserId,CountNumber)
			SELECT  TrackingScheduleId,TrackingUserId,COUNT(TrackingDate) CountNumber FROM TB_TRACKINGS 
			WHERE (@userId ='' OR TrackingUserId = @userId)
			AND ( @startDate ='' OR CONVERT(DATE,@startDate) <= CONVERT(DATE,TrackingDate))
			AND (@endDate ='' OR CONVERT(DATE,TrackingDate) <= CONVERT(DATE,@endDate)
			AND TrackingCheckTuition IS NULL)
			GROUP BY  TrackingUserId , TrackingScheduleId

			UPDATE TB_TRACKINGS
			SET TrackingCheckTuition = 'A'
			WHERE TrackingId IN (
				SELECT TrackingId FROM TB_TRACKINGS 
				WHERE TrackingUserId = @userId
					AND ( @startDate ='' OR CONVERT(DATE,@startDate) <= CONVERT(DATE,TrackingDate))
					AND (@endDate ='' OR CONVERT(DATE,TrackingDate) <= CONVERT(DATE,@endDate)) 
			)

			INSERT INTO #TempResult
			SELECT  SUM(CONVERT(decimal(18,2),(SchedulePrice* t.CountNumber))) TransNumber,'STUDIES' TransType, u.UserId TransUserId, N'he thong' TransNote, GETDATE() TransDateCreated, CONVERT(DATE,@startDate) TransBeginTime,CONVERT(DATE,@endDate) TransEndTime
			FROM #TempCountStudies t
			JOIN TB_CLASSES c 
			ON c.ClassScheduleId = t.ScheduleId
			JOIN TB_SCHEDULES s
			ON t.ScheduleId = s.ScheduleId
			JOIN TB_USERS u 
			ON t.TrackingUserId = u.UserId
			WHERE @userId ='' OR t.TrackingUserId = @userId
			AND 
			t.ScheduleId IN
				(SELECT ScheduleId
				FROM #TempCountStudies)
			GROUP BY  u.UserId , u.UserFullName


			INSERT INTO TB_TRANSACTION( [TransNumber]
									  ,[TransType]
									  ,[TransUserId]
									  ,[TransNote]
									  ,[TransDateCreated]
									  ,[TransBeginTime]
									  ,[TransEndTime])
			SELECT * FROM #TempResult
			SET @ecode = '00'
			SELECT @ecode ECODE , 'SUSCESS' edesc

			SELECT  t.ScheduleId, s.ScheduleCode,t.CountNumber,(CONVERT(decimal(18,2),(SchedulePrice* t.CountNumber))) TuitionStudies, u.UserId , u.UserFullName
			FROM #TempCountStudies t
			JOIN TB_CLASSES c 
			ON c.ClassScheduleId = t.ScheduleId
			JOIN TB_SCHEDULES s
			ON t.ScheduleId = s.ScheduleId
			JOIN TB_USERS u 
			ON t.TrackingUserId = u.UserId
			WHERE @userId ='' OR t.TrackingUserId = @userId
			AND 
			t.ScheduleId IN
				(SELECT ScheduleId
				FROM #TempCountStudies)
			GROUP BY  u.UserId , u.UserFullName,s.ScheduleCode,t.ScheduleId,t.CountNumber,SchedulePrice
			
		END
	ELSE IF @type ='TEACHER'
		BEGIN
			UPDATE TB_TRACKINGS
			SET TrackingCheckSalary = 'A'
			WHERE TrackingId IN (
				SELECT TrackingId FROM TB_TRACKINGS 
				WHERE TrackingScheduleId IN
				(SELECT ScheduleId FROM TB_SCHEDULES WHERE ScheduleUserId = @userId)
					AND ( @startDate ='' OR CONVERT(DATE,@startDate) <= CONVERT(DATE,TrackingDate))
					AND (@endDate ='' OR CONVERT(DATE,TrackingDate) <= CONVERT(DATE,@endDate)) 
			)
			INSERT INTO #TempCountTeacher(ScheduleId,TrackingDate,CountStudies)
			SELECT TrackingScheduleId ScheduleId, TrackingDate, COUNT(TrackingUserId) CountStudies  FROM TB_TRACKINGS 
			WHERE TrackingScheduleId
			IN (SELECT ScheduleId FROM TB_SCHEDULES WHERE @userId ='' OR ScheduleUserId = @userId )
			AND ( @startDate ='' OR CONVERT(DATE,@startDate) <= CONVERT(DATE,TrackingDate))
			AND (@endDate ='' OR CONVERT(DATE,TrackingDate) <= CONVERT(DATE,@endDate)
			AND TrackingCheckSalary = 'A')
			GROUP BY TrackingScheduleId , TrackingDate
			-- tinh luong giao vien  : gia tien 1 buoi lop * so luong hoc sinh theoo buoi * he so luong
			INSERT INTO #TempResult 
			SELECT SUM(CONVERT(decimal(18,2) ,(0.7 * s.SchedulePrice* t.CountStudies))) TransNumber,'TEACHER' TransType, u.UserId TransUserId, N'he thong' TransNote, GETDATE() TransDateCreated, CONVERT(DATE,@startDate) TransBeginTime,CONVERT(DATE,@endDate) TransEndTime
			FROM #TempCountTeacher t
			JOIN TB_SCHEDULES s
			ON t.ScheduleId = s.ScheduleId
			JOIN TB_USERS u 
			ON s.ScheduleUserId = u.UserId
			WHERE @userId ='' OR ScheduleUserId = @userId
			AND t.ScheduleId IN
				(SELECT ScheduleId
				FROM #TempCountTeacher)
			GROUP BY  u.UserId , u.UserFullName
			INSERT INTO TB_TRANSACTION( [TransNumber]
									  ,[TransType]
									  ,[TransUserId]
									  ,[TransNote]
									  ,[TransDateCreated]
									  ,[TransBeginTime]
									  ,[TransEndTime])
			SELECT * FROM #TempResult

			SET @ecode = '00'
			SELECT @ecode ECODE , 'SUSCESS' edesc

			SELECT t.ScheduleId,t.TrackingDate,t.CountStudies, CONVERT(decimal(18,2) ,(0.7 * s.SchedulePrice* t.CountStudies)) SalaryTeacher, u.UserId, u.UserFullName
			FROM #TempCountTeacher t
			JOIN TB_SCHEDULES s
			ON t.ScheduleId = s.ScheduleId
			JOIN TB_USERS u 
			ON s.ScheduleUserId = u.UserId
			WHERE @userId ='' OR ScheduleUserId = @userId
			AND t.ScheduleId IN
				(SELECT ScheduleId
				FROM #TempCountTeacher)
		
		END
	DROP TABLE #TempCountTeacher
	DROP TABLE #TempCountStudies
	DROP TABLE #TempResult
COMMIT
END TRY
BEGIN CATCH
   ROLLBACK
   SET @ecode = '999'
   SELECT '999' ecode, ERROR_MESSAGE() edesc
END CATCH
END