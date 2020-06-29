USE DB_STUDIES
GO
IF OBJECT_ID('GetTranscriptBy', 'P') IS NOT NULL
DROP PROC GetTranscriptBy
GO
CREATE PROCEDURE GetTranscriptBy
(
	@userId VARCHAR(50)
	,@scheduleId VARCHAR(50)
	,@type VARCHAR(50)
	, @pageNumber INT
	, @pageSize INT
) AS
BEGIN
	IF (@userId ='' OR @userId IS NULL) SET @userId = ''
	IF (@scheduleId ='' OR @scheduleId IS NULL) SET @scheduleId = ''
	IF (@type ='' OR @type IS NULL) SET @type = ''
	DECLARE @start int, @end int
	SET @start = (((@pageNumber - 1) * @pageSize) + 1)
	SET @end = (@start + @pageSize - 1)
	CREATE TABLE #Temp(
	[ROW] int IDENTITY(1,1) PRIMARY KEY
		,UserId INT -- lop
	)
	INSERT INTO #Temp
	SELECT UserId
	FROM TB_USERS u 
	JOIN TB_CLASSES c ON u.UserId = c.ClassUserId
	JOIN TB_POINTS p ON p.PointClassId = c.ClassId
	WHERE (@userId ='' OR u.UserId = @userId)
		AND (@scheduleId ='' OR c.ClassScheduleId = @scheduleId)
		AND (@type ='' OR p.PointType = @type)

	SELECT u.UserId, u.UserFullName,c.ClassScheduleId, p.PointNumber,p.PointType
	FROM TB_USERS u 
	JOIN TB_CLASSES c ON u.UserId = c.ClassUserId
	JOIN TB_POINTS p ON p.PointClassId = c.ClassId
	WHERE u.UserId IN 
	(SELECT UserId
		FROM #Temp
		WHERE @start <= [ROW]
			AND [ROW] <= @end)
	ORDER BY u.UserId
	SELECT ISNULL(COUNT(1), 0) FROM #Temp
	DROP TABLE #Temp 
	
END