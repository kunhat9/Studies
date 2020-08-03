USE DB_STUDIES
GO
IF OBJECT_ID('ReportTuition', 'P') IS NOT NULL
DROP PROC ReportTuition
GO
CREATE PROCEDURE ReportTuition
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
		, TrackingUserId int -- ngay 
		, CountNumber INT -- so luong hoc sinh diem danh ngay theoo lop
	)
	INSERT INTO #TempCountStudies(ScheduleId,TrackingUserId,CountNumber)
	SELECT  TrackingScheduleId,TrackingUserId,COUNT(TrackingDate) CountNumber 
	FROM TB_TRACKINGS 
	WHERE (TrackingCheckTuition IS NULL 
	AND @userId ='' OR TrackingUserId = @userId)
	AND ( @startDate ='' OR CONVERT(DATE,@startDate) <= CONVERT(DATE,TrackingDate))
	AND (@endDate ='' OR CONVERT(DATE,TrackingDate) <= CONVERT(DATE,@endDate))
	GROUP BY  TrackingUserId , TrackingScheduleId

	-- tinh hoc phi 
	SELECT  DISTINCT t.ScheduleId,
	 --SUM(CONVERT(decimal(18,2),(SchedulePrice* t.CountNumber))) TuitionStudies,
	 CONVERT(decimal(18,2),(SchedulePrice* t.CountNumber)) TuitionStudies, u.UserId , u.UserFullName
	FROM #TempCountStudies t
	JOIN TB_CLASSES c 
	ON c.ClassScheduleId = t.ScheduleId
	JOIN TB_SCHEDULES s
	ON t.ScheduleId = s.ScheduleId
	JOIN TB_USERS u 
	ON t.TrackingUserId = u.UserId
	WHERE @userId ='' OR t.TrackingUserId = @userId
	AND @scheduleId ='' OR s.ScheduleId = @scheduleId
	AND 
	t.ScheduleId IN
		(SELECT ScheduleId
		FROM #TempCountStudies
		WHERE @start <= [ROW]
			AND [ROW] <= @end)
	--GROUP BY  u.UserId , u.UserFullName,t.ScheduleId
	ORDER BY ScheduleId
	SELECT ISNULL(COUNT(1), 0) FROM #TempCountStudies
	DROP TABLE #TempCountStudies
END