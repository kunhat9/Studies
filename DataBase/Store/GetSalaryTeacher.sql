USE DB_STUDIES
GO
IF OBJECT_ID('GetSalaryTeacher', 'P') IS NOT NULL
DROP PROC GetSalaryTeacher
GO
CREATE PROCEDURE GetSalaryTeacher
(
	@userId VARCHAR(50)
	, @scheduleId VARCHAR(50)
	, @startDate varchar(50)
	, @endDate varchar(50)
	, @pageNumber INT
	, @pageSize INT
) AS
BEGIN
	IF (@userId ='' OR @userId IS NULL) SET @userId = ''
	IF (@scheduleId ='' OR @scheduleId IS NULL) SET @scheduleId = ''
	IF (@startDate ='' OR @startDate IS NULL) SET @startDate = ''
	IF (@endDate ='' OR @endDate IS NULL) SET @endDate = ''
	-- lay ra so hoc sinh theo buoi tung ngay cua giao vien do day theo lop
	CREATE TABLE #TempCountStudies(
		ScheduleId INT -- lop
		, TrackingDate DATETIME -- ngay 
		, CountStudies INT -- so luong hoc sinh diem danh ngay theoo lop
	)
	INSERT INTO #TempCountStudies
	SELECT TrackingScheduleId ScheduleId, TrackingDate, COUNT(TrackingUserId) CountStudies  FROM TB_TRACKINGS 
	WHERE TrackingScheduleId
	IN (SELECT ScheduleId FROM TB_SCHEDULES WHERE @userId ='' OR ScheduleUserId = @userId)
	AND ( @startDate ='' OR CONVERT(DATE,@startDate) <= CONVERT(DATE,TrackingDate))
	AND (@endDate ='' OR CONVERT(DATE,TrackingDate) <= CONVERT(DATE,@endDate))
	GROUP BY TrackingScheduleId , TrackingDate
	-- tinh luong giao vien  : gia tien 1 buoi lop * so luong hoc sinh theoo buoi * he so luong 
	SELECT t.*, CONVERT(decimal(18,2) ,(u.UserNumberSalary * s.SchedulePrice* t.CountStudies)) SalaryTeacher FROM #TempCountStudies t
	JOIN TB_SCHEDULES s
	ON t.ScheduleId = s.ScheduleId
	JOIN TB_USERS u 
	ON s.ScheduleUserId = u.UserId
	WHERE @userId ='' OR ScheduleUserId = @userId
	AND @scheduleId ='' OR s.ScheduleId = @scheduleId

	DROP TABLE #TempCountStudies
END