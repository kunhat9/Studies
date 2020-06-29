USE DB_STUDIES
GO
IF OBJECT_ID('GetTuitionStudies', 'P') IS NOT NULL
DROP PROC GetTuitionStudies
GO
CREATE PROCEDURE GetTuitionStudies
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
	DECLARE @start int, @end int
	SET @start = (((@pageNumber - 1) * @pageSize) + 1)
	SET @end = (@start + @pageSize - 1)
	-- lay ra so buoi hoc cua hoc sinh theo lop
	
	CREATE TABLE #TempCountStudies(
	[ROW] int IDENTITY(1,1) PRIMARY KEY
		,ScheduleId INT -- lop
		, TrackingDate DATETIME -- ngay 
		, CountNumber INT -- so luong hoc sinh diem danh ngay theoo lop
	)
	INSERT INTO #TempCountStudies(ScheduleId,TrackingDate,CountNumber)
	SELECT TrackingScheduleId ScheduleId, TrackingDate, COUNT(TrackingScheduleId) CountNumber  FROM TB_TRACKINGS 
	WHERE (@userId ='' OR TrackingUserId = @userId)
	AND ( @startDate ='' OR CONVERT(DATE,@startDate) <= CONVERT(DATE,TrackingDate))
	AND (@endDate ='' OR CONVERT(DATE,TrackingDate) <= CONVERT(DATE,@endDate))
	GROUP BY TrackingScheduleId, TrackingDate
	-- tinh hoc phi 
	SELECT  t.ScheduleId,t.TrackingDate,t.CountNumber , CONVERT(decimal(18,2) ,(SchedulePrice* t.CountNumber)) TuitionStudies
	FROM #TempCountStudies t
	JOIN TB_CLASSES c 
	ON c.ClassScheduleId = t.ScheduleId
	JOIN TB_SCHEDULES s
	ON t.ScheduleId = s.ScheduleId
	JOIN TB_USERS u 
	ON s.ScheduleUserId = u.UserId
	WHERE @userId ='' OR c.ClassUserId = @userId
	AND @scheduleId ='' OR s.ScheduleId = @scheduleId
	AND t.ScheduleId IN
		(SELECT ScheduleId
		FROM #TempCountStudies
		WHERE @start <= [ROW]
			AND [ROW] <= @end)
	ORDER BY ScheduleId
	SELECT ISNULL(COUNT(1), 0) FROM #TempCountStudies
	DROP TABLE #TempCountStudies
END